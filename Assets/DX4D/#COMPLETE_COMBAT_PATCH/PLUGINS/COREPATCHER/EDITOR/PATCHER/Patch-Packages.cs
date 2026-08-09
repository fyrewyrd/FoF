using System.Collections.Generic;

namespace DX4D
{
    public abstract partial class Patch
    {
        // P A C K A G E S
        /// <summary>
        /// A list of the packages that are registered in the patcher
        /// </summary>
        public List<PackageData> packages = new List<PackageData>();

        /// <summary>
        /// Add a package to the patcher
        /// </summary>
        public bool AddPackage(PackageData info)
        {
            if (packages.Contains(info)) { return false; }
            else { packages.Add(info); return true; }
        }

        /// <summary>
        /// Remove a package from the patcher
        /// </summary>
        public bool RemovePackage(PackageData data)
        {
            bool packageRemoved = false;

            foreach (PackageData pack in packages)
            {
                if (pack.GetHashCode() == data.GetHashCode())
                {
                    packageRemoved = packages.Remove(pack);
                }
            }

            return packageRemoved;
        }

        void InstallAllPackages()
        {
            foreach (PackageData pack in packages)
            {
                IOTools.LoadUnityPackage(pack.path, pack.file, pack.silentInstall);
            }
        }

        void UninstallAllPackages()
        {
            foreach (PackageData pack in packages)
            {
                IOTools.UnloadUnityPackage(pack.file);
            }
        }
    }
}
