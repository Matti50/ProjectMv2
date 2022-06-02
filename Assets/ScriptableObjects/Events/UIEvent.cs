using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UI Event", menuName = "ScriptableObjects/Events/UIEvent", order = 2)]
public class UIEvent : ScriptableObject
{
    private List<UIEventListener> _listeners = new List<UIEventListener>();

    public void Raise(UIEventParam p)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].RaiseEvent(p);
        }
    }

    public void RegisterListener(UIEventListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnregisterListener(UIEventListener listener)
    {
        _listeners.Remove(listener);
    }
}
