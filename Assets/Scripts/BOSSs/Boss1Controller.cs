using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour, EnemyDamageController
{
    private enum STATE {startA,normal,move,jump,lightattack,heavyattack,counterH,down };
    private STATE state=STATE.startA;//enum������
    private STATE saveState;//enum��ς���Ƃ��ω�����ق���ۑ�����ϐ�

    public float MaxHP;//�ő�̗�
    public float moveSpeed,JumpSpeed;//�ړ��X�s�[�h,�W�����v�X�s�[�h
    public float nPosS;//�U���𔭐�����ꍇ�̃v���C���[�Ƃ̋���
    public float MaxStopTime;//Normal���ɍl���鎞��
    public float MaxXposition, MMaxXposition;//�W�����v�p�@�����ɃW�����v����ʒu�A�E���ɃW�����v����ʒu
    
    float MaxjumpPos;
    public float aa;

    private GameObject playerG;//�v���C���[�̃Q�[���I�u�W�F�N�g��ۑ�����
    private Animator anim;
    private Vector3 savePos, savePlayerPos;
    private Vector3 Bpos,SaveBpos;
    private int Attackcount, Maxattackcount;//�U���񐔂��L�^����
    private int Raction;
    private float hp;//�̗�
    private float Jpos;
    private float Savedirection,direction;//�ړ����̌����ۑ��p,�����̒l������p
    private float stopTime;//�~�܂��Ă���Ԃ̌v�鎞��
    private bool JumpStratSwicth,jumpSwicth;//�W�����v���ł��邪�ǂ���

    void StartA()//���߂ɓ����A�j���[�V����
    {
        anim.SetBool("startS", true);
    }
    void Normal()//�ʏ�
    {
        anim.SetBool("move", false);
        anim.SetBool("jump", false);
        anim.SetFloat("jumpN", 0);
        anim.SetBool("startS", false);


        if (stopTime>=MaxStopTime)//���̍s��
        {
            //anim.SetBool("move", true);
            //changeState(STATE.move);
            changeState(STATE.jump);
            stopTime = 0;
            //Debug.Log(stopTime);
        }
        else
        {
            stopTime += 1 * Time.deltaTime;
        }
    }
    void Move()//�ړ�
    {
        //Debug.Log("�ړ�");
        savePlayerPos = playerG.transform.position;//�v���C���[�̈ʒu����
        savePlayerPos = transform.position - savePlayerPos;//�v���C���[�̈ʒu�Ƃ���̈ʒu������

        if (savePlayerPos.x <= nPosS && savePlayerPos.x >= -nPosS)//�v���C���[���͈͓��ɓ�������
        {
            //Attackcount++;
            //if (Maxattackcount<=Attackcount)//���U��
            //{
            //    anim.SetBool("heavyattack", true);
            //    anim.SetBool("move", false);
            //    changeState(STATE.heavyattack);
            //}
            //else//��U��
            //{
                anim.SetBool("lightattack", true);
                anim.SetBool("move", false);
                changeState(STATE.lightattack);
            //}

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
        savePos.x = direction * moveSpeed * Time.deltaTime;
        transform.position += savePos;
    }
    void Jump()//�W�����v
    {
        anim.SetBool("jump", true);
        if (JumpStratSwicth == true)
        {
            
            Bpos = transform.position;//���g�̍��W������
            //Vector3 Bpos = transform.position;//���g�̍��W������
            //if (MaxXposition<=Bpos.x&&MMaxXposition>=Bpos.x)
            //{
            //    Debug.Log("�W�����v���Ȃ�");
            //}
            //else
            //{
            //    Debug.Log("�W�����v����");
            //}

            //int i = Random.Range(0,2);
            MaxjumpPos = Bpos.x - MMaxXposition*-1;
            //Debug.Log(MaxjumpPos);
            //MaxjumpPos = Mathf.Floor(MaxjumpPos)/2;//�؂�̂Ă��Ĕ����ɂ���
            MaxjumpPos = MaxjumpPos/2;//�؂�̂Ă��Ĕ����ɂ���
            Debug.Log(MaxjumpPos);
            JumpStratSwicth = false;
        }
        if (jumpSwicth==true) {
            //���ɃW�����v
            if (MMaxXposition <= Bpos.x)
            {
                
                Bpos = transform.position;//���g�̍��W������
                Vector3 i = new Vector3();
                if (MaxjumpPos >= Bpos.x)//������
                {
                    //Debug.Log("�������Ă�");
                    anim.SetFloat("jumpN",2);
                    aa += 20f * Time.deltaTime;
                    i.y = -1 * aa * Time.deltaTime;
                }
                else //�オ��
                {
                    //Debug.Log("�オ���Ă�");
                    anim.SetFloat("jumpN", 1);
                    aa -= 20f * Time.deltaTime;
                    i.y = 1 * aa * Time.deltaTime;
                }
                i.x = -1 * JumpSpeed * Time.deltaTime;
                transform.position += i;
            }
            else
            {
                anim.SetFloat("jumpN", 3);
                transform.position = new Vector3(transform.position.x,SaveBpos.y,transform.position.z);
            }
        }
    }
    void Lightattack()//��U��
    {
        //Debug.Log("��U��");
    }
    void Heavyattack()//���U��
    {
        Debug.Log("���U��");
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
    public void jumpNamber()//�A�j���[�V������jumpN��ύX����
    {
        if (jumpSwicth==false)
        {
            jumpSwicth = true;
        }
        else
        {
            jumpSwicth = false;
        }
    }
    private void changeState(STATE _state)//�X�e�[�g��؂�ւ���
    {
        //stopTime = 0;
        saveState = _state;
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveBpos = transform.position;
        hp = MaxHP;
        Maxattackcount = 2;//�ŏ���2��
        anim = GetComponent<Animator>();
        JumpStratSwicth = true;
        playerG = GameObject.FindWithTag("Player");//�^�O�Ńv���C���[�̃I�u�W�F�N�g�����f���ē����
        stopTime = MaxStopTime;//�ŏ����������ɓ�����悤�ɂ���
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
            case STATE.jump://����
                Jump();
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
