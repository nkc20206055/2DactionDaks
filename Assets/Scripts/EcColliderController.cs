using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcColliderController : MonoBehaviour
{
    public bool counterHetSwicth;
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
        if (collision.gameObject.tag == "playerCounterattack")
        {
            //Debug.Log("�q�b�g");
            counterHetSwicth = true;
        }
    }
}
