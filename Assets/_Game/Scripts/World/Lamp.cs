using UnityEngine;

namespace LdJam44.World
{
    public class Lamp : MonoBehaviour
    {
        [Header("References")]
        public Light Light;
        
        public void SunRise()
        {
            Light.gameObject.SetActive(false);
        }

        public void SunDawn()
        {
            Light.gameObject.SetActive(true);
        }
    }
}
