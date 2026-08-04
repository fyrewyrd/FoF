#define hotload // default:enabled - Hotload default text styles when they are accessed
using Mirror;
using UnityEngine;

public abstract partial class Entity// : NetworkBehaviour
{
    [Header(" [ CHARACTER SHEET ] ")]
    [Tooltip("Add the character sheet component to your character, then link it here.")]
    [SerializeField] CharacterSheet _character;

    public CharacterSheet my { get { return character; } set { _character = value; } } //alias
    public CharacterSheet character
    {
        get
        {
            //FIND COMPONENTS
            if (_character == null) _character = gameObject.GetComponent<PlayerCharacter>(); //player
            //if (_character == null) _character = gameObject.GetComponent<AI>(); //enemy
            if (_character == null) _character = gameObject.GetComponent<CharacterSheet>(); //npc

#if hotload
            //ADD COMPONENTS
            FetchComponents();
#endif

            return _character;
        }
        set { _character = value; }
    }

#if hotload
    public void FetchComponents()
    {
        if (_character == null && gameObject.GetComponent<Player>())
        {
            _character = gameObject.AddComponent<PlayerCharacter>(); //add player
            #region DEBUG
#if UNITY_EDITOR
            Debug.Log("<color=red><b>ISSUE:</b></color> " + _character.name + " - PlayerCharacter" + " Component added during runtime..."
                + "\n" + "<color=red><b>FIX:</b></color> " + "<b>OPEN: <i>Tools>DX4D>QUICKSTART</i></b> to add the component to your scene prefabs"
                + " " + "(Or add them manually)");
#endif
            #endregion
        }
        /*
        if (_character == null && gameObject.GetComponent<Monster>()) 
        {
            _character = gameObject.AddComponent<AI>(); //add enemy
#if UNITY_EDITOR
            Debug.Log(_character.name + ":AI" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
        }
        */
        if (_character == null)
        {
            //if (!gameObject.GetComponent<Npc>()) //DEPRECIATED: (actually, they do...) NPCS DO NOT NEED A CHARACTER SHEET
            //{
                _character = gameObject.AddComponent<CharacterSheet>(); //add character sheet
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log("<color=red><b>ISSUE:</b></color> " + _character.name + " - CharacterSheet" + " Component added during runtime..."
                    + "\n" + "<color=red><b>FIX:</b></color> " + "<b>OPEN: <i>Tools>DX4D>QUICKSTART</i></b> to add the missing components"
                    + " " + "(Or add them manually to your prefabs)");
#endif
                #endregion
            //}
        }
    }
#endif
}