using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerOni : MonoBehaviour, EnemyDamageController
{
    //ステートマシンで敵AIの作成
    private enum STATE { normal, move, attack,guard, counterMe, damage }
    private STATE state = STATE.normal;//enumのnormalを入れる
    private STATE saveState = STATE.move;//enumを変えるとき変化するほうを保存する変数

    public int MaxHP;//最大HP
    public float moveSpeed;//移動スピード
    public float nPosS;//攻撃を発生する場合のプレイヤーとの距離
    public float actionTime;//次の行動にうつる時間

    private GameObject playerG;//プレイヤーのゲームオブジェクトを保存する
    private Animator anim;//Animator保存用
    private int HP;//体力
    private bool counterHetSwicth;//カウンターが当たったら動くbool型
    void Normal()
    {

    }
    void Move()
    {

    }
    void Attack()
    {

    }
    void Guard()
    {

    }
    void Counter()
    {

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
    void Damage()
    {

    }
    public void EnemyDamage(int h)//ダメージを受けた時※インターフェース
    {
        
    }
    private void changeState(STATE _state)//ステートを切り替える
    {
        saveState = _state;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            case STATE.guard://防御する
                Guard();
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
