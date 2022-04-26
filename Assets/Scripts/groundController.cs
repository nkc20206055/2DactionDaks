using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class groundController : MonoBehaviour
{
    [SerializeField] GameObject CounterObject;

    //public GameObject hpui;//HP��UI������ϐ�
    //public AudioClip Se1;//SE��炷�I�u�W�F�N�g������Ƃ���

    public float counterTime;//�J�E���^�[�ł��鎞��
    public float damageTime;//���G����

    EcColliderController ECC;
    SpriteRenderer SR;//������SpriteRenderer������ϐ�
    public int hp;
    int deletehp;//HP�̕ϐ�

    private CameraController CC;//�J������CameraController���擾����p
    private SpriteRenderer sR;//������SpriteRenderer�������
    private Animator anim;
    private AudioSource AS;
    private Slider slider;//�K�[�h�Q�[�W��UI
    private float CGtime, cTime;//
    private float sliderS;
    private bool MouseSwicth;//�}�E�X���������邩�ǂ���
    private bool gadeSwicth;//�K�[�h���N�����Ă��邩�ǂ���
    private bool counterSwicth, countSpeed;//
    private bool damageSwicth;//�_���[�W��H����Ă��邩�ǂ���
    private bool damageHetSwcith;//�_���[�W��H������Ƃ��ɂ�����
    private bool damageHetOn;//�U����H��������ǂ���
    void animationcancel()//�A�j���[�V������r���ŏI��点��̂Ɏg��
    {
        anim.SetBool("counterattack", false);
        anim.SetBool("grndbreak", false);
        gameObject.layer = LayerMask.NameToLayer("Default");
        MouseSwicth = true;
        Debug.Log("������");
    }
    void counterONA()//�J�E���^�[�������ɃJ������counterON�������悤�ɂ���
    {
        if (countSpeed == true)
        {
            Time.timeScale = 0.8f;
            countSpeed = false;
        }
        else
        {
            Time.timeScale = 1f;
            countSpeed = true;
        }
    }
    void Damage()//�_���[�W����HP�̌���△�G���ԂȂǂ�ݒ�
    {
        if (damageSwicth == true)
        {
            //HP�����炷
            //if (damageHetSwcith == true)
            //{
            //    for (int i = 0; i < deletehp; i++)
            //    {
            //        GameObject s = hpui.transform.GetChild(hp - 1).gameObject;
            //        s.SetActive(false);
            //        hp--;
            //        Debug.Log(hp);
            //        if (hp <= 0)
            //        {
            //            damageSwicth = false;
            //            i = deletehp;
            //        }
            //    }
            //    deletehp = 0;
            //    damageHetSwcith = false;
            //    gameObject.layer = LayerMask.NameToLayer("PlayerDamge");//���C���[�}�X�N��ύX����
            //}


            cTime += 1 * Time.deltaTime;
            if (cTime < damageTime)//�_�ł�����
            {
                float level = Mathf.Abs(Mathf.Sin(Time.time * 10));//Mathf.Abs�͐�Βl�AMathf.Sin��Sin(�T�C��)���o�����
                sR.color = new Color(1f, 1f, 1f, level);

            }
            else if (cTime >= damageTime)
            {
                sR.color = new Color(255, 255, 255, 255);
                gameObject.layer = LayerMask.NameToLayer("Default");//���C���[�}�X�N��߂�
                damageSwicth = false;
                damageHetOn = false;
                cTime = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;//�ő�hp�̐ݒ�
        sR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        slider = GameObject.Find("guardgage").GetComponent<Slider>();
        CC = GameObject.Find("Main Camera").GetComponent<CameraController>();
        MouseSwicth = true;
        countSpeed = true;
        damageHetOn = false;
        sliderS = slider.maxValue;
        Debug.Log(sliderS);
    }

    // Update is called once per frame
    void Update()
    {
        //�K�[�h�u���C�N
        if (sliderS <= 0)
        {
            MouseSwicth = false;
            gadeSwicth = false;
            anim.SetBool("grndbreak", true);
            anim.SetBool("guard", false);
            damageSwicth = false;
            damageHetOn = false;
            sliderS = slider.maxValue;
            slider.value = slider.maxValue;
        }

        //�J�E���^�[������������
        //if (counterSwicth==true)
        //{
        //    if (ECC.counterHetSwicth == true)
        //    {
        //        Debug.Log("��������");
        //        AS.PlayOneShot(Se1);//SE��炷
        //        gameObject.layer = LayerMask.NameToLayer("PlayerDamge");//���C���[�}�X�N��ύX����
        //        anim.SetBool("counterattack", true);
        //        anim.SetBool("counter", false);
        //        CounterObject.SetActive(false);
        //        CC.counterON();
        //    }
        //    counterSwicth = false;
        //}

        //�J�E���^�[�ƃK�[�h�̑���
        if (MouseSwicth == true)
        {
            if (Input.GetMouseButton(1))//�E�N���b�N�������ςȂ�
            {
                CGtime += 1 * Time.deltaTime;
                if (CGtime < counterTime) //�J�E���^�[�m�F
                {
                    //Debug.Log("�J�E���^�[��"+CGtime);
                    gameObject.layer = LayerMask.NameToLayer("PlayerDamge");
                    anim.SetBool("counter", true);
                    CounterObject.SetActive(true);

                }
                else if (CGtime >= counterTime) //�K�[�h��
                {
                    Debug.Log("�K�[�h��");
                    gameObject.layer = LayerMask.NameToLayer("Default");//���C���[�}�X�N��߂�
                    anim.SetBool("guard", true);
                    anim.SetBool("counter", false);
                    CounterObject.SetActive(false);
                    gadeSwicth = true;
                }
            }
            else if (Input.GetMouseButtonUp(1))//�E�}�E�X�{�^�����グ����
            {
                //if (CGtime <= 0.05f)//�����ɂł��Ȃ��悤�P�\������Ă���
                //{
                //    Debug.Log("�J�E���^�[�L�����Z��");
                //    gameObject.layer = LayerMask.NameToLayer("Default");//���C���[�}�X�N��߂�
                //    anim.SetBool("counter", false);
                //    CounterObject.SetActive(false);
                //}
                //else
                if (/*CGtime > 0.05f &&*/ CGtime < counterTime)//�J�E���^�[
                {
                    Debug.Log("�J�E���^�[");
                    //gameObject.layer = LayerMask.NameToLayer("Default");//���C���[�}�X�N��߂�
                    //anim.SetBool("counterattack", true);
                    anim.SetBool("counter", false);
                    CounterObject.SetActive(false);

                }
                else if (CGtime >= counterTime)//�K�[�h���I��点��
                {
                    Debug.Log("�K�[�h�I��");
                    gameObject.layer = LayerMask.NameToLayer("Default");//���C���[�}�X�N��߂�
                    anim.SetBool("guard", false);
                    gadeSwicth = false;
                }
                CGtime = 0;
            }
        }
        else
        {
            Debug.Log("�K�[�h���~");
            gameObject.layer = LayerMask.NameToLayer("Default");//���C���[�}�X�N��߂�
            CGtime = 0;
        }



        Damage();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "counterHet")
        {
            //Debug.Log(collision.gameObject);
            ECC = collision.gameObject.GetComponent<EcColliderController>();
            counterSwicth = true;
            if (counterSwicth == true)
            {
                if (ECC.counterHetSwicth == true)
                {
                    Debug.Log("��������");

                    //AS.PlayOneShot(Se1);//SE��炷

                    gameObject.layer = LayerMask.NameToLayer("PlayerDamge");//���C���[�}�X�N��ύX����
                    anim.SetBool("counterattack", true);
                    anim.SetBool("counter", false);
                    CounterObject.SetActive(false);
                    CC.counterON();
                }
                counterSwicth = false;
            }
        }

        //if (damageHetOn == false)
        //{
        //    if (collision.gameObject.tag == "enemyrightattack")
        //    {

        //        damageHetSwcith = true;
        //        damageSwicth = true;
        //        if (gadeSwicth == true)
        //        {
        //            Debug.Log("�G�̎�U�����y��");
        //            damageHetOn = true;
        //            //�m�[�_���[�W

        //        }
        //        else
        //        {
        //            Debug.Log("�G�̎�U�����q�b�g");
        //            //����̂̓n�[�g1��
        //            deletehp = 2;
        //            damageHetOn = true;

        //        }
        //    }
        //    else if (collision.gameObject.tag == "enemyheavyattack")
        //    {
        //        damageHetSwcith = true;
        //        damageSwicth = true;
        //        if (gadeSwicth == true)
        //        {
        //            Debug.Log("�G�̋��U�����y��");
        //            //����̂̓n�[�g�����ŃK�[�h�Q�[�W������
        //            deletehp = 1;
        //            slider.value -= 20;
        //            sliderS = slider.value;
        //            damageHetOn = true;

        //            //Debug.Log(sliderS);
        //        }
        //        else
        //        {
        //            Debug.Log("�G�̋��U�����q�b�g");
        //            //����̂̓n�[�g1.5��
        //            deletehp = 3;
        //            damageHetOn = true;

        //        }
        //    }
        //}
    }
}
