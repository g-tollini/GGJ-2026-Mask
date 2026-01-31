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

    Renderer[] renderers;
    MaterialPropertyBlock[] mpbs;
    Color[] initialColors;

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
        if (renderers == null)
        {
            renderers = GetComponentsInChildren<Renderer>();
            mpbs = new MaterialPropertyBlock[renderers.Length];
            initialColors = new Color[renderers.Length];
            for (int i = 0; i < mpbs.Length; i++)
            {
                var mat = renderers[i].sharedMaterial;
                mpbs[i] = new MaterialPropertyBlock();
                initialColors[i] = (mat != null && mat.HasProperty(BaseColorId)) ? mat.GetColor(BaseColorId) : Color.white;
            }
        }
        else
        {
            for (int i = 0; i < renderers.Length; i++)
            {

                renderers[i].GetPropertyBlock(mpbs[i]);
                Color c = initialColors[i];
                c.a = alpha;
                mpbs[i].SetColor(BaseColorId, c);
                renderers[i].SetPropertyBlock(mpbs[i]);
            }
        }
    }
}
