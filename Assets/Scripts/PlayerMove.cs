using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D coll;
    SpriteRenderer sprite;
    Animator anim;

    float dirX;

    [SerializeField] float moveSpeed;
    [SerializeField] float JumpForce;

    [SerializeField] LayerMask jumpableGround;

    bool hasDoubleJumped = false;

    enum MovementState { idle, walking, jumping, falling};

    Vector3 respawnPoint;
    [SerializeField] GameObject fallDetector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        respawnPoint = transform.position;
    }

    private void Update()
    {
        UpdateAnimations();

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            hasDoubleJumped = false;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        else if (Input.GetButtonDown("Jump") && !hasDoubleJumped)
        { 
            hasDoubleJumped = true;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    void UpdateAnimations()
    {
        MovementState state;

        if (dirX > 0)
        {
            sprite.flipX = false;
            state = MovementState.walking;
        }

        else if (dirX < 0)
        {
            sprite.flipX = true;
            state = MovementState.walking;
        }

        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }

        else if (rb.velocity.y < -0.1f)
        { 
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        transform.position = respawnPoint;
    }
}
