using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Renderer))]
public class UseColorsList : MonoBehaviour
{
    static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    void OnEnable()
    {
        SetColor();
    }

    void OnValidate()
    {
        SetColor();
    }

    bool colorSet = false;

    void SetColor()
    {
        if (colorSet)
            return;

        var rend = GetComponent<Renderer>();

        var list = FindFirstObjectByType<ColorsList>();
        if (list == null || list.colors == null || list.colors.Length == 0)
            return;

        Color c = list.colors[Random.Range(0, list.colors.Length - 1)];

        var mpb = new MaterialPropertyBlock();
        rend.GetPropertyBlock(mpb);

		mpb.SetColor(BaseColor, c);

        rend.SetPropertyBlock(mpb);

        colorSet = true;
    }
}
