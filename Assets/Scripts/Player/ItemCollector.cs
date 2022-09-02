using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private AudioClip collectSoundEffect;
    [SerializeField] private float clipVolumeScale;

    PlayAudioClip playAudioClip;

    private void Awake()
    {
        UpdateScoreText();
    }

    private void Start()
    {
        playAudioClip = GetComponent<PlayAudioClip>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            TotalScore.totalScore++;
            UpdateScoreText();
            Destroy(collision.gameObject);

            playAudioClip.PlayOneShot(collectSoundEffect,clipVolumeScale);
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score : " + TotalScore.totalScore;
    }
}
