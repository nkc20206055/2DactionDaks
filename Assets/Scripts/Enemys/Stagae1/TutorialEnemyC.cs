using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyC : MonoBehaviour
{
    public float MaxAttackTime;

    float attackTime;

    private GameObject playerG;
    private Animator MiAnim;
    private float Savedirection, direction;
    private bool counterHetSwicth;//カウンターが当たったら動くbool型
    void Normal()//通常状態
    {
        MiAnim.SetBool("attack", false);
        attackTime = 0;
    }
    void Counter()
    {
        Destroy(gameObject);
    }
    void CounterBool()//animationで攻撃中にカウンターされたら起動する用
    {
        if (counterHetSwicth == false)//起動していなかったら
        {
            counterHetSwicth = true;
        }
        else if (counterHetSwicth == true)//起動していたら
        {
            counterHetSwicth = false;
        }
    }
    public void EnemyDamage(int h)//ダメージを受けた時※インターフェース
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        playerG = GameObject.FindWithTag("Player");         //タグでプレイヤーを取得
        MiAnim = GetComponent<Animator>();
        attackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxAttackTime<=attackTime)
        {
            MiAnim.SetBool("attack", true);
        }
        else if (MaxAttackTime>attackTime)
        {
            attackTime += 1 * Time.deltaTime;
        }

        Savedirection = transform.position.x - playerG.transform.position.x;
        direction = 0;
        if (Savedirection >= 0)//右向き
        {
            direction = -1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
        else if (Savedirection < 0)//左向き
        {
            direction = 1;
            Vector3 r = transform.localScale;
            transform.localScale = new Vector3(direction * -1, r.y, r.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playercounter")
        {
            if (counterHetSwicth == true)
            {
                //Debug.Log("ヒット");
                MiAnim.SetBool("counter", true);
            }
        }
    }
}
