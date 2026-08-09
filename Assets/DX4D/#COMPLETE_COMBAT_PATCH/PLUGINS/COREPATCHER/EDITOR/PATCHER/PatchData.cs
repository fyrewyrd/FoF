namespace DX4D
{
    /// <summary>
    /// Contains the information necessary to patch a file.
    /// </summary>
    public partial struct PatchData
    {
        /// <summary>
        /// The path within your project where the target file is located
        /// </summary>
        public string path;

        /// <summary>
        /// The name of the file that we are patching
        /// </summary>
        public string file;

        /// <summary>
        /// The strings that will be replaced in the target file
        /// </summary>
        public string[] replace;

        /// <summary>
        /// The strings that will take the place of the old strings in the target file
        /// </summary>
        public string[] inject;
        
        /// <summary>
        /// Gets the number of patches contained in this patch file
        /// </summary>
        public int PatchCount
        {
            get
            {
                if (replace.Length - inject.Length >= 0) return replace.Length;
                else return inject.Length;
            }
        }

        /// <summary>
        /// Create a patch that replaces a single line in a single file
        /// </summary>
        public PatchData(string folderPath, string fileToModify, string toReplace, string replaceWith)
        {
            path = folderPath;
            file = fileToModify;
            replace = new string[] { toReplace };
            inject = new string[] { replaceWith };
        }

        /// <summary>
        /// Create a patch that replaces two lines in a single file
        /// </summary>
        public PatchData(string folderPath, string fileToModify, string toReplace, string replaceWith, string toReplace2, string replaceWith2)
        {
            path = folderPath;
            file = fileToModify;
            replace = new string[] { toReplace, toReplace2 };
            inject = new string[] { replaceWith, replaceWith2 };
        }

        /// <summary>
        /// Create a patch that replaces three lines in a single file
        /// </summary>
        public PatchData(string folderPath, string fileToModify, string toReplace, string replaceWith, string toReplace2, string replaceWith2, string toReplace3, string replaceWith3)
        {
            path = folderPath;
            file = fileToModify;
            replace = new string[] { toReplace, toReplace2, toReplace3 };
            inject = new string[] { replaceWith, replaceWith2, replaceWith3 };
        }
    }
}
