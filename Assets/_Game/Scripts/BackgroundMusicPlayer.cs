using System.Collections;
using LdJam44.Extensions;
using UnityEngine;

namespace LdJam44
{
    public class BackgroundMusicPlayer : MonoBehaviour
    {
        public AudioClip[] MusicTracks;
        public AudioSource AudioSource;
        private static BackgroundMusicPlayer _instance;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            StartCoroutine(NextMusic());
        }

        private IEnumerator NextMusic()
        {
            var clip = MusicTracks.PickOne();
            AudioSource.PlayOneShot(clip);
            
            yield return new WaitForSecondsRealtime(clip.length);

            StartCoroutine(NextMusic());
        }
    }
}
