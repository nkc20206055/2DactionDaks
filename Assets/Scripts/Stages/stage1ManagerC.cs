using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage1ManagerC : MonoBehaviour
{
    public Image TutorialUI;//�`���[�g���A���ŕ\�����镶���摜

    GameObject playerG,BossG,CameraG;
    PlayerController PC;
    groundController gC;
    jump JumpC;
    cameraController1 cC1;
    Boss1Controller B1C;
    stageManagerC sMC;
    int TutorialNamber;//�`���[�g���A���̏��Ԃ�����
    float tutorialTime, MaxtutorialTime;//�`���[�g���A�������̎���,�`���[�g���A���̍ő厞��
    float ImageFadeTime=0;
    string tutorial;
    bool EventStratSwcith;
    bool tutorialNSwicth/*,fadeSwicth*/;//TutorialNamber�X�V�p�X�C�b�`,�t�F�[�h����ꍇ�ɋN��
    public bool fadeSwicth;//�t�F�[�h����ꍇ�ɋN��


    void Imagefade()//�`���[�g���A���̕����摜���t�F�[�h����
    {
        if (fadeSwicth==true)
        {
            if (ImageFadeTime<=255)
            {
                ImageFadeTime = 1f * Time.deltaTime;
                TutorialUI.color += new Color(0, 0, 0, ImageFadeTime);
            }
        }
        else
        {
            if (ImageFadeTime>=0)
            {
                ImageFadeTime = 1f * Time.deltaTime;
                TutorialUI.color -= new Color(0, 0, 0, ImageFadeTime);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sMC = GetComponent<stageManagerC>();                //���g��stageManagerC���擾
        playerG = GameObject.FindWithTag("Player");         //�^�O�Ńv���C���[���擾
        PC= playerG.GetComponent<PlayerController>();       //�v���C���[�ɂ���PlayerController���擾
        gC = playerG.GetComponent<groundController>();      //�v���C���[�ɂ���groundController���擾
        JumpC = playerG.GetComponent<jump>();               //�v���C���[�ɂ���jump���擾
        BossG = GameObject.FindWithTag("boss");             //�^�O�Ń{�X���擾
        B1C = BossG.GetComponent<Boss1Controller>();        //�{�X�ɂ���Boss1Controller���擾
        CameraG = GameObject.Find("Main Camera");           //���C���J�������擾
        cC1 = CameraG.GetComponent<cameraController1>();    //���C���J�����ɂ���cameraController1���擾
        TutorialNamber = 0;
        MaxtutorialTime = 2;
        EventStratSwcith = true;
        tutorialNSwicth = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sMC.Eventnumber == 0)//�`���[�g���A��
        {
            Imagefade();

            if (TutorialNamber==0)//�ړ�
            {
                if (tutorialNSwicth == true)
                {
                    Debug.Log("�ړ�");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }
            }
            else if (TutorialNamber == 1)//�W�����v
            {
                if (tutorialNSwicth==true)
                {

                    tutorialNSwicth = false;
                }
            }
            else if (TutorialNamber == 2)//�U��
            {
                if (tutorialNSwicth == true)
                {

                    tutorialNSwicth = false;
                }
            }
            else if (TutorialNamber == 3)//�K�[�h
            {
                if (tutorialNSwicth == true)
                {

                    tutorialNSwicth = false;
                }
            }
            else if (TutorialNamber == 4)//�J�E���^�[
            {
                if (tutorialNSwicth == true)
                {

                    tutorialNSwicth = false;
                }
            }

            if (TutorialNamber<4&& TutorialNamber>4) {//�`���[�g���A���\������
                if (tutorialTime >= MaxtutorialTime)
                {
                    TutorialNamber++;
                }
                else
                {
                    tutorialTime += 1 * Time.deltaTime;
                }
            }
        }
        else if (sMC.Eventnumber==2)//�{�X�C�x���g
        {
            if (sMC.EventSwicth==true) {
                if (CameraG.transform.position.x< 51.3f)//�J������x����51.3f�ɂ��ǂ蒅���Ă��ȂƂ�
                {
                    Debug.Log("������");
                    if (EventStratSwcith == true) {
                        PC.EventMode = true;
                        gC.EventMode = true;
                        JumpC.EventMode = true;
                        cC1.EventMode = true;

                        PC.playerPosXClamp = 71.4f;
                        PC.MinsplayerPosXClamp = 31.9f;
                        EventStratSwcith = false;
                    }
                    float ss = 10*1 * Time.deltaTime;
                    CameraG.transform.position += new Vector3(ss,0,0);
                    //CameraG.transform.position = new Vector3(51.3f, CameraG.transform.position.y, 
                    //                                                CameraG.transform.position.z);

                }
                else if (CameraG.transform.position.x >= 51.3f)//�J������x����51.3f�ɂ��ǂ蒅�����Ƃ�
                {
                    
                    if (B1C.StratSwicth==false&&B1C.SAswicth==false)
                    {
                        EventStratSwcith = true;
                        sMC.EventSwicth = false;
                    }
                    B1C.StratSwicth = true;
                }
            }
            else//�{�X��X�^�[�g
            {
                if (EventStratSwcith==true)
                {
                    PC.EventMode = false;
                    gC.EventMode = false;
                    JumpC.EventMode = false;
                    //cC1.EventMode = false;
                    EventStratSwcith = false;
                }
            }
        }
    }
}
