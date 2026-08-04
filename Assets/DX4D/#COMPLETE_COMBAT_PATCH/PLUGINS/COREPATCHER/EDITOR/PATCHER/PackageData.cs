namespace DX4D
{
    /// <summary>
    /// Contains the information necessary to install a package file.
    /// </summary>
    public partial struct PackageData
    {
        /// <summary>
        /// The path within your project where the package file is located
        /// </summary>
        public string path;

        /// <summary>
        /// The name of the package file that we are installing
        /// </summary>
        public string file;

        /// <summary>
        /// Install the package without any input required from the user
        /// </summary>
        public bool silentInstall;

        /// <summary>
        /// Create an installer that installs a unitypackage to your project
        /// </summary>
        public PackageData(string folderPath, string fileToInstall, bool silentInstallation)
        {
            path = folderPath;
            file = fileToInstall;
            silentInstall = silentInstallation;
        }
    }
}
