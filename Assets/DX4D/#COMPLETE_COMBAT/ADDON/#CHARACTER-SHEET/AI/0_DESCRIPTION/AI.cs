#define automatic_components
#define add_missing_components
using UnityEngine;

[DisallowMultipleComponent]
[System.Serializable] public partial class AI : Mirror.NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField][TextArea(1,2)] string componentDescription = 
            "The AI module controls a character's actions.";
#endif
    #endregion

#if automatic_components
    private void OnValidate()
    {
        if (!_character) _character = GetComponent<PlayerCharacter>();
        if (!_character) _character = GetComponent<CharacterSheet>();
    }
#endif

    [Header(" [ CHARACTER SHEET ] ")]
    [Tooltip("Add the character sheet component to your character, then link it here.")]
    [SerializeField] CharacterSheet _character;
    public CharacterSheet my { get { return character; } set { _character = value; } } //alias
    public CharacterSheet character
    {
        get
        {
#if automatic_components
            //FIND
            if (_character == null) _character = gameObject.GetComponent<PlayerCharacter>(); //player
            //if (_character == null) _character = gameObject.GetComponent<AI>(); //enemy
            if (_character == null) _character = gameObject.GetComponent<CharacterSheet>(); //npc
#endif

#if add_missing_components
            //ADD
            if (_character == null && gameObject.GetComponent<Player>()) 
            {
                _character = gameObject.AddComponent<PlayerCharacter>(); //add player
#if UNITY_EDITOR
                Debug.Log(_character.name + ":PLAYERCHARACTER" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            if (_character == null)
            {
                _character = gameObject.AddComponent<CharacterSheet>(); //add character sheet
#if UNITY_EDITOR
                Debug.Log(_character.name + ":CHARACTERSHEET" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
#endif
            return _character;
        }
        set { _character = value; }
    }
}