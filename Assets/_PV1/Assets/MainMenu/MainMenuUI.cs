using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Image m_Image;
    [SerializeField] private float m_FadeDuration;
    [SerializeField] private GameObject m_Button;
    [SerializeField] private int m_SceneToLoad;
    [SerializeField] private CameraScript m_CamScript;

    private Color m_FadeColor;

    void Start()
    {
        m_Button.SetActive(false);
        StartFade();
        m_CamScript.OnCameraRotated += SetButtonActive;
    }

    public void StartFade()
    {
        StartCoroutine(Fading(false));
    }

    public void FadeBack()
    {
        StartCoroutine(Fading(true));
    }

    private void SetButtonActive()
    {
        m_Button.SetActive(true);
        m_CamScript.OnCameraRotated -= SetButtonActive;
    }

    private IEnumerator Fading(bool hasFaded)
    {
        m_FadeColor = m_Image.color;
        Color endColor = new Color(m_FadeColor.r, m_FadeColor.g, m_FadeColor.g, !hasFaded ? 0.0f : 1.0f);

        if (hasFaded)
            m_Button.SetActive(false);

        float time = 0f;
        while (time < m_FadeDuration)
        {
            time += Time.deltaTime;
            m_Image.color = Color.Lerp(m_FadeColor, endColor, time);
            yield return null;
        }

        if(hasFaded)
        {
            if (SceneManager.GetSceneAt(m_SceneToLoad).IsUnityNull())
                SceneManager.LoadScene(m_SceneToLoad);
        }

    }
}
