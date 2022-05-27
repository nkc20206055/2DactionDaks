using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : MonoBehaviour
{
    public bool SaveMode,lasteventSwicth,eventSwicth;
    GameObject CameraG;
    stageManagerC sMC;
    int SaveIeventN;
    // Start is called before the first frame update
    void Start()
    {
        CameraG = GameObject.FindWithTag("MainCamera");
        if (SaveMode==false)
        {
            sMC = GameObject.FindWithTag("stageManager").GetComponent<stageManagerC>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (SaveMode==true) //セーブポイント用にプレイヤーの位置を保存する
            {
                if (PlayerPrefs.HasKey("SaveXpos") && PlayerPrefs.HasKey("SaveYpos"))
                {
                    Debug.Log("ヒット");
                    PlayerPrefs.DeleteKey("SaveXpos");//キーの削除
                    PlayerPrefs.DeleteKey("SaveYpos");//キーの削除
                    PlayerPrefs.DeleteKey("SaveCXpos");
                    PlayerPrefs.DeleteKey("SaveCYpos");
                    Vector2 savepos = collision.gameObject.transform.position;
                    PlayerPrefs.SetFloat("SaveXpos", savepos.x);
                    PlayerPrefs.SetFloat("SaveYpos", transform.position.y);
                    Vector2 SaveposC = CameraG.transform.position;
                    PlayerPrefs.SetFloat("SaveXpos", SaveposC.x);
                    PlayerPrefs.SetFloat("SaveYpos", SaveposC.y);
                    if (PlayerPrefs.HasKey("SaveXpos") && PlayerPrefs.HasKey("SaveYpos"))
                    {
                        Debug.Log("SaveXposデータとSaveYposデータは存在します");
                    }
                }
            }
            else //イベントが発生する場合に使用
            {
                //if (eventSwicth==true) {

                //}
                //else {
                    //sMC.Eventnumber++;
                    if (lasteventSwicth == true)
                    {
                        sMC.Eventnumber++;
                        SaveIeventN = sMC.Eventnumber;
                        SaveIeventN--;
                    }
                    else
                    {
                        sMC.Eventnumber++;
                        SaveIeventN = sMC.Eventnumber;
                    }
                    PlayerPrefs.SetInt("EventN", SaveIeventN);
                    sMC.EventSwicth = true;
                    gameObject.SetActive(false);
                //}
            }
        }
    }
}
