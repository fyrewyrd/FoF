[System.Serializable]
public class PersonalCost
{
    public int total {
        get { return ES + EB + HP + MP + BP + SP + AP + FP; }
    }

    public int shield;
    public int ES { get { return shield; } set { shield = value; } }
	
	public int barrier;
    public int EB { get { return barrier; } set { barrier = value; } }

    public int life;
    public int HP { get { return life; } set { life = value; } }

    public int mana;
    public int MP { get { return mana; } set { mana = value; } }

    public int blood;
    public int BP { get { return blood; } set { blood = value; } }

    public int spirit;
    public int SP { get { return spirit; } set { spirit = value; } }

    public int stamina;
    public int AP { get { return stamina; } set { stamina = value; } }

    public int fury;
    public int FP { get { return fury; } set { fury = value; } }
}