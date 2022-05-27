using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController1 : MonoBehaviour
{
    public bool EventMode;
    public Vector2 moveMax;
    public Vector2 moveMin;
    Vector3 pos;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //タグでプレイヤーのオブジェクトか判断して入れる
        player = GameObject.FindWithTag("Player");
        EventMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventMode==false) {
            float ssp = player.transform.position.y - transform.position.y;
            if (ssp>-4)
            {
                Debug.Log(ssp+"近い");
                float o = -1*(-4 - ssp);
                Debug.Log(o);

                // カメラをプレーヤーの位置に合わせる
                pos = player.GetComponent<Transform>().position;
                pos = new Vector3(pos.x, pos.y + o, pos.z);

                // カメラの移動範囲制限
                pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
                pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);
            }
            else
            {
                // カメラをプレーヤーの位置に合わせる
                pos = player.GetComponent<Transform>().position;

                // カメラの移動範囲制限
                pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
                pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);
            }

            //// カメラをプレーヤーの位置に合わせる
            //pos = player.GetComponent<Transform>().position;

            //// カメラの移動範囲制限
            //pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
            //pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}
