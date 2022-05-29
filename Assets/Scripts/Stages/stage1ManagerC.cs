using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage1ManagerC : MonoBehaviour
{
    [SerializeField] GameObject tutoralY, tutoralY2;//�����p�̖�������
    [SerializeField] GameObject tutoralEnemy;
    public GameObject PlayerUI;
    public GameObject tutorialbackGrund;
    public Text tutorialTextO;
    public Image TutorialUI;//�`���[�g���A���ŕ\�����镶���摜
    public Image whitebackI;

    Vector2 EventPos;
    GameObject playerG,BossG,CameraG;
    PlayerController PC;
    groundController gC;
    jump JumpC;
    cameraController1 cC1;
    Boss1Controller B1C;
    stageManagerC sMC;
    /*public */int TutorialNamber;//�`���[�g���A���̏��Ԃ�����
    float tutorialTime, MaxtutorialTime;//�`���[�g���A�������̎���,�`���[�g���A���̍ő厞��
    float ImageFadeTime=0;
    string tutorial;
    bool GameStartSwicth,EventStratSwcith;
    bool tutorialNSwicth/*,fadeSwicth*/;//TutorialNamber�X�V�p�X�C�b�`,�t�F�[�h����ꍇ�ɋN��
    public bool fadeSwicth,whiteImageSwicth;//�t�F�[�h����ꍇ�ɋN��
    bool TutorialNinSwicth;


    void Imagefade()//�`���[�g���A���̕����摜���t�F�[�h����
    {
        if (fadeSwicth==true)
        {
            if (tutorialTextO.color.a < 1)
            {
                //�摜
                //ImageFadeTime = 1f * Time.deltaTime;
                //TutorialUI.color += new Color(0, 0, 0, ImageFadeTime);

                //�e�L�X�g
                ImageFadeTime = 1f * Time.deltaTime;
                tutorialTextO.color += new Color(0, 0, 0, ImageFadeTime);
                //Debug.Log(tutorialTextO.color.a);
            }
            //else if(ImageFadeTime >= 255)
            //{
            //    tutorialTextO.color = new Color(0, 0, 0, 255f);
            //    if (TutorialNinSwicth==true)
            //    {
            //        TutorialNamber++;
            //        TutorialNinSwicth = false;
            //    }
            //    //fadeSwicth = false;
            //}
        }
        else
        {
            
            if (tutorialTextO.color.a >= 0.1f)
            {
                //�摜
                //ImageFadeTime = 1f * Time.deltaTime;
                //TutorialUI.color -= new Color(0, 0, 0, ImageFadeTime);

                //�e�L�X�g
                ImageFadeTime = 1f * Time.deltaTime;
                tutorialTextO.color -= new Color(0, 0, 0, ImageFadeTime);
                //Debug.Log(tutorialTextO.color.a);

            }else if (tutorialTextO.color.a < 0.1f)
            {
                
                tutorialTextO.color = new Color(0, 0, 0, 0f);
                if (TutorialNinSwicth == true)
                {
                    Debug.Log("���̐���");
                    TutorialNamber++;
                    tutorialNSwicth = true;
                    TutorialNinSwicth = false;
                }
            }

        }
    }
    void whiteImagefade()//��O�̉摜�̃t�F�[�h����
    {
        if (whiteImageSwicth==false)
        {
            if (whitebackI.color.a < 1)
            {
                //�e�L�X�g
                ImageFadeTime = 0.7f * Time.deltaTime;
                whitebackI.color += new Color(0, 0, 0, ImageFadeTime);
                Debug.Log(whitebackI.color.a);

            }
            else if (whitebackI.color.a >=1f)
            {

                whitebackI.color = new Color(1, 1, 1, 1f);
                if (TutorialNinSwicth == true)
                {
                    Debug.Log("���̐���");
                    TutorialNamber++;
                    tutorialNSwicth = true;
                    TutorialNinSwicth = false;
                }
            }
        }
        else
        {
            if (whitebackI.color.a >= 0.1f)
            {
                //�e�L�X�g
                ImageFadeTime = 0.7f * Time.deltaTime;
                whitebackI.color -= new Color(0, 0, 0, ImageFadeTime);
                Debug.Log(whitebackI.color.a);

            }
            else if (whitebackI.color.a < 0.1f)
            {

                whitebackI.color = new Color(1, 1, 1, 0f);
                if (TutorialNamber==5) {
                    tutorialTextO.color = new Color(0f, 0f, 0f, 0f);
                    sMC.Eventnumber = 1;
                }

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
        TutorialNamber = -1;
        MaxtutorialTime = 2;
        //GameStartSwicth = true;
        EventStratSwcith = true;
        tutorialNSwicth = true;
        TutorialNinSwicth = false;
        whiteImageSwicth = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameStartSwicth==true)
        //{

        //}
        if (sMC.Eventnumber == 0)//�`���[�g���A��
        {
            Imagefade();
            whiteImagefade();
            if (TutorialNamber==-1)
            {
                if (tutorialNSwicth == true)
                {
                    Debug.Log("�`���[�g���A��");
                    //fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    tutorialNSwicth = true;
                    TutorialNamber++;
                }
            }
            else if (TutorialNamber==0)//�ړ�
            {
                if (tutorialNSwicth == true)
                {
                    tutorialTextO.text = "A�AD�ňړ�";
                    tutorialTime = 0;
                    Debug.Log("�ړ�");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    TutorialNinSwicth = true;
                    fadeSwicth = false;
                }
            }
            else if (TutorialNamber == 1)//�W�����v
            {
                if (tutorialNSwicth==true)
                {
                    tutorialTextO.text = "space�ŃW�����v";
                    tutorialTime = 0;
                    Debug.Log("�W�����v");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    TutorialNinSwicth = true;
                    fadeSwicth = false;
                }
            }
            else if (TutorialNamber == 2)//�U��
            {
                if (tutorialNSwicth == true)
                {
                    tutorialTextO.text = "�}�E�X���N���b�N��\n��U��";
                    MaxtutorialTime = 6;
                    tutorialTime = 0;
                    Debug.Log("�U��");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    tutoralY.SetActive(false);
                    TutorialNinSwicth = true;
                    fadeSwicth = false;
                }else if (tutorialTime>=2&&tutorialTime < 4)
                {
                    tutoralY.SetActive(true);
                    tutorialTextO.text = "���N���b�N�𒷉��������\n�Q�[�W�����܂�";
                }
                else if (tutorialTime >= 4 && tutorialTime < MaxtutorialTime)
                {
                    tutorialTextO.text = "�Q�[�WMax�ŋ��U��";
                }
            }
            else if (TutorialNamber == 3)//�K�[�h
            {
                if (tutorialNSwicth == true)
                {
                    tutorialTextO.text = "�}�E�X�E�N���b�N��������\n�K�[�h";
                    MaxtutorialTime = 6;
                    tutorialTime = 0;
                    Debug.Log("�K�[�h");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    tutoralY2.SetActive(false);
                    TutorialNinSwicth = true;
                    fadeSwicth = false;
                }
                else if (tutorialTime >= 2 && tutorialTime < 4)
                {
                    tutoralY2.SetActive(true);
                    tutorialTextO.text = "�K�[�h���ɍU�����󂯂��\n�Q�[�W������܂�";
                }
                else if (tutorialTime >= 4 && tutorialTime < MaxtutorialTime)
                {
                    tutorialTextO.text = "���̃Q�[�W���Ȃ��Ȃ��\n�傫�Ȍ������炷�̂Œ���";
                }
            }
            else if (TutorialNamber == 4)//�J�E���^�[
            {
                if (tutorialNSwicth == true)
                {
                    tutorialTextO.text = "�}�E�X�E�N���b�N��\n�J�E���^�[";
                    MaxtutorialTime = 6;
                    tutorialTime = 0;
                    Debug.Log("�J�E���^�[");
                    fadeSwicth = true;
                    tutorialNSwicth = false;
                }

                if (tutorialTime >= MaxtutorialTime)
                {
                    if (tutoralEnemy==null) {
                        TutorialNinSwicth = true;
                        whiteImageSwicth = false;
                    }
                }
                else if (tutorialTime >= 2 && tutorialTime < 4)
                {
                    tutorialTextO.text = "�G�̃I�����W�F�̍U����\n���킹�Ďg�����Ƃ�";
                }
                else if (tutorialTime >= 4 && tutorialTime < MaxtutorialTime)
                {
                    tutoralEnemy.SetActive(true);
                    tutorialTextO.text = "�U�����͂����āA�G�������炯�ɂł��܂��B";
                }
            }else if (TutorialNamber == 5)
            {
                if (tutorialNSwicth == true)
                {
                    //tutorialTextO.color= new Color(0f,0f,0f,0f);
                    tutorialTextO.text = "";
                    tutorialbackGrund.SetActive(false);
                    Vector3 spp= new Vector3(54.32f, 0.16f, 0);
                    playerG.transform.position = spp;
                    PlayerPrefs.DeleteKey("SaveXpos");//�L�[�̍폜
                    PlayerPrefs.DeleteKey("SaveYpos");//�L�[�̍폜
                    PlayerPrefs.DeleteKey("EventN");//�L�[�̍폜(EventN�̒l���폜)
                    Vector2 savepos = playerG.transform.position;
                    PlayerPrefs.SetFloat("SaveXpos", spp.x);
                    PlayerPrefs.SetFloat("SaveYpos", spp.y);
                    PlayerPrefs.SetInt("EventN", 1);
                    PC.playerPosXClamp = 500f;
                    PC.MinsplayerPosXClamp = 43.52f;
                    PlayerPrefs.DeleteKey("SaveCXpos");
                    PlayerPrefs.DeleteKey("SaveCYpos");
                    CameraG.transform.position = new Vector3(63.6f, 7f, -10);
                    PlayerPrefs.SetFloat("SaveCXpos",63.6f);
                    PlayerPrefs.SetFloat("SaveCYpos",7f);
                    cC1.moveMax.x=500f;
                    cC1.moveMin.x = 63.6f;
                    //�v���C���[�̈ړ��͈�
                    PlayerPrefs.SetFloat("CMaxPosx", PC.playerPosXClamp);
                    PlayerPrefs.SetFloat("CMinPosx", PC.MinsplayerPosXClamp);
                    //�J�����̈ړ��͈�
                    PlayerPrefs.SetFloat("CaMaxPosx", cC1.moveMax.x);
                    PlayerPrefs.SetFloat("CaMinPosx", cC1.moveMin.x);
                    whiteImageSwicth = true;
                    tutorialNSwicth = false;
                    //sMC.Eventnumber = 1;
                }
            }


            if (tutorialTime < MaxtutorialTime)
            {
                tutorialTime += 1 * Time.deltaTime;
                //Debug.Log(tutorialTime);
            }

            //if (TutorialNamber<4&& TutorialNamber>4) {//�`���[�g���A���\������
            //    if (tutorialTime >= MaxtutorialTime)
            //    {
            //        TutorialNamber++;
            //    }
            //    else
            //    {
            //        tutorialTime += 1 * Time.deltaTime;
            //    }
            //}
        }else if (sMC.Eventnumber == 1)
        {
            if (tutorialNSwicth==true)
            {
                tutorialTextO.text = "";
                tutorialbackGrund.SetActive(false);
                //PC.playerPosXClamp = 500f;
                //PC.MinsplayerPosXClamp = 43.52f;
                //playerG.transform.position = new Vector3(54.32f, 0.2f, 0);
                //cC1.moveMax.x = 500f;
                //cC1.moveMin.x = 63.6f;
                //CameraG.transform.position = new Vector3(63.6f, 7.02f, -10);
                tutorialNSwicth = false;
            }
            //Debug.Log("�ʏ�X�e�[�W");
        }
        else if (sMC.Eventnumber==2)//�{�X�C�x���g
        {
            if (sMC.EventSwicth==true) {
                if (CameraG.transform.position.x< 327.2f)//�J������x����51.3f�ɂ��ǂ蒅���Ă��ȂƂ�
                {
                    Debug.Log("������");
                    if (EventStratSwcith == true) {
                        PlayerUI.SetActive(false);
                        PC.EventMode = true;
                        gC.EventMode = true;
                        JumpC.EventMode = true;
                        cC1.EventMode = true;
                        CameraG.transform.position=new Vector3(CameraG.transform.position.x,15.3f, 
                                                                    CameraG.transform.position.z);
                        PC.playerPosXClamp = 364.1f;
                        //PC.playerPosXClamp = 347.82f;


                        PC.MinsplayerPosXClamp = 307.24f;
                        EventStratSwcith = false;
                    }
                    if (CameraG.transform.position.y< 23.6f)
                    {
                        EventPos.y = 3.5f*1 * Time.deltaTime;
                    }
                    EventPos.x = 10*1 * Time.deltaTime;
                    CameraG.transform.position += new Vector3(EventPos.x, EventPos.y, 0);
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
                    PlayerUI.SetActive(true);
                    cC1.moveMax.x = 343.6f;
                    //cC1.moveMax.x = 327.2f;

                    cC1.moveMin.x = 327.2f;
                    cC1.EventMode = false;
                    cC1.bossMode = true;
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
