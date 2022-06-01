using UnityEngine;

public class PistolController : MonoBehaviour, IGun
{
    [SerializeField]
    private Gun _gunData;

    public int Id { get; } = 1;

    private void Start()
    {
        GunData = _gunData;
    }

    public Gun GunData { get => _gunData; set => _gunData = value; } 
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
