using Mirror;

public partial class PlayerCharacter : CharacterSheet
{
    //SIEGE WEAPON SKILL
    public bool IsCastingSiegeWeaponSkill
    {
        get
        {
            if (HasSiegeWeapon && siegeWeapon.weaponSkills != null)
            {
                for (int i = 0; i < siegeWeapon.weaponSkills.Count; i++)
                {
                    if (IsCastingSkill(i)) return true;
                }
            }

            return false;
        }
    }
    //MAIN WEAPON SKILL
    public bool IsCastingMainWeaponSkill
    {
        get
        {
            if (HasMainWeapon && mainWeapon.weaponSkills != null)
            {
                for (int i = 0; i < mainWeapon.weaponSkills.Count; i++)
                {
                    if (IsCastingSkill(i)) return true;
                }
            }

            return false;
        }
    }
    //OFFHAND WEAPON SKILL
    public bool IsCastingOffhandWeaponSkill
    {
        get
        {
            if (HasOffhandWeapon && offhandWeapon.weaponSkills != null)
            {
                int startingIndex = 0;

                if (HasMainWeapon && mainWeapon.weaponSkills != null)
                {
                    startingIndex = mainWeapon.weaponSkills.Count;
                }

                for (int i = startingIndex; i < (startingIndex + offhandWeapon.weaponSkills.Count); i++)
                {
                    if (IsCastingSkill(startingIndex + i)) return true;
                }
            }

            return false;
        }
    }
    //UNARMED WEAPON SKILL
    public bool IsCastingUnarmedSkill
    {
        get
        {
            if (IsUnarmed && unarmedWeapon != null && unarmedWeapon.weaponSkills != null)
            {
                for (int i = 0; i < unarmedWeapon.weaponSkills.Count; i++)
                {
                    if (IsCastingSkill(i)) return true;
                }
            }

            return false;
        }
    }
}

public partial class CharacterSheet : NetworkBehaviour
{
    // I S  C A S T I N G  S K I L L
    public bool IsCastingSkill(int skillSlotNumber)
    {
        return currentSkill == skillSlotNumber;
        //get { return (!IsNotCastingSkill); }
    }
    public bool IsCastingPrimarySkill
    {
        get { return IsCastingSkill(0); }
    }
    // - - - - - - - - - - - - - - -
    // I S  N O T  C A S T I N G  S K I L L
    public bool IsNotCastingASkill
    {
        get { return currentSkill < 0; }
    }
}
