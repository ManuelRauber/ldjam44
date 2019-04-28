using LdJam44.EventSystem;
using UnityEngine;

namespace LdJam44.World
{
    public class Sun : MonoBehaviour
    {
        [Header("References")]
        public Light Light;

        public GameEvent SunDawnEvent;
        public GameEvent SunRiseEvent;
        
        [Header("Variables")]
        public float MaxIntensity = 4;

        public float DayPercentage = 0.7f;
        public float SunRisePercentageOfDayPercentage = 0.2f;
        public float SunDawnPercentageOfNightPercentage = 0.2f;
        public float DayNightCycleTimeInSeconds = 60;
        public bool StartAtDay = true;

        [Header("Diagnostics")]
        [SerializeField]
        private float DayTimeLength;

        [SerializeField]
        private float SunRiseLength;

        [SerializeField]
        private float NightTimeLength;

        [SerializeField]
        private float SunDawnLength;

        [SerializeField]
        private float CurrentDayNightCycle;

        [SerializeField]
        private bool SunRiseEventRaised;
        
        [SerializeField]
        private bool SunDawnEventRaised;

        private void Start()
        {
            DayTimeLength = DayNightCycleTimeInSeconds * DayPercentage;
            SunRiseLength = DayTimeLength * SunRisePercentageOfDayPercentage;
            NightTimeLength = DayNightCycleTimeInSeconds - DayTimeLength;
            SunDawnLength = NightTimeLength * SunDawnPercentageOfNightPercentage;

            if (StartAtDay)
            {
                CurrentDayNightCycle = SunRiseLength;
            }
        }

        private void Update()
        {
            CurrentDayNightCycle += Time.deltaTime;

            if (CurrentDayNightCycle > DayNightCycleTimeInSeconds)
            {
                CurrentDayNightCycle = 0;
                SunDawnEventRaised = false;
                SunRiseEventRaised = false;
            }

            if (CurrentDayNightCycle <= DayTimeLength)
            {
                SimulateDayTime();
                return;
            }

            SimulateNightTime();
        }

        private void SimulateNightTime()
        {
            var t = Mathf.Clamp01((CurrentDayNightCycle - DayTimeLength) / SunDawnLength);
            Light.intensity = Mathf.Lerp(MaxIntensity, 0, t);

            if (t > 0.5 && !SunDawnEventRaised)
            {
                SunDawnEventRaised = true;
                SunDawnEvent.Raise();
            }
        }

        private void SimulateDayTime()
        {
            var t = Mathf.Clamp01(CurrentDayNightCycle / SunRiseLength);
            Light.intensity = Mathf.Lerp(0, MaxIntensity, t);
            
            if (t > 0.5 && !SunRiseEventRaised)
            {
                SunRiseEventRaised = true;
                SunRiseEvent.Raise();
            }
        }
    }
}
