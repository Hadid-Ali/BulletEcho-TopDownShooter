using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesRegistry : MonobehaviourSingleton<AbilitiesRegistry>
{
    [SerializeField] private List<PlayerAbilityModelObject> m_Abilities;

    public List<PlayerAbilityModelObject> Abilities => m_Abilities;

    public PlayerAbilityModelObject GetAbility(AbilityName abilityName) =>
        m_Abilities.Find(ability => ability.AbilityName == abilityName);
}
