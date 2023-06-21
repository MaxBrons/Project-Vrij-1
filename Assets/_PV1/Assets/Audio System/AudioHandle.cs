
using UnityEngine;

namespace PV.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioHandle : MonoBehaviour
    {
        public AudioSource Source { get; private set; }

        public void Init(AudioClip clip, AudioManager.AudioSettings settings)
        {
            Source.clip = clip;
            Source.volume = settings.Volume;
            Source.pitch = settings.Pitch;
            Source.minDistance = settings.MinDistance;
            Source.maxDistance = settings.MaxDistance;
            Source.rolloffMode = settings.RolloffMode;
            Source.spatialBlend = settings.SpatialBlend;
            Source.spatialize = settings.Spatialize;
            Source.spread = settings.Spread;
        }

        public void Play(float startTime = 0.0f)
        {
            Source.Play();
            Source.SetScheduledEndTime(startTime);
        }

        public void Stop()
        {
            Source.Stop();
        }

        public void Pauze()
        {
            Source.Pause();
        }

        public void Resume()
        {
            Source.UnPause();
        }
    }
}