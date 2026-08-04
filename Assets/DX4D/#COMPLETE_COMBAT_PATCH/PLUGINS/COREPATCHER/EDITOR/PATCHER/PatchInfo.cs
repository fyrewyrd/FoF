namespace DX4D
{
    /// <summary>
    /// Contains basic info about a patch
    /// </summary>
    public partial class PatchInfo
    {
        ///
        public string name;

        ///
        public string description;

        ///
        public string category;

        ///
        public float version;

        //public bool installed { get { return PatchManager.PatchIsActivated(this); } }

        /// <summary>
        /// Contains info about this patches activation and registration status
        /// </summary>
        public PatchStatusInfo status = new PatchStatusInfo();
    }
}
