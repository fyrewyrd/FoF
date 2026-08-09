using System.Collections.Generic;

namespace DX4D
{
    /// <summary>
    /// A Patch is a collection of small patches that are applied to various files in your project
    /// </summary>
    public abstract partial class Patch
    {
        /// <summary>
        /// Contains info about this patch
        /// </summary>
        public PatchInfo info = new PatchInfo();

        /// Patch Name
        public string name { get { return info.name; } set { info.name = value; } }
        /// Patch Description
        public string description { get { return info.description; } set { info.description = value; } }
        /// Patch Category
        public string category { get { return info.category; } set { info.category = value; } }
        /// Patch Version
        public float version { get { return info.version; } set { info.version = value; } }
        /// Patch Status
        public PatchStatusInfo status { get { return info.status; } set { info.status = value; } }

        // A C T I V A T E
        /// <summary>
        /// Install all patches and packages in this patch
        /// </summary>
        public void InstallAll()
        {
            InstallAllPatches();
            InstallAllPackages();
        }

        // D E A C T I V A T E
        /// <summary>
        /// Uninstall all patches and packages in this patch
        /// </summary>
        public void UninstallAll()
        {
            UninstallAllPatches();
            UninstallAllPackages();
        }

        // P A T C H E S
        /// <summary>
        /// A list of the patches that are contained in this patch
        /// </summary>
        public List<PatchData> patches = new List<PatchData>();

        // B U I L D  P A T C H
        // 1 file | 1 patch data
        /// <summary>
        /// Add another patch to this patch
        /// </summary>
        public bool AddPatch(PatchData data)
        {
            if (patches.Contains(data)) { return false; }
            else { patches.Add(data); return true; }
        }
        /// 1 File | 1 Substitution
        public bool AddPatch(string folderPath, string fileToModify, string toReplace, string replaceWith)
        {
            PatchData patchToBuild = new PatchData(folderPath, fileToModify, toReplace, replaceWith);
            return AddPatch(patchToBuild);
        }
        /// 1 File | 2 Substitutions
        public bool AddPatch(string folderPath, string fileToModify, string toReplace, string replaceWith, string toReplace2, string replaceWith2)
        {
            PatchData patchToBuild = new PatchData(folderPath, fileToModify, toReplace, replaceWith, toReplace2, replaceWith2);
            return AddPatch(patchToBuild);
        }
        /// 1 File | 3 Substitutions
        public bool AddPatch(string folderPath, string fileToModify, string toReplace, string replaceWith, string toReplace2, string replaceWith2, string toReplace3, string replaceWith3)
        {
            PatchData patchToBuild = new PatchData(folderPath, fileToModify, toReplace, replaceWith, toReplace2, replaceWith2, toReplace3, replaceWith3);
            return AddPatch(patchToBuild);
        }

        // R E M O V E
        /// <summary>
        /// Remove a patch from this patch
        /// </summary>
        public bool RemovePatch(PatchData data)
        {
            bool removed = false;
            while (patches.Contains(data)) { removed = patches.Remove(data); }
            return removed;
        }

        void InstallAllPatches()
        {
            foreach (PatchData info in patches)
            {
                for (int i = 0; i < info.PatchCount; i++)
                {
                    //Undo Previous Patching
                    IOTools.ReplaceTextInFile(info.path, info.file, info.inject[i], info.replace[i]);

                    //Apply Replacement Text
                    IOTools.ReplaceTextInFile(info.path, info.file, info.replace[i], info.inject[i]);
                }
            }
        }

        void UninstallAllPatches()
        {
            foreach (PatchData info in patches)
            {
                for (int i = 0; i < info.PatchCount; i++)
                {
                    //Undo Previous Patching
                    IOTools.ReplaceTextInFile(info.path, info.file, info.inject[i], info.replace[i]);
                }
            }
        }
    }
}
