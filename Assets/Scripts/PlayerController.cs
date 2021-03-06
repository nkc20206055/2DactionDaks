using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioClip[] Clip;

    GameObject SM;
    stageManagerC SMC;

    public float MoveSpeed;   //移動スピード

    Vector3 SaveVec;   //移動などを保存する
    Rigidbody2D Rd2D;　//Rigidbody2Dを保存する
    pacController pacC;
    AudioSource AS;
    private Animator anim = null;
    float InputVec;    //横移動時の向きの値を入れる

    public bool EventMode;

    public float gravity; //重力
    //攻撃
    public float MaxattackTime;//外部から変更できる最大ため時間
    private Slider chargeSlider;//攻撃を貯めるのを見やすくするためのスライダー
    private float attackAutTime;//
    private float attackTime;//攻撃を貯めた時間
    private bool normalSwicth;//通常の状態に戻すための変数
    private bool isAttack = false;
    float Power = 0;
    private bool animationcancelSwicth = true;

    //移動制限処理用変数
    private Vector2 playerPos;
    //private readonly float playerPosXClamp = 15.0f;
    //private readonly float playerPosYClamp = 15.0f;
    public float playerPosXClamp = 15.0f;//右側
    public float playerPosYClamp = 15.0f;//上側
    public float MinsplayerPosXClamp;//左側
    public float MinsplayerPosYClamp;//下側

    public void SE(int i)
    {
        AS.PlayOneShot(Clip[i]);
    }
    // Start is called before the first frame update
    void Start()
    {
        normalSwicth = false;
        EventMode = false;
        SM = GameObject.FindWithTag("stageManager");
        SMC = SM.GetComponent<stageManagerC>();
        Rd2D = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        //一時的に動かないようにしている
        chargeSlider = GameObject.Find("attackchargeSlider").GetComponent<Slider>();
        pacC = transform.GetChild(2).gameObject.GetComponent<pacController>();
    }

    // Update is called sonce per frame
    void Update()
    {
        if (SMC.pauseSwicth == false)
        {
            if (EventMode == false)
            {
                //移動制限　//一時的に動かないようにしている
                this.MovingRestrictions();

                //移動
                InputVec = Input.GetAxisRaw("Horizontal");
                if (InputVec != 0)//プレイヤーの向き
                {
                    Vector3 SavelocalScale = transform.localScale;//現在の向きを保存
                    transform.localScale = new Vector3(/*SavelocalScale.x **/ InputVec, SavelocalScale.y, SavelocalScale.z);
                }
                if (InputVec > 0)
                {
                    anim.SetBool("run", true);
                }
                else if (InputVec < 0)
                {
                    anim.SetBool("run", true);
                }
                else
                {
                    anim.SetBool("run", false);
                }
                {
                    //攻撃
                    if (Input.GetMouseButton(0))//攻撃を貯める
                    {
                        if (attackTime < MaxattackTime)
                        {
                            attackTime += 1 * Time.deltaTime;
                            chargeSlider.value += 1 * Time.deltaTime;
                            //Debug.Log(attackTime);
                        }
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        chargeSlider.value = 0;
                        if (attackTime >= MaxattackTime)//強攻撃
                        {
                            anim.SetBool("hevayAttack", true);
                            isAttack = true;
                            pacC.heavyattackSwicth = true;
                            StartCoroutine("WaitForAttack");
                            attackTime = 0;
                            attackAutTime = 0.6f;//戻るまでの時間(書き換えていい)
                            normalSwicth = true;
                        }
                        else if (attackTime < MaxattackTime)//弱攻撃
                        {
                            anim.SetBool("lightAttack", true);
                            isAttack = true;
                            pacC.rightattackSwicth = true;
                            StartCoroutine("WaitForAttack");
                            attackTime = 0;
                            attackAutTime = 0.5f;//戻るまでの時間(書き換えていい)
                            normalSwicth = true;
                        }
                    }
                    else if (normalSwicth == true)//攻撃を戻す
                    {
                        attackTime += 1 * Time.deltaTime;
                        if (attackTime >= attackAutTime)
                        {

                            anim.SetBool("lightAttack", false);
                            anim.SetBool("hevayAttack", false);
                            attackTime = 0;
                            normalSwicth = false;
                        }
                    }

                }//一時的に動かないようにしている
            }
        }

        if(EventMode == true)
        {
            if (animationcancelSwicth == true)
            {
                Debug.Log("動いた");
                anim.SetBool("run", false);
                anim.SetBool("lightAttack", false);
                anim.SetBool("hevayAttack", false);
                anim.SetBool("counter", false);
                anim.SetBool("counterattack", false);
                anim.SetBool("guard", false);
                anim.SetBool("guardbreak", false);
                animationcancelSwicth = false;
            }
        }

    }


    void FixedUpdate()
    {
        if (SMC.pauseSwicth == false)
        {
            if (EventMode == false) {
                SaveVec.x = MoveSpeed * InputVec * Time.deltaTime;
                transform.position += SaveVec;
            }
        }
        //攻撃
        //if(Input.GetMouseButtonDown(0))
        //{           
        //        anim.SetBool("lightAttack", true);
        //        isAttack = true;
        //    pacC.rightattackSwicth = true;
        //        StartCoroutine("WaitForAttack");   
        //}
        //else if(Input.GetMouseButton(0))
        //{
        //        anim.SetBool("hevayAttack", true);
        //        isAttack = true;
        //    pacC.heavyattackSwicth = true;
        //        StartCoroutine("WaitForAttack");
        //}
        //else
        //{
        //    anim.SetBool("lightAttack", false);
        //    anim.SetBool("hevayAttack", false);
        //}


    }
    //カメラ位置制限
    private void MovingRestrictions()
    {
        //変数に自分の今の位置を入れる
        this.playerPos = transform.position;

        //playerPos変数のxとyに制限した値を入れる
        //playerPos.xという値を-playerPosXClamp〜playerPosXClampの間に収める
        //this.playerPos.x = Mathf.Clamp(this.playerPos.x, -this.playerPosXClamp, this.playerPosXClamp);
        //this.playerPos.y = Mathf.Clamp(this.playerPos.y, -this.playerPosYClamp, this.playerPosYClamp);

        this.playerPos.x = Mathf.Clamp(this.playerPos.x, this.MinsplayerPosXClamp, this.playerPosXClamp);
        this.playerPos.y = Mathf.Clamp(this.playerPos.y, this.MinsplayerPosYClamp, this.playerPosYClamp);

        transform.position = new Vector2(this.playerPos.x, this.playerPos.y);
    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttack = false;

    }


}
