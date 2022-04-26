using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class groundController : MonoBehaviour
{
    [SerializeField] GameObject CounterObject;

    //public GameObject hpui;//HPのUIを入れる変数
    //public AudioClip Se1;//SEを鳴らすオブジェクトを入れるところ

    public float counterTime;//カウンターできる時間
    public float damageTime;//無敵時間

    EcColliderController ECC;
    SpriteRenderer SR;//自分のSpriteRendererを入れる変数
    public int hp;
    int deletehp;//HPの変数

    private CameraController CC;//カメラのCameraControllerを取得する用
    private SpriteRenderer sR;//自分のSpriteRendererをいれる
    private Animator anim;
    private AudioSource AS;
    private Slider slider;//ガードゲージのUI
    private float CGtime, cTime;//
    private float sliderS;
    private bool MouseSwicth;//マウスが動かせるかどうか
    private bool gadeSwicth;//ガードが起動しているかどうか
    private bool counterSwicth, countSpeed;//
    private bool damageSwicth;//ダメージを食らっているかどうか
    private bool damageHetSwcith;//ダメージを食らったときにうごく
    private bool damageHetOn;//攻撃を食らったかどうか
    void animationcancel()//アニメーションを途中で終わらせるのに使う
    {
        anim.SetBool("counterattack", false);
        anim.SetBool("grndbreak", false);
        gameObject.layer = LayerMask.NameToLayer("Default");
        MouseSwicth = true;
        Debug.Log("動いた");
    }
    void counterONA()//カウンター成功時にカメラのcounterONが動くようにする
    {
        if (countSpeed == true)
        {
            Time.timeScale = 0.8f;
            countSpeed = false;
        }
        else
        {
            Time.timeScale = 1f;
            countSpeed = true;
        }
    }
    void Damage()//ダメージ時のHPの減りや無敵時間などを設定
    {
        if (damageSwicth == true)
        {
            //HPを減らす
            //if (damageHetSwcith == true)
            //{
            //    for (int i = 0; i < deletehp; i++)
            //    {
            //        GameObject s = hpui.transform.GetChild(hp - 1).gameObject;
            //        s.SetActive(false);
            //        hp--;
            //        Debug.Log(hp);
            //        if (hp <= 0)
            //        {
            //            damageSwicth = false;
            //            i = deletehp;
            //        }
            //    }
            //    deletehp = 0;
            //    damageHetSwcith = false;
            //    gameObject.layer = LayerMask.NameToLayer("PlayerDamge");//レイヤーマスクを変更する
            //}


            cTime += 1 * Time.deltaTime;
            if (cTime < damageTime)//点滅させる
            {
                float level = Mathf.Abs(Mathf.Sin(Time.time * 10));//Mathf.Absは絶対値、Mathf.SinはSin(サイン)が出される
                sR.color = new Color(1f, 1f, 1f, level);

            }
            else if (cTime >= damageTime)
            {
                sR.color = new Color(255, 255, 255, 255);
                gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                damageSwicth = false;
                damageHetOn = false;
                cTime = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;//最大hpの設定
        sR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        slider = GameObject.Find("guardgage").GetComponent<Slider>();
        CC = GameObject.Find("Main Camera").GetComponent<CameraController>();
        MouseSwicth = true;
        countSpeed = true;
        damageHetOn = false;
        sliderS = slider.maxValue;
        Debug.Log(sliderS);
    }

    // Update is called once per frame
    void Update()
    {
        //ガードブレイク
        if (sliderS <= 0)
        {
            MouseSwicth = false;
            gadeSwicth = false;
            anim.SetBool("grndbreak", true);
            anim.SetBool("guard", false);
            damageSwicth = false;
            damageHetOn = false;
            sliderS = slider.maxValue;
            slider.value = slider.maxValue;
        }

        //カウンターが成功した時
        //if (counterSwicth==true)
        //{
        //    if (ECC.counterHetSwicth == true)
        //    {
        //        Debug.Log("当たった");
        //        AS.PlayOneShot(Se1);//SEを鳴らす
        //        gameObject.layer = LayerMask.NameToLayer("PlayerDamge");//レイヤーマスクを変更する
        //        anim.SetBool("counterattack", true);
        //        anim.SetBool("counter", false);
        //        CounterObject.SetActive(false);
        //        CC.counterON();
        //    }
        //    counterSwicth = false;
        //}

        //カウンターとガードの操作
        if (MouseSwicth == true)
        {
            if (Input.GetMouseButton(1))//右クリック押しっぱなし
            {
                CGtime += 1 * Time.deltaTime;
                if (CGtime < counterTime) //カウンター確認
                {
                    //Debug.Log("カウンター中"+CGtime);
                    gameObject.layer = LayerMask.NameToLayer("PlayerDamge");
                    anim.SetBool("counter", true);
                    CounterObject.SetActive(true);

                }
                else if (CGtime >= counterTime) //ガード中
                {
                    Debug.Log("ガード中");
                    gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                    anim.SetBool("guard", true);
                    anim.SetBool("counter", false);
                    CounterObject.SetActive(false);
                    gadeSwicth = true;
                }
            }
            else if (Input.GetMouseButtonUp(1))//右マウスボタンを上げた時
            {
                //if (CGtime <= 0.05f)//すぐにできないよう猶予を作っている
                //{
                //    Debug.Log("カウンターキャンセル");
                //    gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                //    anim.SetBool("counter", false);
                //    CounterObject.SetActive(false);
                //}
                //else
                if (/*CGtime > 0.05f &&*/ CGtime < counterTime)//カウンター
                {
                    Debug.Log("カウンター");
                    //gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                    //anim.SetBool("counterattack", true);
                    anim.SetBool("counter", false);
                    CounterObject.SetActive(false);

                }
                else if (CGtime >= counterTime)//ガードを終わらせる
                {
                    Debug.Log("ガード終了");
                    gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
                    anim.SetBool("guard", false);
                    gadeSwicth = false;
                }
                CGtime = 0;
            }
        }
        else
        {
            Debug.Log("ガード中止");
            gameObject.layer = LayerMask.NameToLayer("Default");//レイヤーマスクを戻す
            CGtime = 0;
        }



        Damage();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "counterHet")
        {
            //Debug.Log(collision.gameObject);
            ECC = collision.gameObject.GetComponent<EcColliderController>();
            counterSwicth = true;
            if (counterSwicth == true)
            {
                if (ECC.counterHetSwicth == true)
                {
                    Debug.Log("当たった");

                    //AS.PlayOneShot(Se1);//SEを鳴らす

                    gameObject.layer = LayerMask.NameToLayer("PlayerDamge");//レイヤーマスクを変更する
                    anim.SetBool("counterattack", true);
                    anim.SetBool("counter", false);
                    CounterObject.SetActive(false);
                    CC.counterON();
                }
                counterSwicth = false;
            }
        }

        //if (damageHetOn == false)
        //{
        //    if (collision.gameObject.tag == "enemyrightattack")
        //    {

        //        damageHetSwcith = true;
        //        damageSwicth = true;
        //        if (gadeSwicth == true)
        //        {
        //            Debug.Log("敵の弱攻撃を軽減");
        //            damageHetOn = true;
        //            //ノーダメージ

        //        }
        //        else
        //        {
        //            Debug.Log("敵の弱攻撃がヒット");
        //            //減るのはハート1つ
        //            deletehp = 2;
        //            damageHetOn = true;

        //        }
        //    }
        //    else if (collision.gameObject.tag == "enemyheavyattack")
        //    {
        //        damageHetSwcith = true;
        //        damageSwicth = true;
        //        if (gadeSwicth == true)
        //        {
        //            Debug.Log("敵の強攻撃を軽減");
        //            //減るのはハート半分でガードゲージが減る
        //            deletehp = 1;
        //            slider.value -= 20;
        //            sliderS = slider.value;
        //            damageHetOn = true;

        //            //Debug.Log(sliderS);
        //        }
        //        else
        //        {
        //            Debug.Log("敵の強攻撃がヒット");
        //            //減るのはハート1.5つ
        //            deletehp = 3;
        //            damageHetOn = true;

        //        }
        //    }
        //}
    }
}
