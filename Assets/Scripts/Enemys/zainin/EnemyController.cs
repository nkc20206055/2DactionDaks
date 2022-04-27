using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,EnemyDamageController
{
    //ステートマシンで敵AIの作成
    private enum STATE { normal,move,attack,counterMe,damage}
    private STATE state = STATE.normal;//enumのnormalを入れる
    private STATE saveState = STATE.move;//enumを変えるとき変化するほうを保存する変数

    public bool otamseimode;//お試しで動かす場合true
    public int MaxHP;//最大HP
    public float moveSpeed;//移動スピード
    public float nPosS;//攻撃を発生する場合のプレイヤーとの距離

    private GameObject playerG;//プレイヤーのゲームオブジェクトを保存する
    private Animator anim;//Animator保存用
    private int HP;//体力
    void Normal()//通常時や、行動を元に戻す場合に通る
    {

    }
    void Move()//移動
    {
        
    }
    void Attack()//攻撃
    {

    }
    void Counter()//カウンターをくらったとき
    {

    }
    void Damage()//ダメージ
    {

    }
    void DestroyM()//死亡時
    {
        Destroy(gameObject);
    }
    public void EnemyDamage(int h)
    {
        HP -= h;
        Debug.Log("ダメージ　"+HP);
        if (HP <= 0)
        {
            DestroyM();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        playerG = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (otamseimode==true)
        {

        }
        else
        {
            //現在のステートを呼び出す
            switch (state)
            {
                case STATE.normal://通常時
                    Normal();
                    break;
                case STATE.move://歩く
                    Move();
                    break;
                case STATE.attack://攻撃する
                    Attack();
                    break;
                case STATE.counterMe://カウンターを食らったとき
                    Counter();
                    break;
                case STATE.damage://ダメージ
                    Damage();
                    break;
            }

            //ステートが変わったとき
            if (state != saveState)
            {
                //次のステートに切り替わる
                state = saveState;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
