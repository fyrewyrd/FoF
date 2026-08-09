[System.Serializable] public class CastingCost
{
    public PersonalCost me = new PersonalCost();
    public PersonalCost self { get { return me; } }

    public PersonalCost pet = new PersonalCost();
    //public PersonalCost ally { get { return pet; } }
    //public PersonalCost pets { get { return pet; } }
    public PersonalCost mount = new PersonalCost();
}
