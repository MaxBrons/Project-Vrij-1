using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PV
{
    public class Door : Interaction.Interactable
    {
        [SerializeField] private Animator m_Animator;

        protected override void OnInteraction(bool success)
        {
            m_Animator.SetBool("Open", success);
        }
    }
}
