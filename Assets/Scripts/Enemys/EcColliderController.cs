using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcColliderController : MonoBehaviour
{
    public bool counterHetSwicth;

    int sa;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == /*"playerCounterattack"*/"playercounter")
        {
            //sa++;
            Debug.Log("�q�b�g" /*+ sa*/);
            counterHetSwicth = true;
        }
    }
}
