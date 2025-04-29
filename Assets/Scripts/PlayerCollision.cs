using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float reloadSceneDelay = 1f;
    [SerializeField] float nextSceneDelay = 1f;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] MeshRenderer spaceShipRenderer;

    [Header("Crash")]
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem crashParticles;

    [Header("Success")]
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem successParticles;
    

    bool isPlaySFX = false;
    bool isControlable = true;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControlable) return;

       switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartCoroutine(FinishProgress());
                break;
            case "Fuel":
                Debug.Log("Refill");
                break;
            default:
                StartCoroutine(CrashProgress());
                break;
        }
    }

    IEnumerator FinishProgress()
    {
        successParticles.Play();
        isControlable = false;
        PlaySFX(successSFX);
        GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(nextSceneDelay);
        NextScene();
    }

    IEnumerator CrashProgress()
    {
        crashParticles.Play();
        isControlable = false;
        PlaySFX(crashSFX);
        spaceShipRenderer.enabled = false;
        yield return new WaitForSeconds(reloadSceneDelay);
        ReloadScene();
    }

    void PlaySFX(AudioClip audioClip)
    {
        if (!isPlaySFX) StartCoroutine(PlaySFXOnce(audioClip));
    }

    IEnumerator PlaySFXOnce(AudioClip audioClip)
    {
        isPlaySFX = true;
        sfxSource.PlayOneShot(audioClip);
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

    public bool CheckControlable()
    {
        return isControlable;
    }
}
