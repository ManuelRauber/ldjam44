using LdJam44.Extensions;
using UnityEngine;

namespace LdJam44
{
    public class RandomSprite : MonoBehaviour
    {
        public Sprite[] Sprites;

        private void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Sprites.PickOne();
        }
    }
}
