using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public float moveSpeed,PMpos;//�ړ��X�s�[�h,�ړ��������
    public float MaxdestroyTime;

    float destroyTime;

    private Vector3 Mipos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mipos.x = PMpos * moveSpeed * Time.deltaTime;
        transform.position += Mipos;
        destroyTime += 1 * Time.deltaTime;
        if (destroyTime>=MaxdestroyTime)
        {
            Destroy(gameObject);
        }
    }
}
