using UnityEngine;

namespace TopDownCharacter2D.FX
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        [SerializeField] [Range(0f, 1f)] private float soundEffectVolume;
        [SerializeField] [Range(0f, 1f)] private float soundEffectPitchVariance;
        [SerializeField] [Range(0f, 1f)] private float musicVolume;

        private AudioSource musicAudioSource;

        private void Awake()
        {
            instance = this;
            musicAudioSource = GetComponent<AudioSource>();
        }

        public static void PlaySoundEffect(AudioClip audio, Vector3 position)
        {
            PlayClipAt(audio, position, true);
        }

        public static void PlaySoundEffect(AudioClip audio)
        {
            PlayClipAt(audio, Vector3.zero, false);
        }

        public static void ChangeBackGroundMusic(AudioClip music)
        {
            instance.musicAudioSource.Stop();
            instance.musicAudioSource.clip = music;
            instance.musicAudioSource.Play();
        }

        private static void PlayClipAt(AudioClip clip, Vector3 pos, bool spatialize)
        {
            GameObject gameObject = new GameObject("TempAudio");
            gameObject.transform.position = pos;
            AudioSource source = gameObject.AddComponent<AudioSource>(); // add an audio source
            source.clip = clip; // define the clip
            source.volume = instance.soundEffectVolume;
            source.Play(); // start the sound
            source.pitch = 1f + Random.Range(-instance.soundEffectPitchVariance, instance.soundEffectPitchVariance);
            source.spatialize = spatialize;
            Destroy(gameObject, clip.length * 2f); // destroy object after clip duration
        }
    }
}