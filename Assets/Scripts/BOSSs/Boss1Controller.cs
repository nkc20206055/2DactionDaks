using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour, EnemyDamageController
{
    private enum STATE {startA,normal,move,jump,lightattack,heavyattack,counterH,down };
    private STATE state=STATE.startA;//enumを入れる
    private STATE saveState;//enumを変えるとき変化するほうを保存する変数

    public float MaxHP;//最大体力
    public float moveSpeed;//移動スピード
    public float nPosS;//攻撃を発生する場合のプレイヤーとの距離
    public float MaxStopTime;//Normal時に考える時間
    public float MaxXposition, MMaxXposition;//ジャンプ用　左側にジャンプする位置、右側にジャンプする位置

    private GameObject playerG;//プレイヤーのゲームオブジェクトを保存する
    private Animator anim;
    private Vector3 savePos, savePlayerPos;
    private int Attackcount, Maxattackcount;//攻撃回数を記録する
    private int Raction;
    private float hp;//体力
    private float Savedirection,direction;//移動時の向き保存用,向きの値を入れる用
    private float stopTime;//止まっている間の計る時間
    void StartA()//初めに動くアニメーション
    {
        anim.SetBool("startS", true);
    }
    void Normal()//通常
    {
        anim.SetBool("move", false);
        anim.SetBool("startS", false);


        if (stopTime>=MaxStopTime)//次の行動
        {
            //anim.SetBool("move", true);
            //changeState(STATE.move);
            changeState(STATE.jump);
            stopTime = 0;
            Debug.Log(stopTime);
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
            //Attackcount++;
            //if (Maxattackcount<=Attackcount)//強攻撃
            //{
            //    anim.SetBool("heavyattack", true);
            //    anim.SetBool("move", false);
            //    changeState(STATE.heavyattack);
            //}
            //else//弱攻撃
            //{
                anim.SetBool("lightattack", true);
                anim.SetBool("move", false);
                changeState(STATE.lightattack);
            //}

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
    }
    void Lightattack()//弱攻撃
    {
        //Debug.Log("弱攻撃");
    }
    void Heavyattack()//強攻撃
    {
        Debug.Log("強攻撃");
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
    public void jumpNamber(int i)//アニメーションのjumpNを変更する
    {
        anim.SetFloat("jumpN", i);
    }
    private void changeState(STATE _state)//ステートを切り替える
    {
        //stopTime = 0;
        saveState = _state;
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = MaxHP;
        Maxattackcount = 2;//最初は2回
        anim = GetComponent<Animator>();
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
