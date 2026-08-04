using UnityEngine;
using Mirror;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public class FieldWeapon : NetworkBehaviour
{
    [SerializeField] public EquipmentItem weapon;
    /*
    //ENTER COLLIDER
    protected virtual void OnTriggerEnter(Collider col)
    {
        if (!col.isTrigger) return; //NOT A TRIGGER
        CharacterSheet character = col.GetComponent<CharacterSheet>();
        if (!character || !character.player) return; //NOT A PLAYER

        if (character.combat) character.combat.isAttackable = false;

        #region DEBUG
#if UNITY_EDITOR
        Debug.Log("<b>[FIELDWEAPON]</b> " + character.name + " <b>entered</b> " + name);
#endif
        #endregion
    }
    //EXIT COLLIDER
    protected virtual void OnTriggerExit(Collider col)
    {
        if (!col.isTrigger) return; //NOT A TRIGGER

        CharacterSheet character = col.GetComponent<CharacterSheet>();
        if (!character || !character.player) return; //NOT A PLAYER

        if (character.combat) character.combat.isAttackable = true;

        #region DEBUG
#if UNITY_EDITOR
        Debug.Log("<b>[FIELDWEAPON]</b> " + character.name + " <b>exited</b> " + name);
#endif
        #endregion
    }
    */
}
