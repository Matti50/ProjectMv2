using UnityEngine;

public class PickPistolMissionSuccess : MonoBehaviour
{
    [SerializeField]
    private UIEvent _changeMission;

    [SerializeField]
    private Mission _objective;

    private Collider _pistolCollider;

    private void Awake()
    {
        _pistolCollider = gameObject.GetComponent<Collider>();
    }

    private void OnDisable()
    {
        _changeMission.Raise(new UIMissionChanged { MissionDescription = _objective.Description });
        this.enabled = false;
    }
}
