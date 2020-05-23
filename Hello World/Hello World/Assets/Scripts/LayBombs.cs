using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LayBombs : MonoBehaviour
{
    public GameObject bomb;
    public int bombCount = 0;
    public AudioClip bombsAway;
    public AudioMixer mixer;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && bombCount > 0)
        {
            if (audio != null)
            {
                if (!audio.isPlaying)
                {
                    audio.clip = bombsAway;
                    audio.Play();
                    mixer.SetFloat("hero", 0);
                }
            }

            bombCount--;
            Instantiate(bomb, transform.position, transform.rotation);
        }
    }
}
