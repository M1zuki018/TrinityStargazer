// SimpleCharacterEffect.cs
using UnityEngine;

[ExecuteInEditMode]
public class SimpleCharacterEffect : MonoBehaviour
{
    public Material characterMaterial;
    
    [Header("Outline Settings")]
    [Range(0.001f, 0.03f)] public float outlineWidth = 0.005f;
    public Color outlineColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    
    [Header("Rim Light Settings")]
    public Color rimColor = new Color(0.26f, 0.19f, 0.16f, 0.0f);
    [Range(0.5f, 8.0f)] public float rimPower = 3.0f;
    
    private void Update()
    {
        if (characterMaterial != null)
        {
            characterMaterial.SetFloat("_OutlineWidth", outlineWidth);
            characterMaterial.SetColor("_OutlineColor", outlineColor);
            characterMaterial.SetColor("_RimColor", rimColor);
            characterMaterial.SetFloat("_RimPower", rimPower);
        }
    }
}