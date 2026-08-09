using System.Collections.Generic;
using UnityEngine;

namespace DX4D
{
    public static partial class PatchManager
    {
        // R E G I S T E R E D  P A T C H E S
        /// <summary>
        /// A list of patches that are registered in the patcher.
        /// </summary>
        public static List<Patch> registeredPatches
        {
            get
            {
                if(_registeredPaches == null)
                {
                    _registeredPaches = new List<Patch>();
                }

                return _registeredPaches;
            }
        }
        [SerializeField] static List<Patch> _registeredPaches;

        // R E G I S T E R  &  U N R E G I S T E R  ( L O A D  &  U N L O A D )
        /// <summary>
        /// Registers a patch to the patcher
        /// </summary>
        /// <returns>Success?</returns>
        public static bool Register(Patch patch)
        {
            if (PatchIsNotRegistered(patch)) { registeredPatches.Add(patch); }

            return PatchIsRegistered(patch);
        }
        
        /// <summary>
        /// Unregisters a patch from the patcher
        /// </summary>
        /// <returns>Success?</returns>
        public static bool Unregister(Patch patch)
        {
            if (PatchIsRegistered(patch)) { registeredPatches.Remove(patch); }

            return PatchIsNotRegistered(patch);
        }

        // R E G I S T E R E D ? ? ?
        /// <summary>
        /// Checks if a patch is registered in the patcher
        /// </summary>
        public static bool PatchIsRegistered(Patch patch)
        {
            if (patch == null || registeredPatches == null || registeredPatches.Count == 0) return false;

            foreach (Patch checkPatch in registeredPatches)
            {
                if (checkPatch != null && checkPatch.name == patch.name) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a patch is NOT registered in the patcher
        /// </summary>
        public static bool PatchIsNotRegistered(Patch patch)
        {
            return !PatchIsRegistered(patch);
        }
    }
}
