using UnityEngine;

public class GameObjectives : MonoBehaviour
{
    public float DamageCount = 0f;

    public void Destroyed(Destroyable destroyable)
    {
        DamageCount += destroyable.Price;
        Destroy(destroyable.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
