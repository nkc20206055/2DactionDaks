using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : MonoBehaviour
{
    public bool SaveMode,lasteventSwicth;
    stageManagerC sMC;
    int SaveIeventN;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveMode==false)
        {
            sMC = GameObject.FindWithTag("stageManager").GetComponent<stageManagerC>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (SaveMode==true) //�Z�[�u�|�C���g�p�Ƀv���C���[�̈ʒu��ۑ�����
            {
                if (PlayerPrefs.HasKey("SaveXpos") && PlayerPrefs.HasKey("SaveYpos"))
                {
                    Debug.Log("�q�b�g");
                    PlayerPrefs.DeleteKey("SaveXpos");//�L�[�̍폜
                    PlayerPrefs.DeleteKey("SaveYpos");//�L�[�̍폜
                    Vector2 savepos = collision.gameObject.transform.position;
                    PlayerPrefs.SetFloat("SaveXpos", savepos.x);
                    PlayerPrefs.SetFloat("SaveYpos", transform.position.y);
                    if (PlayerPrefs.HasKey("SaveXpos") && PlayerPrefs.HasKey("SaveYpos"))
                    {
                        Debug.Log("SaveXpos�f�[�^��SaveYpos�f�[�^�͑��݂��܂�");
                    }
                }
            }
            else //�C�x���g����������ꍇ�Ɏg�p
            {
                //sMC.Eventnumber++;
                //if (lasteventSwicth==true)
                //{
                    sMC.Eventnumber++;
                    SaveIeventN = sMC.Eventnumber;
                    SaveIeventN--;
                //}
                //else
                //{
                //    sMC.Eventnumber++;
                //    SaveIeventN = sMC.Eventnumber;
                //}
                PlayerPrefs.SetInt("EventN", SaveIeventN);
                sMC.EventSwicth = true;
                gameObject.SetActive(false);
            }
        }
    }
}
