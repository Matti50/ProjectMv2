using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private int _maxLife = 100;

    [SerializeField]
    private int _currentLife;

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

    public int GetCurrentLife()
    {
        return _currentLife;
    }

    public void TakeDamage(int incomingDamage)
    {
        _currentLife = Mathf.Max(_minLife, _currentLife - incomingDamage);
    }
}
