using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectTintoggle : MonoBehaviour
{
    [SerializeField]
    Transform[] toggleList;

    bool toggleable = true;

    SpriteRenderer sprRenderer;

    void Awake() {
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActivateToggle() {
        if (toggleable) {
            foreach(Transform toggle in toggleList) {
                toggle.GetComponent<TilemapCollider2D>().enabled = true;
                toggle.GetComponent<TilemapRenderer>().sortingLayerName = "Platform";
                toggle.GetComponent<Tilemap>().color = new Color(172, 255, 200, 255);

                sprRenderer.flipX = false;
                toggleable = false;
            }
        }
    }
}
