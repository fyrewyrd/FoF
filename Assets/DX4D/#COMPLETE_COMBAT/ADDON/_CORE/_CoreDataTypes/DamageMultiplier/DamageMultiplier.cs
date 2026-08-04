using System;
using UnityEngine;

//public partial class DamageModifier : NetworkBehaviour
//{
    // -- M U L T I P L I E R S --

    // D A M A G E  M E T H O D  A T T R I B U T E  M U L T I P L I E R S
    [Serializable] public struct DamageMultiplier
    {
        [Header("Multipliers")]
        [SerializeField] public LinearFloat physical;// = new LinearFloat { baseValue = 1.0f };
        [SerializeField] public LinearFloat spell;// = new LinearFloat { baseValue = 1.0f };
        [SerializeField] public LinearFloat blood;// = new LinearFloat { baseValue = 1.0f };
        [SerializeField] public LinearFloat spirit;// = new LinearFloat { baseValue = 1.0f };
        [SerializeField] public LinearFloat fury;// = new LinearFloat { baseValue = 1.0f };
        [SerializeField] public LinearFloat stamina;// = new LinearFloat { baseValue = 1.0f };
        [SerializeField] public LinearFloat poison;// = new LinearFloat { baseValue = 1.0f };
        [SerializeField] public LinearFloat mana;// = new LinearFloat { baseValue = 1.0f };

        public DamageMultiplier(float defaultValue)
        {
            physical = new LinearFloat { baseValue = defaultValue };
            spell = new LinearFloat { baseValue = defaultValue };
            blood = new LinearFloat { baseValue = defaultValue };
            spirit = new LinearFloat { baseValue = defaultValue };
            fury = new LinearFloat { baseValue = defaultValue };
            stamina = new LinearFloat { baseValue = defaultValue };
            poison = new LinearFloat { baseValue = defaultValue };
            mana = new LinearFloat { baseValue = defaultValue };
        }
    }
    // E L E M E N T A L  A T T R I B U T E  M U L T I P L I E R S
    [Serializable] public struct ElementalMultiplier
    {
        [Header("Multipliers")]
        [SerializeField] public LinearFloat fire;
        [SerializeField] public LinearFloat ice;
        [SerializeField] public LinearFloat lightning;
        [SerializeField] public LinearFloat water;
        [SerializeField] public LinearFloat wind;
        [SerializeField] public LinearFloat earth;
        [SerializeField] public LinearFloat arcane;
        [SerializeField] public LinearFloat holy;
		[SerializeField] public LinearFloat ancient;
		[SerializeField] public LinearFloat spirit;
		[SerializeField] public LinearFloat runic;

        public ElementalMultiplier(float defaultValue)
        {
            fire = new LinearFloat { baseValue = defaultValue };
            ice = new LinearFloat { baseValue = defaultValue };
            lightning = new LinearFloat { baseValue = defaultValue };
            water = new LinearFloat { baseValue = defaultValue };
            wind = new LinearFloat { baseValue = defaultValue };
            earth = new LinearFloat { baseValue = defaultValue };
            arcane = new LinearFloat { baseValue = defaultValue };
            holy = new LinearFloat { baseValue = defaultValue };
			ancient = new LinearFloat { baseValue = defaultValue };
			spirit = new LinearFloat { baseValue = defaultValue };
			runic = new LinearFloat { baseValue = defaultValue };
        }
    }
//}
