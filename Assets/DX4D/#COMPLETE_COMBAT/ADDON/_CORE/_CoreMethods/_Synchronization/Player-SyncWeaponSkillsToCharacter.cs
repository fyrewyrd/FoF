using Mirror;
using UnityEngine;

public partial class PlayerCharacter : CharacterSheet
{
    [ClientRpc] void RpcSetSkillBarSlot(int slotNumber, string slotName)
    {
        if (slotNumber > -1) AssignSkillBarSlot(slotNumber, slotName);
    }
    [Server] public void SyncWeapon(CombatWeapon weapon) { LoadWeaponSkills(weapon); LoadAmmoSkills(weapon); }

    [Server] public void LoadWeaponSkills(CombatWeapon weapon)
    {
        #region DEBUG
#if UNITY_EDITOR
        System.Text.StringBuilder log = new System.Text.StringBuilder("<b>| <color=blue>W E A P O N  S K I L L S  S Y N C</color> |</b>"); //DEBUG
        log.Append(" - [" + netId.ToString() + "]\n <b>" + name.ToUpper() + "</b>"); //DEBUG
                                                                                     //#endif
                                                                                     //#if UNITY_EDITOR
        log.Append("\n <b>-" + weapon.weaponCategory.ToString() + "-</b>");
        log.Append("\n    |" + weapon.name + "|"); //DEBUG
#endif
        #endregion

        //WEAPON SKILLS
        if (weapon.weaponSkills != null && weapon.weaponSkills.Count > 0)
        {
            #region DEBUG
#if UNITY_EDITOR
            log.Append(" " + weapon.weaponSkills.Count + " skill" + (weapon.weaponSkills.Count != 1 ? "s" : "")); //DEBUG
#endif
            #endregion

            for (int i = 0; i < weapon.weaponSkills.Count; i++)
            {
                if (SKILLS.Count > i)
                {
                    if (SKILLS[i].name != weapon.weaponSkills[i].name)
                    {
                        AssignSkillSlot(i, new Skill(weapon.weaponSkills[i]));
                        //RpcSetSkillBarSlot(i, weapon.weaponSkills[i].name);
                        //AssignSkillBarSlot(0, weapon.weaponSkills[i].name);

                        #region DEBUG
#if UNITY_EDITOR
                        log.Append("\n        <b>|" + weapon.weaponSkills[i].name + "|</b>[" + i + "]"); //DEBUG
#endif
                        #endregion
                    }
                }
            }
        }
        else
        {
            if (defaultMainWeaponSkill)
            {
                if (SKILLS[0].name != defaultMainWeaponSkill.name)
                {
                    AssignSkillSlot(0, new Skill(defaultMainWeaponSkill));
                    //RpcSetSkillBarSlot(0, "Attack");
                    //AssignSkillBarSlot(0, "Attack");
                //skillbar[0].reference = "Attack";
                    #region DEBUG
#if UNITY_EDITOR
                log.Append("\n        <b>|" + defaultMainWeaponSkill.name + "|</b>[" + 0 + "]"); //DEBUG
#endif
                #endregion
                }
            }
        }

#if UNITY_EDITOR
        Debug.Log(log.ToString()); //DEBUG
#endif
    }

    [Server]
    public void LoadAmmoSkills(CombatWeapon weapon)
    {
        //AMMO ATTACHED SKILL
        if (weapon.requiresAmmo && weapon.HasAmmo && weapon.ammunition[0].attachedSkill != null)
        {
            AssignSkillSlot(0, new Skill(weapon.ammunition[0].attachedSkill));
            #region DEBUG
#if UNITY_EDITOR
            Debug.Log(name.ToUpper() + " - SYNCING AMMUNITION SKILLS - \n" + weapon.name.ToUpper() + "[" + weapon.ammunition[0].name.ToUpper() + "]"); //DEBUG
#endif
            #endregion
            //RpcSetSkillBarSlot(0, weapon.ammunition[0].attachedSkill.name);
            //AssignSkillBarSlot(0, weapon.ammunition[0].attachedSkill.name); //DEPRECIATED
        }
    }

