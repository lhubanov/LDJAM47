using UnityEngine;
using System.Collections;

public class HighlightOnClick : MonoBehaviour
{
    [SerializeField]
    private Material material;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if( renderer )
        {
            material = renderer.material;
        }
    }

    void OnMouseOver()
    {
        // FIXME: LH: set to green
        material.SetColor("_OutlineColor", new Color(0f, 1f, 0.39f, 1f));
        material.SetFloat("_Outline", 0.1f);
    }

    private void OnMouseExit()
    {
        material.SetColor("_OutlineColor", new Color(0f, 0f, 0f, 1f));
        material.SetFloat("_Outline", 0f);
    }
}