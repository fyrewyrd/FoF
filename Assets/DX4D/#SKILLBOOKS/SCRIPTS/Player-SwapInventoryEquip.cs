using Mirror;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Entity
{
    [SerializeField] bool autofillSkillbar = true;

    void Start_LoadItemSkills()
    //void OnStartServer_LoadItemSkills()
    {
        if (equipment == null || equipment.Count <= 0) return;
        foreach (ItemSlot slot in equipment)
        {
            if (slot.amount > 0) LoadItemSkills(slot.item);
        }
    }

    [Server] void LoadItemSkills(Item item)
    {
        //if (item.hash <= 0 || item.data == null || item.data.skillSets.Count <= 0) return;

        //ADD WEAPON SKILLS FIRST
        if (item.data is WeaponItem)
        {
            List<Skill> modifiedSkillList = new List<Skill>();
            foreach (ScriptableSkillSet skillSet in item.data.skillSets)
            {
                foreach (Skill skillData in skillSet.skills)
                {
                    modifiedSkillList.Add(skillData);
                }
            }

            Skill[] skillList = new Skill[skills.Count];
            skills.CopyTo(skillList, 0);

            foreach (Skill skill in skillList)
            {
                modifiedSkillList.Add(skill);
            }

            skills = new SyncListSkill();// .Clear();
            foreach (Skill skill in modifiedSkillList)
            {
                skills.Add(skill);
            }
        }
        else
        {
            //ADD SKILLS
            foreach (ScriptableSkillSet skillSet in item.data.skillSets)
            {
                foreach (Skill skill in skillSet.skills)
                {
                    //ScriptableSkill newSkill = skillData;
                    //if (!skill.data.learnDefault) { skill.data.learnDefault = true; }

                    skills.Add(skill);
                }
            }
        }

        //FILL SKILLBAR
        if (autofillSkillbar)
        {
            for (int i = 0; i < item.data.skillSets.Count; i++)
            {
                if (i < skillbar.Length)
                {
                    skillbar[i].reference = skills[i].name;
                }
            }
        }
        #region DEBUG
#if UNITY_EDITOR
        Debug.Log(name + " equipped " + item.name + ".");
#endif
        #endregion
    }

    [Server] public virtual void OnEquip(Item item)
    {
        LoadItemSkills(item);
    }
    [Server] public virtual void OnUnequip(Item item)
    {
        //REMOVE SKILLS
        foreach (ScriptableSkillSet skillSet in item.data.skillSets)
        {
            foreach (Skill skill in skillSet.skills)
            {
                int removeIndex = GetSkillIndexByName(skill.name);
                if (removeIndex > -1) skills.RemoveAt(removeIndex);
            }
        }
        #region DEBUG
#if UNITY_EDITOR
        Debug.Log(name + " removed " + item.name + ".");
#endif
        #endregion
    }
    
        // swap inventory & equipment slots to equip/unequip. used in multiple places
        /*
    [Server] public void SwapInventoryEquip(int inventoryIndex, int equipmentIndex)
    {
        // validate: make sure that the slots actually exist in the inventory
        // and in the equipment
        if (health > 0 &&
            0 <= inventoryIndex && inventoryIndex < inventory.Count &&
            0 <= equipmentIndex && equipmentIndex < equipment.Count)
        {
            // item slot has to be empty (unequip) or equipable
            ItemSlot slot = inventory[inventoryIndex];
            if (slot.amount == 0 ||
                slot.item.data is EquipmentItem &&
                ((EquipmentItem)slot.item.data).CanEquip(this, inventoryIndex, equipmentIndex))
            {
                // swap them
                ItemSlot temp = equipment[equipmentIndex];
                equipment[equipmentIndex] = slot;
                inventory[inventoryIndex] = temp;

                //TRIGGER EVENTS
                if (slot.amount > 0) OnEquip(slot.item);
                if (temp.amount > 0) OnUnequip(temp.item);
            }
        }
    }
    */
}
