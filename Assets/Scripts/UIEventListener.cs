using UnityEngine;
using UnityEngine.Events;

public struct UIEventParam
{
    public int Value;
}

public class UIEventListener : MonoBehaviour
{
    [SerializeField]
    private UIEvent _eventToListen;

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
