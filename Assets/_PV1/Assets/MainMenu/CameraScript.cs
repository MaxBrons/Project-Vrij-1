using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Action OnCameraRotated;

    [SerializeField] private float m_Rotation;
    [SerializeField] private float m_RotateDuration;

    private void Start()
    {
        StartRotationCam(1);
    }

    public void StartRotationCam(float direction)
    {
        StartCoroutine(RotateCam(direction));
    }

    public IEnumerator RotateCam(float direction)
    {
        Quaternion camRotation = Camera.main.transform.rotation;
        float time = 0f;
        while (time < m_RotateDuration)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(camRotation, Quaternion.Euler(camRotation.x + m_Rotation * direction, camRotation.y, camRotation.z), time);
            yield return null;
        }
        OnCameraRotated?.Invoke();
    }

    public void ReverseRotateCam()
    {
        StartRotationCam(-1);
    }
}
