using Mirror;

public partial class PlayerCharacter : CharacterSheet
{
    [Server] public void AssignSkillSlot(int slotNumber, Skill skill, string prefixLabel = "")
    {
        if (slotNumber > -1 && skill.data != null)
        {
            //while (slotNumber >= skills.Count)
            //{
            //    skills.Add(new Skill(skill.data)); //FILL SKILL LIST
            //}

            //if (slotNumber >= skills.Count && !skills.Contains(skill)) skills.Add(skill);
            if (slotNumber < SKILLS.Count)
            {
                if (SKILLS[slotNumber].data == null || SKILLS[slotNumber].data.name != prefixLabel + skill.name)
                {
                    //string prefixedName = prefixLabel + skill.name;
                    //skill.data.name = prefixedName;
                    for (int i = 0; i < SKILLS.Count; i++)
                    {
                        if (SKILLS[i].name == skill.name) return;
                    }

                    SKILLS[slotNumber] = new Skill(skill.data); //SET THE SKILL
                    //skills[slotNumber].data.name = prefixedName;
                }
                //DO NOT REASSIGN A SLOT A SECOND TIME OR IT WILL RESET COOLDOWNS
            }
            else
            {
                SKILLS.Add(skill);
            }
        }
    }

    //[Server]
    [Client]
    public void AssignSkillBarSlot(int slotNumber, string skillName)
    {
        if (slotNumber > -1 && slotNumber < skillbar.Length) skillbar[slotNumber].reference = skillName; //ASSIGN TO SKILLBAR SLOT
    }
}
