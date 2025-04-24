using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 1f;
    [SerializeField] float nextSceneDelay = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] MeshRenderer spaceShipRenderer;

    AudioSource audioSource;

    bool isPlaySFX = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       switch (collision.gameObject.tag)
        {
            case "Friendly":
                PlaySFX(successSFX);
                break;
            case "Finish":
                GetComponent<PlayerMovement>().enabled = false;
                Invoke("NextScene", nextSceneDelay);
                break;
            case "Fuel":
                Debug.Log("Refill");
                break;
            default:
                PlaySFX(crashSFX);
                spaceShipRenderer.enabled = false;
                Invoke("ReloadScene", reloadSceneDelay);
                break;
        }
    }

    void PlaySFX(AudioClip audioClip)
    {
        if (!isPlaySFX) StartCoroutine(PlaySFXOnce(audioClip));
    }

    IEnumerator PlaySFXOnce(AudioClip audioClip)
    {
        isPlaySFX = true;
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        isPlaySFX = false;
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
