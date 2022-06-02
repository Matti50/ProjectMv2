using UnityEngine;
using UnityEngine.Events;

public class UIEventListener : MonoBehaviour
{
    [SerializeField]
    private UIEvent _eventToListen;

    [SerializeField]
    private UnityEvent<UIEventParam> _unityEvent;

    private void Awake()
    {
        _eventToListen.RegisterListener(this);
    }

    private void OnDestroy()
    {
        _eventToListen.UnregisterListener(this);
    }

    public void RaiseEvent(UIEventParam uIEventParam)
    {
        _unityEvent.Invoke(uIEventParam);
    }
}
