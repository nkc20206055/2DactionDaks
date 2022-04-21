using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Maxtime;
    public bool SizuSwicth;
    GameObject Player;//プレイヤーを取得
    Camera MiCamera;//自身のCameraを取得
    Vector3 SaveMiPos, SavePos;
    float Sizutime = 0;
    float MaxSizu, counterSizu;
    private bool GamespeedSwicth, counterSwicth;
    public void counterON()
    {
        SizuSwicth = true;
        counterSwicth = true;
    }
    void CameraSizu()//カメラをプレイヤーに近づける
    {
        if (Maxtime > Sizutime)
        {
            Sizutime += 1 * Time.deltaTime;
            //if (GamespeedSwicth == true)
            //{
            //    //Time.timeScale = 1f;
            //    GamespeedSwicth = false;
            //}
            if (MiCamera.orthographicSize > counterSizu)
            {
                MiCamera.orthographicSize -= 0.3f;
            }
            //else
            //{
            //    GamespeedSwicth = true;
            //}
        }
        else if (Maxtime <= Sizutime)
        {
            //if (GamespeedSwicth==true)
            //{
            //    Time.timeScale = 1f;
            //    GamespeedSwicth = false;
            //}
            //gameObject.transform.position = SaveMiPos;
            if (MiCamera.orthographicSize < MaxSizu)
            {
                MiCamera.orthographicSize += 1f;
            }
            else
            {
                gameObject.transform.position = SaveMiPos;
                //MiCamera.orthographicSize = MaxSizu;
                SizuSwicth = false;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveMiPos = gameObject.transform.position;
        Player = GameObject.Find("prototypePlayer");
        //Player = GameObject.Find("/Player");
        Debug.Log(Player);
        MiCamera = GetComponent<Camera>();
        SizuSwicth = false;
        GamespeedSwicth = false;
        MaxSizu = MiCamera.orthographicSize;
        counterSizu = 5;
        //Time.timeScale = 2.0f;//ゲームスピードを指定
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))//お試し
        //{
        //    Sizutime = 0;
        //    SavePos = Player.transform.position;
        //    gameObject.transform.position = new Vector3(SavePos.x,SavePos.y,-10);
        //    Time.timeScale = 0.5f;
        //    SizuSwicth = true;
        //}

        if (SizuSwicth == true)
        {
            if (counterSwicth == true)
            {
                Sizutime = 0;
                SavePos = Player.transform.position;
                gameObject.transform.position = new Vector3(SavePos.x, SavePos.y, -10);
                //Time.timeScale = 0.8f;
                //GamespeedSwicth = true;
                counterSwicth = false;
            }
            CameraSizu();
        }
    }
}
