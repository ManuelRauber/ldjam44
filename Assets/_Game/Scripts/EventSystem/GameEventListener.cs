using UnityEngine;
using UnityEngine.Events;

namespace LdJam44.EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent UnityEvent;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            UnityEvent.Invoke();
        }
    }
}