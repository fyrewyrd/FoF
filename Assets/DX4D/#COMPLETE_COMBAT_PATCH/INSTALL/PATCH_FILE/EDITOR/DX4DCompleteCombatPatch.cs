using UnityEngine;
using UnityEditor;
using DX4D;

#region REGISTER PATCH
namespace DX4D
{
    public partial class PatchManagerWindow : EditorWindow
    {
        private void OnEnable_DX4DCompleteCombatPatch() {
            PatchManager.Register(new DX4DCompleteCombatPatch());
        }
    }
}
#endregion

public class DX4DCompleteCombatPatch : Patch
{
    public DX4DCompleteCombatPatch()
    {
        //UnityEditor.AssetDatabase.Refresh(); //Make sure we are editing the latest files

        // V E R S I O N  I N F O
        version = 2.13f; //NOTE: if (version >= 2.03f) { } <-- use this if you want versioning on patches...we don't need it now, but you might if you add patches here

        // P A T C H  I N F O
        name = "[DX4D]Complete Combat Patch v" + version;
        description = "Patches the ummorpg core to enable this addon to function.\n\nWARNING:\nTHIS PATCH WILL CAUSE A BUNCH OF ERRORS TO POP UP...JUST IGNORE THEM AND INSTALL THE ADDON FROM ITS UNITYPACKAGE";

        //VERSION 2.13 - ummorpg3d 1.193 support added

        //VERSION 2.12 - ummorpg2D support
        
        // Forward Movement parameters to CharacterSheet (For ummorpg 2D)
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Monster.cs",
        "public float moveDistance = 3;",
        "public float moveDistance { get { return my.ai.wanderDistance; } }",
        "public float followDistance = 5;",
        "public float followDistance { get { return my.ai.chaseDistance; } }/*"
        ));

        // Remove Header from property accessors (For ummorpg 2D)
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Npc.cs",
        "public TextMesh questOverlay;",
        "/**/public TextMesh questOverlay = null;"
        ));

        // Remove Header from property accessors (For ummorpg 2D)
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Pet.cs",
        "public TextMesh ownerNameOverlay;",
        "/**/public TextMesh ownerNameOverlay = null;"
        ));

        //SAPLING ANIMATION CONTROLLER
        // Modify Normal Attack Animation
        AddPatch(new PatchData(
        "/uMMORPG/Sprites/Entities/Sapling/",
        "Controller.controller",
        "m_Name: Normal Attack (Sapling)",
        "m_Name: Attack",
        "m_ConditionEvent: Normal Attack (Sapling)",
        "m_ConditionEvent: Attack"
        ));

        // Modify Secondary Attack Animation
        /*AddPatch(new PatchData(
        "/uMMORPG/Sprites/Entities/Sapling/",
        "Controller.controller",
        "m_Name: Secondary Attack (Sapling)",
        "m_Name: Offhand Attack",
        "m_ConditionEvent: Secondary Attack (Sapling)",
        "m_ConditionEvent: Offhand Attack"
        ));*/


        //VERSION 2.11 - Connect AI parameters

        // Forward Movement parameters to CharacterSheet
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Monster.cs",
        "public float moveProbability = 0.1f;",
        "*/public float moveProbability { get { return my.ai.moveProbability; } }",
        "public float moveDistance = 10;",
        "public float moveDistance { get { return my.ai.wanderDistance; } }",
        "public float followDistance = 20;",
        "public float followDistance { get { return my.ai.chaseDistance; } }/*"
        ));

        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Monster.cs",
        "public float attackToMoveRangeRatio = 0.8f;",
        "*/public float attackToMoveRangeRatio { get { return my.ai.optimalRange; } }"
        ));

        //VERSION 2.10 - Navmesh

        // Forward NetworkNavmeshAgent to CharacterSheet
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Monster.cs",
        "public partial class Monster : Entity",
        "public partial class Monster  : Entity {/*",
        "public NetworkNavMeshAgent networkNavMeshAgent;",
        "*/public NetworkNavMeshAgent networkNavMeshAgent { get { return my.networkNavMeshAgent; } set { my.networkNavMeshAgent = value; } }/*",
        "public float moveProbability = 0.1f;",
        "*/public float moveProbability { get { return my.ai.moveProbability; } }"
        ));

        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Npc.cs",
        "public NetworkNavMeshAgent networkNavMeshAgent;",
        "*/public NetworkNavMeshAgent networkNavMeshAgent { get { return my.networkNavMeshAgent; } set { my.networkNavMeshAgent = value; } }",
        "public partial class Npc : Entity",
        "public partial class Npc  : Entity {/*"
        ));

        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Pet.cs",
        "public NetworkNavMeshAgent networkNavMeshAgent;",
        "*/public NetworkNavMeshAgent networkNavMeshAgent { get { return my.networkNavMeshAgent; } set { my.networkNavMeshAgent = value; } }",
        "public partial class Pet : Summonable",
        "public partial class Pet  : Summonable {/*"
        ));


        //VERSION 2.09 - Movement

        // Forward Warp and StunTimeEnd to CharacterSheet
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "public abstract void Warp(Vector3 destination);",
        "public virtual void Warp(Vector3 destination) { character.Warp(destination); }",
        "protected double stunTimeEnd;",
        "protected double stunTimeEnd { get { return my.stunTimeEnd; } set { my.stunTimeEnd = value; } }"
        ));

        // Disable ResetMovement and Forward rubberbanding to PlayerCharacter
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Player.cs",
        "public override void ResetMovement()",
        "public void OldResetMovement()",
        "public NetworkNavMeshAgentRubberbanding rubberbanding;",
        "public NetworkNavMeshAgentRubberbanding rubberbanding { get { return character.player.rubberbanding; } set { character.player.rubberbanding = value; } }"
        ));

        // Forward inSafeZone and ResetMovement to CharacterSheet
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "[HideInInspector] public bool inSafeZone;",
        "[HideInInspector] public bool inSafeZone { get { return !my.combat.isAttackable; } set { my.combat.isAttackable = !value; } }",
        "public abstract void ResetMovement();",
        "public virtual void ResetMovement() { character.ResetMovement(); }"
        ));


        //VERSION 2.08 - Hide unused variables

        // Hide Damage and Defense
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "[SerializeField] protected LinearInt _damage",
        "[SerializeField] [HideInInspector] protected LinearInt _damage",
        "[SerializeField] protected LinearInt _defense",
        "[SerializeField] [HideInInspector] protected LinearInt _defense"
        ));

        // Hide Block and Critical
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "[SerializeField] protected LinearFloat _blockChance",
        "[SerializeField] [HideInInspector] protected LinearFloat _blockChance",
        "[SerializeField] protected LinearFloat _criticalChance",
        "[SerializeField] [HideInInspector] protected LinearFloat _criticalChance"
        ));

        // Hide Health Recovery
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "public bool healthRecovery",
        "private bool healthRecovery",
        "[SerializeField] protected LinearInt _healthRecoveryRate",
        "[SerializeField] [HideInInspector] protected LinearInt _healthRecoveryRate"
        ));

        // Hide Mana Recovery
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "public bool manaRecovery",
        "private bool manaRecovery",
        "[SerializeField] protected LinearInt _manaRecoveryRate",
        "[SerializeField] [HideInInspector] protected LinearInt _manaRecoveryRate"
        ));

        // Hide Damage Popup Prefab and Effect Mount
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "public GameObject damagePopupPrefab",
        "[HideInInspector] public GameObject damagePopupPrefab",
        "[SerializeField] Transform _effectMount",
        "[SerializeField] [HideInInspector] Transform _effectMount"
        ));


        //VERSION 2.07 - Player Animations

        //WARRIOR ANIMATION CONTROLLER
        // Modify Normal Attack Animation
        AddPatch(new PatchData(
        "/uMMORPG/Models/Entities/Warrior/",
        "Controller.controller",
        "m_Name: Normal Attack (Warrior)",
        "m_Name: Attack",
        "m_ConditionEvent: Normal Attack (Warrior)",
        "m_ConditionEvent: Attack"
        ));

        //ARCHER ANIMATION CONTROLLER
        // Modify Normal Attack Animation
        AddPatch(new PatchData(
        "/uMMORPG/Models/Entities/Archer/",
        "Controller.controller",
        "m_Name: Normal Attack (Archer)",
        "m_Name: Attack",
        "m_ConditionEvent: Normal Attack (Archer)",
        "m_ConditionEvent: Attack"
        ));

        //VERSION 2.06 - Monster Animations

        //BANDIT ANIMATION CONTROLLER
        // Modify Normal Attack Animation
        AddPatch(new PatchData(
        "/uMMORPG/Models/Entities/Bandit/",
        "Controller.controller",
        "m_Name: Normal Attack (Bandit)",
        "m_Name: Attack",
        "m_ConditionEvent: Normal Attack (Bandit)",
        "m_ConditionEvent: Attack"
        ));
        // Modify Secondary Attack Animation
        AddPatch(new PatchData(
        "/uMMORPG/Models/Entities/Bandit/",
        "Controller.controller",
        "m_Name: Secondary Attack (Bandit)",
        "m_Name: Offhand Attack",
        "m_ConditionEvent: Secondary Attack (Bandit)",
        "m_ConditionEvent: Offhand Attack"
        ));

        //SKELETON ANIMATION CONTROLLER
        // Modify Normal Attack Animation
        AddPatch(new PatchData(
        "/uMMORPG/Models/Entities/Skeleton/",
        "Controller.controller",
        "m_Name: Normal Attack (Skeleton)",
        "m_Name: Attack",
        "m_ConditionEvent: Normal Attack (Skeleton)",
        "m_ConditionEvent: Attack"
        ));
        // Modify Secondary Attack Animation
        AddPatch(new PatchData(
        "/uMMORPG/Models/Entities/Skeleton/",
        "Controller.controller",
        "m_Name: Secondary Attack (Skeleton)",
        "m_Name: Offhand Attack",
        "m_ConditionEvent: Secondary Attack (Skeleton)",
        "m_ConditionEvent: Offhand Attack"
        ));

        //BABY SKELETON ANIMATION CONTROLLER
        // Modify Normal Attack Animation
        AddPatch(new PatchData(
        "/uMMORPG/Models/Entities/BabySkeleton/",
        "Controller.controller",
        "m_Name: Normal Attack (Baby Skeleton)",
        "m_Name: Attack",
        "m_ConditionEvent: Normal Attack (Baby Skeleton)",
        "m_ConditionEvent: Attack"
        ));
        // Modify Secondary Attack Animation
        AddPatch(new PatchData(
        "/uMMORPG/Models/Entities/BabySkeleton/",
        "Controller.controller",
        "m_Name: Secondary Attack (Baby Skeleton)",
        "m_Name: Offhand Attack",
        "m_ConditionEvent: Secondary Attack (Baby Skeleton)",
        "m_ConditionEvent: Offhand Attack"
        ));

        //VERSION 2.05

        // Disable AddonExample.cs File
        AddPatch(new PatchData(
        "/uMMORPG/Addons/",
        "AddonExample.cs",
        "// Understanding the Addon System:",
        "/*// Understanding the ummorpg Addon System:",
        "void OnSubmit_Example(string text) {}",
        "void OnSubmit_Example(string text) { }*/class blank_addon_method {"
        ));
        //"[Server] void DealDamageAt_Example(Entity entity, int amount, int damageDealt, DamageType damageType) {}",
        //"//[Server] void DealOldDamageAt_Example(Entity entity, int amount, int damageDealt, DamageType damageType) {}"

        // Reroute Equipment Info to Character Sheet
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Player.cs",
        "public EquipmentInfo[] equipmentInfo",
        "*/private EquipmentInfo[] oldEquipmentInfo",
        "[SyncVar] public ItemSlot trash;",
        "[SyncVar] public ItemSlot trash = new ItemSlot(); public EquipmentInfo[] equipmentInfo { get { return my.gear.slots; } set { my.gear.slots = value; } }/*"
        ));

        //VERSION 2.04

        // Replace health check with IsDead to account for other life pools
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Monster.cs",
        "return health == 0;",
        "return character.IsDead;"
        ));
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Pet.cs",
        "return health == 0;",
        "return character.IsDead;"
        ));
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Mount.cs",
        "return health == 0;",
        "return character.IsDead;"
        ));
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Player.cs",
        "return health == 0;",
        "return character.IsDead;"
        ));

        //VERSION 2.03

        // Replace _state in Entity with an enum based FSM
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "InvokeRepeating(nameof(Recover), 1, 1);",
        "//InvokeRepeating(nameof(OldRecover), 1, 1);"
        ));

        // Replace _state in Entity with an enum based FSM
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Skill.cs",
        "public float cooldown => data.cooldown.Get(level);",
        "public float cooldown => (data.cooldown.Get(level) + data.addedCooldown);"
        ));

        //VERSION 2.02

        // Add "Attack" to Skeleton Animation Controller
        AddPatch(new PatchData(
        "/uMMORPG/Models/Entities/Skeleton/",
        "Controller.controller",
        "m_Name: Normal Attack (Skeleton)",
        "m_Name: Attack",
        "m_ConditionEvent: Normal Attack (Skeleton)",
        "m_ConditionEvent: Attack"
        ));
        
        // Reroute level, disable entity serialized max health and max mana
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "[SyncVar] public int level = 1;",
        "[HideInInspector] public int _level = 1; public int level { get { return my.level; } set { my.level = value; } }",
        "[SerializeField] protected LinearInt _healthMax = new LinearInt{baseValue=100};",
        "[HideInInspector] protected LinearInt _healthMax = new LinearInt { baseValue = 100 };",
        "[SerializeField] protected LinearInt _manaMax = new LinearInt{baseValue=100};",
        "[HideInInspector] protected LinearInt _manaMax = new LinearInt { baseValue = 100 };"
        ));
        
        // Reroute max health, max mana, and invincible
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "return _healthMax.Get(level) + passiveBonus + buffBonus;",
        "return my.LIFEMAX + passiveBonus + buffBonus;",
        "return _manaMax.Get(level) + passiveBonus + buffBonus;",
        "return my.MANAMAX + passiveBonus + buffBonus;",
        "bool invincible = false;",
        "bool invincible { get { return character.combat.invincible; } }"
        ));

        // Reroute last combat time
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "[SyncVar] public double lastCombatTime;",
        "public double lastCombatTime { get { return character.combat.timeOfLastCombat; } set { character.combat.timeOfLastCombat = value; } }"
        ));

        // Reroute health and mana
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "[SyncVar] int _health = 1;",
        "int _health { get { return my.LIFE; } set { my.LIFE = value; } }",
        "[SyncVar] int _mana = 1;",
        "int _mana { get { return my.MANA; } set { my.MANA = value; } }"
        ));

        // Reroute effect mount from ummorpg Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "public virtual Transform effectMount { get { return _effectMount; } }",
        "public virtual Transform effectMount { get { return my.launchOrigin; } }"
        ));

        // Reroute gold from ummorpg Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "// note: int is not enough (can have > 2 mil. easily)",
        "/* note: int is not enough (can have > 2 mil. easily)",
        "[SyncVar, SerializeField] long _gold = 0;",
        "long _gold = 0;*/",
        "public long gold { get { return _gold; } set { _gold = Math.Max(value, 0); } }",
        "public long gold { get { return my.GOLD; } set { my.GOLD = Math.Max(value, 0); } }"
        ));

        // Reroute inventory list from ummorpg Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "// useful for monster loot, chests, etc.",
        "/* useful for monster loot, chests, etc.",
        "public SyncListItemSlot inventory = new SyncListItemSlot();",
        "*/public SyncListItemSlot inventory { get { return my.INVENTORY; } set { my.INVENTORY = value; } }"
        ));
        
        // Reroute equipment list from ummorpg Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "// check if the entity has enough arrows",
        "/* check if the entity has enough arrows",
        "public SyncListItemSlot equipment = new SyncListItemSlot();",
        "*/public SyncListItemSlot equipment { get { return my.EQUIPMENT; } set { my.EQUIPMENT = value; } }"
        ));

        // Reroute starting skills from ummorpg Entity
        // NOTE: Requires the next patch to add the */
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "// 'skills' are the loaded skills with cooldowns etc.",
        "/* 'skills' are the loaded skills with cooldowns etc.",
        "public ScriptableSkill[] skillTemplates;",
        "*/public ScriptableSkill[] skillTemplates { get { return my.skills.startingSkills.ToArray(); } set { my.skills.startingSkills = new System.Collections.Generic.List<ScriptableSkill>(value); } }"
        ));
        
        // Reroute skills buffs from ummorpg Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "public SyncListSkill skills = new SyncListSkill();",
        "public SyncListSkill skills { get { return my.SKILLS; } set { my.SKILLS = value; } }",
        "public SyncListBuff buffs = new SyncListBuff(); // active buffs",
        "public SyncListBuff buffs { get { return my.BUFFS; } set { my.BUFFS = value; } }",
        "[SyncVar, HideInInspector] public int currentSkill = -1;",
        "public int currentSkill { get { return my.currentSkill; } set { my.currentSkill = value; } }"
        ));
        
        // Replace _state in Entity with an enum based FSM
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "// -> state only writable by entity class to avoid all kinds of confusion",
        "/* -> state only writable by entity class to avoid all kinds of confusion",
        "[SyncVar, SerializeField] string _state",
        "*/string _state { get { return my.state.ToString(); } set { my.state = (ActiveState)Enum.Parse(typeof(ActiveState), value); } }//"
        //"public string state => _state;",
        //"public string state => _state.ToString();"
        ));
        
        // Reroute _target from ummorpg Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "// so we use [SyncVar] GameObject and wrap it for simplicity",
        "/* so we use [SyncVar] GameObject and wrap it for simplicity",
        "[SyncVar] GameObject _target;",
        "*/public GameObject _target{get{ return my.targeting._target; } set { my.targeting._target = value; } } //[SyncVar] GameObject _target;"
        ));

        // Disable UpdateClient & UpdateServer
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Monster.cs",
        "override void UpdateClient()",
        "void OldUpdateClient()",
        "override string UpdateServer()",
        "string OldUpdateServer()"
        ));

        // Replace _state in Entity with an enum based FSM
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "_state = UpdateServer();",
        "my.state = (ActiveState)Enum.Parse(typeof(ActiveState), UpdateServer());",
        "if (health == 0) _state",// = "+"\""+"DEAD"+"\""+";",
        "//if (health == 0) _oldstate",
        "// dead if spawned without health",
        "if (character.IsDead) my.state = ActiveState.DEAD;"
        ));

        // Disable OnAggro & CanAttack in Monster
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Monster.cs",
        "override void OnAggro(Entity entity)",
        "void OldOnAggro(Entity entity)",
        "override bool CanAttack(Entity entity)",
        "bool OldCanAttack(Entity entity)"//,
        //"override void OnDeath()",
        //"void OldOnDeath()"
        ));
        
        // Disable HasLoot & OnDeath in Monster
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Monster.cs",
        "bool HasLoot()",
        "bool OldHasLoot()",
        //"public override bool CanAttack(Entity entity)",
        //"public bool OldCanAttack(Entity entity)",
        "override void OnDeath()",
        "void OldOnDeath()"
        ));

        //VERSION 2.01

        // Inject weaponModelOffset and showWeaponModel
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Player.cs",
        "if (itemData.modelPrefab != null)",
        "if ( itemData.showWeaponModel && itemData.modelPrefab != null )",
        "GameObject go = Instantiate(itemData.modelPrefab, info.location, false);",
        "GameObject go = Instantiate(itemData.modelPrefab, info.location, false); go.transform.position += itemData.weaponModelOffset;"
        ));

        // D I S A B L E  M E T H O D S
        // Disable castRange
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/",
        //"ScriptableSkill.cs",
        //"public LinearFloat castRange;",
        //"//public LinearFloat oldCastRange;"));

        // Disable DealDamageAt in Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "void DealDamageAt(Entity entity, int amount, float stunChance=0, float stunTime=0)",
        "void DealOldDamageAt(Entity entity, int amount, float stunChance=0, float stunTime=0)",
        "DealDamageAt_",
        "DealOldDamageAt_"));

        // Disable DealDamageAt in Player
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Player.cs",
        "void DealDamageAt(Entity entity, int amount, float stunChance=0, float stunTime=0)",
        "void DealOldDamageAt(Entity entity, int amount, float stunChance=0, float stunTime=0)",
        "DealDamageAt_",
        "DealOldDamageAt_"));

        // Disable RpcOnDamageReceived
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "void RpcOnDamageReceived(int amount, DamageType damageType)",
        "void RpcOldOnDamageReceivedMethod(int amount, DamageType damageType)",
        "entity.RpcOnDamageReceived(damageDealt, damageType);",
        "entity.RpcOldOnDamageReceivedMethod(damageDealt, damageType);"));

        // Disable ShowDamagePopup
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "void ShowDamagePopup(int amount, DamageType damageType)",
        "void ShowOldDamagePopupMethod(int amount, DamageType damageType)",
        "ShowDamagePopup(amount, damageType);",
        "ShowOldDamagePopupMethod(amount, damageType);"));

        // Disable Tooltip in DamageSkill
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/ScriptableSkills/",
        "DamageSkill.cs",
        "public override string ToolTip",
        "public string OldToolTip"));

        // Disable Apply in TargetDamageSkill to handle damage dealing in DamageSkill.cs instead.
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/ScriptableSkills/",
        "TargetDamageSkill.cs",
        "public override void Apply(Entity caster, int skillLevel)",
        "public void OldApply(Entity caster, int skillLevel)"));

        // Disable Revive and Recover
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "public void Recover",
        "public void OldRecover",
        "public void Revive",
        "public void OldRevive"));



        // I N J E C T  M E T H O D S

        //MOUNTED COMBAT
        // Inject Cast While Mounted (ummorpg v150+)
        //NOTE: THIS PATCHES TWICE ON PURPOSE
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Player.cs",
        "// don't cast while mounted",
        "if (enableMountedCombat && IsMounted()) return character.CastMountedSkill(skills[currentSkill], character.target);",
        "// don't cast while mounted",
        "if (enableMountedCombat && IsMounted()) return character.CastMountedSkill(skills[currentSkill], character.target);"));
        
        //SCRIPTED STARTING CHARACTER CONFIGURATION
        // Inject Scripted Starting Character Configuration into NetworkManagerMMO
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "NetworkManagerMMO.cs",
        "player.name = characterName;",
        "player.character.LoadCharacterDefaults(); player.name = characterName; //DX4D"));

        //"// (instantiate temporary player)",
        //"classes[message.classIndex].LoadCharacterDefaults();"));
        //"if (classes[message.classIndex].classConfig != null) { classes[message.classIndex].defaultItems = classes[message.classIndex].classConfig.inventory; classes[message.classIndex].equipmentInfo = classes[message.classIndex].classConfig.equipment; classes[message.classIndex].inventorySize = classes[message.classIndex].classConfig.inventorySize; classes[message.classIndex].skillTemplates = classes[message.classIndex].classConfig.skills; classes[message.classIndex].characterClass = classes[message.classIndex].classConfig.startingCharacterClass; for (int i = 0; i < classes[message.classIndex].equipmentInfo.Length; ++i) { if (classes[message.classIndex].gearLocations.Length > i) { classes[message.classIndex].equipmentInfo[i].location = classes[message.classIndex].gearLocations[i]; } } }"));
        //"prefab.name = message.name;",
        //"prefab.name = (message.name); if (prefab.startingGear != null) { prefab.defaultItems = prefab.startingGear.inventory; prefab.equipmentInfo = prefab.startingGear.equipment; prefab.inventorySize = prefab.startingGear.inventorySize; }"));
        //"for (int i = 0; i < prefab.equipmentInfo.Length; ++i)",
        //"for (int i = 0; (prefab.startingGear != null) ? (i < prefab.startingGear.gearList.Length) : (i < prefab.equipmentInfo.Length); ++i)",
        //"EquipmentInfo info = prefab.equipmentInfo[i];",
        //"EquipmentInfo info = (prefab.startingGear != null) ? (prefab.startingGear.gearList[i]) : (prefab.equipmentInfo[i]);"));

        //PROJECTILE SKILL EFFECT
        // Make ProjectileSkillEffect inherit from LaunchedEffect
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/SkillEffects/",
        //"ProjectileSkillEffect.cs",
        //"public class ProjectileSkillEffect : SkillEffect",
        //"public class ProjectileSkillEffect : LaunchedEffect"));

        // Inject DealProjectileDamage into ProjectileSkillEffect
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/SkillEffects/",
        //"ProjectileSkillEffect.cs",
        //"caster.DealDamageAt(target, caster.damage + damage, stunChance, stunTime);",
        //"DealProjectileDamage();"));

        // Inject Scripted Damage into ProjectileSkillEffect
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/SkillEffects/",
        //"ProjectileSkillEffect.cs",
        //"caster.DealDamageAt(target, caster.damage + damage, stunChance, stunTime);",
        //"if (addedDamage == null || addedDamage.Count < 1) { caster.DealDamageAt(target, (caster.damage + damage), stunChance, stunTime); } else { caster.StartCoroutine(caster.ApplyDamageOnInterval(caster.target, addedDamage, delayBetweenDamage)); }"));

        //VALIDATE SKILL USAGE COSTS

        //ADDED IN UMMORPG 1.166
        // Inject Skill Usage Costs Check into Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Skill.cs",
        "return (!checkSkillReady || IsReady()) &&",
        "return (!checkSkillReady || IsReady()) && caster.CanPayCastingCost(costs) &&"));
        
        //DEPRECIATED IN UMMORPG 1.166
        // Inject Skill Usage Costs Check into Entity
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/",
        //"Entity.cs",
        //"// has a weapon (important for projectiles etc.), no cooldown, hp, mp?",
        //"bool canPayCosts = (health >= skill.healthCosts && mana >= (skill.manaCosts) && blood >= skill.bloodCosts && spirit >= skill.spiritCosts && fury >= skill.furyCosts && stamina >= skill.staminaCosts);"));
        
        // Inject Skill Usage Costs Check into Entity (ummorpg v147 or less)
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/",
        //"Entity.cs",
        //"mana >= skill.manaCosts;",
        //"canPayCosts;"));

        // Inject Skill Usage Costs Check into Entity (ummorpg v148+)
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "skill.CheckSelf(this, checkSkillReady);",
        "CanPayCastingCost(skill.costs.me) && skill.CheckSelf(this, checkSkillReady);"));

        //DEDUCT SKILL USAGE COSTS
        // Inject Skill Usage Costs into Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "mana -= skill.manaCosts;",
        "my.SHIELD -= skill.costs.me.shield; my.LIFE -= skill.costs.me.life; my.BLOOD -= skill.costs.me.blood; my.SPIRIT -= skill.costs.me.spirit; my.MANA -= skill.costs.me.mana; my.FURY -= skill.costs.me.fury; my.STAMINA -= skill.costs.me.stamina;"));
        
        //SPEED MULTIPLIERS
        // Inject variable Movement Speed into Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "return _speed.Get(level) + passiveBonus + buffBonus;",
        "return ((_speed.Get(level) * my.combat.moveSpeedMultiplier) + passiveBonus + buffBonus);"));

        // Inject castSpeedMultiplier into Entity
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Entity.cs",
        "skill.castTimeEnd = NetworkTime.time - skill.castTime;",
        "skill.castTimeEnd = NetworkTime.time + skill.castTime / gameObject.GetComponent<CombatStats>().castSpeedMultiplier;"));

        //FOLLOWUP ATTACK
        // Inject FollowupAttack into Player
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Player.cs",
        "TryUseSkill(0, true);",
        "StartCoroutine(player.FollowupAttack(0, skill.cooldown));"));

        //SYNC EQUIPMENT
        // Inject SyncEquipment into Player
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/",
        "Player.cs",
        //"RefreshLocation(i);",
        //"{ RefreshLocation( i ); } if (character is PlayerCharacter) (character as PlayerCharacter).SyncGear();",
        "RefreshLocation(index);",
        "RefreshLocation( index ); CmdSyncMyEquipment();"));

        // Disable Apply in TargetDamageSkill to handle damage dealing in DamageSkill.cs instead.
        //"// deal damage directly with base damage + skill damage",
        //"base.Apply(caster, skillLevel); return;",
        //"caster.DealDamageAt(caster.target, caster.damage + damage.Get(skillLevel));",
        //"base.Apply(caster, skillLevel);"));

        // Inject variable Movement Speed into Entity
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/",
        //"Entity.cs",
        //"agent.speed = speed;",
        //"agent.speed = (int)ActiveMoveState * moveSpeedMultiplier;"));

        // Inject RefreshGear into Player
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/",
        //"Player.cs",
        //"//    (hence OnStartClient and not Start)",
        //"SyncMyEquipment();",
        //"// update the model",
        //"SyncMyEquipment();"));

        // Inject damage amount validation into TargetProjectileSkill
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/ScriptableSkills/",
        //"TargetProjectileSkill.cs",
        //"if (projectile != null)",
        //"if (projectile != null && damage.baseValue > 0)"));

        // Inject CheckTarget into TargetDamageSkill
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/ScriptableSkills/",
        //"TargetDamageSkill.cs",
        //"target != null && caster.CanAttack",
        //"target != null && base.CheckTarget(caster) && caster.CanAttack"));

        // Inject CheckTarget into TargetProjectileSkill
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/ScriptableSkills/",
        //"TargetProjectileSkill.cs",
        //"target != null && caster.CanAttack",
        //"target != null && base.CheckTarget(caster) && caster.CanAttack"));


        // M A K E  P A R T I A L  C L A S S E S
        // Partial BonusSkill
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/ScriptableSkills/",
        "BonusSkill.cs",
        "abstract class BonusSkill",
        "abstract partial class BonusSkill"));

        // Partial BuffSkill
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/ScriptableSkills/",
        "BuffSkill.cs",
        "abstract class BuffSkill",
        "abstract partial class BuffSkill"));

        // Partial DamageSkill & Inherit from ActiveSkill
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/ScriptableSkills/",
        "DamageSkill.cs",
        "class DamageSkill : ScriptableSkill",
        "class DamageSkill : ActiveSkill",
        "abstract class DamageSkill",
        "abstract partial class DamageSkill"));

        // Partial HealSkillTemplate //DEPRECIATED v2.05
        //AddPatch(new PatchData(
        //"/uMMORPG/Scripts/ScriptableSkills/",
        //"HealSkillTemplate.cs",
        //"abstract class HealSkillTemplate : ScriptableSkill",
        //"abstract partial class HealSkillTemplate : ActiveSkill"));

        // Partial EquipmentItem
        AddPatch(new PatchData(
        "/uMMORPG/Scripts/ScriptableItems/",
        "EquipmentItem.cs",
        "public class EquipmentItem : UsableItem",
        "public partial class EquipmentItem : UsableItem"));


        Debug.Log(name + " : BUILT");
        UnityEditor.AssetDatabase.Refresh(); // REFRESH ALL THE ASSETS IN THE PROJECT
    }
}
