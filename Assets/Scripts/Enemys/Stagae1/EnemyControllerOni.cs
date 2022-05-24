using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerOni : MonoBehaviour, EnemyDamageController
{
    //�X�e�[�g�}�V���œGAI�̍쐬
    private enum STATE { normal, move, attack,guard, counterMe, damage }
    private STATE state = STATE.normal;//enum��normal������
    private STATE saveState = STATE.move;//enum��ς���Ƃ��ω�����ق���ۑ�����ϐ�

    GameObject SM;
    stageManagerC SMC;

    public int MaxHP;//�ő�HP
    public float moveSpeed;//�ړ��X�s�[�h
    public float nPosS;//�U���𔭐�����ꍇ�̃v���C���[�Ƃ̋���
    public float Maxattacktime;//�U������܂ł̎���
    //public float actionTime;//���̍s���ɂ��鎞��

    Renderer targetRenderer; // ���肵�����I�u�W�F�N�g��renderer�ւ̎Q��


    private GameObject playerG;//�v���C���[�̃Q�[���I�u�W�F�N�g��ۑ�����
    private Animator anim;//Animator�ۑ��p
    private Vector3 savePos, savePlayerPos;
    private int HP;//�̗�
    private float Savedirection,direction;//�ړ����̌����ۑ��p,�����̒l������p
    private float attackTime;//�U������܂ł̎��Ԃ����߂�
    private bool attackSwicth,attacktimeSwicth;
    private bool counterHetSwicth;//�J�E���^�[�����������瓮��bool�^
    private bool guardSwicth;//�K�[�h���Ă��邩�ǂ���
    void Normal()
    {
        Debug.Log("�ʏ�");
        anim.SetFloat("moveSpeed", 1);//�A�j���[�V������ʏ�Đ�
        anim.SetBool("move", false);
        anim.SetBool("attack", false);
        anim.SetBool("guard", false);
        anim.SetBool("counterhet", false);
        anim.SetBool("damage", false);

        guardSwicth = true;
        if (attacktimeSwicth==false) {
            attackTime = 0;
        }
        attackSwicth = false;

        changeState(STATE.move);
    }
    void Move()
    {
        anim.SetBool("move", true);
        if (attackTime>= Maxattacktime)//attackTime�����܂�����
        {
            attackSwicth = true;
            attacktimeSwicth = false;
        }
        else if (attackTime < Maxattacktime)//attackTime�����܂��Ă��Ȃ�
        {
            attackTime += 1 * Time.deltaTime;
            attacktimeSwicth = true;
        }
        savePlayerPos = playerG.transform.position;//�v���C���[�̈ʒu����
        savePlayerPos = transform.position - savePlayerPos;//�v���C���[�̈ʒu�Ƃ���̈ʒu������
        if (savePlayerPos.x <= nPosS && savePlayerPos.x >= -nPosS)//�v���C���[���͈͓��ɓ�������
        {
            if (attackSwicth==true) //�U���J�n
            {
                guardSwicth = false;
                anim.SetBool("move", false);
                changeState(STATE.attack);
            }
            else {
                anim.SetFloat("moveSpeed", -1);//�A�j���[�V�������t�Đ�
                Savedirection = transform.position.x - playerG.transform.position.x;//�v���C���[�̌����𒲂ׂ�
                direction = 0;
                if (Savedirection >= 0)//�E����
                {
                    direction = -1;
                    Vector3 r = transform.localScale;
                    transform.localScale = new Vector3(direction * -1.607602f, r.y, r.z);
                }
                else if (Savedirection < 0)//������
                {
                    direction = 1;
                    Vector3 r = transform.localScale;
                    transform.localScale = new Vector3(direction * -1.607602f, r.y, r.z);

                }
                savePos.x = -direction * moveSpeed * Time.deltaTime;
                transform.position += savePos;
            }
        }
        else /*if (savePlayerPos.x >= nPosS && savePlayerPos.x <= -nPosS)*/
        {
            anim.SetFloat("moveSpeed", 1);//�A�j���[�V������ʏ�Đ�
            Savedirection = transform.position.x - playerG.transform.position.x;//�v���C���[�̌����𒲂ׂ�
            direction = 0;
            if (Savedirection >= 0)//�E����
            {
                direction = -1;
                Vector3 r = transform.localScale;
                transform.localScale = new Vector3(direction * -1.607602f, r.y, r.z);
            }
            else if (Savedirection < 0)//������
            {
                direction = 1;
                Vector3 r = transform.localScale;
                transform.localScale = new Vector3(direction * -1.607602f, r.y, r.z);

            }
            savePos.x = direction * moveSpeed * Time.deltaTime;
            transform.position += savePos;
        }
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    anim.SetFloat("moveSpeed", -1);//�A�j���[�V�������t�Đ�
        //}
    }
    void Attack()//�U��
    {
        //Debug.Log("�U��");
        anim.SetBool("attack", true);
    }
    void Guard()//�h��
    {
        anim.SetBool("guard", true);
    }
    void Counter()
    {
        counterHetSwicth = false;
        guardSwicth = false;
        anim.SetBool("counterhet", true);
        anim.SetBool("attack", false);
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
        anim.SetBool("damage", true);
    }
    void Destroy()//���S��
    {
        Destroy(gameObject);
    }
    public void EnemyDamage(int h)//�_���[�W���󂯂������C���^�[�t�F�[�X
    {
        //Debug.Log("������");
        if (guardSwicth==true)//�K�[�h�ł�����
        {
            anim.SetBool("move", false);
            anim.SetBool("attack", false);
            anim.SetBool("counterhet", false);
            changeState(STATE.guard);
        }
        else
        {
            Debug.Log("damage");
            anim.SetBool("move", false);
            anim.SetBool("attack", false);
            anim.SetBool("guard", false);
            anim.SetBool("counterhet", false);
            HP -= h;
            if (HP<=0)//HP��0�ɂȂ�����
            {
                Destroy();//���S
            }
            changeState(STATE.damage);
        }
    }
    private void changeState(STATE _state)//�X�e�[�g��؂�ւ���
    {
        saveState = _state;
    }
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.FindWithTag("stageManager");
        SMC = SM.GetComponent<stageManagerC>();
        HP = MaxHP;
        attackTime = 0;
        anim = GetComponent<Animator>();
        playerG = GameObject.FindWithTag("Player");//�^�O�Ńv���C���[�̃I�u�W�F�N�g�����f���ē����
        targetRenderer = GetComponent<Renderer>();
        guardSwicth = true;
        attacktimeSwicth = true;
        counterHetSwicth = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetRenderer.isVisible)//�J�������Ȃ瓮��
        {
            if (SMC.pauseSwicth == false)
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playercounter")
        {
            if (counterHetSwicth == true)
            {
                //Debug.Log("�q�b�g");
                changeState(STATE.counterMe);
            }
        }
    }
}
