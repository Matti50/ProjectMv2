using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private int _maxLife = 100;

    [SerializeField]
    private float _currentLife;

    private const int _minLife = 0;

    void Start()
    {
        _currentLife = _maxLife;
    }

    void Update()
    {
        
    }

    public bool DidIDie()
    {
        return GetCurrentLife() == 0;
    }

    public float GetCurrentLife()
    {
        return _currentLife;
    }

    public void TakeDamage(float incomingDamage)
    {
        _currentLife = Mathf.Max(_minLife, _currentLife - incomingDamage);
    }
}
