public abstract partial class Entity// : Mirror.NetworkBehaviour
{
    public PlayerCharacter player
    {
        get { return character.gameObject.GetComponent<PlayerCharacter>(); }
        //set { character.player = value; }
    }
}