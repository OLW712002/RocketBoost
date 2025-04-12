using UnityEngine;

public class GravityCustom : MonoBehaviour
{
    [SerializeField] Vector3 customGravity = new Vector3(0, -9.81f/6, 0);

    void Start()
    {
        Physics.gravity = customGravity;
    }
}
