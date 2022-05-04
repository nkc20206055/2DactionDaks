using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,EnemyDamageController
{
    //ステートマシンで敵AIの作成
    private enum STATE { normal,move,attack,counterMe,damage}
    private STATE state = STATE.normal;//enumのnormalを入れる
    private STATE saveState = STATE.move;//enumを変えるとき変化するほうを保存する変数

    //public bool otamseimode;//お試しで動かす場合true
    public int MaxHP;//最大HP
    public float moveSpeed;//移動スピード
    public float nPosS;//攻撃を発生する場合のプレイヤーとの距離
    public float actionTime;//次の行動にうつる時間

    float SaveTime;//
    bool Onlyonce;//一回きり動く

    private GameObject playerG;//プレイヤーのゲームオブジェクトを保存する
    private Animator anim;//Animator保存用
    private Vector3 savePos, savePlayerPos;
    private int HP;//体力
    private float Savedirection, PMd,direction;//移動時の向き保存用,向きの値を入れる用
    public bool counterHetSwicth;//カウンターが当たったら動くbool型
    void Normal()//通常時や、行動を元に戻す場合に通る
    {
        anim.SetBool("move", false);
        anim.SetBool("attack", false);
        anim.SetBool("counter", false);
        anim.SetBool("damage", false);
        //if (actionTime> SaveTime) {

        //}
        //else {
        //    changeState(STATE.move);
        //}

        changeState(STATE.move);

    }
    void Move()//移動
    {
        //if (Onlyonce==true)
        //{
        //    actionTime = 3;
        //    SaveTime = 0;
        //    Onlyonce = false;
        //}
        anim.SetBool("move", true);
        //Debug.Log(playerG.transform.position);
        savePlayerPos = playerG.transform.position;
        savePlayerPos = transform.position - savePlayerPos;
        //Debug.Log(savePlayerPos);
        if (savePlayerPos.x <= nPosS && savePlayerPos.x >= -nPosS)//プレイヤーが範囲内に入ったら
        {
            Debug.Log("攻撃開始");
            Onlyonce = true;
            anim.SetBool("move", false);
            changeState(STATE.attack);
        }
        Savedirection = transform.position.x - playerG.transform.position.x;
        //PMd = playerG.transform.position.x / -playerG.transform.position.x;
        //Debug.Log(PMd);
        direction = 0;
        if (Savedirection>=0)//右向き
        {
            direction = -1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
        else if (Savedirection<0)//左向き
        {
            direction = 1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
        savePos.x = direction * moveSpeed * Time.deltaTime;
        transform.position += savePos;
    }
    void Attack()//攻撃　※ステートはanimatorの方で変えている
    {
        anim.SetBool("attack", true);
        changeStateAndTime(STATE.normal, 2.5f);
    }
    void Counter()//カウンターをくらったとき
    {
        //Debug.Log("カウンター成功");
        counterHetSwicth = false;
        anim.SetBool("counter", true);
        anim.SetBool("attack", false);
        changeStateAndTime(STATE.normal, 2.5f);
    }
    void CounterBool()//animationで攻撃中にカウンターされたら起動する用
    {
        if (counterHetSwicth==false)//起動していなかったら
        {
            counterHetSwicth = true;
        }
        else if (counterHetSwicth==true)//起動していたら
        {
            counterHetSwicth = false;
        }
    }
    void Damage()//ダメージ ※ステートはanimatorの方で変えている
    {
        anim.SetBool("damage", true);
        changeStateAndTime(STATE.normal, 2.5f);
    }
    void DestroyM()//死亡時
    {
        Destroy(gameObject);
    }
    public void EnemyDamage(int h)//ダメージを受けた時※インターフェース
    {
        anim.SetBool("move", false);
        anim.SetBool("attack", false);
        anim.SetBool("counter", false);
        HP -= h;
        Debug.Log("ダメージ　"+HP);
        if (HP <= 0)//HPが0になったら
        {
            DestroyM();
        }
        Onlyonce = true;
        changeState(STATE.damage);
    }
    private void changeState(STATE _state)//ステートを切り替える
    {
        saveState = _state;
    }
    private void changeStateAndTime(STATE Cstate,float t)//待機時間を設けてからステートを切り替える
    {
        if (Onlyonce == true)//最初に動く
        {
            actionTime = t;
            SaveTime = 0;
            Onlyonce = false;
        }
        if (actionTime>SaveTime)
        {
            SaveTime += 1 * Time.deltaTime;
        }
        else
        {
            Debug.Log(SaveTime + " , " + actionTime);
            saveState = Cstate;//ステートを切り替える
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        Onlyonce = true;
        counterHetSwicth = false;
        anim = GetComponent<Animator>();
        playerG = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        
        //
        //現在のステートを呼び出す
        switch (state){

            case STATE.normal://通常時
                Normal();
                break;
            case STATE.move://歩く
                Move();
                break;
            case STATE.attack://攻撃する
                Attack();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="playercounter")
        {
            if (counterHetSwicth==true) {
                //Debug.Log("ヒット");
                changeState(STATE.counterMe);
            }
        }
    }
}
