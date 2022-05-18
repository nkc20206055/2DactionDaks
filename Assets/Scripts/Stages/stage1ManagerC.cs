using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage1ManagerC : MonoBehaviour
{
    GameObject playerG,BossG,CameraG;
    PlayerController PC;
    groundController gC;
    jump JumpC;
    cameraController1 cC1;
    Boss1Controller B1C;
    stageManagerC sMC;
    bool EventStratSwcith;
    // Start is called before the first frame update
    void Start()
    {
        sMC = GetComponent<stageManagerC>();
        playerG = GameObject.FindWithTag("Player");
        PC= playerG.GetComponent<PlayerController>();
        gC = playerG.GetComponent<groundController>();
        JumpC= playerG.GetComponent<jump>();
        BossG = GameObject.FindWithTag("boss");
        B1C = BossG.GetComponent<Boss1Controller>();
        CameraG = GameObject.Find("Main Camera");
        cC1 = CameraG.GetComponent<cameraController1>();
        EventStratSwcith = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sMC.Eventnumber==1)//ボスイベント
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
