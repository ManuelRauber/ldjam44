using System.Collections.Generic;
using UnityEngine;

namespace LdJam44.EventSystem
{
    [CreateAssetMenu(menuName = "LD44/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly IList<GameEventListener> _listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener gameEventListener)
        {
            _listeners.Add(gameEventListener);
        }

        public void UnregisterListener(GameEventListener gameEventListener)
        {
            _listeners.Remove(gameEventListener);
        }
    }
}
