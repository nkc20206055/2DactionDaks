using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyC : MonoBehaviour
{
    public float MaxAttackTime;

    float attackTime;

    private GameObject playerG;
    private Animator MiAnim;
    private float Savedirection, direction;
    private bool counterHetSwicth;//�J�E���^�[�����������瓮��bool�^
    void Normal()//�ʏ���
    {
        MiAnim.SetBool("attack", false);
        attackTime = 0;
    }
    void Counter()
    {
        Destroy(gameObject);
    }
    void CounterBool()//animation�ōU�����ɃJ�E���^�[���ꂽ��N������p
    {
        if (counterHetSwicth == false)//�N�����Ă��Ȃ�������
        {
            counterHetSwicth = true;
        }
        else if (counterHetSwicth == true)//�N�����Ă�����
        {
            counterHetSwicth = false;
        }
    }
    public void EnemyDamage(int h)//�_���[�W���󂯂������C���^�[�t�F�[�X
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        playerG = GameObject.FindWithTag("Player");         //�^�O�Ńv���C���[���擾
        MiAnim = GetComponent<Animator>();
        attackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxAttackTime<=attackTime)
        {
            MiAnim.SetBool("attack", true);
        }
        else if (MaxAttackTime>attackTime)
        {
            attackTime += 1 * Time.deltaTime;
        }

        Savedirection = transform.position.x - playerG.transform.position.x;
        direction = 0;
        if (Savedirection >= 0)//�E����
        {
            direction = -1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
        else if (Savedirection < 0)//������
        {
            direction = 1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playercounter")
        {
            if (counterHetSwicth == true)
            {
                //Debug.Log("�q�b�g");
                MiAnim.SetBool("counter", true);
            }
        }
    }
}
