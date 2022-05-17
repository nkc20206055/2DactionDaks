using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage1ManagerC : MonoBehaviour
{
    GameObject playerG,BossG;
    groundController gC;
    Boss1Controller B1C;
    stageManagerC sMC;
    // Start is called before the first frame update
    void Start()
    {
        playerG= GameObject.FindWithTag("Player");
        gC = playerG.GetComponent<groundController>();
        BossG= GameObject.FindWithTag("boss");
        B1C = BossG.GetComponent<Boss1Controller>();
        sMC = GetComponent<stageManagerC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sMC.Eventnumber==1)
        {
            Debug.Log("“®‚¢‚½");
        }
    }
}
