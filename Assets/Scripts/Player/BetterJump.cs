using UnityEngine;

public class BetterJump : MonoBehaviour
{
    Rigidbody2D rb;
    [Range(1, 100)]
    [SerializeField] private float fallMultiplier = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
}
