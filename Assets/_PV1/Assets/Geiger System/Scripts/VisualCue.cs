using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace _PV1.Assets.Geiger_System.Scripts
{
    public class VisualCue : MonoBehaviour
    {
        [SerializeField] private Volume m_volumeProfile;

        private FilmGrain m_filmGrain;
        private ColorAdjustments m_colorAdjustments;
        private Vignette m_vignette;

        // Start is called before the first frame update
        void Start()
        {
            m_volumeProfile.profile.TryGet(typeof(FilmGrain), out FilmGrain filmGrain);

            if (filmGrain != null)
            {
                m_filmGrain = filmGrain;
            }

            m_volumeProfile.profile.TryGet(typeof(ColorAdjustments), out ColorAdjustments colorAdjustments);

            if (colorAdjustments != null)
            {
                m_colorAdjustments = colorAdjustments;
            }

            m_volumeProfile.profile.TryGet(typeof(Vignette), out Vignette vignette);

            if (vignette != null)
            {
                m_vignette = vignette;
            }
        }

        public void UpdateRadiationCue(float value)
        {
            var saturationValue = -100 * value;
            m_filmGrain.intensity.value = value;
            m_colorAdjustments.saturation.value = saturationValue;
            m_vignette.intensity.value = value;
        }
    }
}