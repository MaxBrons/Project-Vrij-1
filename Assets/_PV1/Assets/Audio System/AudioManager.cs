using UnityEngine;

namespace PV.Audio
{
    [System.Serializable]
    public class AudioManager : Core.Behaviour
    {
        [System.Serializable]
        public struct AudioSettings
        {
            public float Volume;
            public float Pitch;
            public float MinDistance;
            public float MaxDistance;
            public AudioRolloffMode RolloffMode;
            public float SpatialBlend;
            public bool Spatialize;
            public float Spread;

            public AudioSettings(float volume, float pitch, float minDistance, float maxDistance, AudioRolloffMode rolloffMode, float spatialBlend, bool spatialize, float spread)
            {
                Volume = volume;
                Pitch = pitch;
                MinDistance = minDistance;
                MaxDistance = maxDistance;
                RolloffMode = rolloffMode;
                SpatialBlend = spatialBlend;
                Spatialize = spatialize;
                Spread = spread;
            }
        }

        [SerializeField] private AudioSettings m_DefaultAudioSettings;

        public static AudioHandle Play(AudioClip clip, AudioSettings settings)
        {
            var gameObject = MonoBehaviour.Instantiate(new GameObject("AudioHandle"));
            var handle = gameObject.AddComponent<AudioHandle>();
            handle.Init(clip, settings);

            return handle;
        }

        public static AudioHandle Play2D(AudioClip clip, AudioSettings settings)
        {
            var gameObject = MonoBehaviour.Instantiate(new GameObject("AudioHandle"));
            var handle = gameObject.AddComponent<AudioHandle>();

            settings.SpatialBlend = 0.0f;
            handle.Init(clip, settings);

            return handle;
        }
    }
}
