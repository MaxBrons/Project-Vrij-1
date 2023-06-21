using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PV.Interaction
{
    public class InteractableUI : MonoBehaviour
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private TextMeshProUGUI m_TitleText;
        [SerializeField] private TextMeshProUGUI m_InteractionTypeText;
        [SerializeField] private Button m_Button;
        [SerializeField] private Image m_FillOnHoldImage;

        private float m_HeldTime = 0.0f;
        private bool m_ShouldUpdate = false;
        private bool m_ShouldHold = false;
        private float m_HoldDuration = 0.0f;

        private void Update()
        {
            if (!m_ShouldUpdate)
                return;

            m_Button.Select();

            if (m_ShouldHold) {
                m_FillOnHoldImage.fillAmount = Mathf.Clamp01(1 / m_HoldDuration * m_HeldTime);
                m_HeldTime += Time.deltaTime;

                if (m_HeldTime >= m_HoldDuration) {
                    m_ShouldHold = false;
                    m_FillOnHoldImage.fillAmount = 0;
                }
            }
        }

        public void Init(string title, Sprite buttonSprite, string buttonContent, bool shouldHold, float holdDuration, bool holding)
        {
            m_TitleText.text = title;
            m_Image.sprite = buttonSprite != null ? buttonSprite : m_Image.sprite;
            m_InteractionTypeText.text = buttonContent.ToUpper();
            m_ShouldUpdate = holding;
            m_ShouldHold = shouldHold;
            m_HoldDuration = holdDuration;

            m_FillOnHoldImage.fillAmount = 0.0f;
            m_HeldTime = 0.0f;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}