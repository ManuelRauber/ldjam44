using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LdJam44
{
    public class BlueLightScript : MonoBehaviour
    {

        [Header("General")]
        public float LightSpeed;
        public bool isBlueLightOn;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isBlueLightOn) {
                transform.Rotate(0, LightSpeed, 0);
            }
        }
    }
}