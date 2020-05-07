using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float moveForce = 400f;
    public float jumpForce = 100f;

    private bool grounded = false;
    [HideInInspector]
    public bool jump = false;
    private Transform groundCheck;
    private Rigidbody2D heroBody;
    [HideInInspector]
    public bool bFaceRight = true;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
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
}
