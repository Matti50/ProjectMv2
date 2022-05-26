using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="New UI Event", menuName = "ScriptableObjects/Events/UIEvent",order = 1)]
public class UIEvent : ScriptableObject
{
    private List<UIEventListener> listeners = new List<UIEventListener>();

    public void Raise()
    {
        for(int i = 0; i < listeners.Count; i++)
        {
            listeners[i].RaiseEvent();
        }
    }

    public void RegisterListener(UIEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(UIEventListener listener)
    {
        listeners.Remove(listener);
    }
}
