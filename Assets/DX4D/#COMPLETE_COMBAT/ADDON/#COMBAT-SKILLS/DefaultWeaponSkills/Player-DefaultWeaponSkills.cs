#define prefetch // default:enabled - Preload default skills when this component is loaded
#define hotload // default:enabled - Hotload default skills when they are accessed
using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    public virtual void LoadCharacterDefaults() { }
}
public partial class PlayerCharacter : CharacterSheet
{
    [Header("DEFAULT WEAPON SKILLS")]
    [SerializeField] public ScriptableSkill defaultMainWeaponSkill;
    [SerializeField] public ScriptableSkill defaultOffhandWeaponSkill;

    private void OnValidate() //TODO: Eval this - This is the original way that worked but showed a warning
    //private void Start()
    {
        if (!defaultMainWeaponSkill) { defaultMainWeaponSkill = Resources.Load<ScriptableSkill>("SKILLS/Attack"); }
        if (!defaultOffhandWeaponSkill) { defaultOffhandWeaponSkill = Resources.Load<ScriptableSkill>("SKILLS/Offhand Attack"); }
    }

    public override void LoadCharacterDefaults()
    {
        //LoadCharacterSheet(); //TODO: Add conditional define #CHARACTER_SHEET
        LoadDefaultWeaponSkills(); //after skills loaded from character sheet
    }

    public virtual void LoadDefaultWeaponSkills()
    {
        //DEFAULT SKILLS
        if (!defaultMainWeaponSkill && skills.startingSkills.Count >= 1) defaultMainWeaponSkill = skills.startingSkills[0];
        if (!defaultOffhandWeaponSkill && skills.startingSkills.Count >= 2) defaultOffhandWeaponSkill = skills.startingSkills[1];
        //LoadCharacterSheet();
    }
}
            /* //DEPRECIATED
            for (int i = 0; i < equipmentInfo.Length; ++i)
            {
                if (characterSheet.gearLocationInfo.Length > i)
                {
                    if (characterSheet.gearLocationInfo[i].requiredCategory != string.Empty) equipmentInfo[i].requiredCategory = characterSheet.gearLocationInfo[i].requiredCategory;
                    if (characterSheet.gearLocationInfo[i].location != null) equipmentInfo[i].location = characterSheet.gearLocationInfo[i].location;

                    if (characterSheet.gearLocationInfo[i].defaultItem != null && characterSheet.gearLocationInfo[i].defaultItemAmount > 0)
                    {
                        equipmentInfo[i].defaultItem = characterSheet.gearLocationInfo[i].defaultItem;
                        equipmentInfo[i].defaultItemAmount = characterSheet.gearLocationInfo[i].defaultItemAmount;
                    }
                }
            }
            */

            //characterSheetLog.Append("\n{" + name.ToUpper() + "} <b>EQUIPMENT</b> \n" + equipmentInfo.ToString() + "\n<b>STARTING GOLD</b> " + gold.ToString()); //DEBUG
    /* //VIS2K - Default Equipment Slots
    new EquipmentInfo{requiredCategory="Weapon", location=null, defaultItem=null},
        new EquipmentInfo{requiredCategory="Head", location=null, defaultItem=null},
        new EquipmentInfo{requiredCategory="Chest", location=null, defaultItem=null},
        new EquipmentInfo{requiredCategory="Legs", location=null, defaultItem=null},
        new EquipmentInfo{requiredCategory="Shield", location=null, defaultItem=null},
        new EquipmentInfo{requiredCategory="Shoulders", location=null, defaultItem=null},
        new EquipmentInfo{requiredCategory="Hands", location=null, defaultItem=null},
        new EquipmentInfo{requiredCategory="Feet", location=null, defaultItem=null}
        */
