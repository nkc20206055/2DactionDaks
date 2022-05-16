using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour, EnemyDamageController
{
    private enum STATE {startA,normal,move,jump,lightattack,heavyattack,counterH,down,change,jumpattack,destoy};
    private STATE state=STATE.startA;//enumを入れる
    private STATE saveState;//enumを変えるとき変化するほうを保存する変数

    GameObject SM;
    stageManagerC SMC;

    [SerializeField] public GameObject attackC;
    [SerializeField] public GameObject bulletObject; 
    public float MaxHP;//最大体力
    public float moveSpeed,JumpSpeed;//移動スピード,ジャンプスピード
    public float nPosS,jumpAPpos;//攻撃を発生する場合のプレイヤーとの距離,ジャンプ攻撃を発生する場合のプレイヤーとの距離
    public float MaxStopTime;//Normal時に考える時間
    public float MaxXposition, MMaxXposition;//ジャンプ用　左側にジャンプする位置、右側にジャンプする位置
    
    float MaxjumpPos;
    public float aa;

    private GameObject MI;//自身のオブジェクト
    private GameObject playerG;//プレイヤーのゲームオブジェクトを保存する
    private Animator anim;
    private Vector3 savePos, savePlayerPos;
    private Vector3 Bpos,SaveBpos;
    private int Attackcount, Maxattackcount;//強攻撃までの攻撃回数を記録する
    private int attackNO, attackNcount;//攻撃する回数,回数の記録
    private int Raction,jumpN,jumpattackR;
    private int counterCount,MaxcounterCount;//カウンターされた回数の記録、最大でカウンターされていい回数
    public float hp;//体力
    private float Jpos;
    private float Savedirection,direction;//移動時の向き保存用,向きの値を入れる用
    private float stopTime;//止まっている間の計る時間
    private bool JumpStratSwicth,jumpSwicth;//ジャンプができるがどうか
    private bool attackSwicth;//攻撃開始のスイッチ
    private bool attackNumberS, directionSwicth;
    private bool counterHetSwicth,counterOneSwicth;//カウンターが当たったら動くbool型
    private bool DownSwcith,Downdamgemode;//ダウンした時に1回だけ動く
    private bool changeSwicth, SchangeSwicth, CjumpSwcith;//
    private bool destroySwicth;
    private bool bulletSwicth;

    void StartA()//初めに動くアニメーション
    {
        anim.SetBool("startS", true);
    }
    void Normal()//通常
    {
        gameObject.tag = "boss";
        jumpN = 0;
        jumpattackR = 0;
        attackNumberS = true;
        directionSwicth = true;
        counterHetSwicth = false;
        DownSwcith = true;
        bulletSwicth = true;
        attackNO = 0;
        attackNcount = 0;
        anim.SetBool("move", false);
        anim.SetBool("jump", false);
        anim.SetFloat("jumpN", 0);
        anim.SetBool("lightattack", false);
        anim.SetBool("heavyattack", false);
        anim.SetBool("counter", false);
        anim.SetBool("down", false);
        //anim.SetBool("change", false);
        anim.SetBool("startS", false);
        anim.SetBool("jumpattack", false);

        if (stopTime>=MaxStopTime)//次の行動
        {
            anim.SetBool("move", true);
            changeState(STATE.move);

            //changeState(STATE.jump);
            //JumpStratSwicth = true;
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
        if (attackNumberS==true)
        {
            attackNO = Random.Range(1, 4);
            jumpattackR = Random.Range(1, 7);//通常ジャンプかジャンプ攻撃か
            if (attackNO==2||attackNO==3)//2回攻撃をする
            {
                attackNO = 2;
            }else if (attackNO==1)//1回攻撃をする
            {
                attackNO = 3;
            }
            Debug.Log(attackNO+"回攻撃をする");
            attackNumberS = false;
        }
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
            gameObject.tag = "bossjump";
            anim.SetBool("lightattack", false);
            anim.SetBool("heavyattack", false);
            anim.SetBool("down", false);
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
                //Debug.Log("偶数");
                //右ジャンプ
                jumpN = 1;
                MaxjumpPos = (Bpos.x + MaxXposition) / 2f;
                //Debug.Log(MaxjumpPos);
                Vector3 s = transform.localScale;
                transform.localScale = new Vector3(-1f, s.y, s.z);
            }
            else
            {
                //Debug.Log("奇数");
                //左ジャンプ
                jumpN = -1;
                MaxjumpPos = (Bpos.x + MMaxXposition) / 2f;
                //Debug.Log(MaxjumpPos);
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
            attackC.tag = "enemyrightattack";
            attackNcount++;
            //Raction = Random.Range(1, 11);
            if (directionSwicth==true) //次にジャンプする方向を決める(一回きり)
            {
                if (Raction == 1)//右
                {
                    Raction = 2;
                }
                else if (Raction == 2)//左
                {
                    Raction = 1;
                }
                directionSwicth = false;
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
            attackC.tag = "enemyheavyattack";
            attackNcount++;
            if (directionSwicth == true)//次にジャンプする方向を決める(一回きり)
            {
                if (Raction == 1)//右
                {
                    Raction = 2;
                }
                else if (Raction == 2)//左
                {
                    Raction = 1;
                }
                directionSwicth = false;
            }
            Attackcount = 0;
            Maxattackcount = Random.Range(2,4);
            JumpStratSwicth = true;
            attackSwicth = false;
        }
    }
    void attackMigration()
    {
        if (attackNO<=attackNcount)
        {
            if (CjumpSwcith==true) {
                Debug.Log("ジャンプ");
                //if (jumpattackR <= 4)
                //{
                    savePlayerPos = playerG.transform.position;//プレイヤーの位置を代入
                    //Debug.Log("ジャンプアタック");
                    if (savePlayerPos.x <= jumpAPpos && savePlayerPos.x >= -jumpAPpos)//プレイヤーが範囲内に入ったら
                    {
                        changeState(STATE.jump);
                    }
                    else
                    {
                        bulletSwicth = true;
                        changeState(STATE.jumpattack);
                    }
                //}
                //else if (jumpattackR > 4)
                //{
                //    changeState(STATE.jump);
                //}
            }
            else
            {
                changeState(STATE.jump);
            }
        }
        else if (attackNO > attackNcount)
        {
            changeState(STATE.move);
            anim.SetBool("move", true);
            anim.SetBool("lightattack", false);
            anim.SetBool("heavyattack", false);
        }
    }
    void jumpattack()
    {
        anim.SetBool("jumpattack", true);
        if (JumpStratSwicth==true)
        {
            Debug.Log("ジャンプアタック");
            attackC.tag = "enemyrightattack";
            gameObject.tag = "bossjump";
            anim.SetBool("lightattack", false);
            anim.SetBool("heavyattack", false);
            savePlayerPos = playerG.transform.position;//プレイヤーの位置を代入
            //if (savePlayerPos.x <= jumpAPpos && savePlayerPos.x >= -jumpAPpos)//プレイヤーが範囲内に入ったら
            //{
            //    changeState(STATE.jump);
            //    anim.SetBool("jumpattack", false);
            //    //JumpStratSwicth = false;
            //}
            //else
            //{
                Bpos = transform.position;
                if (savePlayerPos.x>=Bpos.x)//右ジャンプ
                {
                    jumpN = 1;
                    MaxjumpPos = (Bpos.x + savePlayerPos.x) / 2f;
                    Vector3 s = transform.localScale;
                    transform.localScale = new Vector3(-1f, s.y, s.z);
                }
                else if (savePlayerPos.x < Bpos.x)//左ジャンプ
                {
                    jumpN = -1;
                    MaxjumpPos = (Bpos.x + savePlayerPos.x) / 2f;
                    Vector3 s = transform.localScale;
                    transform.localScale = new Vector3(1f, s.y, s.z);
                }
            //}

            JumpStratSwicth = false;
        }

        if (jumpSwicth == true)
        {
            if (jumpN == 1)
            {
                //右にジャンプ
                if (savePlayerPos.x >= Bpos.x)
                {

                    Bpos = transform.position;//自身の座標を入れる
                    Vector3 i = new Vector3();
                    anim.SetFloat("jumpN", 1);
                    if (MaxjumpPos <= Bpos.x)//下がる
                    {
                        //Debug.Log("下がってる");
                        //anim.SetFloat("jumpN", 2);
                        aa += 20f * Time.deltaTime;
                        i.y = -1 * aa * Time.deltaTime;
                    }
                    else //上がる
                    {
                        //Debug.Log("上がってる");
                        //anim.SetFloat("jumpN", 1);
                        aa -= 20f * Time.deltaTime;
                        i.y = 1 * aa * Time.deltaTime;
                    }
                    i.x = 1 * JumpSpeed * Time.deltaTime;
                    transform.position += i;
                }
                else
                {
                    anim.SetFloat("jumpN", 2);
                    if (bulletSwicth == true)
                    {//弾が発射される
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject b = Instantiate(bulletObject);
                            Vector3 Mi = transform.position;
                            bulletController bc = b.GetComponent<bulletController>();
                            if (i == 0)
                            {
                                b.transform.position = new Vector3(Mi.x /*+ 8f*/, Mi.y - 4f, Mi.z);
                                bc.PMpos = 1;
                            }
                            else if (i == 1)
                            {
                                b.transform.position = new Vector3(Mi.x /*- 8f*/, Mi.y - 4f, Mi.z);
                                bc.PMpos = -1;
                            }
                        }
                        bulletSwicth = false;
                    }

                    transform.position = new Vector3(transform.position.x, SaveBpos.y, transform.position.z);
                }
            }
            else if (jumpN == -1)
            {
                //左にジャンプ
                if (savePlayerPos.x <= Bpos.x)
                {

                    Bpos = transform.position;//自身の座標を入れる
                    Vector3 i = new Vector3();
                    anim.SetFloat("jumpN", 1);
                    if (MaxjumpPos >= Bpos.x)//下がる
                    {
                        //Debug.Log("下がってる");
                        //anim.SetFloat("jumpN", 2);
                        aa += 20f * Time.deltaTime;
                        i.y = -1 * aa * Time.deltaTime;
                    }
                    else //上がる
                    {
                        //Debug.Log("上がってる");
                        //anim.SetFloat("jumpN", 1);
                        aa -= 20f * Time.deltaTime;
                        i.y = 1 * aa * Time.deltaTime;
                    }
                    i.x = -1 * JumpSpeed * Time.deltaTime;
                    transform.position += i;
                }
                else
                {
                    anim.SetFloat("jumpN", 2);
                    if (bulletSwicth==true) {//弾が発射される
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject b = Instantiate(bulletObject);
                            Vector3 Mi = transform.position;
                            bulletController bc = b.GetComponent<bulletController>();
                            if (i == 0)
                            {
                                b.transform.position = new Vector3(Mi.x /*+ 8f*/, Mi.y-4f, Mi.z);
                                bc.PMpos = 1;
                            }
                            else if (i == 1)
                            {
                                b.transform.position = new Vector3(Mi.x /*- 8f*/, Mi.y - 4f, Mi.z);
                                bc.PMpos = -1;
                            }
                        }
                        bulletSwicth = false;
                    }
                    transform.position = new Vector3(transform.position.x, SaveBpos.y, transform.position.z);
                }
            }
        }
    }
    void Counter()//カウンターをくらったとき
    {
        if (counterOneSwicth==true) {
            counterCount++;
            anim.SetBool("counter", true);
            anim.SetBool("heavyattack", false);
            counterOneSwicth = false;
        }
    }
    void Down()//ダウン状態
    {
        if (DownSwcith==true) {
            counterCount=0;
            MaxcounterCount++;
            anim.SetBool("down", true);
            anim.SetBool("heavyattack", false);
            DownSwcith = false;
        }
    }
    void Change()//状態変化
    {
        if (changeSwicth == true)
        {
            Debug.Log("半分");
            gameObject.tag = "bossjump";
            anim.SetBool("change", true);
            anim.SetBool("startS", true);
            jumpN = 0;
            attackNumberS = true;
            directionSwicth = true;
            counterHetSwicth = false;
            DownSwcith = true;
            attackNO = 0;
            attackNcount = 0;
            CjumpSwcith = true;
            anim.SetBool("move", false);
            anim.SetBool("jump", false);
            anim.SetFloat("jumpN", 0);
            anim.SetBool("lightattack", false);
            anim.SetBool("heavyattack", false);
            anim.SetBool("counter", false);
            anim.SetBool("down", false);
            changeSwicth = false;
        }
    }
    void Destory()//死亡時
    {
        if (destroySwicth==true)
        {
            anim.SetBool("destoy", true);
            destroySwicth = false;
        }
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
    public void EnemyDamage(int h)//ダメージを受けた時※インターフェース
    {
        if (Downdamgemode==true)
        {
            hp -= h * 1.5f;
        }
        else
        {
            hp -= h;
        }

        if (MaxHP/2>hp)//HPが半分になったら
        {
            if (SchangeSwicth==true) {
                gameObject.tag = "bossjump";
                changeState(STATE.change);
                SchangeSwicth = false;
            }
        }
        if (hp<=0)//死亡
        {
            Debug.Log("死亡");
            changeState(STATE.destoy);
        }
        

        
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
        SM = GameObject.FindWithTag("stageManager");
        SMC = SM.GetComponent<stageManagerC>();
        SaveBpos = transform.position;
        hp = MaxHP;
        Maxattackcount = 2;//最初は2回
        Raction = 2;
        attackNO = 0;
        attackNcount = 0;
        MaxcounterCount = 2;
        counterCount = 0;
        anim = GetComponent<Animator>();
        JumpStratSwicth = true;
        counterHetSwicth = false;
        attackSwicth = true;
        DownSwcith = true;
        changeSwicth = true;
        SchangeSwicth = true;
        destroySwicth = true;
        bulletSwicth = true;
        playerG = GameObject.FindWithTag("Player");//タグでプレイヤーのオブジェクトか判断して入れる
        stopTime = MaxStopTime;//最初だけすぐに動けるようにする

    }

    // Update is called once per frame
    void Update()
    {
        if (SMC.pauseSwicth == false)
        {
            //現在のステートを呼び出す
            switch (state)
            {
                case STATE.startA://初めに動く
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
                case STATE.jumpattack://ジャンプ攻撃する
                    jumpattack();
                    break;
                case STATE.counterH://カウンターを食らったとき
                    Counter();
                    break;
                case STATE.down://ダウンしたとき
                    Down();
                    break;
                case STATE.change://ダウンしたとき
                    Change();
                    break;
                case STATE.destoy://死亡時
                    Destory();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playercounter")
        {
            if (counterHetSwicth == true)
            {
                if (MaxcounterCount<=counterCount) {
                    Downdamgemode = true;
                    changeState(STATE.down);

                } else if (MaxcounterCount > counterCount) {
                    //Debug.Log("ヒット");
                    counterOneSwicth = true;
                    changeState(STATE.counterH);
                }
            }

        }
    }
}
