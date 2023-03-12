using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float followDelay = 0.1f;

    void Update()
    {
        float t =  1f - Mathf.Pow(followDelay, Time.deltaTime);
        Vector3 newPos = Vector3.Lerp(transform.position, player.transform.position, t);
        newPos.z = -10;
        transform.position = newPos;
    }
}
