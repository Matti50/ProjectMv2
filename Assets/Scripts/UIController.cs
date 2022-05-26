using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Start()
    {
        _missionDescription.text = GameManager._instance.GetMissionDescription();
    }

    [SerializeField]
    private TextMeshProUGUI _lifeAmmount;
    [SerializeField]
    private TextMeshProUGUI _bulletsAmmount;
    [SerializeField]
    private TextMeshProUGUI _missionDescription;

    public void SetLife(int life)
    {
        _lifeAmmount.text = life.ToString();
    }

    public void SetBullets(int bullets)
    {
        _bulletsAmmount.text = bullets.ToString();
    }
}
