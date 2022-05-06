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
    private Vector3 savePos, savePlayerPos;
    private int HP;//�̗�
    private float Savedirection,direction;//�ړ����̌����ۑ��p,�����̒l������p
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

        changeState(STATE.move);
    }
    void Move()
    {
        anim.SetBool("move", true);
        savePlayerPos = playerG.transform.position;//�v���C���[�̈ʒu����
        savePlayerPos = transform.position - savePlayerPos;//�v���C���[�̈ʒu�Ƃ���̈ʒu������
        if (savePlayerPos.x <= nPosS && savePlayerPos.x >= -nPosS)//�v���C���[���͈͓��ɓ�������
        {
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

    }
    void Guard()//�h��
    {
        anim.SetBool("guard", true);
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
        Debug.Log("������");
        if (guardSwicth==true)//�K�[�h�ł�����
        {
            anim.SetBool("move", false);
            changeState(STATE.guard);
        }
    }
    private void changeState(STATE _state)//�X�e�[�g��؂�ւ���
    {
        saveState = _state;
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        anim = GetComponent<Animator>();
        playerG = GameObject.FindWithTag("Player");//�^�O�Ńv���C���[�̃I�u�W�F�N�g�����f���ē����
        guardSwicth = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag=="")
        //{

        //}
    }
}
