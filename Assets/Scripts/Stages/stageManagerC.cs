using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageManagerC : MonoBehaviour
{
    [SerializeField] GameObject stopUI;//ˆêŽž’âŽ~‚ÅoŒ»‚·‚éUI
    [SerializeField] GameObject GameoverUI;//Gameover‚ÅoŒ»‚·‚éUI
    public GameObject stopUIhaikei;

    public bool normalSwicth;
    public bool pauseSwicth;

    GameObject /*playerG,*/ bossG;
    groundController gC;

    private Image stopI;
    private bool stopmodeSwicth, DestroyMi;
    void stopmode()//ˆêŽž’âŽ~
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
    void GamaOver()//ƒQ[ƒ€ƒI[ƒo[
    {
        if (stopI.color.a>=0.7f)
        {
            Debug.Log("Ž€–S");
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
            gC = GameObject.FindWithTag("Player").GetComponent<groundController>();
            //bossG = GameObject.FindWithTag("boss");
            stopI = stopUIhaikei.GetComponent<Image>();
            stopmodeSwicth = false;
            DestroyMi = false;
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
    }
}
