using UnityEngine;

namespace LdJam44
{
    public class BlueLightScript : MonoBehaviour
    {

        [Header("Variables")]
        public Light[] Lights;

        public float FlashTime = 0.25f;
        public bool IsBlueLightOn;

        private int _nextLight;
        private float _timeToNextLightChange;

        private void Start()
        {
            _timeToNextLightChange = Time.time;
        }

        void Update()
        {
            if (!IsBlueLightOn || Time.time < _timeToNextLightChange)
            {
                return;
            }

            _timeToNextLightChange = Time.time + FlashTime;
            
            foreach (var light in Lights)
            {
                light.gameObject.SetActive(false);
            }
            
            Lights[_nextLight].gameObject.SetActive(true);

            _nextLight = ++_nextLight % Lights.Length;
        }
    }
}
