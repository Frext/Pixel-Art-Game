using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed;
    [Range(1, 10)]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask jumpableGround;

    Rigidbody2D rb2;
    SpriteRenderer spriteRenderer;
    Animator animator;
    new Collider2D collider;
    PlayAudioClip playAudioClip;

    private enum MovementState { idle, run, jump, fall }
    private MovementState currentState = MovementState.idle;

    [SerializeField] private AudioClip jumpSoundEffect;
    [SerializeField] private float clipVolumeScale;

    [HideInInspector] public bool isInputEnabled;
    [HideInInspector] public bool isStickToWall;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        playAudioClip = GetComponent<PlayAudioClip>();

        isInputEnabled = true;
        isStickToWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = 0.0f;

        if (isInputEnabled)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            rb2.velocity = new Vector2(dirX * speed, rb2.velocity.y);

            if (Input.GetButtonDown("Jump") && (IsGrounded() || isStickToWall))
            {
                rb2.velocity = new Vector2(rb2.velocity.x, jumpForce);
                playAudioClip.PlayOneShot(jumpSoundEffect, clipVolumeScale);
            }
        }

        UpdateAnimationState(dirX);
    }

    private void UpdateAnimationState(float dirX)
    {
        switch (dirX)
        {
            case 1: // Going right
                spriteRenderer.flipX = false;
                currentState = MovementState.run;
                break;

            case -1: // Going left
                spriteRenderer.flipX = true;
                currentState = MovementState.run;
                break;

            default:
                currentState = MovementState.idle;
                break;
        }

        if (rb2.velocity.y > 0.1f)
        {
            currentState = MovementState.jump;
        }
        else if (rb2.velocity.y < -0.1f)
        {
            currentState = MovementState.fall;
        }

        animator.SetInteger("state", (int)currentState);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center - new Vector3(0, collider.bounds.extents.y), new Vector3((collider.bounds.extents.x * 4 / 3), 0.001f), 0f, Vector2.down, 0.01f, jumpableGround);
    }
}
