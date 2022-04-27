using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,EnemyDamageController
{
    //�X�e�[�g�}�V���œGAI�̍쐬
    private enum STATE { normal,move,attack,counterMe,damage}
    private STATE state = STATE.normal;//enum��normal������
    private STATE saveState = STATE.move;//enum��ς���Ƃ��ω�����ق���ۑ�����ϐ�

    public bool otamseimode;//�������œ������ꍇtrue
    public int MaxHP;//�ő�HP
    public float moveSpeed;//�ړ��X�s�[�h
    public float nPosS;//�U���𔭐�����ꍇ�̃v���C���[�Ƃ̋���

    private GameObject playerG;//�v���C���[�̃Q�[���I�u�W�F�N�g��ۑ�����
    private Animator anim;//Animator�ۑ��p
    private int HP;//�̗�
    void Normal()//�ʏ펞��A�s�������ɖ߂��ꍇ�ɒʂ�
    {

    }
    void Move()//�ړ�
    {
        
    }
    void Attack()//�U��
    {

    }
    void Counter()//�J�E���^�[����������Ƃ�
    {

    }
    void Damage()//�_���[�W
    {

    }
    void DestroyM()//���S��
    {
        Destroy(gameObject);
    }
    public void EnemyDamage(int h)
    {
        HP -= h;
        Debug.Log("�_���[�W�@"+HP);
        if (HP <= 0)
        {
            DestroyM();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        playerG = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (otamseimode==true)
        {

        }
        else
        {
            //���݂̃X�e�[�g���Ăяo��
            switch (state)
            {
                case STATE.normal://�ʏ펞
                    Normal();
                    break;
                case STATE.move://����
                    Move();
                    break;
                case STATE.attack://�U������
                    Attack();
                    break;
                case STATE.counterMe://�J�E���^�[��H������Ƃ�
                    Counter();
                    break;
                case STATE.damage://�_���[�W
                    Damage();
                    break;
            }

            //�X�e�[�g���ς�����Ƃ�
            if (state != saveState)
            {
                //���̃X�e�[�g�ɐ؂�ւ��
                state = saveState;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
