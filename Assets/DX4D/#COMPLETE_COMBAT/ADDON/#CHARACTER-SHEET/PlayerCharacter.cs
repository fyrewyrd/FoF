using Mirror;
using UnityEngine;
using System.Collections.Generic;

#if RPG2D
[RequireComponent(typeof(NetworkNavMeshAgentRubberbanding2D))]
#else
[RequireComponent(typeof(NetworkNavMeshAgentRubberbanding))]
#endif

[System.Serializable] public partial class PlayerCharacter : CharacterSheet
{
    [Header("RUBBERBANDING")]
#if RPG2D
    public NetworkNavMeshAgentRubberbanding2D rubberbanding;
#else
    [SerializeField] private NetworkNavMeshAgentRubberbanding _rubberbanding;
    public NetworkNavMeshAgentRubberbanding rubberbanding
    {
        get
        {
            if (!_rubberbanding) _rubberbanding = GetComponent<NetworkNavMeshAgentRubberbanding>();
            return _rubberbanding;
        }
        set { _rubberbanding = value; }
    }
#endif

    // cached SkinnedMeshRenderer bones without equipment, by name
    Dictionary<string, Transform> skinAndBones = new Dictionary<string, Transform>();
    
    [Header("SKILLBAR")]
    public SkillbarEntry[] skillbar = {
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha1},
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha2},
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha3},
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha4},
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha5},
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha6},
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha7},
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha8},
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha9},
        new SkillbarEntry{reference="", hotKey=KeyCode.Alpha0},
    };


    // A N I M A T I O N

    // equipment ///////////////////////////////////////////////////////////////
    void OnEquipmentChanged(SyncListItemSlot.Operation op, int index, ItemSlot slot)
    {
        // update the model
        { RefreshLocation(index); } SyncMyEquipment();
    }

    bool CanReplaceAllBones(SkinnedMeshRenderer equipmentSkin)
    {
        // are all equipment SkinnedMeshRenderer bones in the player bones?
        // (avoid Linq because it is HEAVY(!) on GC and performance)
        foreach (Transform bone in equipmentSkin.bones)
            if (!skinAndBones.ContainsKey(bone.name))
                return false;
        return true;
    }

    // replace all equipment SkinnedMeshRenderer bones with the original player
    // bones so that the equipment animation works with IK too
    // (make sure to check CanReplaceAllBones before)
    void ReplaceAllBones(SkinnedMeshRenderer equipmentSkin)
    {
        // get equipment bones
        Transform[] bones = equipmentSkin.bones;

        // replace each one
        for (int i = 0; i < bones.Length; ++i)
        {
            string boneName = bones[i].name;
            if (!skinAndBones.TryGetValue(boneName, out bones[i]))
                Debug.LogWarning(equipmentSkin.name + " bone " + boneName + " not found in original player bones. Make sure to check CanReplaceAllBones before.");
        }

        // reassign bones
        equipmentSkin.bones = bones;
    }

    void RebindAnimators()
    {
        foreach (Animator anim in GetComponentsInChildren<Animator>())
            anim.Rebind();
    }

    //TODO: Move to Gear
    public void RefreshLocation(int index)
    {
#if RPG2D
        return;
#else
        ItemSlot slot = EQUIPMENT[index];
        EquipmentInfo info = gear.slots[index];

        // valid category and valid location? otherwise don't bother
        if (info.requiredCategory != "" && info.location != null)
        {
            // clear previous one in any case (when overwriting or clearing)
            if (info.location.childCount > 0) Destroy(info.location.GetChild(0).gameObject);

            //  valid item?
            if (slot.amount > 0)
            {
                // has a model? then set it
                EquipmentItem itemData = (EquipmentItem)slot.item.data;
                if (itemData.showWeaponModel && itemData.modelPrefab != null)
                {
                    // load the model
                    GameObject go = Instantiate(itemData.modelPrefab); go.transform.position += itemData.weaponModelOffset;
                    go.name = itemData.modelPrefab.name; // avoid "(Clone)"
                    go.transform.SetParent(info.location, false);
                    // skinned mesh and all bones can be be replaced?
                    // then replace all. this way the equipment can follow IK
                    // too (if any).
                    // => this is the RECOMMENDED method for animated equipment.
                    //    name all equipment bones the same as player bones and
                    //    everything will work perfectly
                    // => this is the ONLY way for equipment to follow IK, e.g.
                    //    in games where arms aim up/down.
                    // NOTE: uMMORPG doesn't use IK at the moment, but it might
                    //       need this later.
                    SkinnedMeshRenderer equipmentSkin = go.GetComponentInChildren<SkinnedMeshRenderer>();
                    if (equipmentSkin != null && CanReplaceAllBones(equipmentSkin))
                        ReplaceAllBones(equipmentSkin);

                    // animator? then replace controller to follow player's
                    // animations
                    // => this is the ALTERNATIVE method for animated equipment.
                    //    add the Animator and use the player's avatar. works
                    //    for animated pants, etc. but not for IK.
                    // => this is NECESSARY for 'external' equipment like wings,
                    //    staffs, etc. that should be animated but don't contain
                    //    the same bones as the player.
                    Animator anim = go.GetComponent<Animator>();
                    if (anim != null)
                    {
                        // assign main animation controller to it
                        anim.runtimeAnimatorController = animator.runtimeAnimatorController;

                        // restart all animators, so that skinned mesh equipment will be
                        // in sync with the main animation
                        RebindAnimators();
                    }
                }
            }
        }
#endif
    }
    [Command]
    public void CmdSetTarget(GameObject ni)
    {
        // validate
        if (ni != null)
        {
            // can directly change it, or change it after casting?
            if (state == ActiveState.IDLE || state == ActiveState.MOVING || state == ActiveState.STUNNED)
                target = ni.GetComponent<CharacterSheet>();
            else if (state == ActiveState.CASTING)
                nextTarget = ni.GetComponent<CharacterSheet>();
        }
    }
}