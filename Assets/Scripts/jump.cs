using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public bool EventMode;

    GameObject SM;
    stageManagerC SMC;

    Rigidbody2D Rd2D;　//Rigidbody2Dを保存する
    //ジャンプ変数1
    enum Status
    {
        GROUND = 1,
        UP = 2,
        DOWN = 3
    }

    Status playerStatus = Status.GROUND;   //プレイヤーの状態
    //float firstSpeed = 16f;   //初速
    float firstSpeed = 21f;   //初速（変更したほう）
    //const float gravity = 120.0f;    //重力
    const float gravity = 60.0f;    //重力（変更したほう）
    const float jumpLowerLimit = 0.03f;
    private Animator anim = null;

    float timer = 0f;  //経過時間
    bool jumpKey = false;    //ジャンプキー
    bool keyLook = false;

    // Start is called before the first frame update
    void Start()
    {
        EventMode = false;
        Rd2D = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ////ジャンプ
        //if (EventMode==false) {
        if (Input.GetKey(KeyCode.Space))
        {
            if (EventMode == false)
            {
                if (!keyLook)
                {
                    jumpKey = true;
                }
                else
                {
                    jumpKey = false;
                }
            }
        }
        else
        {
                jumpKey = false;
                keyLook = false;

        }
        //}
    }

    private void FixedUpdate()
    {
        Vector2 newvec = Vector2.zero;
        switch (playerStatus)
        {
            // 接地時
            case Status.GROUND:
                if (jumpKey)
                {
                    playerStatus = Status.UP;
                }
                break;

            // 上昇時
            case Status.UP:
                timer += Time.deltaTime;

                if (jumpKey || jumpLowerLimit > timer)
                {
                    newvec.y = firstSpeed;
                    newvec.y -= (gravity * Mathf.Pow(timer, 2));
                }
                else
                {
                    timer += Time.deltaTime; // 落下を早める
                    newvec.y = firstSpeed;
                    newvec.y -= (gravity * Mathf.Pow(timer, 2));
                }
                if (0f > newvec.y)
                {
                    playerStatus = Status.DOWN;
                    newvec.y = 0f;
                    timer = 0.1f;
                }
                break;

            // 落下時
            case Status.DOWN:
                timer += Time.deltaTime;

                newvec.y = 0f;
                newvec.y = -(gravity * Mathf.Pow(timer, 2));
                break;

            default:
                break;
        }

        Rd2D.velocity = newvec;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (playerStatus == Status.DOWN &&
            collision.gameObject.name.Contains("Ground"))
        {
            playerStatus = Status.GROUND;
            timer = 0f;
            keyLook = true; // キー操作をロックする
        }
    }
}
