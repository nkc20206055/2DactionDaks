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
        //�^�O�Ńv���C���[�̃I�u�W�F�N�g�����f���ē����
        player = GameObject.FindWithTag("Player");
        //pos = player.GetComponent<Transform>().position;
        EventMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (EventMode==false) {
        //    //// �J�������v���[���[�̈ʒu�ɍ��킹��
        //    //pos = player.GetComponent<Transform>().position;
        //    //rondC = player.transform.position.y - transform.position.y;
        //    ////Debug.Log(rondC);
        ////if (rondC > -2 && rondC < 0)
        ////{
        ////    Debug.Log("��ԉ���");
        ////    //Debug.Log(rondC + "�߂�");
        ////    //Savepos = -1 * (-4-rondC);
        ////    //Savepos = Mathf.Floor(Savepos);
        ////    //Debug.Log(Savepos);

        ////    pos = new Vector3(pos.x, pos.y + 9f, pos.z);

        ////    //// �J�����̈ړ��͈͐���
        ////    //pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
        ////    //pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);
        ////}
        ////else if (rondC > -4 && rondC < -2)
        ////{
        ////    Debug.Log("����");
        ////    pos = new Vector3(pos.x, pos.y + 4.5f, pos.z);

        ////}
        ////else if (rondC < 0)
        ////{
        ////    Debug.Log("�܂��߂�");
        ////    pos = new Vector3(pos.x, pos.y, pos.z);
        ////}


        //    // �J�������v���[���[�̈ʒu�ɍ��킹��
        //    pos = player.GetComponent<Transform>().position;
        //    rondC = player.transform.position.y - transform.position.y;
        //    Debug.Log(rondC);
        //    string aa = rondC.ToString();//������ɕϊ�
        //    aa = aa.Substring(0, 4);
        //    float ss = float.Parse(aa);//�����_�^�ɕϊ�
        //    Debug.Log(ss);

        //    // �J�����̈ړ��͈͐���
        //    pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
        //    pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);

        //    transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        //}
    }

    void LateUpdate()//����Update����������I����Ă��瓮��
    {
        if (EventMode == false)
        {
            if (bossMode==true)
            {
                // �J�������v���[���[�̈ʒu�ɍ��킹��
                pos = player.GetComponent<Transform>().position;

                //rondC = player.transform.position.y - transform.position.y;
                //Debug.Log(rondC);

                // �J�����̈ړ��͈͐���
                pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
                pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);

                transform.position = new Vector3(pos.x, /*pos.y+7f*/transform.position.y, transform.position.z);
            } else {
                // �J�������v���[���[�̈ʒu�ɍ��킹��
                pos = player.GetComponent<Transform>().position;

                //rondC = player.transform.position.y - transform.position.y;
                //Debug.Log(rondC);

                // �J�����̈ړ��͈͐���
                pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
                pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);

                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            }
        }
    }
}
