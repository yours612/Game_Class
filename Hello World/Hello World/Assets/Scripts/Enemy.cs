using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int HP = 2;
    public Sprite deadEnemy;
    public Sprite hurtEnemy;
    public GameObject UI_100points;          // 怪物死后得分
    public float deathSpinMin = -100f;       // 怪物死后旋转量
    public float deathSpinMax = 100f;

    private Transform frontCheck;
    private SpriteRenderer ren;              // 更换对应图片
    private Rigidbody2D enemyBody;           // 敌人的刚体
    private bool bDeath = false;                     // 敌人已死
    private PlayerControl playerControl;

    void Start()
    {
        frontCheck = transform.Find("FrontCheck").transform;
        ren = transform.Find("char_enemy_alienShip").GetComponent<SpriteRenderer>();
        enemyBody = GetComponent<Rigidbody2D>();
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    private void FixedUpdate()
    {
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1<<LayerMask.NameToLayer("Wall"));
        foreach (Collider2D c in frontHits)
        {
            if (c.tag == "Wall")
            {
                flip();
                break;
            }

        }
        enemyBody.velocity = new Vector2(moveSpeed * transform.localScale.x, enemyBody.velocity.y);

        if (HP == 1 && hurtEnemy != null)
            ren.sprite = hurtEnemy;

        if (HP == 0 && !bDeath)
        {
            death();
            playerControl.taunt();
        }
    }

    public void death()
    {
        bDeath = true;
        SpriteRenderer[] renders = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in renders)
        {
            s.enabled = false;
        }

        ren.enabled = true;

        if (deadEnemy != null)
            ren.sprite = deadEnemy;

        enemyBody.AddTorque(Random.Range(deathSpinMin, deathSpinMax));

        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D c in colliders)
        {
            c.isTrigger = true;
        }

        Vector3 scorepos = transform.position;
        scorepos.y += 1f;
        Instantiate(UI_100points,scorepos,Quaternion.identity);

    }

    public void Hurt()
    {
        HP--;
    }

    public void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
