using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageManagerC : MonoBehaviour
{
    [SerializeField] GameObject stopUI;//一時停止で出現するUI
    [SerializeField] GameObject GameoverUI;//Gameoverで出現するUI
    [SerializeField] GameObject GameclearUI;
    public GameObject stopUIhaikei;

    public AudioClip ac;
    public int Eventnumber;//イベントを行った回数
    //public float SavePosY;//セーブポイントのY座標保存用
    public string retrySceneName;//リトライした時に行くSceneの名前をいれる
    public string GOSceneName;//ゲームオーバーした時に行くSceneの名前をいれる
    public bool normalSwicth;//お試しなどのSceneではtrueにしておく
    public bool pauseSwicth;
    public bool EventSwicth;//イベントが起きてるかどうか

    GameObject playerG, bossG,CameraG;
    PlayerController pc;
    groundController gC;
    cameraController1 cc1;
    Boss1Controller B1C;
    AudioSource AS;

    private Image stopI;
    private bool stopmodeSwicth, DestroyMi;
    private bool clearSwicth;

    //ボタン操作用
    public void Retry()//リトライ
    {
        AS.PlayOneShot(ac);
        //PlayerPrefs.Save();//"SaveXpos","SaveYpos"を保存
        Time.timeScale = 1f;
        //SceneManager.LoadScene(retrySceneName);
        FadeManager.Instance.LoadScene(retrySceneName, 0.5f);
    }
    public void GameOver()//ゲームオーバー
    {
        AS.PlayOneShot(ac);
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("SaveXpos");//キーの削除
        PlayerPrefs.DeleteKey("SaveYpos");//キーの削除
        PlayerPrefs.DeleteKey("EventN");//キーの削除(EventNの値を削除)
        //プレイヤーの移動範囲
        PlayerPrefs.DeleteKey("CMaxPosx");
        PlayerPrefs.DeleteKey("CMaxPosy");
        PlayerPrefs.DeleteKey("CMinPosx");
        PlayerPrefs.DeleteKey("CMinPosy");
        //カメラの移動範囲
        PlayerPrefs.DeleteKey("CaMaxPosx");
        PlayerPrefs.DeleteKey("CaMaxPosy");
        PlayerPrefs.DeleteKey("CaMinPosx");
        PlayerPrefs.DeleteKey("CaMinPosy");
        //SceneManager.LoadScene(GOSceneName);

        //SceneManager.LoadScene(GOSceneName);
        FadeManager.Instance.LoadScene(GOSceneName, 1f);
    }


    void stopmode()//一時停止
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (stopmodeSwicth == false)
            {
                Cursor.visible = true;//カーソル表示
                stopI.color = new Color(0, 0, 0, 0.5f);
                stopUI.SetActive(true);
                pauseSwicth = true;
                stopmodeSwicth = true;
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.visible = false;//カーソル非表示
                stopI.color = new Color(0, 0, 0, 0);
                stopUI.SetActive(false);
                pauseSwicth = false;
                stopmodeSwicth = false;
                Time.timeScale = 1f;
            }
        }
    }
    void Gamaover()//ゲームオーバー
    {
        if (stopI.color.a>=0.7f)
        {
            Debug.Log("死亡");
            Cursor.visible = true;//カーソル表示
            GameoverUI.SetActive(true);
        }
        else if (stopI.color.a< 0.7f)
        {
            float ss = 0.2f * Time.deltaTime;
            stopI.color+= new Color(0,0,0,ss);
        }
    }
    void GameClear()
    {
        if (stopI.color.a >= 1f)
        {
            Debug.Log("クリア");
            Cursor.visible = true;//カーソル表示
            GameclearUI.SetActive(true);
        }
        else if (stopI.color.a < 1f)
        {
            if (clearSwicth==true)
            {
                stopI.color = new Color(255, 255, 255, 0);
                clearSwicth = false;
            }
            float ss = 0.5f * Time.deltaTime;
            stopI.color += new Color(0, 0, 0, ss);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;//ゲームスピードを通常に戻す
        if (normalSwicth==false) {
            Cursor.visible = false;//カーソル非表示
            playerG = GameObject.FindWithTag("Player");
            CameraG = GameObject.FindWithTag("MainCamera");
            cc1 = CameraG.GetComponent<cameraController1>();
            pc = playerG.GetComponent<PlayerController>();
            gC = playerG.GetComponent<groundController>();
            bossG = GameObject.FindWithTag("boss");
            B1C = bossG.GetComponent<Boss1Controller>();
            stopI = stopUIhaikei.GetComponent<Image>();
            AS = GetComponent<AudioSource>();
            stopmodeSwicth = false;
            DestroyMi = false;
            EventSwicth = true;
            clearSwicth = true;
            //PlayerPrefs.SetString("SaveXpos","SaveYpos");//位置のセーブ用
            if (PlayerPrefs.HasKey("SaveXpos") && PlayerPrefs.HasKey("SaveYpos"))//セーブがあったら
            {
                Debug.Log("SaveXposデータとSaveYposデータは存在します");
                Vector3 pos = new Vector3(PlayerPrefs.GetFloat("SaveXpos"), PlayerPrefs.GetFloat("SaveYpos"), 0);
                playerG.transform.position = pos;
                Vector3 Cpos = new Vector3(PlayerPrefs.GetFloat("SaveCXpos"), PlayerPrefs.GetFloat("SaveCYpos"), -10);
                CameraG.transform.position = Cpos;
                if (PlayerPrefs.HasKey("CMaxPosx") && PlayerPrefs.HasKey("CMaxPosy") &&
                    PlayerPrefs.HasKey("CMinPosx") && PlayerPrefs.HasKey("CMinPosy"))
                {
                    Debug.Log("四つのデータは存在します");
                    pc.playerPosXClamp = PlayerPrefs.GetFloat("CMaxPosx");
                    pc.playerPosYClamp = PlayerPrefs.GetFloat("CMaxPosy");
                    pc.MinsplayerPosXClamp = PlayerPrefs.GetFloat("CMinPosx");
                    pc.MinsplayerPosYClamp = PlayerPrefs.GetFloat("CMinPosy");

                }
                else
                {
                    Debug.Log("四つのデータは存在しません");
                    //プレイヤーの移動範囲
                    PlayerPrefs.SetFloat("CMaxPosx", pc.playerPosXClamp);
                    PlayerPrefs.SetFloat("CMaxPosy", pc.playerPosYClamp);
                    PlayerPrefs.SetFloat("CMinPosx", pc.MinsplayerPosXClamp);
                    PlayerPrefs.SetFloat("CMinPosy", pc.MinsplayerPosYClamp);
                }

                if (PlayerPrefs.HasKey("CaMaxPosx") && PlayerPrefs.HasKey("CaMaxPosy") &&
                    PlayerPrefs.HasKey("CaMinPosx") && PlayerPrefs.HasKey("CaMinPosy"))
                {
                    Debug.Log("カメラPosのデータは存在します");
                    cc1.moveMax.x = PlayerPrefs.GetFloat("CaMaxPosx");
                    cc1.moveMax.y = PlayerPrefs.GetFloat("CaMaxPosy");
                    cc1.moveMin.x = PlayerPrefs.GetFloat("CaMinPosx");
                    cc1.moveMin.y = PlayerPrefs.GetFloat("CaMinPosy");

                }
                else
                {
                    Debug.Log("カメラPosのデータは存在しません");
                    //カメラの移動範囲
                    PlayerPrefs.SetFloat("CaMaxPosx", cc1.moveMax.x);
                    PlayerPrefs.SetFloat("CaMaxPosy", cc1.moveMax.y);
                    PlayerPrefs.SetFloat("CaMinPosx", cc1.moveMin.x);
                    PlayerPrefs.SetFloat("CaMinPosy", cc1.moveMin.y);
                }
            }
            else//セーブがなかったら
            {
                Debug.Log("SaveXposデータとSaveYposデータは存在しません");
                Vector2 Ppos = new Vector2(playerG.transform.position.x, 
                                           playerG.transform.position.y);
                //PlayerPrefs.SetString("SaveXpos", "SaveYpos","EventN");//位置のセーブ用
                PlayerPrefs.SetFloat("SaveXpos", Ppos.x);
                PlayerPrefs.SetFloat("SaveYpos", Ppos.y);
                //プレイヤーの移動範囲
                PlayerPrefs.SetFloat("CMaxPosx", pc.playerPosXClamp);
                PlayerPrefs.SetFloat("CMaxPosy", pc.playerPosYClamp);
                PlayerPrefs.SetFloat("CMinPosx", pc.MinsplayerPosXClamp);
                PlayerPrefs.SetFloat("CMinPosy", pc.MinsplayerPosYClamp);
                //カメラの移動範囲
                PlayerPrefs.SetFloat("CaMaxPosx", cc1.moveMax.x);
                PlayerPrefs.SetFloat("CaMaxPosy", cc1.moveMax.y);
                PlayerPrefs.SetFloat("CaMinPosx", cc1.moveMin.x);
                PlayerPrefs.SetFloat("CaMinPosy", cc1.moveMin.y);

                playerG.transform.position = new Vector3(Ppos.x,Ppos.y,0);
            }

            if (PlayerPrefs.HasKey("EventN"))//イベント用の値があったら
            {
                Debug.Log("EventNデータは存在します");
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
            if (B1C.hp<=0)//ボスのHPが0になったら 
            {
                GameClear();
            }
            else if (gC.hp<=0)
            {
                Gamaover();
            } else if (gC.hp>0) {
                stopmode();
            }
        }

        ////お試し
        //if (Input.GetKeyDown(KeyCode.Delete))//PlayerPrefsは勝手に保存されているんで注意
        //{
        //    PlayerPrefs.DeleteKey("SaveXpos");//キーの削除
        //    PlayerPrefs.DeleteKey("SaveYpos");//キーの削除
        //    PlayerPrefs.DeleteKey("EventN");//キーの削除(EventNの値を削除)

        //    PlayerPrefs.DeleteKey("SaveCXpos");
        //    PlayerPrefs.DeleteKey("SaveCYpos");
        //    //プレイヤーの移動範囲
        //    PlayerPrefs.DeleteKey("CMaxPosx");
        //    PlayerPrefs.DeleteKey("CMaxPosy");
        //    PlayerPrefs.DeleteKey("CMinPosx");
        //    PlayerPrefs.DeleteKey("CMinPosy");
        //    //カメラの移動範囲
        //    PlayerPrefs.DeleteKey("CaMaxPosx");
        //    PlayerPrefs.DeleteKey("CaMaxPosy");
        //    PlayerPrefs.DeleteKey("CaMinPosx");
        //    PlayerPrefs.DeleteKey("CaMinPosy");
        //}
    }
    
}
