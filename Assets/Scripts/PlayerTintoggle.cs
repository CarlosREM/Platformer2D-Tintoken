using UnityEngine;

public class PlayerTintoggle : MonoBehaviour
{
    Transform toggle = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && toggle != null) {
            toggle.GetComponent<ObjectTintoggle>().ActivateToggle();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Tintoggle")) {
            toggle = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Tintoggle")) {
            toggle = null;
        }
    }
} 
