using UnityEngine;

namespace LdJam44.World
{
    public class StreetLamp : MonoBehaviour
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
