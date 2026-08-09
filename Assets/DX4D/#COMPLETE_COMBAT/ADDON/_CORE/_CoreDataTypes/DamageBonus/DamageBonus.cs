using System;
using UnityEngine;

//public partial class CombatStats : NetworkBehaviour
//{
    // -- B O N U S E S --

    // D A M A G E  M E T H O D  B O N U S E S
    [Serializable] public struct DamageBonus
    {
        [Header("Bonuses")]
        [SerializeField] public LinearInt physical;
        [SerializeField] public LinearInt spell;
        [SerializeField] public LinearInt blood;
        [SerializeField] public LinearInt spirit;
        [SerializeField] public LinearInt fury;
        [SerializeField] public LinearInt stamina;
        [SerializeField] public LinearInt poison;
        [SerializeField] public LinearInt mana;

        public DamageBonus(int defaultValue)
        {
            physical = new LinearInt { baseValue = defaultValue };
            spell = new LinearInt { baseValue = defaultValue };
            blood = new LinearInt { baseValue = defaultValue };
            spirit = new LinearInt { baseValue = defaultValue };
            fury = new LinearInt { baseValue = defaultValue };
            stamina = new LinearInt { baseValue = defaultValue };
            poison = new LinearInt { baseValue = defaultValue };
            mana = new LinearInt { baseValue = defaultValue };
        }
    }
    // E L E M E N T A L  B O N U S E S
    [Serializable] public struct ElementalBonus
    {
        [Header("Bonuses")]
        [SerializeField] public LinearInt fire;
        [SerializeField] public LinearInt ice;
        [SerializeField] public LinearInt lightning;
        [SerializeField] public LinearInt water;
        [SerializeField] public LinearInt wind;
        [SerializeField] public LinearInt earth;
        [SerializeField] public LinearInt arcane;
        [SerializeField] public LinearInt holy;
		[SerializeField] public LinearInt ancient;
		[SerializeField] public LinearInt spirit;
		[SerializeField] public LinearInt runic;

        public ElementalBonus(int defaultValue)
        {
            fire = new LinearInt { baseValue = defaultValue };
            ice = new LinearInt { baseValue = defaultValue };
            lightning = new LinearInt { baseValue = defaultValue };
            water = new LinearInt { baseValue = defaultValue };
            wind = new LinearInt { baseValue = defaultValue };
            earth = new LinearInt { baseValue = defaultValue };
            arcane = new LinearInt { baseValue = defaultValue };
            holy = new LinearInt { baseValue = defaultValue };
			ancient = new LinearInt { baseValue = defaultValue };
			spirit = new LinearInt { baseValue = defaultValue };
			runic = new LinearInt { baseValue = defaultValue };
        }
    }
//}
