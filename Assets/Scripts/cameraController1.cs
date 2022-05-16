using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController1 : MonoBehaviour
{
    public Vector2 moveMax;
    public Vector2 moveMin;
    Vector3 pos;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //タグでプレイヤーのオブジェクトか判断して入れる
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // カメラをプレーヤーの位置に合わせる
        pos = player.GetComponent<Transform>().position;

        // カメラの移動範囲制限
        pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
        pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
