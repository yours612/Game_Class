using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bomb : MonoBehaviour
{
    public float bombRadius = 10f;  // 杀伤范围
    public float bombForce = 100f;  // 冲击力
    public float fuseTime = 1.5f;   // 引线时间
    public GameObject explosion;    // 爆炸背景圆
    public AudioClip boom;
    public AudioClip fuses;
    public AudioMixer mixer;

    private ParticleSystem explosionFX;     // 爆炸粒子效果
    private LayBombs layBombs;              // Hero脚本
    private PickupSpawner pickupSpawner;    // 道具生成脚本，启动新协程用
 

    void Awake()
    {
        explosionFX = GameObject.Find("explosionParticle").GetComponent<ParticleSystem>();
        pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
        layBombs = GameObject.FindGameObjectWithTag("Player").GetComponent<LayBombs>();
    }

    void Start()
    {
        if (transform.root == transform)
            StartCoroutine(BombDetonation());
    }

    IEnumerator BombDetonation()
    {
        // 等待两秒，用于播放引信燃烧效果
        AudioSource.PlayClipAtPoint(fuses, GameObject.Find("Main Camera").transform.position);
        
        mixer.SetFloat("props", 20);

        yield return new WaitForSeconds(fuseTime);

        Explode();
    }


    public void Explode()
    {
        pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());    

        // 在杀伤范围内查找敌人
        int nLayer = 1 << LayerMask.NameToLayer("Enemy");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, bombRadius, nLayer);

        foreach (Collider2D en in enemies)
        {
            Rigidbody2D enemyBody = en.GetComponent<Rigidbody2D>();
            if (enemyBody != null && enemyBody.tag == "Enemy")
            {
                enemyBody.gameObject.GetComponent<Enemy>().HP = 0;

                Vector3 deltaPos = enemyBody.transform.position - transform.position;

                Vector3 force = deltaPos.normalized * bombForce;
                enemyBody.AddForce(force);
            }
        }

        // 播放爆炸后粒子效果
        explosionFX.transform.position = transform.position;
        explosionFX.Play();

        // 实列化爆炸背景圆
        Instantiate(explosion, transform.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(boom, GameObject.Find("Main Camera").transform.position);
        mixer.SetFloat("props", 20);

        // 销毁Bomb
        Destroy (gameObject);
    }
}
