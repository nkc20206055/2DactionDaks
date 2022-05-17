using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageManagerC : MonoBehaviour
{
    [SerializeField] GameObject stopUI;//�ꎞ��~�ŏo������UI
    [SerializeField] GameObject GameoverUI;//Gameover�ŏo������UI
    public GameObject stopUIhaikei;

    //public float SavePosY;//�Z�[�u�|�C���g��Y���W�ۑ��p
    public string retrySceneName;//���g���C�������ɍs��Scene�̖��O�������
    public string GOSceneName;//�Q�[���I�[�o�[�������ɍs��Scene�̖��O�������
    public bool normalSwicth;//�������Ȃǂ�Scene�ł�true�ɂ��Ă���
    public bool pauseSwicth;

    GameObject playerG, bossG;
    groundController gC;

    private Image stopI;
    private bool stopmodeSwicth, DestroyMi;

    //�{�^������p
    public void Retry()//���g���C
    {
        //PlayerPrefs.Save();//"SaveXpos","SaveYpos"��ۑ�
        SceneManager.LoadScene(retrySceneName);
    }
    public void GameOver()//�Q�[���I�[�o�[
    {
        PlayerPrefs.DeleteKey("SaveXpos");//�L�[�̍폜
        PlayerPrefs.DeleteKey("SaveYpos");//�L�[�̍폜
        //SceneManager.LoadScene(GOSceneName);
    }


    void stopmode()//�ꎞ��~
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (stopmodeSwicth == false)
            {
                stopI.color = new Color(0, 0, 0, 0.5f);
                stopUI.SetActive(true);
                pauseSwicth = true;
                stopmodeSwicth = true;
                Time.timeScale = 0f;
            }
            else
            {
                stopI.color = new Color(0, 0, 0, 0);
                stopUI.SetActive(false);
                pauseSwicth = false;
                stopmodeSwicth = false;
                Time.timeScale = 1f;
            }
        }
    }
    void GamaOver()//�Q�[���I�[�o�[
    {
        if (stopI.color.a>=0.7f)
        {
            Debug.Log("���S");
            GameoverUI.SetActive(true);
        }
        else if (stopI.color.a< 0.7f)
        {
            float ss = 0.2f * Time.deltaTime;
            stopI.color+= new Color(0,0,0,ss);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (normalSwicth==false) {
            playerG = GameObject.FindWithTag("Player");
            gC = playerG.GetComponent<groundController>();
            //bossG = GameObject.FindWithTag("boss");
            stopI = stopUIhaikei.GetComponent<Image>();
            stopmodeSwicth = false;
            DestroyMi = false;

            //PlayerPrefs.SetString("SaveXpos","SaveYpos");//�ʒu�̃Z�[�u�p
            if (PlayerPrefs.HasKey("SaveXpos") && PlayerPrefs.HasKey("SaveYpos"))//�Z�[�u����������
            {
                Debug.Log("SaveXpos�f�[�^��SaveYpos�f�[�^�͑��݂��܂�");
                Vector3 pos = new Vector3(PlayerPrefs.GetFloat("SaveXpos"), PlayerPrefs.GetFloat("SaveYpos"), 0);
                playerG.transform.position = pos;
            }
            else//�Z�[�u���Ȃ�������
            {
                Debug.Log("SaveXpos�f�[�^��SaveYpos�f�[�^�͑��݂��܂���");
                Vector2 Ppos = new Vector2(playerG.transform.position.x, 
                                           playerG.transform.position.y);
                PlayerPrefs.SetString("SaveXpos", "SaveYpos");//�ʒu�̃Z�[�u�p
                PlayerPrefs.SetFloat("SaveXpos", Ppos.x);
                PlayerPrefs.SetFloat("SaveYpos", Ppos.y);
                playerG.transform.position = new Vector3(Ppos.x,Ppos.y,0);
            }
        }

        pauseSwicth = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (normalSwicth == false)
        {
            if (gC.hp<=0)
            {
                GamaOver();
            } else if (gC.hp>0) {
                stopmode();
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))//PlayerPrefs�͏���ɕۑ�����Ă����Œ���
        {
            PlayerPrefs.DeleteKey("SaveXpos");//�L�[�̍폜
            PlayerPrefs.DeleteKey("SaveYpos");//�L�[�̍폜
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")//�Z�[�u�|�C���g�p�Ƀv���C���[�̈ʒu��ۑ�����
        {
            Debug.Log("�q�b�g");
            PlayerPrefs.DeleteKey("SaveXpos");//�L�[�̍폜
            PlayerPrefs.DeleteKey("SaveYpos");//�L�[�̍폜
            Vector2 savepos = collision.gameObject.transform.position;
            PlayerPrefs.SetFloat("SaveXpos", savepos.x);
            PlayerPrefs.SetFloat("SaveYpos", savepos.y);
            if (PlayerPrefs.HasKey("SaveXpos") && PlayerPrefs.HasKey("SaveYpos"))
            {
                Debug.Log("SaveXpos�f�[�^��SaveYpos�f�[�^�͑��݂��܂�");
            }
            //if (PlayerPrefs.HasKey("SaveYpos"))
            //{
            //    Debug.Log("SaveYpos�f�[�^�͑��݂��܂�");
            //}

        }
    }
}
