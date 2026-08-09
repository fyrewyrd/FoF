using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    //DAMAGE
    //[ClientRpc] public void RpcShowDamagePopup(int amount, DefensiveState defensiveState, MethodOfDamage method, Element element)
    [ClientRpc] public void RpcShowDamagePopup(int amount, DefensiveState defensiveState, MethodOfDamage method, Element element)
    {
        string textPrefix = string.Empty;
        string textBody = string.Empty;
        string textSuffix = string.Empty;

        bool zeroDamage = false;
        //ScriptedTextStyle textStyle = text.damageTextStyle;

        //METHOD OF DAMAGE
        switch (method)
        {
            case MethodOfDamage.Physical: textSuffix = string.Empty; break;
            case MethodOfDamage.Magic: textSuffix = string.Empty; break;
            case MethodOfDamage.NoDamage: textSuffix = string.Empty; break;

            case MethodOfDamage.Blood: textSuffix = " blood"; break;
            case MethodOfDamage.Spirit: textSuffix = " spirit"; break;
            case MethodOfDamage.Poison: textSuffix = " poison"; break;
            case MethodOfDamage.Mana: textSuffix = " mana"; break;
            case MethodOfDamage.Fury: textSuffix = " fury"; break;
            case MethodOfDamage.Stamina: textSuffix = " stamina"; break;
                //default: textSuffix = ""; break;
        }
        switch (element)
        {
            case Element.Neutral: text.damageTextStyle.color = Config.Text.Element.neutralColor; break;
            case Element.Fire: text.damageTextStyle.color = Config.Text.Element.fireColor; break;
            case Element.Ice: text.damageTextStyle.color = Config.Text.Element.iceColor; break;
            case Element.Lightning: text.damageTextStyle.color = Config.Text.Element.lightningColor; break;
            case Element.Water: text.damageTextStyle.color = Config.Text.Element.waterColor; break;
            case Element.Wind: text.damageTextStyle.color = Config.Text.Element.windColor; break;
            case Element.Earth: text.damageTextStyle.color = Config.Text.Element.earthColor; break;
            case Element.Arcane: text.damageTextStyle.color = Config.Text.Element.arcaneColor; break;
            case Element.Holy: text.damageTextStyle.color = Config.Text.Element.holyColor; break;
			case Element.Ancient: text.damageTextStyle.color = Config.Text.Element.ancientColor; break;
			case Element.Spirit: text.damageTextStyle.color = Config.Text.Element.spiritColor; break;
			case Element.Runic: text.damageTextStyle.color = Config.Text.Element.runicColor; break;
                //default: textStyle.color = Config.Text.Element.neutralColor; break;
        }
        switch (defensiveState)
        {
            //idle
            case DefensiveState.Idle: textPrefix = "**surprised**\n-"; break; //TODO: FlatFooted? Firstblood?
                                                                              //healing
            case DefensiveState.BeingHealed: textPrefix = "+"; break;
            //damage weakness achilles
            case DefensiveState.TakingDamage: textPrefix = "-"; break;
            case DefensiveState.TakingSpellDamage: textPrefix = "-"; break;
            case DefensiveState.TakingWeakToDamage: textPrefix = "[weakness]\n-"; break;
            case DefensiveState.TakingAchillesHeelDamage: textPrefix = "[deadly]\n-"; break;
            //reflect resist absorb ignore
            case DefensiveState.ReflectingDamage: textPrefix = "[Reflected!]"; zeroDamage = true; break;//textSuffix = string.Empty; break;
            case DefensiveState.AbsorbingDamage: textPrefix = "[Absorbed!]\n+"; text.damageTextStyle.color = text.healTextStyle.color; break;
            case DefensiveState.IgnoringDamage: textPrefix = "[Nullified!]"; zeroDamage = true; break;//textSuffix = string.Empty; break;
            case DefensiveState.ResistingDamage: textPrefix = "[Resisted!]\n-"; break;
            //dodge
            case DefensiveState.DodgingAttack: textPrefix = "[Evaded]"; zeroDamage = true; break;
            case DefensiveState.DodgingSpell: textPrefix = "[Evaded]"; zeroDamage = true; break;
            //block
            case DefensiveState.BlockingAttack: textPrefix = "[Blocked!]\n-"; break;
            case DefensiveState.BlockingSpell: textPrefix = "[Spell Blocked!]\n-"; break;
            //critical
            case DefensiveState.TakingCriticalDamage: textPrefix = "[Crit Taken]\n-"; break;
            case DefensiveState.TakingCriticalSpellDamage: textPrefix = "[Crit Spell Taken]\n-"; break;
            //backstab
            case DefensiveState.TakingBackstabDamage: textPrefix = "[Backstabbed!]\n-"; break;
            case DefensiveState.TakingCriticalBackstabDamage: textPrefix = "[BACKSTABBED!]\n"; break;
            //flank
            case DefensiveState.TakingFlankDamage: textPrefix = "[Flanked]\n-"; break;
            case DefensiveState.TakingCriticalFlankDamage: textPrefix = "[FLANKED!]\n"; break;
			//overwhelm
            case DefensiveState.TakingOverwhelmDamage: textPrefix = "[Overwhelmed]\n-"; break;
            case DefensiveState.TakingCriticalOverwhelmDamage: textPrefix = "[OVERWHELMED!]\n"; break;

                //default: text.popup.textPrefix = "-"; break;
        }

        if (amount <= 0) { textPrefix = textPrefix.Replace('-', ' '); }// textBody = "0"; }

        if (!zeroDamage) textBody = amount.ToString();
        
        HandleTextPopup(transform, (textPrefix + textBody + textSuffix), text.damageTextStyle);// message, popup.style.color, popup.style.size, new Vector3((v.x * loc.x), (v.y * loc.y), (v.z * loc.z)), popup.style.animation.velocity);
    }
}
