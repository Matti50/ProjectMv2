using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [SerializeField]
    private Mission _firstMission;

    private void Start()
    {
        GameManager._instance.ChangeMission(_firstMission);
    }
}
