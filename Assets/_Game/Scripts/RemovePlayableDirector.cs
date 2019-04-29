using UnityEngine;
using UnityEngine.Playables;

namespace LdJam44
{
    public class RemovePlayableDirector : MonoBehaviour
    {
        public void RemoveDirector()
        {
            Destroy(GetComponent<PlayableDirector>());
        }
    }
}
