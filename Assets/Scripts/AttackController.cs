using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Rigidbody2D rd; //Rigidbody�I�u�W�F�N�g
    float attspeed = 6.0f;  //�I�u�W�F�N�g�ړ��X�s�[�h

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();   //Rigidbody�R���|�[�l���g�擾
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //�U���J�n��(Space�L�[�������ƍU���J�n)
        {
            rd.velocity = new Vector2(attspeed, 0);
        }//�X�s�[�h�����čU���I�u�W�F�N�g���ړ�
        else if (Input.GetMouseButton(0))
        {
            rd.velocity = new Vector2(attspeed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);    //�U���I�u�W�F�N�g�̔j��
    }
}
