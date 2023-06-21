using PV.Entities;
using PV.Systems.Geiger;
using UnityEngine;

namespace PV
{
    public class HUD : MonoBehaviour
    {

        [SerializeField] private GasMask m_Mask;
        [SerializeField] private Player m_Player;
        [SerializeField] private ScriptSlider m_SliderHealth;
        [SerializeField] private ScriptSlider m_SliderMask;

        private void Start()
        {
            m_SliderHealth.SetMaxValue((int)m_Player.GetHealth());
            m_SliderMask.SetMaxValue((int)m_Mask.GetDurability());
        }

        // Update is called once per frame
        void Update()
        {
            m_SliderHealth.SetValue((int)m_Player.GetHealth());
            m_SliderMask.SetValue((int)m_Mask.GetDurability());
        }
    }
}
