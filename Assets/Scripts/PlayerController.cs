using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;   //�ړ��X�s�[�h

    Vector3 SaveVec;   //�ړ��Ȃǂ�ۑ�����
    Rigidbody2D Rd2D;�@//Rigidbody2D��ۑ�����
    pacController pacC;
    private Animator anim = null;
    float InputVec;    //���ړ����̌����̒l������


    public float gravity; //�d��
    //�U��
    public float MaxattackTime;//�O������ύX�ł���ő傽�ߎ���
    private Slider chargeSlider;//�U���𒙂߂�̂����₷�����邽�߂̃X���C�_�[
    private float attackAutTime;//
    private float attackTime;//�U���𒙂߂�����
    private bool normalSwicth;//�ʏ�̏�Ԃɖ߂����߂̕ϐ�
    private bool isAttack = false;
    float Power = 0;

    //�ړ����������p�ϐ�
    private Vector2 playerPos;
    //private readonly float playerPosXClamp = 15.0f;
    //private readonly float playerPosYClamp = 15.0f;
    public float playerPosXClamp = 15.0f;
    public float playerPosYClamp = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        normalSwicth = false;
        Rd2D = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //�ꎞ�I�ɓ����Ȃ��悤�ɂ��Ă���
        //chargeSlider = GameObject.Find("attackchargeSlider").GetComponent<Slider>();
        //pacC = transform.GetChild(2).gameObject.GetComponent<pacController>();
    }

    // Update is called sonce per frame
    void Update()
    {

        //�ړ������@//�ꎞ�I�ɓ����Ȃ��悤�ɂ��Ă���
        //this.MovingRestrictions();

        //�ړ�
        InputVec = Input.GetAxisRaw("Horizontal");
        if (InputVec != 0)//�v���C���[�̌���
        {
            Vector3 SavelocalScale = transform.localScale;//���݂̌�����ۑ�
            transform.localScale = new Vector3(/*SavelocalScale.x **/ InputVec, SavelocalScale.y, SavelocalScale.z);
        }
        if (InputVec > 0)
        {
            anim.SetBool("run", true);
        }
        else if (InputVec < 0)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
        { 
            ////�U��
            //if (Input.GetMouseButton(0))//�U���𒙂߂�
            //{
            //    if (attackTime < MaxattackTime)
            //    {
            //        attackTime += 1 * Time.deltaTime;
            //        chargeSlider.value += 1 * Time.deltaTime;
            //        //Debug.Log(attackTime);
            //    }
            //}
            //else if (Input.GetMouseButtonUp(0))
            //{
            //    chargeSlider.value = 0;
            //    if (attackTime >= MaxattackTime)//���U��
            //    {
            //        anim.SetBool("hevayAttack", true);
            //        isAttack = true;
            //        pacC.heavyattackSwicth = true;
            //        StartCoroutine("WaitForAttack");
            //        attackTime = 0;
            //        attackAutTime = 0.6f;//�߂�܂ł̎���(���������Ă���)
            //        normalSwicth = true;
            //    }
            //    else if (attackTime < MaxattackTime)//��U��
            //    {
            //        anim.SetBool("lightAttack", true);
            //        isAttack = true;
            //        pacC.rightattackSwicth = true;
            //        StartCoroutine("WaitForAttack");
            //        attackTime = 0;
            //        attackAutTime = 0.5f;//�߂�܂ł̎���(���������Ă���)
            //        normalSwicth = true;
            //    }
            //}
            //else if (normalSwicth == true)//�U����߂�
            //{
            //    attackTime += 1 * Time.deltaTime;
            //    if (attackTime >= attackAutTime)
            //    {

            //        anim.SetBool("lightAttack", false);
            //        anim.SetBool("hevayAttack", false);
            //        attackTime = 0;
            //        normalSwicth = false;
            //    }
            //}
        }//�ꎞ�I�ɓ����Ȃ��悤�ɂ��Ă���

    }


    void FixedUpdate()
    {
        SaveVec.x = MoveSpeed * InputVec * Time.deltaTime;
        transform.position += SaveVec;
        //�U��
        //if(Input.GetMouseButtonDown(0))
        //{           
        //        anim.SetBool("lightAttack", true);
        //        isAttack = true;
        //    pacC.rightattackSwicth = true;
        //        StartCoroutine("WaitForAttack");   
        //}
        //else if(Input.GetMouseButton(0))
        //{
        //        anim.SetBool("hevayAttack", true);
        //        isAttack = true;
        //    pacC.heavyattackSwicth = true;
        //        StartCoroutine("WaitForAttack");
        //}
        //else
        //{
        //    anim.SetBool("lightAttack", false);
        //    anim.SetBool("hevayAttack", false);
        //}


    }
    //�J�����ʒu����
    private void MovingRestrictions()
    {
        //�ϐ��Ɏ����̍��̈ʒu������
        this.playerPos = transform.position;

        //playerPos�ϐ���x��y�ɐ��������l������
        //playerPos.x�Ƃ����l��-playerPosXClamp�`playerPosXClamp�̊ԂɎ��߂�
        this.playerPos.x = Mathf.Clamp(this.playerPos.x, -this.playerPosXClamp, this.playerPosXClamp);
        this.playerPos.y = Mathf.Clamp(this.playerPos.y, -this.playerPosYClamp, this.playerPosYClamp);

        transform.position = new Vector2(this.playerPos.x, this.playerPos.y);
    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttack = false;

    }


}
