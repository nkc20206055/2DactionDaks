using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Maxtime;
    public bool SizuSwicth;
    GameObject Player;//�v���C���[���擾
    Camera MiCamera;//���g��Camera���擾
    Vector3 SaveMiPos, SavePos;
    float Sizutime = 0;
    float MaxSizu, counterSizu;
    private bool GamespeedSwicth, counterSwicth;
    
    // Start is called before the first frame update
    void Start()
    {
        SaveMiPos = gameObject.transform.position;
        Player = GameObject.FindWithTag("Player");//�^�O�Ńv���C���[�̃I�u�W�F�N�g�����f���ē����
        //Player = GameObject.Find("/Player");
        //Debug.Log(Player);
        MiCamera = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
