using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,EnemyDamageController
{
    //�X�e�[�g�}�V���œGAI�̍쐬
    private enum STATE { normal,move,attack,counterMe,damage}
    private STATE state = STATE.normal;//enum��normal������
    private STATE saveState = STATE.move;//enum��ς���Ƃ��ω�����ق���ۑ�����ϐ�

    //public bool otamseimode;//�������œ������ꍇtrue
    public int MaxHP;//�ő�HP
    public float moveSpeed;//�ړ��X�s�[�h
    public float nPosS;//�U���𔭐�����ꍇ�̃v���C���[�Ƃ̋���
    public float actionTime;//���̍s���ɂ��鎞��

    float SaveTime;//
    bool Onlyonce;//��񂫂蓮��

    private GameObject playerG;//�v���C���[�̃Q�[���I�u�W�F�N�g��ۑ�����
    private Animator anim;//Animator�ۑ��p
    private Vector3 savePos, savePlayerPos;
    private int HP;//�̗�
    private float Savedirection, direction;//�ړ����̌����ۑ��p,�����̒l������p
    void Normal()//�ʏ펞��A�s�������ɖ߂��ꍇ�ɒʂ�
    {
        anim.SetBool("move", false);
        anim.SetBool("attack", false);
        anim.SetBool("counter", false);
        anim.SetBool("damage", false);
        //if (actionTime> SaveTime) {

        //}
        //else {
        //    changeState(STATE.move);
        //}

        changeState(STATE.move);

    }
    void Move()//�ړ�
    {
        //if (Onlyonce==true)
        //{
        //    actionTime = 3;
        //    SaveTime = 0;
        //    Onlyonce = false;
        //}
        anim.SetBool("move", true);
        //Debug.Log(playerG.transform.position);
        savePlayerPos = playerG.transform.position;
        savePlayerPos = transform.position - savePlayerPos;
        Debug.Log(savePlayerPos);
        if (savePlayerPos.x <= nPosS && savePlayerPos.x >= -nPosS)//�v���C���[���͈͓��ɓ�������
        {
            Debug.Log("�U���J�n");
            Onlyonce = true;
            anim.SetBool("move", false);
            changeState(STATE.attack);
        }
        Savedirection = transform.position.x - playerG.transform.position.x;
        direction = 0;
        if (Savedirection>=0)//�E����
        {
            direction = -1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
        else if (Savedirection<0)//������
        {
            direction = 1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
        savePos.x = direction * moveSpeed * Time.deltaTime;
        transform.position += savePos;
    }
    void Attack()//�U���@���X�e�[�g��animator�̕��ŕς��Ă���
    {
        anim.SetBool("attack", true);
        changeStateAndTime(STATE.normal,3);
    }
    void Counter()//�J�E���^�[����������Ƃ�
    {

    }
    void Damage()//�_���[�W ���X�e�[�g��animator�̕��ŕς��Ă���
    {
        anim.SetBool("damage", true);
    }
    void DestroyM()//���S��
    {
        Destroy(gameObject);
    }
    public void EnemyDamage(int h)//�_���[�W���󂯂������C���^�[�t�F�[�X
    {
        anim.SetBool("move", false);
        anim.SetBool("attack", false);
        anim.SetBool("counter", false);
        HP -= h;
        Debug.Log("�_���[�W�@"+HP);
        if (HP <= 0)//HP��0�ɂȂ�����
        {
            DestroyM();
        }
        changeState(STATE.damage);
    }
    private void changeState(STATE _state)//�X�e�[�g��؂�ւ���
    {
        saveState = _state;
    }
    private void changeStateAndTime(STATE Cstate,float t)
    {
        if (Onlyonce == true)//�ŏ��ɓ���
        {
            actionTime = t;
            SaveTime = 0;
            Onlyonce = false;
        }
        if (actionTime>SaveTime)
        {
            SaveTime += 1 * Time.deltaTime;
        }
        else
        {
            saveState = Cstate;//�X�e�[�g��؂�ւ���
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        Onlyonce = true;
        anim = GetComponent<Animator>();
        playerG = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        
        //
        //���݂̃X�e�[�g���Ăяo��
        switch (state){

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
