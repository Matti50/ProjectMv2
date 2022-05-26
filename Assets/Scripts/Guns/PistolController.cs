using UnityEngine;

public class PistolController : MonoBehaviour, IGun
{
    [SerializeField]
    private GunData _gunData;

    private void Start()
    {
        GunData = _gunData;
    }

    public GunData GunData { get => _gunData; set => _gunData = value; } 
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
