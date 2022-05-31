using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlemanager : MonoBehaviour
{
    [SerializeField] GameObject SetumeiUI;
    public AudioClip ac;
    public string GameStartscene;

    AudioSource aS;
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("SaveXpos");//�L�[�̍폜
        PlayerPrefs.DeleteKey("SaveYpos");//�L�[�̍폜
        PlayerPrefs.DeleteKey("EventN");//�L�[�̍폜(EventN�̒l���폜)

        PlayerPrefs.DeleteKey("SaveCXpos");
        PlayerPrefs.DeleteKey("SaveCYpos");
        //�v���C���[�̈ړ��͈�
        PlayerPrefs.DeleteKey("CMaxPosx");
        PlayerPrefs.DeleteKey("CMaxPosy");
        PlayerPrefs.DeleteKey("CMinPosx");
        PlayerPrefs.DeleteKey("CMinPosy");
        //�J�����̈ړ��͈�
        PlayerPrefs.DeleteKey("CaMaxPosx");
        PlayerPrefs.DeleteKey("CaMaxPosy");
        PlayerPrefs.DeleteKey("CaMinPosx");
        PlayerPrefs.DeleteKey("CaMinPosy");
        aS.PlayOneShot(ac);
        //SceneManager.LoadScene(GameStartscene);
        FadeManager.Instance.LoadScene(GameStartscene, 0.5f);
    }
    public void GameStart()
    {
        aS.PlayOneShot(ac);
        //SceneManager.LoadScene(GameStartscene);
        FadeManager.Instance.LoadScene(GameStartscene, 0.5f);
    }

    public void UIbON()
    {
        SetumeiUI.SetActive(true);
    }

    public void UIbOFF()
    {
        SetumeiUI.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        aS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
