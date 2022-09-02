using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCheckpoint : MonoBehaviour
{
    Animator animator;
    AudioSource audioSourceOfPlayer;

    [SerializeField] private AudioClip finishLevelSoundEffect;
    [SerializeField] private float clipVolumeScale;

    private bool isLevelCompleted;

    void Start()
    {
        isLevelCompleted = false;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isLevelCompleted)
        {
            audioSourceOfPlayer = collision.gameObject.GetComponent<AudioSource>();

            animator.SetTrigger("checkpointEntered");

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<PlayerMovement>().isInputEnabled = false;

            Invoke("LoadNextLevel", 3f);

            isLevelCompleted = true;
        }
    }

    public void PlayCheckpointSoundEffect()    // This function is used inside the animation as an event.
    {
        audioSourceOfPlayer.PlayOneShot(finishLevelSoundEffect ,Mathf.Clamp01(clipVolumeScale));
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
