using UnityEngine;

namespace Code.Gameplay.Common.Visuals.StatusVisuals
{
  public class StatusVisuals : MonoBehaviour, IStatusVisuals
  {
    // URP: _BaseColor вместо _Color
    private static readonly int BaseColorProperty = Shader.PropertyToID("_BaseColor");
    private static readonly int ColorIntensityProperty = Shader.PropertyToID("_Intensity");
    private static readonly int OutlineSizeProperty = Shader.PropertyToID("_OutlineSize");
    private static readonly int OutlineColorProperty = Shader.PropertyToID("_OutlineColor");
    private static readonly int OutlineSmoothnessProperty = Shader.PropertyToID("_OutlineSmoothness");

    public Renderer Renderer;

    [Header("Freeze")] public Color FreezeOutlineColor = new Color32(56, 163, 190, 255);
    public float FreezeOutlineSize = 3f;
    public float FreezeOutlineSmoothness = 8f;

    [Header("Poison")]
    public Color PoisonColor = new Color32(80, 200, 80, 255); // исправлен баг: был скопирован FreezeColor

    public float PoisonColorIntensity = 0.6f;

    private MaterialPropertyBlock _propertyBlock;

    private void Awake()
    {
      _propertyBlock = new MaterialPropertyBlock();
    }

    // ── Freeze ────────────────────────────────────────────────────────────────

    public void ApplyFreeze()
    {
      Renderer.GetPropertyBlock(_propertyBlock);
      _propertyBlock.SetColor(OutlineColorProperty, FreezeOutlineColor);
      _propertyBlock.SetFloat(OutlineSizeProperty, FreezeOutlineSize);
      _propertyBlock.SetFloat(OutlineSmoothnessProperty, FreezeOutlineSmoothness);
      Renderer.SetPropertyBlock(_propertyBlock);
    }

    public void UnapplyFreeze()
    {
      Renderer.GetPropertyBlock(_propertyBlock);
      _propertyBlock.SetColor(OutlineColorProperty, Color.white);
      _propertyBlock.SetFloat(OutlineSizeProperty, 0f);
      _propertyBlock.SetFloat(OutlineSmoothnessProperty, 0f);
      Renderer.SetPropertyBlock(_propertyBlock);
    }

    // ── Poison ────────────────────────────────────────────────────────────────

    public void ApplyPoison()
    {
      Renderer.GetPropertyBlock(_propertyBlock);
      _propertyBlock.SetColor(BaseColorProperty, PoisonColor);
      _propertyBlock.SetFloat(ColorIntensityProperty, PoisonColorIntensity);
      Renderer.SetPropertyBlock(_propertyBlock);
    }

    public void UnapplyPoison()
    {
      Renderer.GetPropertyBlock(_propertyBlock);
      _propertyBlock.SetColor(BaseColorProperty, Color.white);
      _propertyBlock.SetFloat(ColorIntensityProperty, 0f);
      Renderer.SetPropertyBlock(_propertyBlock);
    }
  }
}