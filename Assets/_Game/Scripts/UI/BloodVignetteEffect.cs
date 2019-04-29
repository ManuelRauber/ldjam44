using LdJam44.Variables;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace LdJam44.UI
{
    public class BloodVignetteEffect : MonoBehaviour
    {
        public PostProcessVolume PostProcessVolume;
        public IntVariable PlayerLife;
        private Vignette _vignette;

        private void Start()
        {
            _vignette = PostProcessVolume.profile.GetSetting<Vignette>();
        }

        private void Update()
        {
            _vignette.intensity.value = 1 - PlayerLife.Value / (float)PlayerLife.InitialValue;
        }
    }
}
