using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour, EnemyDamageController
{
    private enum STATE {startA,normal,move,jump,lightattack,heavyattack,counterH,down };
    private STATE state=STATE.startA;//enum������
    private STATE saveState;//enum��ς���Ƃ��ω�����ق���ۑ�����ϐ�

    private Animator anim;
    void StartA()//���߂ɓ����A�j���[�V����
    {
        anim.SetBool("startS", true);
    }
    void Normal()//�ʏ�
    {
        anim.SetBool("startS", false);
    }
    void Move()//�ړ�
    {

    }
    void Jump()//�W�����v
    {

    }
    void Lightattack()//��U��
    {

    }
    void Heavyattack()//���U��
    {

    }
    void Counter()//�J�E���^�[����������Ƃ�
    {

    }
    void Down()//�_�E�����
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
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //���݂̃X�e�[�g���Ăяo��
        switch (state)
        {
            case STATE.startA://
                StartA();
                break;
            case STATE.normal://�ʏ펞
                Normal();
                break;
            case STATE.move://����
                Move();
                break;
            case STATE.lightattack://��U������
                Lightattack();
                break;
            case STATE.heavyattack://���U������
                Heavyattack();
                break;
            case STATE.counterH://�J�E���^�[��H������Ƃ�
                Counter();
                break;
            case STATE.down://�_�E�������Ƃ�
                Down();
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
