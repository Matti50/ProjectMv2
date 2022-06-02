using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public UIEvent _lifeChange;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _lifeChange.Raise(new UILifeChangedParam(-10));
        }
    }
}
