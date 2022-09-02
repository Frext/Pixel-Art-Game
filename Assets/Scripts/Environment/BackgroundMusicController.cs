using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("BackgroundMusic").Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            if (gameObject.CompareTag("BackgroundMusic"))
            {
                //If there WAS an object in the scene it just tells this object to 
                //destroy itself if this is not the original
                Destroy(gameObject);
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += StopBackgroundMusicOnSomeScenes;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StopBackgroundMusicOnSomeScenes;
    }

    private void StopBackgroundMusicOnSomeScenes(Scene scene, LoadSceneMode mode)
    {
        // Don't play the background music in the start and end scenes.
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1))
        {
            Destroy(gameObject);
        }
    }
}
