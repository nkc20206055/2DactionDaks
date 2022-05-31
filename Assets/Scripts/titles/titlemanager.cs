using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titlemanager : MonoBehaviour
{
    public string GameStartscene;
    public void GameStart()
    {
        SceneManager.LoadScene(GameStartscene);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
