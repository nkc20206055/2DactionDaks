using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage1ManagerC : MonoBehaviour
{
    [SerializeField] GameObject tutoralY, tutoralY2;//説明用の矢印を入れる
    [SerializeField] GameObject tutoralEnemy;
    public GameObject PlayerUI;
    public GameObject tutorialbackGrund;
    public Text tutorialTextO;
    public Image TutorialUI;//チュートリアルで表示する文字画像
    public Image whitebackI;

    Vector2 EventPos;
    GameObject playerG,BossG,CameraG;
    PlayerController PC;
    groundController gC;
    jump JumpC;
    cameraController1 cC1;
    Boss1Controller B1C;
    stageManagerC sMC;
    /*public */int TutorialNamber;//チュートリアルの順番を入れる
    float tutorialTime, MaxtutorialTime;//チュートリアル説明の時間,チュートリアルの最大時間
    float ImageFadeTime=0;
    string tutorial;
    bool GameStartSwicth,EventStratSwcith;
    bool tutorialNSwicth/*,fadeSwicth*/;//TutorialNamber更新用スイッチ,フェードする場合に起動
    public bool fadeSwicth,whiteImageSwicth;//フェードする場合に起動
    bool TutorialNinSwicth;


    void Imagefade()//チュートリアルの文字画像をフェードする
    {
        if (fadeSwicth==true)
        {
            if (tutorialTextO.color.a < 1)
            {
                //画像
                //ImageFadeTime = 1f * Time.deltaTime;
                //TutorialUI.color += new Color(0, 0, 0, ImageFadeTime);

                //テキスト
                ImageFadeTime = 1f * Time.deltaTime;
                tutorialTextO.color += new Color(0, 0, 0, ImageFadeTime);
                //Debug.Log(tutorialTextO.color.a);
            }
            //else if(ImageFadeTime >= 255)
            //{
            //    tutorialTextO.color = new Color(0, 0, 0, 255f);
            //    if (TutorialNinSwicth==true)
            //    {
            //        TutorialNamber++;
            //        TutorialNinSwicth = false;
            //    }
            //    //fadeSwicth = false;
            //}
        }
        else
        {
            
            if (tutorialTextO.color.a >= 0.1f)
            {
                //画像
                //ImageFadeTime = 1f * Time.deltaTime;
                //TutorialUI.color -= new Color(0, 0, 0, ImageFadeTime);

                //テキスト
                ImageFadeTime = 1f * Time.deltaTime;
                tutorialTextO.color -= new Color(0, 0, 0, ImageFadeTime);
                //Debug.Log(tutorialTextO.color.a);

            }else if (tutorialTextO.color.a < 0.1f)
            {
                
                tutorialTextO.color = new Color(0, 0, 0, 0f);
                if (TutorialNinSwicth == true)
                {
                    Debug.Log("次の説明");
                    TutorialNamber++;
                    tutorialNSwicth = true;
                    TutorialNinSwicth = false;
                }
            }

        }
    }
    void whiteImagefade()//手前の画像のフェード処理
    {
        if (whiteImageSwicth==false)
        {
            if (whitebackI.color.a < 1)
            {
                //テキスト
                ImageFadeTime = 0.7f * Time.deltaTime;
                whitebackI.color += new Color(0, 0, 0, ImageFadeTime);
                Debug.Log(whitebackI.color.a);

            }
            else if (whitebackI.color.a >=1f)
            {

                whitebackI.color = new Color(1, 1, 1, 1f);
                if (TutorialNinSwicth == true)
                {
                    Debug.Log("次の説明");
                    TutorialNamber++;
                    tutorialNSwicth = true;
                    TutorialNinSwicth = false;
                }
            }
        }
        else
        {
            if (whitebackI.color.a >= 0.1f)
            {
                //テキスト
                ImageFadeTime = 0.7f * Time.deltaTime;
                whitebackI.color -= new Color(0, 0, 0, ImageFadeTime);
                Debug.Log(whitebackI.color.a);

            }
            else if (whitebackI.color.a < 0.1f)
            {

                whitebackI.color = new Color(1, 1, 1, 0f);
                if (TutorialNamber==5) {
                    tutorialTextO.color = new Color(0f, 0f, 0f, 0f);
                    sMC.Eventnumber = 1;
                }

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sMC = GetComponent<stageManagerC>();                //自身のstageManagerCを取得
        playerG = GameObject.FindWithTag("Player");         //タグでプレイヤーを取得
        PC= playerG.GetComponent<PlayerController>();       //プレイヤーにあるPlayerControllerを取得
        gC = playerG.GetComponent<groundController>();      //プレイヤーにあるgroundControllerを取得
        JumpC = playerG.GetComponent<jump>();               //プレイヤーにあるjumpを取得
        BossG = GameObject.FindWithTag("boss");             //タグでボスを取得
        B1C = BossG.GetComponent<Boss1Controller>();        //ボスにあるBoss1Controllerを取得
        CameraG = GameObject.Find("Main Camera");           //メインカメラを取得
        cC1 = CameraG.GetComponent<cameraController1>();    //メインカメラにあるcameraController1を取得
        TutorialNamber = -1;
        MaxtutorialTime = 2;
        //GameStartSwicth = true;
        EventStratSwcith = true;
        tutorialNSwicth = true;
        TutorialNinSwicth = false;
        whiteImageSwicth = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameStartSwicth==true)
        //{

        //}
        if (sMC.Eventnumber == 0)//チュートリアル
        {
            Imagefade();
            whiteImagefade();
            if (TutorialNamber==-1)
            {
                if (tutorialNSwicth == true)
                {
                    Debug.Log("チュートリアル");
                    //fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    tutorialNSwicth = true;
                    TutorialNamber++;
                }
            }
            else if (TutorialNamber==0)//移動
            {
                if (tutorialNSwicth == true)
                {
                    tutorialTextO.text = "A、Dで移動";
                    tutorialTime = 0;
                    Debug.Log("移動");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    TutorialNinSwicth = true;
                    fadeSwicth = false;
                }
            }
            else if (TutorialNamber == 1)//ジャンプ
            {
                if (tutorialNSwicth==true)
                {
                    tutorialTextO.text = "spaceでジャンプ";
                    tutorialTime = 0;
                    Debug.Log("ジャンプ");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    TutorialNinSwicth = true;
                    fadeSwicth = false;
                }
            }
            else if (TutorialNamber == 2)//攻撃
            {
                if (tutorialNSwicth == true)
                {
                    tutorialTextO.text = "マウス左クリックで\n弱攻撃";
                    MaxtutorialTime = 6;
                    tutorialTime = 0;
                    Debug.Log("攻撃");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    tutoralY.SetActive(false);
                    TutorialNinSwicth = true;
                    fadeSwicth = false;
                }else if (tutorialTime>=2&&tutorialTime < 4)
                {
                    tutoralY.SetActive(true);
                    tutorialTextO.text = "左クリックを長押しすると\nゲージが溜まり";
                }
                else if (tutorialTime >= 4 && tutorialTime < MaxtutorialTime)
                {
                    tutorialTextO.text = "ゲージMaxで強攻撃";
                }
            }
            else if (TutorialNamber == 3)//ガード
            {
                if (tutorialNSwicth == true)
                {
                    tutorialTextO.text = "マウス右クリック長押しで\nガード";
                    MaxtutorialTime = 6;
                    tutorialTime = 0;
                    Debug.Log("ガード");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    tutoralY2.SetActive(false);
                    TutorialNinSwicth = true;
                    fadeSwicth = false;
                }
                else if (tutorialTime >= 2 && tutorialTime < 4)
                {
                    tutoralY2.SetActive(true);
                    tutorialTextO.text = "ガード中に攻撃を受けると\nゲージが減ります";
                }
                else if (tutorialTime >= 4 && tutorialTime < MaxtutorialTime)
                {
                    tutorialTextO.text = "このゲージがなくなると\n大きな隙をさらすので注意";
                }
            }
            else if (TutorialNamber == 4)//カウンター
            {
                if (tutorialNSwicth == true)
                {
                    tutorialTextO.text = "マウス右クリックで\nカウンター";
                    MaxtutorialTime = 6;
                    tutorialTime = 0;
                    Debug.Log("カウンター");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    if (tutoralEnemy==null) {
                        TutorialNinSwicth = true;
                        whiteImageSwicth = false;
                    }
                }
                else if (tutorialTime >= 2 && tutorialTime < 4)
                {
                    tutorialTextO.text = "敵のオレンジ色の攻撃に\n合わせて使うことで";
                }
                else if (tutorialTime >= 4 && tutorialTime < MaxtutorialTime)
                {
                    tutoralEnemy.SetActive(true);
                    tutorialTextO.text = "攻撃をはじいて、敵を隙だらけにできます。";
                }
            }else if (TutorialNamber == 5)
            {
                if (tutorialNSwicth == true)
                {
                    //tutorialTextO.color= new Color(0f,0f,0f,0f);
                    tutorialTextO.text = "";
                    tutorialbackGrund.SetActive(false);
                    Vector3 spp= new Vector3(54.32f, 0.16f, 0);
                    playerG.transform.position = spp;
                    PlayerPrefs.DeleteKey("SaveXpos");//キーの削除
                    PlayerPrefs.DeleteKey("SaveYpos");//キーの削除
                    PlayerPrefs.DeleteKey("EventN");//キーの削除(EventNの値を削除)
                    Vector2 savepos = playerG.transform.position;
                    PlayerPrefs.SetFloat("SaveXpos", spp.x);
                    PlayerPrefs.SetFloat("SaveYpos", spp.y);
                    PlayerPrefs.SetInt("EventN", 1);
                    PC.playerPosXClamp = 500f;
                    PC.MinsplayerPosXClamp = 43.52f;
                    PlayerPrefs.DeleteKey("SaveCXpos");
                    PlayerPrefs.DeleteKey("SaveCYpos");
                    CameraG.transform.position = new Vector3(63.6f, 7f, -10);
                    PlayerPrefs.SetFloat("SaveCXpos",63.6f);
                    PlayerPrefs.SetFloat("SaveCYpos",7f);
                    cC1.moveMax.x=500f;
                    cC1.moveMin.x = 63.6f;
                    //プレイヤーの移動範囲
                    PlayerPrefs.SetFloat("CMaxPosx", PC.playerPosXClamp);
                    PlayerPrefs.SetFloat("CMinPosx", PC.MinsplayerPosXClamp);
                    //カメラの移動範囲
                    PlayerPrefs.SetFloat("CaMaxPosx", cC1.moveMax.x);
                    PlayerPrefs.SetFloat("CaMinPosx", cC1.moveMin.x);
                    whiteImageSwicth = true;
                    tutorialNSwicth = false;
                    //sMC.Eventnumber = 1;
                }
            }


            if (tutorialTime < MaxtutorialTime)
            {
                tutorialTime += 1 * Time.deltaTime;
                //Debug.Log(tutorialTime);
            }

            //if (TutorialNamber<4&& TutorialNamber>4) {//チュートリアル表示時間
            //    if (tutorialTime >= MaxtutorialTime)
            //    {
            //        TutorialNamber++;
            //    }
            //    else
            //    {
            //        tutorialTime += 1 * Time.deltaTime;
            //    }
            //}
        }else if (sMC.Eventnumber == 1)
        {
            if (tutorialNSwicth==true)
            {
                tutorialTextO.text = "";
                tutorialbackGrund.SetActive(false);
                //PC.playerPosXClamp = 500f;
                //PC.MinsplayerPosXClamp = 43.52f;
                //playerG.transform.position = new Vector3(54.32f, 0.2f, 0);
                //cC1.moveMax.x = 500f;
                //cC1.moveMin.x = 63.6f;
                //CameraG.transform.position = new Vector3(63.6f, 7.02f, -10);
                tutorialNSwicth = false;
            }
            //Debug.Log("通常ステージ");
        }
        else if (sMC.Eventnumber==2)//ボスイベント
        {
            if (sMC.EventSwicth==true) {
                if (CameraG.transform.position.x< 327.2f)//カメラがx軸の51.3fにたどり着いていなとき
                {
                    Debug.Log("動いた");
                    if (EventStratSwcith == true) {
                        PlayerUI.SetActive(false);
                        PC.EventMode = true;
                        gC.EventMode = true;
                        JumpC.EventMode = true;
                        cC1.EventMode = true;
                        CameraG.transform.position=new Vector3(CameraG.transform.position.x,15.3f, 
                                                                    CameraG.transform.position.z);
                        PC.playerPosXClamp = 364.1f;
                        //PC.playerPosXClamp = 347.82f;


                        PC.MinsplayerPosXClamp = 307.24f;
                        EventStratSwcith = false;
                    }
                    if (CameraG.transform.position.y< 23.6f)
                    {
                        EventPos.y = 3.5f*1 * Time.deltaTime;
                    }
                    EventPos.x = 10*1 * Time.deltaTime;
                    CameraG.transform.position += new Vector3(EventPos.x, EventPos.y, 0);
                    //CameraG.transform.position = new Vector3(51.3f, CameraG.transform.position.y, 
                    //                                                CameraG.transform.position.z);

                }
                else if (CameraG.transform.position.x >= 51.3f)//カメラがx軸の51.3fにたどり着いたとき
                {
                    
                    if (B1C.StratSwicth==false&&B1C.SAswicth==false)
                    {
                        EventStratSwcith = true;
                        sMC.EventSwicth = false;
                    }
                    B1C.StratSwicth = true;
                }
            }
            else//ボス戦スタート
            {
                if (EventStratSwcith==true)
                {
                    PlayerUI.SetActive(true);
                    cC1.moveMax.x = 343.6f;
                    //cC1.moveMax.x = 327.2f;

                    cC1.moveMin.x = 327.2f;
                    cC1.EventMode = false;
                    cC1.bossMode = true;
                    PC.EventMode = false;
                    gC.EventMode = false;
                    JumpC.EventMode = false;
                    //cC1.EventMode = false;
                    EventStratSwcith = false;
                }
            }
        }
    }
}
