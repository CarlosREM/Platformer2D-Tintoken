using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerPlatformerController : PhysicsObject2D
{

    public float moveSpeed = 7f;
    public float jumpSpeed = 7f;
    
    SpriteRenderer spriteRenderer;

    Animator animator;

    private bool noInputH;
    public bool NoInputH {
        get { return noInputH; }
        set { noInputH = value; }
    }


    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        if (!noInputH) {
            move.x = Input.GetAxis("Horizontal");

            if (move.x != 0) {
                spriteRenderer.flipX = (move.x > 0) ? false : true;
            }
        }

        if (Input.GetButtonDown("Jump") && grounded)
            velocity.y = jumpSpeed;
        
        // Cancel jump mid-air
        else if(Input.GetButtonUp("Jump")) {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        // Set animations
        animator.SetBool("isJumping", !grounded);
        animator.SetFloat("Speed", Mathf.Abs(move.x));


        targetVelocity = move * moveSpeed;
    }

}
