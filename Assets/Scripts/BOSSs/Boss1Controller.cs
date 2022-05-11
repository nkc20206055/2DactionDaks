using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour, EnemyDamageController
{
    private enum STATE {startA,normal,move,jump,lightattack,heavyattack,counterH,down };
    private STATE state=STATE.startA;//enumを入れる
    private STATE saveState;//enumを変えるとき変化するほうを保存する変数

    private Animator anim;
    void StartA()//初めに動くアニメーション
    {
        anim.SetBool("startS", true);
    }
    void Normal()//通常
    {
        anim.SetBool("startS", false);
    }
    void Move()//移動
    {

    }
    void Jump()//ジャンプ
    {

    }
    void Lightattack()//弱攻撃
    {

    }
    void Heavyattack()//強攻撃
    {

    }
    void Counter()//カウンターをくらったとき
    {

    }
    void Down()//ダウン状態
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
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //現在のステートを呼び出す
        switch (state)
        {
            case STATE.startA://
                StartA();
                break;
            case STATE.normal://通常時
                Normal();
                break;
            case STATE.move://歩く
                Move();
                break;
            case STATE.lightattack://弱攻撃する
                Lightattack();
                break;
            case STATE.heavyattack://強攻撃する
                Heavyattack();
                break;
            case STATE.counterH://カウンターを食らったとき
                Counter();
                break;
            case STATE.down://ダウンしたとき
                Down();
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
