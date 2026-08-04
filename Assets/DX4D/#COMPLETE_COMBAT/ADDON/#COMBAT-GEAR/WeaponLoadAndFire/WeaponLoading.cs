using Mirror;
using UnityEngine;

public partial class PlayerCharacter : CharacterSheet
{
    /// <summary>Add ammo to the weapon - returns -1 if reloading failed...
    /// otherwise returns the index in the weapon's ammunition list.
    /// The name of the ammo must contain the weapon's required ammo type</summary>
    /// <returns>The location of the ammo in the magazine/quiver (maxRounds)</returns>
    [Server] public int ReloadWeapon(CombatWeapon weapon, AmmunitionItem ammo)
    {
        //Do we have the required ammo?
        if (ammo.name.ToUpper().Contains(weapon.requiredAmmo.ToString().ToUpper()))
        {
            //Is the weapon full?
            if(weapon.ammunition.Count >= weapon.maxAmmo)
            {
                EjectARound(weapon);
                //UnloadLastRound(weapon);
                //UnloadWeapon(weapon);
                //weapon.ammunition.Clear(); //TODO: Eval this behaviour
                TargetShowTextPopup("unloaded the " + weapon.requiredAmmo.ToString().ToLower());
                /* //DEPRECIATED
                switch (weapon.requiredAmmo)
                {
                    case AmmoCategory.Charge:
                        TargetShowTextPopup("overcharged"); break;
                    case AmmoCategory.Stone:
                        TargetShowTextPopup("dropped a stone"); break;
                    case AmmoCategory.Arrow:
                        TargetShowTextPopup("dropped an arrow"); break;
                    case AmmoCategory.Bullet:
                        TargetShowTextPopup("ejected a round"); break;
                    case AmmoCategory.Fuel:
                        TargetShowTextPopup("drained some fuel"); break;
                    case AmmoCategory.Cannonball:
                        TargetShowTextPopup("dropped a cannonball"); break;
                    default:
                        break;
                }
                */
                //return -1;
            }

            //Add the ammo
            weapon.ammunition.Add(ammo);
            //Make sure the ammo got added
            if (weapon.ammunition.Contains(ammo))
            {
                //RpcShowSpendPopup("loaded " + ammo.name + " into " + weapon.name);
                return (weapon.ammunition.Count - 1);
            }
            else //Somehow the Add method failed...this should not happen
            {
#if UNITY_EDITOR
                Debug.LogError("ENTITY - LOAD AMMO: Something went wrong while trying to load ammunition to a weapon.");
#endif
                TargetShowTextPopup("*jammed*");
                return -1;
            }
        }
        else //not the required ammo type
        {
            TargetShowTextPopup("incorrect ammo type");
            return -1;
        }
    }
    ///Unloads the last round in the ammoRounds list [list.Count-1]
    [Server] public void UnloadWeapon(CombatWeapon weapon)
    {
        for (int i = 0; i < weapon.ammunition.Count; i++)
        {
            //weapon.ammunition[i];
            //Add back to inventory
            if (InventoryAdd(new Item(weapon.ammunition[i]), 1))
            {
                weapon.ammunition.RemoveAt(i);
            }
        }

        weapon.ammunition.Clear();
    }
    ///Unloads the last round in the ammoRounds list [list.Count-1]
    [Server] public void EjectARound(CombatWeapon weapon)
    {
        //TODO: Drop on Ground
        weapon.ammunition.RemoveAt(0);
    }
    ///Unloads the last round in the ammoRounds list [list.Count-1]
    [Server] public void UnloadLastRound(CombatWeapon weapon)
    {
        //TODO: Test in Server
        //if (returnToInventory && InventoryAdd(new Item(weapon.ammunition[weapon.ammunition.Count - 1]), 1))
        //{
        weapon.ammunition.RemoveAt(weapon.ammunition.Count - 1);
        //}
    }
}
