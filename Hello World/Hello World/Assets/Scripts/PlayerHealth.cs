using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float damageInterval = 2f;
    public float hurtForce = 10f;
    public float damageAmout = 10f;
    public AudioClip[] ouchClips;
    public AudioMixer mixer;

    private AudioSource audio;
    private SpriteRenderer healthBar;   // 血条
    private float lastHurtTime;         // 受伤时刻          
    private Vector3 healthScale;        // 血条比例，控制长度
    private PlayerControl playerControl;// 控制脚本
    private Rigidbody2D hero;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        playerControl = GetComponent<PlayerControl>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        healthScale = healthBar.transform.localScale;
        hero = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void UpdateHealthBar()
    {
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }

    void TakeDamage(Transform enemy)
    {
        playerControl.jump = false;
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
        hero.AddForce(hurtVector * hurtForce);

        health -= damageAmout;

        if (audio != null)
        {
            if (!audio.isPlaying)
            {
                int i = Random.Range(0, ouchClips.Length);
                audio.clip = ouchClips[i];
                audio.Play();
                mixer.SetFloat("hero", 0);
            }
        }
        UpdateHealthBar();
    }

    void dead()
    {
        Collider2D[] cols = GetComponents<Collider2D>(); //注意，加s
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in spr)
        {
            s.sortingLayerName = "UI";
        }
        GetComponent<PlayerControl>().enabled = false;
        GetComponentInChildren<Gun>().enabled = false;

        anim.SetTrigger("Death");

        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (Time.time > lastHurtTime + damageInterval)
            {
                if (health > 0f)
                {
                    TakeDamage(col.transform);
                    lastHurtTime = Time.time;

                    if (health <= 0)
                    {
                        dead();
                    }
                }
                else
                {
                    dead();
                }
            }
        }
    }

}
