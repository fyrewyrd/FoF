using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    [Header("DOMINANT HAND")]
    [Tooltip("The hand this character attacks with. Right handed if unchecked.")]
    [SerializeField] public bool leftHanded = false;

    [Header("PLAYER VISUAL FX")]
    //left
    [SerializeField] Transform _leftHandEffectMount = null;
    public virtual Transform leftHandEffectMount
    {
        get
        {
            if (!_leftHandEffectMount) { _leftHandEffectMount = transform; }
            return _leftHandEffectMount;
        }
        set
        {
            _leftHandEffectMount = value;
        }
    }
    [SerializeField] public GameObject leftHandEffect;
    //right
    [SerializeField] Transform _rightHandEffectMount = null;
    public virtual Transform rightHandEffectMount
    {
        get
        {
            if (!_rightHandEffectMount) { _rightHandEffectMount = transform; }
            return _rightHandEffectMount;
        }
        set
        {
            _rightHandEffectMount = value;
        }
    }
    [SerializeField] public GameObject rightHandEffect;

    public virtual Transform launchOrigin
    {
        get
        {
            if (leftHanded) { return leftHandEffectMount; } //LEFT HANDED
            else { return rightHandEffectMount; } //RIGHT HANDED
        }
    }
}
