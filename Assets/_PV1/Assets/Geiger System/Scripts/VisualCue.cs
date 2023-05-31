using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PV.Systems.Geiger
{
    public class VisualCue : MonoBehaviour
    {
        [SerializeField] private Volume m_VolumeProfile;

        private FilmGrain m_FilmGrain;
        private ColorAdjustments m_ColorAdjustments;
        private Vignette m_Vignette;

        // Start is called before the first frame update
        void Start()
        {
            m_VolumeProfile.profile.TryGet(typeof(FilmGrain), out FilmGrain filmGrain);

            if (filmGrain != null)
            {
                m_FilmGrain = filmGrain;
            }

            m_VolumeProfile.profile.TryGet(typeof(ColorAdjustments), out ColorAdjustments colorAdjustments);

            if (colorAdjustments != null)
            {
                m_ColorAdjustments = colorAdjustments;
            }

            m_VolumeProfile.profile.TryGet(typeof(Vignette), out Vignette vignette);

            if (vignette != null)
            {
                m_Vignette = vignette;
            }
        }

        public void UpdateRadiationCue(float value)
        {
            var saturationValue = -100 * value;
            m_FilmGrain.intensity.value = value;
            m_ColorAdjustments.saturation.value = saturationValue;
            m_Vignette.intensity.value = value;
        }
    }
}