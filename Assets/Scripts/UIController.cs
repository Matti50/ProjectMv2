using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private float _health, _maxHealth = 100f;

    private float _lerpSpeed;

    [SerializeField]
    private Image _healthBar;

    [SerializeField]
    private Image _healthCross;

    [SerializeField]
    private TextMeshProUGUI _currentBullets;

    [SerializeField]
    private TextMeshProUGUI _totalBullets;

    [SerializeField]
    private TextMeshProUGUI _itemName;

    [SerializeField]
    private GameObject _item;

    private void Start()
    {
        _item.gameObject.SetActive(false);
        _health = _maxHealth;
    }

    private void Update()
    {
        _lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();
    }

    private void SetLife(float lifeChangeAmmount)
    {
        _health += lifeChangeAmmount;
    }

    private void SetCurrentObjectSprite(Sprite sprite)
    {
        var image = _item.GetComponentInChildren<Image>();
        image.sprite = sprite;
    }

    private void SetCurrentBullets(int? currentBullets)
    {
        if(currentBullets == null)
        {
            _currentBullets.gameObject.SetActive(false);
            return;
        }

        _currentBullets.text = currentBullets.Value.ToString();
        _currentBullets.gameObject.SetActive(true);
    }

    private void SetTotalBullets(int? totalBullets)
    {
        if (totalBullets == null)
        {
            _totalBullets.gameObject.SetActive(false);
            return;
        }

        _totalBullets.text = totalBullets.Value.ToString();
        _totalBullets.gameObject.SetActive(true);
    }

    private void SetItemName(string itemName)
    {
        _itemName.text = itemName;
    }

    private void HealthBarFiller()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _health / _maxHealth, _lerpSpeed);
    }
    
    private void ColorChanger()
    {
        Color color = Color.Lerp(Color.red, Color.green, _health / _maxHealth);
        _healthBar.color = color;
        _healthCross.color = color;
    }

    public void OnLifeChanged(UIEventParam uiEvent)
    {
        UILifeChangedParam changedLifeEvent = uiEvent as UILifeChangedParam;

        SetLife(changedLifeEvent.AmmountOfLifeChanged());
    }

    public void OnItemEquiped(UIEventParam uiEvent)
    {
        if(uiEvent == null)
        {
            _item.gameObject.SetActive(false);
            return;
        }

        _item.gameObject.SetActive(true);

        UiElementEquipedParam objectEquiped = uiEvent as UiElementEquipedParam;

        SetCurrentObjectSprite(objectEquiped.ItemSpray());
        SetCurrentBullets(objectEquiped.CurrentBullets());
        SetTotalBullets(objectEquiped.TotalBullets());
        SetItemName(objectEquiped.ItemName());
    }

    public void OnBulletsChanged(UIEventParam uiEvent)
    {
        BulletsUIParam bullets = uiEvent as BulletsUIParam;

        SetCurrentBullets(bullets.CurrentBullets);
        SetTotalBullets(bullets.TotalBullets);
    }
}
