using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(BoxCollider))]
public class Fade : MonoBehaviour
{
    [Range(0f, 1f)] public float fadedAlpha = 0.25f;

    static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

    FadeCollider fadeCollider;
    BoxCollider boxCollider;

    Renderer rend;
    MaterialPropertyBlock mpb;
    Color initialColor;

    void Update()
    {
        if (fadeCollider == null)
            fadeCollider = FindFirstObjectByType<FadeCollider>();

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        bool overlap = fadeCollider.ComputePenetration(boxCollider, out var direction, out var normalizedDistance);

        ApplyAlpha(Mathf.Lerp(1.0f, fadedAlpha, normalizedDistance));
    }

    void ApplyAlpha(float alpha)
    {
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
            mpb = new MaterialPropertyBlock();
            var mat = rend.sharedMaterial;
            initialColor = (mat != null && mat.HasProperty(BaseColorId)) ? mat.GetColor(BaseColorId) : Color.white;
        }

        rend.GetPropertyBlock(mpb);
        Color c = initialColor; 
        c.a = alpha;
        mpb.SetColor(BaseColorId, c);
        rend.SetPropertyBlock(mpb);
    }
}
