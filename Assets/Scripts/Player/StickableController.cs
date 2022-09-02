using UnityEngine;

[RequireComponent(typeof(FrictionJoint2D))]
public class StickableController : MonoBehaviour
{
    [SerializeField] private FrictionJoint2D frictionJoint;

    private Rigidbody2D playerRigidbody;
    private PlayerMovement playerMovementScript;

    private void Start()
    {
        playerRigidbody = frictionJoint.connectedBody;
        playerMovementScript = playerRigidbody.gameObject.GetComponent<PlayerMovement>();

        DisableJoint();
    }

    private void Update() // Check for motion
    {
        if (frictionJoint.enabled)
        {
            if (!Mathf.Approximately(playerRigidbody.velocity.x, 0))
            {
                DisableJoint();
            }

            else if (Input.GetButtonDown("Jump"))
            {
                DisableJoint();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnableJoint();
            playerMovementScript.isStickToWall = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerMovementScript.isStickToWall && isPlayerStill())
            {
                EnableJoint();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DisableJoint(); // This prevents the joint being enabled if player gets dragged by some game object.
            playerMovementScript.isStickToWall = false;
        }
    }

    private void EnableJoint()
    {
        frictionJoint.enabled = true;
    }

    private void DisableJoint()
    {
        frictionJoint.enabled = false;
    }

    private bool isPlayerStill()
    {
        if(playerRigidbody.velocity.y >= 0.01f)
        {
            return false;
        }

        return true;
    }
}
