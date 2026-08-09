//#define expanded_currency

[System.Serializable] public class Price
{
    public int gold;
    public int GOLD { get { return gold; } set { gold = value; } }

    public int gems;
    public int GEMS { get { return gems; } set { gems = value; } }
    public int COIN { get { return gems; } set { gems = value; } }
    public int TOKEN { get { return gems; } set { gems = value; } }

    public int experience;
    public int EXP { get { return experience; } set { experience = value; } }

#if expanded_currency //TODO: Alternative currencies
    public int copper;
    public int CP { get { return copper; } set { copper = value; } }

    public int silver;
    public int SP { get { return silver; } set { silver = value; } }

    public int platinum;
    public int PP { get { return platinum; } set { platinum = value; } }

    public int electrum;
    public int EP { get { return electrum; } set { electrum = value; } }
#endif
}
    
    //PET
    //public int petLife; // sacrifice your pet
    //public int petMana; // spend your pet's mana
    //public int petBlood;
    //public int petSpirit;
    //public int petStamina;
    //public int petFury;
    //public int petExperience;
