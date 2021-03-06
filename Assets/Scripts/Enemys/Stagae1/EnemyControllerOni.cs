using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerOni : MonoBehaviour, EnemyDamageController
{
    //ステートマシンで敵AIの作成
    private enum STATE { normal, move, attack,guard, counterMe, damage }
    private STATE state = STATE.normal;//enumのnormalを入れる
    private STATE saveState = STATE.move;//enumを変えるとき変化するほうを保存する変数

    [SerializeField] AudioClip[] ac;
    GameObject SM;
    stageManagerC SMC;

    public int MaxHP;//最大HP
    public float moveSpeed;//移動スピード
    public float nPosS;//攻撃を発生する場合のプレイヤーとの距離
    public float Maxattacktime;//攻撃するまでの時間
    //public float actionTime;//次の行動にうつる時間

    Renderer targetRenderer; // 判定したいオブジェクトのrendererへの参照
    AudioSource AS;

    private GameObject playerG;//プレイヤーのゲームオブジェクトを保存する
    private Animator anim;//Animator保存用
    private Vector3 savePos, savePlayerPos;
    private int HP;//体力
    private float Savedirection,direction;//移動時の向き保存用,向きの値を入れる用
    private float attackTime;//攻撃するまでの時間をためる
    private bool attackSwicth,attacktimeSwicth,attackplustime;
    private bool counterHetSwicth;//カウンターが当たったら動くbool型
    private bool guardSwicth;//ガードしているかどうか
    private bool damageSwicth;//ダメージが通るかどうか
    void Normal()
    {
        Debug.Log("通常");
        anim.SetFloat("moveSpeed", 1);//アニメーションを通常再生
        anim.SetBool("move", false);
        anim.SetBool("attack", false);
        anim.SetBool("guard", false);
        anim.SetBool("counterhet", false);
        anim.SetBool("damage", false);

        guardSwicth = true;
        if (attacktimeSwicth==false) {
            attackTime = 0;
        }
        attackSwicth = false;
        attackplustime = true;
        //damageSwicth = true;
        changeState(STATE.move);
    }
    void Move()
    {
        anim.SetBool("move", true);
        if (attackTime>= Maxattacktime)//attackTimeがたまったら
        {
            attackSwicth = true;
            attacktimeSwicth = false;
        }
        else if (attackTime < Maxattacktime)//attackTimeがたまっていない
        {
            attackTime += 1 * Time.deltaTime;
            attacktimeSwicth = true;
        }
        savePlayerPos = playerG.transform.position;//プレイヤーの位置を代入
        savePlayerPos = transform.position - savePlayerPos;//プレイヤーの位置とこれの位置を引く
        if (savePlayerPos.x <= nPosS && savePlayerPos.x >= -nPosS)//プレイヤーが範囲内に入ったら
        {
            if (attackSwicth==true) //攻撃開始
            {
                guardSwicth = false;
                //damageSwicth = false; 
                anim.SetBool("move", false);
                changeState(STATE.attack);
            }
            else {
                anim.SetFloat("moveSpeed", -1);//アニメーションを逆再生
                Savedirection = transform.position.x - playerG.transform.position.x;//プレイヤーの向きを調べる
                direction = 0;
                if (Savedirection >= 0)//右向き
                {
                    direction = -1;
                    Vector3 r = transform.localScale;
                    transform.localScale = new Vector3(direction * -1.607602f, r.y, r.z);
                }
                else if (Savedirection < 0)//左向き
                {
                    direction = 1;
                    Vector3 r = transform.localScale;
                    transform.localScale = new Vector3(direction * -1.607602f, r.y, r.z);

                }
                savePos.x = -direction * moveSpeed * Time.deltaTime;
                transform.position += savePos;
            }
        }
        else /*if (savePlayerPos.x >= nPosS && savePlayerPos.x <= -nPosS)*/
        {
            anim.SetFloat("moveSpeed", 1);//アニメーションを通常再生
            Savedirection = transform.position.x - playerG.transform.position.x;//プレイヤーの向きを調べる
            direction = 0;
            if (Savedirection >= 0)//右向き
            {
                direction = -1;
                Vector3 r = transform.localScale;
                transform.localScale = new Vector3(direction * -1.607602f, r.y, r.z);
            }
            else if (Savedirection < 0)//左向き
            {
                direction = 1;
                Vector3 r = transform.localScale;
                transform.localScale = new Vector3(direction * -1.607602f, r.y, r.z);

            }
            savePos.x = direction * moveSpeed * Time.deltaTime;
            transform.position += savePos;
        }
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    anim.SetFloat("moveSpeed", -1);//アニメーションを逆再生
        //}
    }
    void Attack()//攻撃
    {
        //Debug.Log("攻撃");
        anim.SetBool("attack", true);
    }
    void Guard()//防御
    {
        if (attackplustime==true)
        {
            attackTime += 0.5f;
            attackplustime = false;
        }
        anim.SetBool("guard", true);
    }
    void Counter()
    {
        counterHetSwicth = false;
        guardSwicth = false;
        anim.SetBool("counterhet", true);
        anim.SetBool("attack", false);
    }
    void CounterBool()//animationで攻撃中にカウンターされたら起動する用
    {
        if (counterHetSwicth == false)//起動していなかったら
        {
            counterHetSwicth = true;
        }
        else if (counterHetSwicth == true)//起動していたら
        {
            counterHetSwicth = false;
        }
    }
    void Damage()
    {
        anim.SetBool("damage", true);
    }
    void Destroy()//死亡時
    {
        anim.Play("Destroy");
        gameObject.layer = LayerMask.NameToLayer("Destroy");
        //Destroy(gameObject);
    }
    void sibou()
    {
        Destroy(gameObject);
    }
    public void EnemyDamage(int h)//ダメージを受けた時※インターフェース
    {
        //Debug.Log("動いた");
        if (guardSwicth==true)//ガードできる状態
        {
            anim.SetBool("move", false);
            anim.SetBool("attack", false);
            anim.SetBool("counterhet", false);
            changeState(STATE.guard);
        }
        else
        {   
                Debug.Log("damage");
                anim.SetBool("move", false);
                anim.SetBool("attack", false);
                anim.SetBool("guard", false);
                anim.SetBool("counterhet", false);
                HP -= h;
                if (HP <= 0)//HPが0になったら
                {
                    Destroy();//死亡
                }
                changeState(STATE.damage);  
        }
    }
    private void changeState(STATE _state)//ステートを切り替える
    {
        saveState = _state;
    }

    public void SE(int i)//SE
    {
        AS.PlayOneShot(ac[i]);
    }
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.FindWithTag("stageManager");
        SMC = SM.GetComponent<stageManagerC>();
        HP = MaxHP;
        attackTime = 0;
        anim = GetComponent<Animator>();
        playerG = GameObject.FindWithTag("Player");//タグでプレイヤーのオブジェクトか判断して入れる
        targetRenderer = GetComponent<Renderer>();
        AS = GetComponent<AudioSource>();
        guardSwicth = true;
        attacktimeSwicth = true;
        counterHetSwicth = false;
        //damageSwicth = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetRenderer.isVisible)//カメラ内なら動く
        {
            if (SMC.pauseSwicth == false)
            {
                //現在のステートを呼び出す
                switch (state)
                {

                    case STATE.normal://通常時
                        Normal();
                        break;
                    case STATE.move://歩く
                        Move();
                        break;
                    case STATE.attack://攻撃する
                        Attack();
                        break;
                    case STATE.guard://防御する
                        Guard();
                        break;
                    case STATE.counterMe://カウンターを食らったとき
                        Counter();
                        break;
                    case STATE.damage://ダメージ
                        Damage();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playercounter")
        {
            if (counterHetSwicth == true)
            {
                //Debug.Log("ヒット");
                changeState(STATE.counterMe);
            }
        }
    }
}
