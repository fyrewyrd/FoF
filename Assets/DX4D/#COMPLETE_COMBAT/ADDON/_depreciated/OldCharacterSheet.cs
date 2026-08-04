/* //DEPRECIATED
using Mirror;
using UnityEngine;
using System.Collections.Generic;

namespace DX4D
{
    //[RequireComponent(typeof(DamageEvent))]
    public partial class CharacterSheet : NetworkBehaviour
    {
        private void OnServerInitialized()
        {
            foreach(ScriptedStat stat in attributes)
            {
                Debug.Log("" + stat.name + " is " + stat.baseValue);
            }
            foreach(ScriptedStat stat in stats)
            {
                Debug.Log("" + stat.name + " is " + stat.baseValue);
            }
        }
        public void Start()
        {
            
        }
        public void Update()
        {
            
        }

        [Tooltip("")]
        [SerializeField] public List<ScriptedStat> attributes = new List<ScriptedStat>();
        [SerializeField] public List<ScriptedStat> stats = new List<ScriptedStat>();
        public static class Initialize
        {
            public static void Attributes()
            {
            }
            public static void Stats()
            {
            }
        }
    }
}

namespace DX4D.Stats
{
    #region B L O C K
    public static class Block
    {
        public static class Physical
        {
            public static int damagePercent = (int)NamedAmount.Half;
            #region multiplier
            public static float damageMultiplier { get { return (damagePercent * 0.01f); } }
            #endregion
        }
        public static class Magic
        {
            public static int damagePercent = (int)NamedAmount.Half;
            #region multiplier
            public static float damageMultiplier { get { return (damagePercent * 0.01f); } }
            #endregion
        }
    }
    #endregion
    #region D O D G E
    public static class Dodge
    {
        public static class Physical
        {
            public static int damagePercent = (int)NamedAmount.Half;
            #region multiplier
            public static float damageMultiplier { get { return (damagePercent * 0.01f); } }
            #endregion
        }
        public static class Magic
        {
            public static int damagePercent = (int)NamedAmount.Half;
            #region multiplier
            public static float damageMultiplier { get { return (damagePercent * 0.01f); } }
            #endregion
        }
        //public static int physicalDamageTaken = (int)NamedAmount.Zero;
        //public static int magicDamageTaken = (int)NamedAmount.Zero;
        //#region physical damage multiplier
        //public static float damageMultiplier { get { return (physicalDamageTaken * 0.01f); } }
        //#endregion
        //#region magic damage multiplier
        //public static float magicDamageMultiplier { get { return (magicDamageTaken * 0.01f); } }
        //#endregion
    }
    #endregion
    #region C R I T I C A L
    public static class Critical
    {
        public static int physicalDamageDone = (int)NamedAmount.Double;
        public static int magicDamageDone = (int)NamedAmount.Double;
        #region damage multipliers
        public static float damageMultiplier { get { return (physicalDamageDone * 0.01f); } }
        public static float magicDamageMultiplier { get { return (magicDamageDone * 0.01f); } }
        #endregion
    }
    #endregion
    #region B A C K S T A B
    public static class Backstab
    {
        public static int physicalDamageDone = (int)NamedAmount.Double;
        public static int magicDamageDone = (int)NamedAmount.Normal;
        #region damage multipliers
        public static float damageMultiplier { get { return (physicalDamageDone * 0.01f); } }
        public static float magicDamageMultiplier { get { return (magicDamageDone * 0.01f); } }
        #endregion
    }
    #endregion
    #region F L A N K
    public class Flank : CombatStat
    {
        //public Flank(int physical, int magic) : base(physical, magic) { }
    }
    #endregion
}


namespace DX4D
{
    public enum PoolLevel { Inactive = -1, Empty = 0, Critical = 10, Low = 20, Half = 50, ThreeQuarters = 75, NearlyFull = 85, Full = 100 }

    public abstract partial class ScriptedStat : ScriptableObject
    {
        [SyncVar] internal int baseValue = 100;
    }
    public abstract partial class VariablePool : ScriptedStat
    {
        public int current
        {
            get { return Mathf.Min(baseValue, maximum.total); }
            set { baseValue = Mathf.Clamp(value, 0, maximum.total); }
        }
        [SerializeField] public ScaledValue maximum = new ScaledValue { scale = 1, baseValue = 100, baseBonus = 0, scaledBonus = 0 };
    }
    public abstract partial class ScriptedPool : VariablePool
    {
        public bool replenishes = false;
        public bool decays = false;
        [SerializeField] public ScaledValue replenishRate = new ScaledValue { scale = 1, baseValue = 1, baseBonus = 0, scaledBonus = 0 };
        [SerializeField] public ScaledValue decayRate = new ScaledValue { scale = 1, baseValue = 1, baseBonus = 0, scaledBonus = 0 };

        public float percentage { get { return ((maximum.total > 0 && current > 0) ? current / maximum.total : 0); } }
    }
    [CreateAssetMenu(menuName = "DX4D/CHARACTER/ATTRIBUTE/ResourcePool", order = 999)]
    public abstract class ResourcePool : ScriptedPool
    {
        [SerializeField] public PoolLevel startingAmount = PoolLevel.Full;
        public PoolLevel estimatedAmount
        {
            get
            {
                if (percentage == -1) return PoolLevel.Inactive;
                else if (percentage < (int)PoolLevel.Empty) current = (int)PoolLevel.Empty;

                if (percentage == (int)PoolLevel.Empty) return PoolLevel.Empty;
                else if (percentage <= (int)PoolLevel.Critical) return PoolLevel.Critical;
                else if (percentage <= (int)PoolLevel.Low) return PoolLevel.Low;
                else if (percentage <= (int)PoolLevel.Half) return PoolLevel.Half;
                else if (percentage <= (int)PoolLevel.ThreeQuarters) return PoolLevel.ThreeQuarters;
                else if (percentage <= (int)PoolLevel.NearlyFull) return PoolLevel.NearlyFull;
                else if (percentage >= (int)PoolLevel.Full) return PoolLevel.Full;
                else return PoolLevel.Inactive;
            }
        }
    }
    [CreateAssetMenu(menuName = "DX4D/CHARACTER/ATTRIBUTE/LifePool", order = 999)]
    public class LifePool : ResourcePool
    {
        public PoolLevel dieInState = PoolLevel.Inactive;
    }
    public partial class Player
    {
        [SerializeField] public List<ScriptedStat> attributes = new List<ScriptedStat>();
        public void FixedUpdate()
        {
            foreach (LifePool pool in attributes)
            {
                Debug.Log(pool.name + " is " + pool.estimatedAmount.ToString());
            }
        }
    }
}

namespace DX4D
{
    [CreateAssetMenu(menuName = "DX4D/CHARACTER/STAT/Crafting", order = 999)]
    public class CraftingStat : ScriptedStat
    {
    }
    [CreateAssetMenu(menuName = "DX4D/CHARACTER/STAT/Combat", order = 999)]
    public class CombatStat : ScriptedStat
    {
        public NamedAmount physicalDamage = NamedAmount.Normal;
        public NamedAmount magicDamage = NamedAmount.Normal;
        #region damage multipliers
        public float damageMultiplier { get { return ((int)physicalDamage * 0.01f); } }
        public float magicDamageMultiplier { get { return ((int)magicDamage * 0.01f); } }
        #endregion

    }
}
*/
