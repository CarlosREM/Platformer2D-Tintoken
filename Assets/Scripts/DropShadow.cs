using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropShadow : MonoBehaviour
{
    public Vector2 ShadowOffset;
    public Material ShadowMaterial;

    SpriteRenderer spriteRenderer;
    GameObject shadowGameobject;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //create a new gameobject to be used as drop shadow
        shadowGameobject = new GameObject("Shadow");
        shadowGameobject.transform.parent = transform;
        Animator shadowAnim = shadowGameobject.AddComponent(typeof(Animator)) as Animator ;
        
        shadowAnim = GetComponent<Animator>();

        //create a new SpriteRenderer for Shadow gameobject
        SpriteRenderer shadowSpriteRenderer = shadowGameobject.AddComponent<SpriteRenderer>();

        //set the shadow gameobject's sprite to the original sprite
        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
        
        //set the shadow gameobject's material to the shadow material we created
        shadowSpriteRenderer.material = ShadowMaterial;

        //update the sorting layer of the shadow to always lie behind the sprite
        shadowSpriteRenderer.sortingLayerName = "Background";
        shadowSpriteRenderer.sortingOrder = -5;
    }

    void LateUpdate()
    {
        //update the position and rotation of the sprite's shadow with moving sprite
        shadowGameobject.transform.position = transform.position + (Vector3) ShadowOffset;
        shadowGameobject.transform.rotation = transform.rotation;
    }
}