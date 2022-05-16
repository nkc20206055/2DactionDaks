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
        //�^�O�Ńv���C���[�̃I�u�W�F�N�g�����f���ē����
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // �J�������v���[���[�̈ʒu�ɍ��킹��
        pos = player.GetComponent<Transform>().position;

        // �J�����̈ړ��͈͐���
        pos.x = Mathf.Clamp(pos.x, moveMin.x, moveMax.x);
        pos.y = Mathf.Clamp(pos.y, moveMin.y, moveMax.y);

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
