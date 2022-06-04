using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "ScriptableObjects/Events/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(GameEventParam param = null)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].RaiseEvent(param);
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
