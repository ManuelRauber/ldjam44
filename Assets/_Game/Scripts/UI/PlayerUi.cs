using LdJam44.Variables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LdJam44.UI
{
    public class PlayerUi : UIBehaviour
    {
        [Header("References")]
        public RectTransform FillContainer;

        [Header("Variables")]
        public IntVariable PlayerLife;
        
        private float _maxFillHeight;
        private float _lastPlayerLife;

        protected override void Start()
        {
            _maxFillHeight = FillContainer.rect.height;
        }

        private void Update()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (_lastPlayerLife == PlayerLife.Value)
            {
                return;
            }

            FillContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                PlayerLife.Value * _maxFillHeight / PlayerLife.InitialValue);

            _lastPlayerLife = PlayerLife.Value;
        }
    }
}
