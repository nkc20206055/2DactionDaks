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

    public int Eventnumber;//�C�x���g���s������
    //public float SavePosY;//�Z�[�u�|�C���g��Y���W�ۑ��p
    public string retrySceneName;//���g���C�������ɍs��Scene�̖��O�������
    public string GOSceneName;//�Q�[���I�[�o�[�������ɍs��Scene�̖��O�������
    public bool normalSwicth;//�������Ȃǂ�Scene�ł�true�ɂ��Ă���
    public bool pauseSwicth;
    public bool EventSwicth;//�C�x���g���N���Ă邩�ǂ���

    GameObject playerG, bossG;
    groundController gC;

    private Image stopI;
    private bool stopmodeSwicth, DestroyMi;

    //�{�^������p
    public void Retry()//���g���C
    {
        //PlayerPrefs.Save();//"SaveXpos","SaveYpos"��ۑ�
        Time.timeScale = 1f;
        SceneManager.LoadScene(retrySceneName);
    }
    public void GameOver()//�Q�[���I�[�o�[
    {
        PlayerPrefs.DeleteKey("SaveXpos");//�L�[�̍폜
        PlayerPrefs.DeleteKey("SaveYpos");//�L�[�̍폜
        PlayerPrefs.DeleteKey("EventN");//�L�[�̍폜(EventN�̒l���폜)
        //SceneManager.LoadScene(GOSceneName);

        SceneManager.LoadScene(retrySceneName);
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
        Time.timeScale = 1f;
        if (normalSwicth==false) {
            playerG = GameObject.FindWithTag("Player");
            gC = playerG.GetComponent<groundController>();
            //bossG = GameObject.FindWithTag("boss");
            stopI = stopUIhaikei.GetComponent<Image>();
            stopmodeSwicth = false;
            DestroyMi = false;
            EventSwicth = true;
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
                //PlayerPrefs.SetString("SaveXpos", "SaveYpos","EventN");//�ʒu�̃Z�[�u�p
                PlayerPrefs.SetFloat("SaveXpos", Ppos.x);
                PlayerPrefs.SetFloat("SaveYpos", Ppos.y);
                playerG.transform.position = new Vector3(Ppos.x,Ppos.y,0);
            }

            if (PlayerPrefs.HasKey("EventN"))//�C�x���g�p�̒l����������
            {
                Eventnumber = PlayerPrefs.GetInt("EventN");
            }
            else
            {
                PlayerPrefs.SetInt("EventN", 0);
                Eventnumber = PlayerPrefs.GetInt("EventN");
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
    
}
