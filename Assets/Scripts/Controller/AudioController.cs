using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class AudioController : MonoBehaviour
    {
        [Header("Audio Source")]
        [SerializeField]
        private AudioSource backGroundMusicSource;
        [SerializeField]
        private AudioSource sfx;

        [Header("Audio Clip")]
        [SerializeField]
        private AudioClip mainMenuBackGround;

        [Header("")]
        [SerializeField] private List<AudioClip> inGameBackGround;

        private IEnumerator PlayInGameBackgroundMusic()
        {
            while (true)
            {
                foreach (AudioClip clip in inGameBackGround)
                {
                    backGroundMusicSource.clip = clip;
                    backGroundMusicSource.Play();

                    // Đợi cho đến khi bài hát hiện tại phát xong
                    yield return new WaitForSeconds(clip.length);
                    yield return new WaitForSeconds(1);
                }
            }
        }

        public void PlayMainMenuBackGround()
        {
            backGroundMusicSource.clip = mainMenuBackGround;
            backGroundMusicSource.Play();
        }

        public void StopBackGroundMusic()
        {
            backGroundMusicSource.Stop();
        }

        public void StarPlayingList()
        {
            StartCoroutine(PlayInGameBackgroundMusic());
        }

        public void StopPlayingList()
        {
            StopCoroutine(PlayInGameBackgroundMusic());
        }
    }
}
