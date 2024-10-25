using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    float horizontalMove;
    private BoxCollider2D boxCollider;

    private enum MovementState { idle, running, jumping, falling}
    private MovementState state = MovementState.idle;


    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    public void Message()
    {
        Debug.Log("clicked");
    }




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Karakterin yatay eksende hareketi
        horizontalMove = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(horizontalMove * moveSpeed, rigidbody.velocity.y);


        // Karakterin zýplama fonksiyonu
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }


        AnimationUpdate();
       
    }

    private void AnimationUpdate()
    {
        MovementState state;

         // Karakterin hýz deðerine göre running ya da idle animasyonunu oynatan kontrol
        if (horizontalMove > 0f)
        {
            state = MovementState.running;
            spriteRenderer.flipX = false;
        }

        else if (horizontalMove < 0f)
        {
            state = MovementState.running;
            spriteRenderer.flipX = true;
        }

        else
        {
            state = MovementState.idle;
        }

        if (rigidbody.velocity.y > 0.01f)
        {
            state = MovementState.jumping;
        }

        else if (rigidbody.velocity.y < -0.01f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("State", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

}
