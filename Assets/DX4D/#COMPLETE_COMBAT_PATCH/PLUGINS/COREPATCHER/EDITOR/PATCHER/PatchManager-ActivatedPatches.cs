using System.Collections.Generic;
using UnityEngine;

namespace DX4D
{
    public static partial class PatchManager
    {
        // A C T I V E  P A T C H E S
        /// <summary>
        /// A list of the patches that are currently active in the patcher.
        /// </summary>
        public static List<Patch> activePatches
        {
            get
            {
                if (_activePaches == null)
                {
                    _activePaches = new List<Patch>();
                }

                return _activePaches;
            }
        }
        [SerializeField] static List<Patch> _activePaches;

        // A C T I V A T E  &  D E A C T I V A T E
        /// <summary>
        /// Activates a patch that is already registered in the patcher.
        /// </summary>
        /// <param name="patch">The patch to activate. (must be registered first)</param>
        /// <returns>Success?</returns>
        public static bool Activate(Patch patch)
        {
            if (PatchIsRegisteredAndNotActivated(patch)) { patch.InstallAll(); activePatches.Add(patch);}

            return PatchIsActivated(patch);
        }

        /// <summary>
        /// Deactivates a patch that is registered and active in the patcher.
        /// </summary>
        /// <param name="patch">The patch to deactivate. (must be registered and activated first)</param>
        /// <returns>Success?</returns>
        public static bool Deactivate(Patch patch)
        {
            if (PatchIsRegisteredAndActivated(patch)) { patch.UninstallAll(); activePatches.Remove(patch); }

            return PatchIsNotActivated(patch);
        }
        
        // A C T I V A T E D ? ? ?
        /// <summary>
        /// Checks if a certain patch is activated
        /// </summary>
        public static bool PatchIsActivated(Patch patch)
        {
            if (patch == null || activePatches == null || activePatches.Count == 0) return false;

            foreach (Patch checkPatch in activePatches)
            {
                if (checkPatch != null && checkPatch.name == patch.name) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a certain patch is NOT activated
        /// </summary>
        public static bool PatchIsNotActivated(Patch patch)
        {
            return !PatchIsActivated(patch);
        }
    }
}
