using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 1f;
    [SerializeField] float nextSceneDelay = 1f;

    private void OnCollisionEnter(Collision collision)
    {
       switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("DoNothing");
                break;
            case "Finish":
                GetComponent<PlayerMovement>().enabled = false;
                Invoke("NextScene", nextSceneDelay);
                break;
            case "Fuel":
                Debug.Log("Refill");
                break;
            default:
                gameObject.SetActive(false);
                Invoke("ReloadScene", reloadSceneDelay);
                break;
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCount) nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