    [Server]
    public void SyncWeaponSkillsToCharacter()
    {
        //SIEGE WEAPON
        if (siegeWeapon != null)
        {
            SyncWeapon(siegeWeapon);
            #region DEBUG
#if UNITY_EDITOR
            Debug.Log(name.ToUpper() + " - SYNCING SIEGE WEAPON SKILLS - \n" + siegeWeapon.name.ToUpper()); //DEBUG
#endif
            #endregion
        }
        //MAIN WEAPON
        else if (mainWeapon != null && mainWeapon.weaponCategory != WeaponCategory.UnArmed)
        {
            SyncWeapon(mainWeapon);
            #region DEBUG
#if UNITY_EDITOR
            Debug.Log(name.ToUpper() + " - SYNCING MAIN WEAPON SKILLS - \n" + mainWeapon.name.ToUpper()); //DEBUG
#endif
            #endregion
        }
        else if (defaultMainWeaponSkill != null)
        {
            AssignSkillSlot(0, new Skill(defaultMainWeaponSkill));
            #region DEBUG
#if UNITY_EDITOR
            Debug.Log(name.ToUpper() + " - SYNCING DEFAULT MAIN WEAPON SKILL - \n" + defaultMainWeaponSkill.name.ToUpper()); //DEBUG
#endif
            #endregion
            //RpcSetSkillBarSlot(0, defaultMainWeaponSkill.name);
            //AssignSkillBarSlot(0, defaultMainWeaponSkill.name); //DEPRECIATED
        }

        //OFFHAND WEAPON
        const int offhandOffset = 1;
        if (IsDualWielding)
        {
            if (offhandWeapon != null && offhandWeapon.weaponCategory != WeaponCategory.UnArmed)
            {
                SyncWeapon(offhandWeapon);
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log(name.ToUpper() + " - SYNCING OFFHAND WEAPON SKILLS - \n" + offhandWeapon.name.ToUpper()); //DEBUG
#endif
                #endregion
            }
            else if (defaultOffhandWeaponSkill != null)
            {
                AssignSkillSlot(offhandOffset, new Skill(defaultOffhandWeaponSkill));
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log(name.ToUpper() + " - SYNCING DEFAULT OFFHAND WEAPON SKILL - \n" + defaultOffhandWeaponSkill.name.ToUpper()); //DEBUG
#endif
                #endregion
                //RpcSetSkillBarSlot(offhandOffset, defaultOffhandWeaponSkill.name);
                //AssignSkillBarSlot(offhandOffset, defaultOffhandWeaponSkill.name); //DEPRECIATED
            }
        }

        //UNARMED WEAPON
        if (IsUnarmed)
        {
            if (unarmedWeapon != null)
            {
                SyncWeapon(unarmedWeapon);
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log(name.ToUpper() + " - SYNCING UNARMED WEAPON SKILLS - \n" + unarmedWeapon.name.ToUpper()); //DEBUG
#endif
                #endregion
            }
            else //STANDARD UMMORPG ITEM
            {
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log(name.ToUpper() + " - SYNCING WEAPON SKILLS - \n" + "NON DX4D WEAPON"); //DEBUG
#endif
                #endregion
            }
        }
    }
}
/*
{
#if UNITY_EDITOR
    log.Append("\n <b>-MAIN-</b>");
    log.Append("\n    |" + mainWeapon.name + "|"); //DEBUG
#endif

    if (mainWeapon.weaponSkills != null && mainWeapon.weaponSkills.Count > 0)
    {
#if UNITY_EDITOR
        log.Append(" " + mainWeapon.weaponSkills.Count + " skill" + (mainWeapon.weaponSkills.Count != 1 ? "s" : "")); //DEBUG
#endif

        for (int i = 0; i < mainWeapon.weaponSkills.Count; i++)
        {
            //if (skills[i].name != mainWeapon.weaponSkills[i].name)
            //{
                AssignSkillSlot(i, new Skill(mainWeapon.weaponSkills[i]));
                AssignSkillBarSlot(i, mainWeapon.weaponSkills[i].name);
            //}

#if UNITY_EDITOR
            log.Append("\n        <b>|" + mainWeapon.weaponSkills[i].name + "|</b>[" + i + "]"); //DEBUG
#endif
        }
    }

    //AMMO ATTACHED SKILL
    if (mainWeapon.requiresAmmo && mainWeapon.HasAmmo && mainWeapon.ammunition[0].attachedSkill != null)
    {
        AssignSkillSlot(0, new Skill(mainWeapon.ammunition[0].attachedSkill));
        AssignSkillBarSlot(0, mainWeapon.ammunition[0].attachedSkill.name);
    }
}
else if (defaultMainWeaponSkill != null)
{
    AssignSkillSlot(0, new Skill(defaultMainWeaponSkill));
    AssignSkillBarSlot(0, defaultMainWeaponSkill.name);
}
*/
/*
if (mainWeapon.weaponSkills != null && mainWeapon.weaponSkills.Count > 0)
{
    log.Append(" " + mainWeapon.weaponSkills.Count + " skill" + (mainWeapon.weaponSkills.Count != 1 ? "s" : "")); //DEBUG

    if (mainWeaponSkills == null) mainWeaponSkills = new SyncListSkill();

    for (int i = 0; i < mainWeapon.weaponSkills.Count; i++)
    {
        if (mainWeaponSkills.Count > i)
        {
            if (mainWeaponSkills[i].GetHashCode() != mainWeapon.weaponSkills[i].GetHashCode())
            {
                AssignMainWeaponSkillSlot(i, new Skill(mainWeapon.weaponSkills[i]));
                AssignMainWeaponSkillBarSlot(i, mainWeapon.weaponSkills[i].name);
            }

            log.Append("\n        <b>|" + mainWeaponSkills[i].name + "|</b>[" + i + "]"); //DEBUG
        }
    }
}
*/
/*
//OFFHAND WEAPON
const int offhandOffset = 1;

if (IsDualWielding && offhandWeapon != null && offhandWeapon.weaponCategory != WeaponCategory.Hand)
{
#if UNITY_EDITOR
    log.Append("\n <b>-OFFHAND-</b>");
    log.Append("\n    |" + offhandWeapon.name + "|"); //DEBUG
#endif
    if (offhandWeapon.weaponSkills != null && offhandWeapon.weaponSkills.Count > 0)
    {
#if UNITY_EDITOR
        log.Append(" " + offhandWeapon.weaponSkills.Count + " skill" + (offhandWeapon.weaponSkills.Count != 1 ? "s" : "")); //DEBUG
#endif
        for (int i = 0; i < offhandWeapon.weaponSkills.Count; i++)
        {
            //if (skills.Count <= (offhandOffset + i) && skills[offhandOffset + i].data.name == offhandWeapon.weaponSkills[i].name)
            //{
                //NONE
            //}
            //else
            //{
                AssignSkillSlot(offhandOffset + i, new Skill(offhandWeapon.weaponSkills[i]), "(offhand) ");
                AssignSkillBarSlot(offhandOffset + i, offhandWeapon.weaponSkills[i].name);
            //}

#if UNITY_EDITOR
            log.Append("\n        <b>|" + offhandWeapon.weaponSkills[i].name + "|</b>[" + (offhandOffset + i) + "]"); //DEBUG
#endif
        }
    }
}
else if (defaultOffhandWeaponSkill != null)
{
    AssignSkillSlot(offhandOffset, new Skill(defaultOffhandWeaponSkill) );
    AssignSkillBarSlot(offhandOffset, defaultOffhandWeaponSkill.name);
}
*/
/*
if (offhandWeapon.weaponSkills != null && offhandWeapon.weaponSkills.Count > 0)
{
    log.Append(" " + offhandWeapon.weaponSkills.Count + " skill" + (offhandWeapon.weaponSkills.Count != 1 ? "s" : "")); //DEBUG

    if (offhandWeaponSkills == null) offhandWeaponSkills = new SyncListSkill();

    for (int i = 0; i < offhandWeapon.weaponSkills.Count; i++)
    {
        if (offhandWeaponSkills.Count > i)
        {
            if (offhandWeaponSkills[i].name != offhandWeapon.weaponSkills[i].name)
            {
                AssignOffhandWeaponSkillSlot(i, new Skill(offhandWeapon.weaponSkills[i]));
                AssignOffhandWeaponSkillBarSlot(i, offhandWeapon.weaponSkills[i].name);
            }

            log.Append("\n        <b>|" + offhandWeaponSkills[i].name + "|</b>[" + i + "]"); //DEBUG
        }
    }
}
*/
/*
//UNARMED WEAPON
if (IsUnarmed)
{
    if (unarmedWeapon != null)
    {
#if UNITY_EDITOR
        log.Append("\n <b>-UNARMED-</b>");
        log.Append("\n    |" + unarmedWeapon.name + "|"); //DEBUG
#endif
        if (unarmedWeapon.weaponSkills != null && unarmedWeapon.weaponSkills.Count > 0)
        {
#if UNITY_EDITOR
            log.Append(" " + unarmedWeapon.weaponSkills.Count + " skill" + (unarmedWeapon.weaponSkills.Count != 1 ? "s" : "")); //DEBUG
#endif
            for (int i = 0; i < unarmedWeapon.weaponSkills.Count; i++)
            {
                //if (skills[i].name != unarmedWeapon.weaponSkills[i].name)
                //{
                    AssignSkillSlot(i, new Skill(unarmedWeapon.weaponSkills[i]));
                    AssignSkillBarSlot(i, unarmedWeapon.weaponSkills[i].name);
                //}

#if UNITY_EDITOR
                log.Append("\n        <b>|" + unarmedWeapon.weaponSkills[i].name + "|</b>[" + i + "]"); //DEBUG
#endif
            }
        }
    }
    else //STANDARD UMMORPG ITEM
    {
#if UNITY_EDITOR
        log.Append("\n STANDARD UMMORPG WEAPON"); //DEBUG
#endif
    }
}
*/
