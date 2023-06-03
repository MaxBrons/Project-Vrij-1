using PV.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
    Desktop = 0,
    Console = 1
}

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private List<Utilities.SerializedKeyValuePair<PlatformType, List<InteractionSettings>>> m_Settings = new List<Utilities.SerializedKeyValuePair<PlatformType, List<InteractionSettings>>>();

    //private List<Interactables> m_Interactables = new List<Interactables>();
    private void Start()
    {
        
    }
}

