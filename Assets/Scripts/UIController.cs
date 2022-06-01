using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private float _health, _maxHealth = 100f;

    private float _lerpSpeed;

    [SerializeField]
    private Image _healthBar;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void Update()
    {

        _lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();
    }

    public void SetLife(int life)
    {
        _health = life;
    }

    private void HealthBarFiller()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _health / _maxHealth, _lerpSpeed);
    }
    
    private void ColorChanger()
    {
        Color color = Color.Lerp(Color.red, Color.green, _health / _maxHealth);
        _healthBar.color = color;
    }
}
