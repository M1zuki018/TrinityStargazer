// StarButtonEffect.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class StarButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Material material;
    private Image image;
    
    [Header("Glow Settings")]
    public Color normalGlowColor = new Color(0.5f, 0.5f, 1f, 1f);
    public Color hoverGlowColor = new Color(0.7f, 0.7f, 1f, 1f);
    public Color pressedGlowColor = new Color(1f, 0.5f, 0.5f, 1f);
    
    [Header("Animation Settings")]
    public float normalGlowIntensity = 0.5f;
    public float hoverGlowIntensity = 1.0f;
    public float pressedGlowIntensity = 1.5f;
    public float animationSpeed = 2.0f;
    
    [Header("Outline Settings")]
    public float normalOutlineWidth = 0.02f;
    public float hoverOutlineWidth = 0.04f;
    public float pressedOutlineWidth = 0.03f;
    
    [Header("Animation")]
    public float normalPulseSpeed = 1.0f;
    public float hoverPulseSpeed = 2.0f;
    public float pressedPulseSpeed = 3.0f;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        material = new Material(Shader.Find("Custom/StarButtonShader"));
        image.material = material;
        
        // デフォルト状態の設定
        SetNormalState(true);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetHoverState();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        SetNormalState();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        SetPressedState();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(
            image.rectTransform, eventData.position, eventData.pressEventCamera))
        {
            SetHoverState();
        }
        else
        {
            SetNormalState();
        }
    }
    
    private void SetNormalState(bool instant = false)
    {
        StartCoroutine(AnimateProperties(
            normalGlowColor, normalGlowIntensity, normalOutlineWidth, normalPulseSpeed, instant ? 0 : animationSpeed));
    }
    
    private void SetHoverState()
    {
        StartCoroutine(AnimateProperties(
            hoverGlowColor, hoverGlowIntensity, hoverOutlineWidth, hoverPulseSpeed, animationSpeed));
    }
    
    private void SetPressedState()
    {
        StartCoroutine(AnimateProperties(
            pressedGlowColor, pressedGlowIntensity, pressedOutlineWidth, pressedPulseSpeed, animationSpeed));
    }
    
    private System.Collections.IEnumerator AnimateProperties(
        Color targetGlowColor, float targetGlowIntensity, float targetOutlineWidth, float targetPulseSpeed, float duration)
    {
        if (duration <= 0)
        {
            material.SetColor("_GlowColor", targetGlowColor);
            material.SetFloat("_GlowIntensity", targetGlowIntensity);
            material.SetFloat("_OutlineWidth", targetOutlineWidth);
            material.SetFloat("_PulseSpeed", targetPulseSpeed);
            yield break;
        }
        
        Color startGlowColor = material.GetColor("_GlowColor");
        float startGlowIntensity = material.GetFloat("_GlowIntensity");
        float startOutlineWidth = material.GetFloat("_OutlineWidth");
        float startPulseSpeed = material.GetFloat("_PulseSpeed");
        
        float elapsedTime = 0;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            
            // イージング関数を適用（スムーズな遷移のため）
            t = t * t * (3f - 2f * t); // スムーズステップ
            
            material.SetColor("_GlowColor", Color.Lerp(startGlowColor, targetGlowColor, t));
            material.SetFloat("_GlowIntensity", Mathf.Lerp(startGlowIntensity, targetGlowIntensity, t));
            material.SetFloat("_OutlineWidth", Mathf.Lerp(startOutlineWidth, targetOutlineWidth, t));
            material.SetFloat("_PulseSpeed", Mathf.Lerp(startPulseSpeed, targetPulseSpeed, t));
            
            yield return null;
        }
        
        // 最終値を設定
        material.SetColor("_GlowColor", targetGlowColor);
        material.SetFloat("_GlowIntensity", targetGlowIntensity);
        material.SetFloat("_OutlineWidth", targetOutlineWidth);
        material.SetFloat("_PulseSpeed", targetPulseSpeed);
    }
}