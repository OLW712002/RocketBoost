using UnityEngine;

public class FinishParticles : MonoBehaviour
{
    Transform player;
    Vector3 offset;

    private void Awake()
    {
        player = transform.parent;
        offset = transform.position - player.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 worldTargetPos = player.position + offset;
        transform.position = worldTargetPos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
