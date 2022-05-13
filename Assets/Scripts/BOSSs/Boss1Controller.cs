using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour, EnemyDamageController
{
    private enum STATE {startA,normal,move,jump,lightattack,heavyattack,counterH,down };
    private STATE state=STATE.startA;//enumを入れる
    private STATE saveState;//enumを変えるとき変化するほうを保存する変数

    public float MaxHP;//最大体力
    public float moveSpeed,JumpSpeed;//移動スピード,ジャンプスピード
    public float nPosS;//攻撃を発生する場合のプレイヤーとの距離
    public float MaxStopTime;//Normal時に考える時間
    public float MaxXposition, MMaxXposition;//ジャンプ用　左側にジャンプする位置、右側にジャンプする位置
    
    float MaxjumpPos;
    public float aa;

    private GameObject playerG;//プレイヤーのゲームオブジェクトを保存する
    private Animator anim;
    private Vector3 savePos, savePlayerPos;
    private Vector3 Bpos,SaveBpos;
    private int Attackcount, Maxattackcount;//攻撃回数を記録する
    private int Raction,jumpN;
    private float hp;//体力
    private float Jpos;
    private float Savedirection,direction;//移動時の向き保存用,向きの値を入れる用
    private float stopTime;//止まっている間の計る時間
    private bool JumpStratSwicth,jumpSwicth;//ジャンプができるがどうか
    private bool attackSwicth;//攻撃開始のスイッチ

    void StartA()//初めに動くアニメーション
    {
        anim.SetBool("startS", true);
    }
    void Normal()//通常
    {
        jumpN = 0;
        anim.SetBool("move", false);
        anim.SetBool("jump", false);
        anim.SetFloat("jumpN", 0);
        anim.SetBool("lightattack", false);
        anim.SetBool("startS", false);


        if (stopTime>=MaxStopTime)//次の行動
        {
            anim.SetBool("move", true);
            changeState(STATE.move);

            //changeState(STATE.jump);
            //JumpStratSwicth = true;
            stopTime = 0;
            //Debug.Log(stopTime);
        }
        else
        {
            stopTime += 1 * Time.deltaTime;
        }
    }
    void Move()//移動
    {
        //Debug.Log("移動");
        savePlayerPos = playerG.transform.position;//プレイヤーの位置を代入
        savePlayerPos = transform.position - savePlayerPos;//プレイヤーの位置とこれの位置を引く

        if (savePlayerPos.x <= nPosS && savePlayerPos.x >= -nPosS)//プレイヤーが範囲内に入ったら
        {
            Attackcount++;
            if (Maxattackcount <= Attackcount)//強攻撃
            {
                anim.SetBool("heavyattack", true);
                anim.SetBool("move", false);
                attackSwicth = true;
                changeState(STATE.heavyattack);
            }
            else//弱攻撃
            {
                anim.SetBool("lightattack", true);
                anim.SetBool("move", false);
                attackSwicth = true;
                changeState(STATE.lightattack);
            }

        }

        Savedirection = transform.position.x - playerG.transform.position.x;
        direction = 0;
        if (Savedirection >= 0)//右向き
        {
            direction = -1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
        else if (Savedirection < 0)//左向き
        {
            direction = 1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
        savePos.x = direction * moveSpeed * Time.deltaTime;
        transform.position += savePos;
    }
    void Jump()//ジャンプ
    {
        anim.SetBool("jump", true);
        if (JumpStratSwicth == true)
        {
            anim.SetBool("lightattack", false);
            anim.SetBool("heavyattack", false);
            Bpos = transform.position;
            //自身の座標を入れる
            //Vector3 Bpos = transform.position;//自身の座標を入れる 
            //if (MaxXposition <= Bpos.x && MMaxXposition >= Bpos.x)
            //{
            //    Debug.Log("ジャンプしない");
            //}
            //else
            //{
            //    Debug.Log("ジャンプする");
            //}


            if (Raction % 2 == 0)
            {
                Debug.Log("偶数");
                //右ジャンプ
                jumpN = 1;
                MaxjumpPos = (Bpos.x + MaxXposition) / 2f;
                Debug.Log(MaxjumpPos);
                Vector3 s = transform.localScale;
                transform.localScale = new Vector3(-1f, s.y, s.z);
            }
            else
            {
                Debug.Log("奇数");
                //左ジャンプ
                jumpN = -1;
                MaxjumpPos = (Bpos.x + MMaxXposition) / 2f;
                Debug.Log(MaxjumpPos);
                Vector3 s = transform.localScale;
                transform.localScale = new Vector3(1f, s.y, s.z);
            }
            //MaxjumpPos = Mathf.Floor(MaxjumpPos)/2;//切り捨てして半分にする
            Debug.Log(MaxjumpPos);
            JumpStratSwicth = false;
        }
        if (jumpSwicth==true) {
            if (jumpN == 1) {
                //右にジャンプ
                if (MaxXposition >= Bpos.x)
                {

                    Bpos = transform.position;//自身の座標を入れる
                    Vector3 i = new Vector3();
                    if (MaxjumpPos <= Bpos.x)//下がる
                    {
                        //Debug.Log("下がってる");
                        anim.SetFloat("jumpN", 2);
                        aa += 20f * Time.deltaTime;
                        i.y = -1 * aa * Time.deltaTime;
                    }
                    else //上がる
                    {
                        //Debug.Log("上がってる");
                        anim.SetFloat("jumpN", 1);
                        aa -= 20f * Time.deltaTime;
                        i.y = 1 * aa * Time.deltaTime;
                    }
                    i.x = 1 * JumpSpeed * Time.deltaTime;
                    transform.position += i;
                }
                else
                {
                    anim.SetFloat("jumpN", 3);
                    transform.position = new Vector3(transform.position.x, SaveBpos.y, transform.position.z);
                }
            } else if (jumpN==-1) {

                //左にジャンプ
                if (MMaxXposition <= Bpos.x)
                {

                    Bpos = transform.position;//自身の座標を入れる
                    Vector3 i = new Vector3();
                    if (MaxjumpPos >= Bpos.x)//下がる
                    {
                        //Debug.Log("下がってる");
                        anim.SetFloat("jumpN", 2);
                        aa += 20f * Time.deltaTime;
                        i.y = -1 * aa * Time.deltaTime;
                    }
                    else //上がる
                    {
                        //Debug.Log("上がってる");
                        anim.SetFloat("jumpN", 1);
                        aa -= 20f * Time.deltaTime;
                        i.y = 1 * aa * Time.deltaTime;
                    }
                    i.x = -1 * JumpSpeed * Time.deltaTime;
                    transform.position += i;
                }
                else
                {
                    anim.SetFloat("jumpN", 3);
                    transform.position = new Vector3(transform.position.x, SaveBpos.y, transform.position.z);
                }
            }
        }
    }
    void Lightattack()//弱攻撃
    {
        if(attackSwicth==true)
        {
            Debug.Log("弱攻撃");
            //Raction = Random.Range(1, 11);
            if (Raction==1)
            {
                Raction = 2;
            }
            else if (Raction == 2)
            {
                Raction = 1;
            }
            JumpStratSwicth = true;
            attackSwicth = false;
        }
    }
    void Heavyattack()//強攻撃
    {
        if (attackSwicth == true)
        {
            Debug.Log("強攻撃");
            if (Raction == 1)
            {
                Raction = 2;
            }
            else if (Raction == 2)
            {
                Raction = 1;
            }
            Attackcount = 0;
            Maxattackcount = 3;
            JumpStratSwicth = true;
            attackSwicth = false;
        }
    }
    void Counter()//カウンターをくらったとき
    {

    }
    void Down()//ダウン状態
    {

    }
    public void EnemyDamage(int h)//ダメージを受けた時※インターフェース
    {

    }
    public void jumpNamber()//アニメーションのjumpNを変更する
    {
        if (jumpSwicth==false)
        {
            jumpSwicth = true;
        }
        else
        {
            jumpSwicth = false;
        }
    }
    private void changeState(STATE _state)//ステートを切り替える
    {
        //stopTime = 0;
        saveState = _state;
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveBpos = transform.position;
        hp = MaxHP;
        Maxattackcount = 2;//最初は2回
        Raction = 2;
        anim = GetComponent<Animator>();
        JumpStratSwicth = true;
        attackSwicth = true;
        playerG = GameObject.FindWithTag("Player");//タグでプレイヤーのオブジェクトか判断して入れる
        stopTime = MaxStopTime;//最初だけすぐに動けるようにする
    }

    // Update is called once per frame
    void Update()
    {
        //現在のステートを呼び出す
        switch (state)
        {
            case STATE.startA://
                StartA();
                break;
            case STATE.normal://通常時
                Normal();
                break;
            case STATE.move://歩く
                Move();
                break;
            case STATE.jump://歩く
                Jump();
                break;
            case STATE.lightattack://弱攻撃する
                Lightattack();
                break;
            case STATE.heavyattack://強攻撃する
                Heavyattack();
                break;
            case STATE.counterH://カウンターを食らったとき
                Counter();
                break;
            case STATE.down://ダウンしたとき
                Down();
                break;
        }

        //ステートが変わったとき
        if (state != saveState)
        {
            //次のステートに切り替わる
            state = saveState;
        }
    }
}
