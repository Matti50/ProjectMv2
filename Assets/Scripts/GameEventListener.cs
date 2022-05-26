using UnityEngine.Events;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEvent _eventToListen;

    [SerializeField]
    private UnityEvent _unityEvent;

    private void Awake()
    {
        _eventToListen.RegisterListener(this);
    }

    private void OnDestroy()
    {
        _eventToListen.UnregisterListener(this);
    }

    public void RaiseEvent()
    {
        _unityEvent.Invoke();
    }
}
