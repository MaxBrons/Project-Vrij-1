using PV.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Settings", menuName = "Interaction System/InteractionSettings", order = 1)]
public class InteractionSettings : ScriptableObject
{
    public enum InteractionType
    {
        None = 0,
        Interact = 1,
        PickUp
    }

    [SerializeField] private List<Utilities.SerializedKeyValuePair<InteractionType, string>> m_InteractionSettings = new List<Utilities.SerializedKeyValuePair<InteractionType, string>>();
}

