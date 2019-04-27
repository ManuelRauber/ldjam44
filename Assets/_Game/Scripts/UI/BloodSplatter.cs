using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LdJam44.UI
{
    public class BloodSplatter : UIBehaviour
    {
        [Header("References")]
        public Image[] BloodSplatterImages;

        [Header("Variables")]
        public float Alpha = 0.5f;

        public void PlayerHasBeenHit()
        {
            StopCoroutine(DoEffect());
            StartCoroutine(DoEffect());
        }

        private IEnumerator DoEffect()
        {
            var images = BloodSplatterImages.Select(p =>
            {
                var color = p.color;
                return new
                {
                    Image = p,
                    StartColor = new Color(color.r, color.g, color.b, 0),
                    TargetColor = new Color(color.r, color.g, color.b, Alpha)
                };
            }).ToArray();
            
            for (var time = 0f; time <= 1; time += Time.deltaTime / 0.1f)
            {
                foreach (var image in images)
                {
                    image.Image.color = Color.Lerp(image.StartColor, image.TargetColor, time);
                }
                
                yield return new WaitForEndOfFrame();
            }
            
            for (var time = 0f; time <= 1; time += Time.deltaTime / 0.1f)
            {
                foreach (var image in images)
                {
                    image.Image.color = Color.Lerp(image.TargetColor, image.StartColor, time);
                }
                
                yield return new WaitForEndOfFrame();
            }
            
            foreach (var image in images)
            {
                image.Image.color = image.StartColor;
            }
        }
    }
}
