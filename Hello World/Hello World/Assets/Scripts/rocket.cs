using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public GameObject explosion;
    //private Enemy enemy;
    void Start()
    {
        Destroy(gameObject, 2);
        //enemy = GameObject.Find("Enemy1").GetComponent<Enemy>();
    }
    void OnExplode()
    {
        Quaternion randRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        Instantiate(explosion, transform.position, randRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnExplode();
        Destroy(gameObject);

        if (collision.tag == "Enemy")
            collision.gameObject.GetComponent<Enemy>().Hurt();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
