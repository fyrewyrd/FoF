namespace DX4D
{
    /// <summary>
    /// The Patch Manager is responsible for tracking patches. Maintains lists of the patches that are activated and registered.
    /// </summary>
    public static partial class PatchManager
    {
        // I S R E G I S T E R E D  &  I S A C T I V A T E D
        /// <summary>
        /// Check if the patch is activated and registered in the patcher
        /// </summary>
        public static bool PatchIsRegisteredAndActivated(Patch patch)
        {
            return (PatchIsRegistered(patch) && PatchIsActivated(patch));
        }

        /// <summary>
        /// Check if the patch is NOT activated, but is registered in the patcher.
        /// </summary>
        public static bool PatchIsRegisteredAndNotActivated(Patch patch)
        {
            return (PatchIsRegistered(patch) && PatchIsNotActivated(patch));
        }
    }
}
