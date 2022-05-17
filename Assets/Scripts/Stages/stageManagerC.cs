using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageManagerC : MonoBehaviour
{
    [SerializeField] GameObject stopUI;//一時停止で出現するUI
    [SerializeField] GameObject GameoverUI;//Gameoverで出現するUI
    public GameObject stopUIhaikei;

    //public float SavePosY;//セーブポイントのY座標保存用
    public string retrySceneName;//リトライした時に行くSceneの名前をいれる
    public string GOSceneName;//ゲームオーバーした時に行くSceneの名前をいれる
    public bool normalSwicth;//お試しなどのSceneではtrueにしておく
    public bool pauseSwicth;

    GameObject playerG, bossG;
    groundController gC;

    private Image stopI;
    private bool stopmodeSwicth, DestroyMi;

    //ボタン操作用
    public void Retry()//リトライ
    {
        //PlayerPrefs.Save();//"SaveXpos","SaveYpos"を保存
        SceneManager.LoadScene(retrySceneName);
    }
    public void GameOver()//ゲームオーバー
    {
        PlayerPrefs.DeleteKey("SaveXpos");//キーの削除
        PlayerPrefs.DeleteKey("SaveYpos");//キーの削除
        //SceneManager.LoadScene(GOSceneName);
    }


    void stopmode()//一時停止
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
    void GamaOver()//ゲームオーバー
    {
        if (stopI.color.a>=0.7f)
        {
            Debug.Log("死亡");
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

            //PlayerPrefs.SetString("SaveXpos","SaveYpos");//位置のセーブ用
            if (PlayerPrefs.HasKey("SaveXpos") && PlayerPrefs.HasKey("SaveYpos"))//セーブがあったら
            {
                Debug.Log("SaveXposデータとSaveYposデータは存在します");
                Vector3 pos = new Vector3(PlayerPrefs.GetFloat("SaveXpos"), PlayerPrefs.GetFloat("SaveYpos"), 0);
                playerG.transform.position = pos;
            }
            else//セーブがなかったら
            {
                Debug.Log("SaveXposデータとSaveYposデータは存在しません");
                Vector2 Ppos = new Vector2(playerG.transform.position.x, 
                                           playerG.transform.position.y);
                PlayerPrefs.SetString("SaveXpos", "SaveYpos");//位置のセーブ用
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

        if (Input.GetKeyDown(KeyCode.Delete))//PlayerPrefsは勝手に保存されているんで注意
        {
            PlayerPrefs.DeleteKey("SaveXpos");//キーの削除
            PlayerPrefs.DeleteKey("SaveYpos");//キーの削除
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")//セーブポイント用にプレイヤーの位置を保存する
        {
            Debug.Log("ヒット");
            PlayerPrefs.DeleteKey("SaveXpos");//キーの削除
            PlayerPrefs.DeleteKey("SaveYpos");//キーの削除
            Vector2 savepos = collision.gameObject.transform.position;
            PlayerPrefs.SetFloat("SaveXpos", savepos.x);
            PlayerPrefs.SetFloat("SaveYpos", savepos.y);
            if (PlayerPrefs.HasKey("SaveXpos") && PlayerPrefs.HasKey("SaveYpos"))
            {
                Debug.Log("SaveXposデータとSaveYposデータは存在します");
            }
            //if (PlayerPrefs.HasKey("SaveYpos"))
            //{
            //    Debug.Log("SaveYposデータは存在します");
            //}

        }
    }
}
