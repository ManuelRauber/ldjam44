using LdJam44.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LdJam44.UI
{
    public class PlayerUi : UIBehaviour
    {
        [Header("References")]
        public RectTransform FillContainer;

        public TextMeshProUGUI PointsText;

        [Header("Variables")]
        public IntVariable PlayerLife;
        public IntVariable Points;

        private float _maxFillHeight;
        private float _lastPlayerLife;

        protected override void Start()
        {
            _maxFillHeight = FillContainer.rect.height;
        }

        private void Update()
        {
            PointsText.text = Points.Value.ToString();
            
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
