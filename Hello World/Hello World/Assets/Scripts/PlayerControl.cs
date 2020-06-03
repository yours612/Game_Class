using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerControl : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float moveForce = 400f;
    public float jumpForce = 100f;
    public AudioClip[] jumpClips;
    public AudioMixer mixer;
    public AudioClip[] taunts;
    [HideInInspector]
    public bool bFaceRight = true;
    [HideInInspector]
    public bool jump = false;
    //public static int grade = 0;

    private AudioSource audio;
    private bool grounded = false;
    private Transform groundCheck;
    private Rigidbody2D heroBody;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        heroBody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (h * heroBody.velocity.x < maxSpeed)
            heroBody.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed,
                                            heroBody.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(heroBody.velocity.x));

        if (h < 0 && bFaceRight)
            flip();
        else if (h > 0 && !bFaceRight)
            flip();

        if (jump)
        {

            anim.SetTrigger("Jump");
            heroBody.AddForce(new Vector2(0, jumpForce));
            jump = false;

            if (audio != null)
            {
                if (!audio.isPlaying)
                {
                    int i = Random.RandomRange(0, jumpClips.Length);
                    audio.clip = jumpClips[i];
                    audio.Play();
                    mixer.SetFloat("hero", 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position
                                        , 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    public void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }

    public void taunt()
    {
        Score.x += 100;
        Score.score.text = "Score: " + Score.x;
        
        if (audio != null)
        {
            if (!audio.isPlaying)
            {
                int i = Random.Range(0, taunts.Length);
                audio.clip = taunts[i];
                audio.Play();
                mixer.SetFloat("hero", 0);
            }
        }
    }
}
