using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    float delayTime = 2f; //Delay time for the Invoke method

    bool isTransitioning = false; //Failing or end of the game condition

    bool disableCollisions = false;

    AudioSource audioSource;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip failure;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem failureParticles;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void Update() 
    {
        if (Input.GetKey(KeyCode.C))
            disableCollisions = !disableCollisions;    
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning)
        {
            return;
        }

        switch (other.gameObject.tag) //According to the collided object's tag, different operations are applied
        {
            case "Friendly":
                break;
            case "Finish":
                NextSceneScenario();
                break;
            case "Fuel":
                Debug.Log("Congrats bud! You have the fuel.");
                other.gameObject.SetActive(false);
                break;
            default:
                CrashScenario();
                break;
        };
    }

    void CrashScenario()
    {
        if (!disableCollisions)
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(failure);
            GetComponent<Movement>().enabled = false;
            failureParticles.Play();
            Invoke("Respawn", delayTime);
        }
    }

    void NextSceneScenario()
    {
        isTransitioning = true;
        audioSource.Stop(); 
        successParticles.Play();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", delayTime);
    }

    void LoadNextScene() //46
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = activeSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;    
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void Respawn()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}