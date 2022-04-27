using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacController : MonoBehaviour
{
    public GameObject playerG = null;
    public bool rightattackSwicth;//ãUŒ‚‚Ìê‡
    public bool heavyattackSwicth;//‹­UŒ‚‚Ìê‡

    private CircleCollider2D CC2D;//©g‚ÌCircleCollider2D‚ğ•Û‘¶
    private Vector3 savePpos;
    private int Edamage;//“G‚É—^‚¦‚éƒ_ƒ[ƒW—Ê
    private float speed;
    public bool StartSwicth;
    // Start is called before the first frame update
    void Start()
    {
        CC2D = GetComponent<CircleCollider2D>();
        rightattackSwicth = false;
        heavyattackSwicth = false;
        StartSwicth = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightattackSwicth == true)//ãUŒ‚
        {
            if (StartSwicth == true)
            {
                Edamage = 3;
                CC2D.radius = 0.5f;
                heavyattackSwicth = false;
                savePpos.x = 0;
                speed = 5;
                Debug.Log("rightattackSwicth" + rightattackSwicth);
                transform.position = playerG.transform.position;
                StartSwicth = false;
            }

            if (savePpos.x >= 3.04f)
            {
                Edamage = 0;
                transform.position = playerG.transform.position;
                CC2D.radius = 0.2f;
                StartSwicth = true;
                rightattackSwicth = false;
            }
            else if (savePpos.x < 3.04f)
            {
                savePpos.x += speed * Time.deltaTime;
                transform.localPosition = savePpos;
            }
        }
        else if (heavyattackSwicth == true)//‹­UŒ‚
        {
            if (StartSwicth == true)
            {
                Edamage = 5;
                //gameObject.tag = "playerHeavyattack";//tag‚ğ•ÏX
                CC2D.radius = 0.5f;
                rightattackSwicth = false;
                savePpos.x = 0;
                speed = 5;
                Debug.Log("heavyattackSwicth" + heavyattackSwicth);
                transform.localPosition = playerG.transform.position;
                StartSwicth = false;
            }

            if (savePpos.x >= 3.84f)
            {
                //gameObject.tag = "playerRightattack";//tag‚ğŒ³‚É–ß‚·
                Edamage = 0;
                transform.position = playerG.transform.position;
                CC2D.radius = 0.2f;
                StartSwicth = true;
                heavyattackSwicth = false;
            }
            else if (savePpos.x < 3.84f)
            {
                savePpos.x += speed * Time.deltaTime;
                transform.localPosition = savePpos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="enemy")
        {
            //GetComponent‚Å‚ÍEnemyController‚ğæ“¾‚µ‚Ä‚µ‚Ü‚¤‚Ì‚ÅGetComponentsInChildren‚ğg‚¤
            //EnemyDamageController damageV = collision.GetComponentsInChildren<EnemyDamageController>();
            EnemyDamageController damageV = collision.GetComponent<EnemyDamageController>();
            damageV.EnemyDamage(Edamage);
            //Debug.Log(damageV);
        }
    }
}
