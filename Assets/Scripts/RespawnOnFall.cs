using UnityEngine;

public class RespawnOnFall : MonoBehaviour
{
    [SerializeField] float yDeadzone;

    [SerializeField] Transform currentRespawn;

    void Update()
    {
        if (transform.position.y <= yDeadzone)
            transform.position = currentRespawn.position;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Respawn")) {
            currentRespawn = other.transform;
        }
    }
}
