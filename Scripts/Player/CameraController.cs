using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float offsetX = 10;
    [SerializeField]
    private float offsetY = 10;
    [SerializeField]
    private float offsetZ = 10;

    public GameObject target;

    void Start()
    {
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(target.transform.position.x - offsetX,
            10, target.transform.position.z - offsetZ);
    }

}
