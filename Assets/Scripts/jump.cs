using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public bool EventMode;

    GameObject SM;
    stageManagerC SMC;

    Rigidbody2D Rd2D;�@//Rigidbody2D��ۑ�����
    //�W�����v�ϐ�1
    enum Status
    {
        GROUND = 1,
        UP = 2,
        DOWN = 3
    }

    Status playerStatus = Status.GROUND;   //�v���C���[�̏��
    //float firstSpeed = 16f;   //����
    float firstSpeed = 21f;   //�����i�ύX�����ق��j
    //const float gravity = 120.0f;    //�d��
    const float gravity = 60.0f;    //�d�́i�ύX�����ق��j
    const float jumpLowerLimit = 0.03f;
    private Animator anim = null;

    float timer = 0f;  //�o�ߎ���
    bool jumpKey = false;    //�W�����v�L�[
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
        ////�W�����v
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
            // �ڒn��
            case Status.GROUND:
                if (jumpKey)
                {
                    playerStatus = Status.UP;
                }
                break;

            // �㏸��
            case Status.UP:
                timer += Time.deltaTime;

                if (jumpKey || jumpLowerLimit > timer)
                {
                    newvec.y = firstSpeed;
                    newvec.y -= (gravity * Mathf.Pow(timer, 2));
                }
                else
                {
                    timer += Time.deltaTime; // �����𑁂߂�
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

            // ������
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
            keyLook = true; // �L�[��������b�N����
        }
    }
}
