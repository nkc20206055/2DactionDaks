using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController1 : MonoBehaviour
{
    public bool EventMode,bossMode;
    public Vector2 moveMax;
    public Vector2 moveMin;
    Vector3 pos;
    GameObject player;
    float rondC, Savepos;
    // Start is called before the first frame update
    void Start()
    {
        //タグでプレイヤーのオブジェクトか判断して入れる
        player = GameObject.FindWithTag("Player");
        //pos = player.GetComponent<Transform>().position;
        EventMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (EventMode==false) {
        //    //// カメラをプレーヤーの位置に合わせる
        //    //pos = player.GetComponent<Transform>().position;
        //    //rondC = player.transform.position.y - transform.position.y;
        //    ////Debug.Log(rondC);
        ////if (rondC > -2 && rondC < 0)
        ////{
        ////    Debug.Log("一番遠い");
        ////    //Debug.Log(rondC + "近い");
        ////    //Savepos = -1 * (-4-rondC);
        ////    //Savepos = Mathf.Floor(Savepos);
        ////    //Debug.Log(Savepos);

        ////    pos = new Vector3(pos.x, pos.y + 9f, pos.z);

        ////    //// カメラの移動範囲制限
        ////    //pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
        ////    //pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);
        ////}
        ////else if (rondC > -4 && rondC < -2)
        ////{
        ////    Debug.Log("遠い");
        ////    pos = new Vector3(pos.x, pos.y + 4.5f, pos.z);

        ////}
        ////else if (rondC < 0)
        ////{
        ////    Debug.Log("まだ近い");
        ////    pos = new Vector3(pos.x, pos.y, pos.z);
        ////}


        //    // カメラをプレーヤーの位置に合わせる
        //    pos = player.GetComponent<Transform>().position;
        //    rondC = player.transform.position.y - transform.position.y;
        //    Debug.Log(rondC);
        //    string aa = rondC.ToString();//文字列に変換
        //    aa = aa.Substring(0, 4);
        //    float ss = float.Parse(aa);//小数点型に変換
        //    Debug.Log(ss);

        //    // カメラの移動範囲制限
        //    pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
        //    pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);

        //    transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        //}
    }

    void LateUpdate()//他のUpdateがいったん終わってから動く
    {
        if (EventMode == false)
        {
            if (bossMode==true)
            {
                // カメラをプレーヤーの位置に合わせる
                pos = player.GetComponent<Transform>().position;

                //rondC = player.transform.position.y - transform.position.y;
                //Debug.Log(rondC);

                // カメラの移動範囲制限
                pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
                pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);

                transform.position = new Vector3(pos.x, /*pos.y+7f*/transform.position.y, transform.position.z);
            } else {
                // カメラをプレーヤーの位置に合わせる
                pos = player.GetComponent<Transform>().position;

                //rondC = player.transform.position.y - transform.position.y;
                //Debug.Log(rondC);

                // カメラの移動範囲制限
                pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
                pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);

                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            }
        }
    }
}
