using System.Collections.Generic;
using UnityEngine;

namespace PV.Interaction
{
    public enum PlatformType
    {
        None = -1,
        Desktop = 0,
        Console = 1
    }

    [System.Serializable]
    public struct InteractionSetting
    {
        public enum InteractionType
        {
            None = 0,
            Interact = 1,
            PickUp
        }

        public InteractionType Type;
        public Sprite ButtonSprite;
        public string Content;

        public InteractionSetting(InteractionType type, Sprite buttonSprite, string content)
        {
            Type = type;
            ButtonSprite = buttonSprite;
            Content = content;
        }
    }

    [CreateAssetMenu(fileName = "Interaction Settings", menuName = "Interaction System/InteractionSettings", order = 1)]
    public class InteractionSettings : ScriptableObject
    {
        public PlatformType Type;
        public List<InteractionSetting> Settings;
    }
}


