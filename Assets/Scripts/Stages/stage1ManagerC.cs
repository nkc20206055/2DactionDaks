using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage1ManagerC : MonoBehaviour
{
    public Image TutorialUI;//チュートリアルで表示する文字画像

    GameObject playerG,BossG,CameraG;
    PlayerController PC;
    groundController gC;
    jump JumpC;
    cameraController1 cC1;
    Boss1Controller B1C;
    stageManagerC sMC;
    int TutorialNamber;//チュートリアルの順番を入れる
    float tutorialTime, MaxtutorialTime;//チュートリアル説明の時間,チュートリアルの最大時間
    float ImageFadeTime=0;
    string tutorial;
    bool EventStratSwcith;
    bool tutorialNSwicth/*,fadeSwicth*/;//TutorialNamber更新用スイッチ,フェードする場合に起動
    public bool fadeSwicth;//フェードする場合に起動


    void Imagefade()//チュートリアルの文字画像をフェードする
    {
        if (fadeSwicth==true)
        {
            if (ImageFadeTime<=255)
            {
                ImageFadeTime = 1f * Time.deltaTime;
                TutorialUI.color += new Color(0, 0, 0, ImageFadeTime);
            }
        }
        else
        {
            if (ImageFadeTime>=0)
            {
                ImageFadeTime = 1f * Time.deltaTime;
                TutorialUI.color -= new Color(0, 0, 0, ImageFadeTime);
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
        TutorialNamber = 0;
        MaxtutorialTime = 2;
        EventStratSwcith = true;
        tutorialNSwicth = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sMC.Eventnumber == 0)//チュートリアル
        {
            Imagefade();

            if (TutorialNamber==0)//移動
            {
                if (tutorialNSwicth == true)
                {
                    Debug.Log("移動");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }
            }
            else if (TutorialNamber == 1)//ジャンプ
            {
                if (tutorialNSwicth==true)
                {

                    tutorialNSwicth = false;
                }
            }
            else if (TutorialNamber == 2)//攻撃
            {
                if (tutorialNSwicth == true)
                {

                    tutorialNSwicth = false;
                }
            }
            else if (TutorialNamber == 3)//ガード
            {
                if (tutorialNSwicth == true)
                {

                    tutorialNSwicth = false;
                }
            }
            else if (TutorialNamber == 4)//カウンター
            {
                if (tutorialNSwicth == true)
                {

                    tutorialNSwicth = false;
                }
            }

            if (TutorialNamber<4&& TutorialNamber>4) {//チュートリアル表示時間
                if (tutorialTime >= MaxtutorialTime)
                {
                    TutorialNamber++;
                }
                else
                {
                    tutorialTime += 1 * Time.deltaTime;
                }
            }
        }
        else if (sMC.Eventnumber==2)//ボスイベント
        {
            if (sMC.EventSwicth==true) {
                if (CameraG.transform.position.x< 51.3f)//カメラがx軸の51.3fにたどり着いていなとき
                {
                    Debug.Log("動いた");
                    if (EventStratSwcith == true) {
                        PC.EventMode = true;
                        gC.EventMode = true;
                        JumpC.EventMode = true;
                        cC1.EventMode = true;

                        PC.playerPosXClamp = 71.4f;
                        PC.MinsplayerPosXClamp = 31.9f;
                        EventStratSwcith = false;
                    }
                    float ss = 10*1 * Time.deltaTime;
                    CameraG.transform.position += new Vector3(ss,0,0);
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
