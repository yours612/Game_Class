using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VoiceControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject explosion;
    public GameObject Gun;
    private Slider slider;
    

    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Controlvoice()
    {
        explosion.GetComponent<AudioSource>().volume = slider.value;
        Gun.GetComponent<AudioSource>().volume = slider.value;
        Bomb.volunme = slider.value;
        HealthPickup.volunme = slider.value;
        BombPickup.volunme = slider.value;
        if (slider.value > 0)
        {
   
            audioMixer.SetFloat("master", slider.value * 40f - 30);
            audioMixer.SetFloat("bk", slider.value * 40f - 30);
            audioMixer.SetFloat("hero", slider.value * 40f - 30);
            audioMixer.SetFloat("props", slider.value * 40f - 30);
        }

        else
        {
            audioMixer.SetFloat("master", -80);
            audioMixer.SetFloat("bk", -80);
            audioMixer.SetFloat("hero", -80);
            audioMixer.SetFloat("props", -80);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Controlvoice();

    }
}
