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

    [SerializeField]
    private TextMeshProUGUI _missionText;


    private float _timeToHideHint = 5f;
    private float _counterToHideHint;

    private void Start()
    {
        _item.gameObject.SetActive(false);
        _health = _maxHealth;
        _counterToHideHint = Time.time + _timeToHideHint;
    }

    private void Update()
    {
        _lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorChanger();
        HideHint();
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

    private void SetMission(string missionText)
    {
        _missionText.text = missionText;
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

    public void OnMissionChanged(UIEventParam uiEvent)
    {
        UIMissionChanged missionChanged = uiEvent as UIMissionChanged;

        SetMission(missionChanged.MissionDescription);
        ShowHint();
    }

    private void HideHint()
    {
        if(_missionText.gameObject.activeSelf == true && _counterToHideHint <= Time.time)
        {
            _missionText.gameObject.SetActive(false);
        }
    }

    public void ShowHint()
    {
        _missionText.gameObject.SetActive(true);
        _counterToHideHint = Time.time + _timeToHideHint;
    }
}
