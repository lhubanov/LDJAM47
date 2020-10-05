using UnityEngine;
using System.Collections;

public class HighlightOnClick : MonoBehaviour
{
    [SerializeField]
    private Material material;

    private float cachedOutlineThickness = 0.1f;
    private Color cachedOutlineColour = new Color(0.1650943f, 1f, 0.2573221f, 1f);

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        cachedOutlineColour = new Color( 0.0f, 0.9f, 0.1f );
        if( renderer )
        {
            material = renderer.material;

            // initialise material state
            material.SetColor("_OutlineColor", new Color(0f, 0f, 0f, 1f));
            material.SetFloat("_Outline", 0f);
        }
    }

    void OnMouseOver()
    {
        material.SetColor("_OutlineColor", cachedOutlineColour);
        material.SetFloat("_Outline", cachedOutlineThickness);
    }

    private void OnMouseExit()
    {
        material.SetColor("_OutlineColor", new Color(0f, 0f, 0f, 1f));
        material.SetFloat("_Outline", 0f);
    }
}