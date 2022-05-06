using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerOni : MonoBehaviour, EnemyDamageController
{
    //�X�e�[�g�}�V���œGAI�̍쐬
    private enum STATE { normal, move, attack,guard, counterMe, damage }
    private STATE state = STATE.normal;//enum��normal������
    private STATE saveState = STATE.move;//enum��ς���Ƃ��ω�����ق���ۑ�����ϐ�

    public int MaxHP;//�ő�HP
    public float moveSpeed;//�ړ��X�s�[�h
    public float nPosS;//�U���𔭐�����ꍇ�̃v���C���[�Ƃ̋���
    public float actionTime;//���̍s���ɂ��鎞��

    private GameObject playerG;//�v���C���[�̃Q�[���I�u�W�F�N�g��ۑ�����
    private Animator anim;//Animator�ۑ��p
    private int HP;//�̗�
    private bool counterHetSwicth;//�J�E���^�[�����������瓮��bool�^
    void Normal()
    {

    }
    void Move()
    {

    }
    void Attack()
    {

    }
    void Guard()
    {

    }
    void Counter()
    {

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
    void Damage()
    {

    }
    public void EnemyDamage(int h)//�_���[�W���󂯂������C���^�[�t�F�[�X
    {
        
    }
    private void changeState(STATE _state)//�X�e�[�g��؂�ւ���
    {
        saveState = _state;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            case STATE.guard://�h�䂷��
                Guard();
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
