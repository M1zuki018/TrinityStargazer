// GlowingButton.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class GlowingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Material material;
    private Image image;
    
    [Header("Base Settings")]
    public Color buttonColor = new Color(0.1f, 0.1f, 0.1f, 1f); // 黒に近い色
    
    [Header("Glow Settings")]
    public Color normalGlowColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public Color hoverGlowColor = new Color(0.7f, 0.7f, 0.9f, 1f);
    
    [Header("Animation Settings")]
    public float normalGlowIntensity = 0.3f;
    public float hoverGlowIntensity = 0.8f;
    public float edgeGlowStrength = 1.5f;
    public float pulseSpeed = 1.5f;
    public float pulseAmount = 0.1f;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        material = new Material(Shader.Find("Custom/GlowingButtonShader"));
        image.material = material;
        
        ApplyDefaultSettings();
    }
    
    private void ApplyDefaultSettings()
    {
        material.SetColor("_Color", buttonColor);
        material.SetColor("_GlowColor", normalGlowColor);
        material.SetFloat("_GlowIntensity", normalGlowIntensity);
        material.SetFloat("_EdgeGlowStrength", edgeGlowStrength);
        material.SetFloat("_PulseSpeed", pulseSpeed);
        material.SetFloat("_PulseAmount", pulseAmount);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        material.SetColor("_GlowColor", hoverGlowColor);
        material.SetFloat("_GlowIntensity", hoverGlowIntensity);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        material.SetColor("_GlowColor", normalGlowColor);
        material.SetFloat("_GlowIntensity", normalGlowIntensity);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        // クリック時のエフェクト
        material.SetFloat("_PulseAmount", pulseAmount * 2);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        material.SetFloat("_PulseAmount", pulseAmount);
    }
}