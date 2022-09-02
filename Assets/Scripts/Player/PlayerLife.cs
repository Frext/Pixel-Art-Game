using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2;
    private int totalScore; // Total score at the beginning of the scene

    [SerializeField] private AudioClip deathSoundEffect;
    [SerializeField] private float clipVolumeScale;

    PlayAudioClip playAudioClip;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
        playAudioClip = GetComponent<PlayAudioClip>();

        totalScore = TotalScore.totalScore;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            Die();

            TotalScore.totalScore = totalScore; // Restore the total score at the beginning of the current level
        }
    }

    private void Die()
    {
        rb2.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");

        playAudioClip.PlayOneShot(deathSoundEffect, clipVolumeScale);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
