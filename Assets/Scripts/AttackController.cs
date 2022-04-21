using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Rigidbody2D rd; //Rigidbodyオブジェクト
    float attspeed = 6.0f;  //オブジェクト移動スピード

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();   //Rigidbodyコンポーネント取得
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //攻撃開始時(Spaceキーを押すと攻撃開始)
        {
            rd.velocity = new Vector2(attspeed, 0);
        }//スピードをつけて攻撃オブジェクトを移動
        else if (Input.GetMouseButton(0))
        {
            rd.velocity = new Vector2(attspeed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);    //攻撃オブジェクトの破棄
    }
}
