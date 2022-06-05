using UnityEngine;

public class GastTankController : MonoBehaviour, IPickeable
{
    [SerializeField]
    private GasTank _gasTank;

    [SerializeField]
    private GameEvent _ableToWin;

    [SerializeField]
    private UIEvent _updateMission;

    [SerializeField]
    private Mission _nextObjective;

    public GameObject GetItem => gameObject;

    public Sprite GetImage()
    {
        return _gasTank.Image;
    }

    public string GetName()
    {
        return _gasTank.Name;
    }

    public (Vector3, Vector3) GetOkRotationAndPosition()
    {
        return (new Vector3(), new Vector3());
    }

    public void GetPickedUp()
    {
        _ableToWin.Raise();
        _updateMission.Raise(new UIMissionChanged {MissionDescription = _nextObjective.Description});
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);
    }
}
