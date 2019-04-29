using UnityEngine;

namespace LdJam44.World
{
    public class Lamp : MonoBehaviour
    {
        [Header("References")]
        public Light Light;
        
        public void SunRise()
        {
            Light.enabled = false;
        }

        public void SunDawn()
        {
            Light.enabled = true;
        }
    }
}
