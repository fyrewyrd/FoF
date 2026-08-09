public partial struct Skill
{
    public CastingCost costs
    {
        get
        {
            if (!data) return new CastingCost();
            else if (data is ActiveSkill) return ((data as ActiveSkill).costs);
            else return new CastingCost();
        }

    }
}
