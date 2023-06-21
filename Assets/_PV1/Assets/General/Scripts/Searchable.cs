using PV.Systems.Geiger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PV
{
    public class Searchable : Interaction.Interactable
    {
        [SerializeField] private GasMask m_GasMask;
        private bool m_Searched = false;

        protected override void OnInteraction(bool success)
        {
            if (!success || Random.Range(0, 2) == 0)
                return;

            m_GasMask.Heal(1000.0f);
            m_Searched = true;
        }
    }
}
