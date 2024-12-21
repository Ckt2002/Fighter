using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class OptionController : MonoBehaviour
    {
        private const string MusicVolume = "MusicVolume";
        private const string SfxVolume = "SFXVolume";

        [Header("Sliders")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        [Header("Audio Mixer")]
        [SerializeField]
        private AudioMixer audioMixer;

        private void Start()
        {
            LoadVolumeValue();
        }

        public void SetMusicVolume()
        {
            float volume = musicSlider.value;
            audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);

            SaveVolumeValue(MusicVolume, volume);
        }

        public void SetSfxVolume()
        {
            float volume = sfxSlider.value;
            audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);

            SaveVolumeValue(SfxVolume, volume);
        }

        private void SaveVolumeValue(string key, float volume)
        {
            PlayerPrefs.SetFloat(key, volume);
        }

        private void LoadVolumeValue()
        {
            if (PlayerPrefs.HasKey(MusicVolume))
            {
                musicSlider.value = PlayerPrefs.GetFloat(MusicVolume);

                audioMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat(MusicVolume)) * 20);

                SetMusicVolume();
            }

            if (PlayerPrefs.HasKey(SfxVolume))
            {
                sfxSlider.value = PlayerPrefs.GetFloat(SfxVolume);

                audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat(SfxVolume)) * 20);

                SetSfxVolume();
            }
        }
    }
}
