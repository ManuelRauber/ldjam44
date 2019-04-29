using UnityEngine;
using UnityEngine.EventSystems;

namespace LdJam44.UI
{
    public class SkipStartupSequence : UIBehaviour
    {
        public float FastForwardTimeScale = 2.5f;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = FastForwardTimeScale;
            }
        }

        protected override void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}
