using TMPro;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    public GameObjectives objectives;
    public TextMeshProUGUI damages;

    private void Start()
    {
        objectives = FindFirstObjectByType<GameObjectives>();

        if (objectives == null)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        damages.text = $"Total damages: {objectives.DamageCount}$";
    }
}
