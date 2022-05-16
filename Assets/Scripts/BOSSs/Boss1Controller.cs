using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour, EnemyDamageController
{
    private enum STATE {startA,normal,move,jump,lightattack,heavyattack,counterH,down,change,jumpattack,destoy};
    private STATE state=STATE.startA;//enum������
    private STATE saveState;//enum��ς���Ƃ��ω�����ق���ۑ�����ϐ�

    GameObject SM;
    stageManagerC SMC;

    [SerializeField] public GameObject attackC;
    [SerializeField] public GameObject bulletObject; 
    public float MaxHP;//�ő�̗�
    public float moveSpeed,JumpSpeed;//�ړ��X�s�[�h,�W�����v�X�s�[�h
    public float nPosS,jumpAPpos;//�U���𔭐�����ꍇ�̃v���C���[�Ƃ̋���,�W�����v�U���𔭐�����ꍇ�̃v���C���[�Ƃ̋���
    public float MaxStopTime;//Normal���ɍl���鎞��
    public float MaxXposition, MMaxXposition;//�W�����v�p�@�����ɃW�����v����ʒu�A�E���ɃW�����v����ʒu
    
    float MaxjumpPos;
    public float aa;

    private GameObject MI;//���g�̃I�u�W�F�N�g
    private GameObject playerG;//�v���C���[�̃Q�[���I�u�W�F�N�g��ۑ�����
    private Animator anim;
    private Vector3 savePos, savePlayerPos;
    private Vector3 Bpos,SaveBpos;
    private int Attackcount, Maxattackcount;//���U���܂ł̍U���񐔂��L�^����
    private int attackNO, attackNcount;//�U�������,�񐔂̋L�^
    private int Raction,jumpN,jumpattackR;
    private int counterCount,MaxcounterCount;//�J�E���^�[���ꂽ�񐔂̋L�^�A�ő�ŃJ�E���^�[����Ă�����
    public float hp;//�̗�
    private float Jpos;
    private float Savedirection,direction;//�ړ����̌����ۑ��p,�����̒l������p
    private float stopTime;//�~�܂��Ă���Ԃ̌v�鎞��
    private bool JumpStratSwicth,jumpSwicth;//�W�����v���ł��邪�ǂ���
    private bool attackSwicth;//�U���J�n�̃X�C�b�`
    private bool attackNumberS, directionSwicth;
    private bool counterHetSwicth,counterOneSwicth;//�J�E���^�[�����������瓮��bool�^
    private bool DownSwcith,Downdamgemode;//�_�E����������1�񂾂�����
    private bool changeSwicth, SchangeSwicth, CjumpSwcith;//
    private bool destroySwicth;
    private bool bulletSwicth;

    void StartA()//���߂ɓ����A�j���[�V����
    {
        anim.SetBool("startS", true);
    }
    void Normal()//�ʏ�
    {
        gameObject.tag = "boss";
        jumpN = 0;
        jumpattackR = 0;
        attackNumberS = true;
        directionSwicth = true;
        counterHetSwicth = false;
        DownSwcith = true;
        bulletSwicth = true;
        attackNO = 0;
        attackNcount = 0;
        anim.SetBool("move", false);
        anim.SetBool("jump", false);
        anim.SetFloat("jumpN", 0);
        anim.SetBool("lightattack", false);
        anim.SetBool("heavyattack", false);
        anim.SetBool("counter", false);
        anim.SetBool("down", false);
        //anim.SetBool("change", false);
        anim.SetBool("startS", false);
        anim.SetBool("jumpattack", false);

        if (stopTime>=MaxStopTime)//���̍s��
        {
            anim.SetBool("move", true);
            changeState(STATE.move);

            //changeState(STATE.jump);
            //JumpStratSwicth = true;
            stopTime = 0;
            Debug.Log(stopTime);
        }
        else
        {
            stopTime += 1 * Time.deltaTime;
        }
    }
    void Move()//�ړ�
    {
        //Debug.Log("�ړ�");
        if (attackNumberS==true)
        {
            attackNO = Random.Range(1, 4);
            jumpattackR = Random.Range(1, 7);//�ʏ�W�����v���W�����v�U����
            if (attackNO==2||attackNO==3)//2��U��������
            {
                attackNO = 2;
            }else if (attackNO==1)//1��U��������
            {
                attackNO = 3;
            }
            Debug.Log(attackNO+"��U��������");
            attackNumberS = false;
        }
        savePlayerPos = playerG.transform.position;//�v���C���[�̈ʒu����
        savePlayerPos = transform.position - savePlayerPos;//�v���C���[�̈ʒu�Ƃ���̈ʒu������

        if (savePlayerPos.x <= nPosS && savePlayerPos.x >= -nPosS)//�v���C���[���͈͓��ɓ�������
        {
            Attackcount++;
            if (Maxattackcount <= Attackcount)//���U��
            {
                anim.SetBool("heavyattack", true);
                anim.SetBool("move", false);
                attackSwicth = true;
                changeState(STATE.heavyattack);
            }
            else//��U��
            {
                anim.SetBool("lightattack", true);
                anim.SetBool("move", false);
                attackSwicth = true;
                changeState(STATE.lightattack);
            }

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
            gameObject.tag = "bossjump";
            anim.SetBool("lightattack", false);
            anim.SetBool("heavyattack", false);
            anim.SetBool("down", false);
            Bpos = transform.position;
            //���g�̍��W������
            //Vector3 Bpos = transform.position;//���g�̍��W������ 
            //if (MaxXposition <= Bpos.x && MMaxXposition >= Bpos.x)
            //{
            //    Debug.Log("�W�����v���Ȃ�");
            //}
            //else
            //{
            //    Debug.Log("�W�����v����");
            //}


            if (Raction % 2 == 0)
            {
                //Debug.Log("����");
                //�E�W�����v
                jumpN = 1;
                MaxjumpPos = (Bpos.x + MaxXposition) / 2f;
                //Debug.Log(MaxjumpPos);
                Vector3 s = transform.localScale;
                transform.localScale = new Vector3(-1f, s.y, s.z);
            }
            else
            {
                //Debug.Log("�");
                //���W�����v
                jumpN = -1;
                MaxjumpPos = (Bpos.x + MMaxXposition) / 2f;
                //Debug.Log(MaxjumpPos);
                Vector3 s = transform.localScale;
                transform.localScale = new Vector3(1f, s.y, s.z);
            }
            //MaxjumpPos = Mathf.Floor(MaxjumpPos)/2;//�؂�̂Ă��Ĕ����ɂ���
            Debug.Log(MaxjumpPos);
            JumpStratSwicth = false;
        }
        if (jumpSwicth==true) {
            if (jumpN == 1) {
                //�E�ɃW�����v
                if (MaxXposition >= Bpos.x)
                {

                    Bpos = transform.position;//���g�̍��W������
                    Vector3 i = new Vector3();
                    if (MaxjumpPos <= Bpos.x)//������
                    {
                        //Debug.Log("�������Ă�");
                        anim.SetFloat("jumpN", 2);
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
                    i.x = 1 * JumpSpeed * Time.deltaTime;
                    transform.position += i;
                }
                else
                {
                    anim.SetFloat("jumpN", 3);
                    transform.position = new Vector3(transform.position.x, SaveBpos.y, transform.position.z);
                }
            } else if (jumpN==-1) {

                //���ɃW�����v
                if (MMaxXposition <= Bpos.x)
                {

                    Bpos = transform.position;//���g�̍��W������
                    Vector3 i = new Vector3();
                    if (MaxjumpPos >= Bpos.x)//������
                    {
                        //Debug.Log("�������Ă�");
                        anim.SetFloat("jumpN", 2);
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
                    transform.position = new Vector3(transform.position.x, SaveBpos.y, transform.position.z);
                }
            }
        }
    }
    void Lightattack()//��U��
    {
        if(attackSwicth==true)
        {
            Debug.Log("��U��");
            attackC.tag = "enemyrightattack";
            attackNcount++;
            //Raction = Random.Range(1, 11);
            if (directionSwicth==true) //���ɃW�����v������������߂�(��񂫂�)
            {
                if (Raction == 1)//�E
                {
                    Raction = 2;
                }
                else if (Raction == 2)//��
                {
                    Raction = 1;
                }
                directionSwicth = false;
            }
            JumpStratSwicth = true;
            attackSwicth = false;
        }
    }
    void Heavyattack()//���U��
    {
        if (attackSwicth == true)
        {
            Debug.Log("���U��");
            attackC.tag = "enemyheavyattack";
            attackNcount++;
            if (directionSwicth == true)//���ɃW�����v������������߂�(��񂫂�)
            {
                if (Raction == 1)//�E
                {
                    Raction = 2;
                }
                else if (Raction == 2)//��
                {
                    Raction = 1;
                }
                directionSwicth = false;
            }
            Attackcount = 0;
            Maxattackcount = Random.Range(2,4);
            JumpStratSwicth = true;
            attackSwicth = false;
        }
    }
    void attackMigration()
    {
        if (attackNO<=attackNcount)
        {
            if (CjumpSwcith==true) {
                Debug.Log("�W�����v");
                //if (jumpattackR <= 4)
                //{
                    savePlayerPos = playerG.transform.position;//�v���C���[�̈ʒu����
                    //Debug.Log("�W�����v�A�^�b�N");
                    if (savePlayerPos.x <= jumpAPpos && savePlayerPos.x >= -jumpAPpos)//�v���C���[���͈͓��ɓ�������
                    {
                        changeState(STATE.jump);
                    }
                    else
                    {
                        bulletSwicth = true;
                        changeState(STATE.jumpattack);
                    }
                //}
                //else if (jumpattackR > 4)
                //{
                //    changeState(STATE.jump);
                //}
            }
            else
            {
                changeState(STATE.jump);
            }
        }
        else if (attackNO > attackNcount)
        {
            changeState(STATE.move);
            anim.SetBool("move", true);
            anim.SetBool("lightattack", false);
            anim.SetBool("heavyattack", false);
        }
    }
    void jumpattack()
    {
        anim.SetBool("jumpattack", true);
        if (JumpStratSwicth==true)
        {
            Debug.Log("�W�����v�A�^�b�N");
            attackC.tag = "enemyrightattack";
            gameObject.tag = "bossjump";
            anim.SetBool("lightattack", false);
            anim.SetBool("heavyattack", false);
            savePlayerPos = playerG.transform.position;//�v���C���[�̈ʒu����
            //if (savePlayerPos.x <= jumpAPpos && savePlayerPos.x >= -jumpAPpos)//�v���C���[���͈͓��ɓ�������
            //{
            //    changeState(STATE.jump);
            //    anim.SetBool("jumpattack", false);
            //    //JumpStratSwicth = false;
            //}
            //else
            //{
                Bpos = transform.position;
                if (savePlayerPos.x>=Bpos.x)//�E�W�����v
                {
                    jumpN = 1;
                    MaxjumpPos = (Bpos.x + savePlayerPos.x) / 2f;
                    Vector3 s = transform.localScale;
                    transform.localScale = new Vector3(-1f, s.y, s.z);
                }
                else if (savePlayerPos.x < Bpos.x)//���W�����v
                {
                    jumpN = -1;
                    MaxjumpPos = (Bpos.x + savePlayerPos.x) / 2f;
                    Vector3 s = transform.localScale;
                    transform.localScale = new Vector3(1f, s.y, s.z);
                }
            //}

            JumpStratSwicth = false;
        }

        if (jumpSwicth == true)
        {
            if (jumpN == 1)
            {
                //�E�ɃW�����v
                if (savePlayerPos.x >= Bpos.x)
                {

                    Bpos = transform.position;//���g�̍��W������
                    Vector3 i = new Vector3();
                    anim.SetFloat("jumpN", 1);
                    if (MaxjumpPos <= Bpos.x)//������
                    {
                        //Debug.Log("�������Ă�");
                        //anim.SetFloat("jumpN", 2);
                        aa += 20f * Time.deltaTime;
                        i.y = -1 * aa * Time.deltaTime;
                    }
                    else //�オ��
                    {
                        //Debug.Log("�オ���Ă�");
                        //anim.SetFloat("jumpN", 1);
                        aa -= 20f * Time.deltaTime;
                        i.y = 1 * aa * Time.deltaTime;
                    }
                    i.x = 1 * JumpSpeed * Time.deltaTime;
                    transform.position += i;
                }
                else
                {
                    anim.SetFloat("jumpN", 2);
                    if (bulletSwicth == true)
                    {//�e�����˂����
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject b = Instantiate(bulletObject);
                            Vector3 Mi = transform.position;
                            bulletController bc = b.GetComponent<bulletController>();
                            if (i == 0)
                            {
                                b.transform.position = new Vector3(Mi.x /*+ 8f*/, Mi.y - 4f, Mi.z);
                                bc.PMpos = 1;
                            }
                            else if (i == 1)
                            {
                                b.transform.position = new Vector3(Mi.x /*- 8f*/, Mi.y - 4f, Mi.z);
                                bc.PMpos = -1;
                            }
                        }
                        bulletSwicth = false;
                    }

                    transform.position = new Vector3(transform.position.x, SaveBpos.y, transform.position.z);
                }
            }
            else if (jumpN == -1)
            {
                //���ɃW�����v
                if (savePlayerPos.x <= Bpos.x)
                {

                    Bpos = transform.position;//���g�̍��W������
                    Vector3 i = new Vector3();
                    anim.SetFloat("jumpN", 1);
                    if (MaxjumpPos >= Bpos.x)//������
                    {
                        //Debug.Log("�������Ă�");
                        //anim.SetFloat("jumpN", 2);
                        aa += 20f * Time.deltaTime;
                        i.y = -1 * aa * Time.deltaTime;
                    }
                    else //�オ��
                    {
                        //Debug.Log("�オ���Ă�");
                        //anim.SetFloat("jumpN", 1);
                        aa -= 20f * Time.deltaTime;
                        i.y = 1 * aa * Time.deltaTime;
                    }
                    i.x = -1 * JumpSpeed * Time.deltaTime;
                    transform.position += i;
                }
                else
                {
                    anim.SetFloat("jumpN", 2);
                    if (bulletSwicth==true) {//�e�����˂����
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject b = Instantiate(bulletObject);
                            Vector3 Mi = transform.position;
                            bulletController bc = b.GetComponent<bulletController>();
                            if (i == 0)
                            {
                                b.transform.position = new Vector3(Mi.x /*+ 8f*/, Mi.y-4f, Mi.z);
                                bc.PMpos = 1;
                            }
                            else if (i == 1)
                            {
                                b.transform.position = new Vector3(Mi.x /*- 8f*/, Mi.y - 4f, Mi.z);
                                bc.PMpos = -1;
                            }
                        }
                        bulletSwicth = false;
                    }
                    transform.position = new Vector3(transform.position.x, SaveBpos.y, transform.position.z);
                }
            }
        }
    }
    void Counter()//�J�E���^�[����������Ƃ�
    {
        if (counterOneSwicth==true) {
            counterCount++;
            anim.SetBool("counter", true);
            anim.SetBool("heavyattack", false);
            counterOneSwicth = false;
        }
    }
    void Down()//�_�E�����
    {
        if (DownSwcith==true) {
            counterCount=0;
            MaxcounterCount++;
            anim.SetBool("down", true);
            anim.SetBool("heavyattack", false);
            DownSwcith = false;
        }
    }
    void Change()//��ԕω�
    {
        if (changeSwicth == true)
        {
            Debug.Log("����");
            gameObject.tag = "bossjump";
            anim.SetBool("change", true);
            anim.SetBool("startS", true);
            jumpN = 0;
            attackNumberS = true;
            directionSwicth = true;
            counterHetSwicth = false;
            DownSwcith = true;
            attackNO = 0;
            attackNcount = 0;
            CjumpSwcith = true;
            anim.SetBool("move", false);
            anim.SetBool("jump", false);
            anim.SetFloat("jumpN", 0);
            anim.SetBool("lightattack", false);
            anim.SetBool("heavyattack", false);
            anim.SetBool("counter", false);
            anim.SetBool("down", false);
            changeSwicth = false;
        }
    }
    void Destory()//���S��
    {
        if (destroySwicth==true)
        {
            anim.SetBool("destoy", true);
            destroySwicth = false;
        }
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
        if (Downdamgemode==true)
        {
            hp -= h * 1.5f;
        }
        else
        {
            hp -= h;
        }

        if (MaxHP/2>hp)//HP�������ɂȂ�����
        {
            if (SchangeSwicth==true) {
                gameObject.tag = "bossjump";
                changeState(STATE.change);
                SchangeSwicth = false;
            }
        }
        if (hp<=0)//���S
        {
            Debug.Log("���S");
            changeState(STATE.destoy);
        }
        

        
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
        SM = GameObject.FindWithTag("stageManager");
        SMC = SM.GetComponent<stageManagerC>();
        SaveBpos = transform.position;
        hp = MaxHP;
        Maxattackcount = 2;//�ŏ���2��
        Raction = 2;
        attackNO = 0;
        attackNcount = 0;
        MaxcounterCount = 2;
        counterCount = 0;
        anim = GetComponent<Animator>();
        JumpStratSwicth = true;
        counterHetSwicth = false;
        attackSwicth = true;
        DownSwcith = true;
        changeSwicth = true;
        SchangeSwicth = true;
        destroySwicth = true;
        bulletSwicth = true;
        playerG = GameObject.FindWithTag("Player");//�^�O�Ńv���C���[�̃I�u�W�F�N�g�����f���ē����
        stopTime = MaxStopTime;//�ŏ����������ɓ�����悤�ɂ���

    }

    // Update is called once per frame
    void Update()
    {
        if (SMC.pauseSwicth == false)
        {
            //���݂̃X�e�[�g���Ăяo��
            switch (state)
            {
                case STATE.startA://���߂ɓ���
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
                case STATE.jumpattack://�W�����v�U������
                    jumpattack();
                    break;
                case STATE.counterH://�J�E���^�[��H������Ƃ�
                    Counter();
                    break;
                case STATE.down://�_�E�������Ƃ�
                    Down();
                    break;
                case STATE.change://�_�E�������Ƃ�
                    Change();
                    break;
                case STATE.destoy://���S��
                    Destory();
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
        if (collision.gameObject.tag == "playercounter")
        {
            if (counterHetSwicth == true)
            {
                if (MaxcounterCount<=counterCount) {
                    Downdamgemode = true;
                    changeState(STATE.down);

                } else if (MaxcounterCount > counterCount) {
                    //Debug.Log("�q�b�g");
                    counterOneSwicth = true;
                    changeState(STATE.counterH);
                }
            }

        }
    }
}
