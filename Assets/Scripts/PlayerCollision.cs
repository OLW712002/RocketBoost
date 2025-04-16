using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
       switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("DoNothing");
                break;
            case "Finish":
                Debug.Log("YouWin");
                break;
            case "Fuel":
                Debug.Log("Refill");
                break;
            default:
                Destroy(gameObject);
                Invoke("ReloadScene", 1f);
                break;
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
