using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // declare cache references
    int currentSceneIndex;
    int totalSceneCount;
    AudioSource audioSource;
    // declare serializable parameters 
    [SerializeField] float delay = 1.0f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    Movement movementComponent;
    void Start() // get scene index upon initialization of parent object (the rocket ship)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        totalSceneCount = SceneManager.sceneCountInBuildSettings;
        movementComponent = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();

        if (movementComponent == null)
        {
            Debug.LogError("Movement component is missing from this GameObject");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from this GameObject.");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("This is the starting point. Get going!");
                break;
            case "Finish":
                StartSuccessSequence(delay);
                break;
            default: // reset level on ship crash
                StartCrashSequence(delay);
                break;
        }
    }

    void ReloadLevel() // will be invoked in StartCrashSequence()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() // will be invoked in StartSuccessSequence()
    {
        if (currentSceneIndex + 1 < totalSceneCount)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void StartCrashSequence(float delay)
    {
        Debug.Log("Ship crashed, respawning at launch pad.");
        audioSource.PlayOneShot(crashAudio);
        // disable movement of parent object
        movementComponent.enabled = false;
        // after a delay, reload the level
        Invoke("ReloadLevel", delay);
    }
    void StartSuccessSequence(float delay)
    {
        Debug.Log("Congratulations, stage completed.");
        audioSource.PlayOneShot(successAudio);
        // disable movement of parent object
        movementComponent.enabled = false;
        // after a delay, reload the level
        Invoke("LoadNextLevel", delay);
    }
}
