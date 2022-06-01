using UnityEngine;

public class HealController :MonoBehaviour, IPickeable
{
    [SerializeField]
    private Heal heal;

    public int Id { get; } = 2;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void Destroy() 
    {
        Destroy(gameObject);
    }
}
