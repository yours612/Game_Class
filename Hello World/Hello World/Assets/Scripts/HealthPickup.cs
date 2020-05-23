using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HealthPickup : MonoBehaviour
{
    public float healthBonus;
    public AudioClip healthPickup;
    public AudioMixer mixer;

    private PickupSpawner pickupSpawner;
    private Animator anim;
    private bool landed = false;

    private void Awake()
    {
        anim = transform.root.GetComponent<Animator>();
        pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            playerHealth.health += healthBonus;
            if (playerHealth.health > 100)
                playerHealth.health = 100;

            playerHealth.UpdateHealthBar();

            pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

            AudioSource.PlayClipAtPoint(healthPickup, transform.position);
            mixer.SetFloat("props", 0);

            Destroy(transform.root.gameObject);
        }
        else if (collision.tag == "Ground" && !landed)
        {
            landed = true;
            anim.SetTrigger("Land");
            transform.parent = null;
            gameObject.AddComponent<Rigidbody2D>();
        }
    }
}
