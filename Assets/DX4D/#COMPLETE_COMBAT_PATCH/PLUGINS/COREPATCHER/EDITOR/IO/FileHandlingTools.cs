using System;
using System.IO;
using UnityEngine;

namespace DX4D
{
    /// <summary>
    /// A collection of tools to make development easier.
    /// </summary>
    public partial class IOTools
    {
        // U N I T Y  P A C K A G E  I N S T A L L
        /// <summary>
        /// Load a .unitypackage file.
        /// </summary>
        /// <param name="filePath">The path to the file (starting in the Assets folder).</param>
        /// <param name="fileName">The name of the file (including extension)</param>
        /// <param name="silentInstall">Install without a prompt?</param>
        /// <returns>Package imported sucessfully.</returns>
        public static bool LoadUnityPackage(string filePath, string fileName, bool silentInstall)
        {
#if UNITY_EDITOR
            try
            {
                UnityEditor.AssetDatabase.ImportPackage((filePath + fileName), !silentInstall);
            }
            catch(Exception e)
            {
                Debug.LogWarning("DX4D.PATCHER: CAN'T IMPORT PACKAGE: @" + filePath + fileName + "\n " + e.ToString());
                return false;
            }
#endif
            return true;
        }

        /// <summary>
        /// Unloads a .unitypackage from your project
        /// </summary>
        /// <param name="fileName">The file name of the .unitypackage (can be without the extension)</param>
        /// <returns>Success?</returns>
        public static bool UnloadUnityPackage(string fileName)
        {
            try
            {
                UnityEditor.AssetDatabase.RemoveAssetBundleName(fileName.Trim(".unitypackage".ToCharArray()), true);
            }
            catch (Exception e)
            {
                Debug.LogWarning("DX4D.PATCHER: CAN'T REMOVE PACKAGE: @" + fileName + "\n " + e.ToString());
                return false;
            }
            return true;
        }

        // F I L E  H A N D L I N G  H E L P E R S
        /// <summary>
        /// Delete a file without asking permission
        /// </summary>
        /// <param name="filePath">The path to the file (starting in the Assets folder).</param>
        public static void DeleteFileSilently(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);

                    Debug.Log("DX4D.PATCHER: SUCCESS: " + filePath + " was deleted sucesfully.");
                }
                catch (IOException e)
                {
                    Debug.LogWarning("DX4D.PATCHER: CAN'T DELETE FILE: @" + filePath + "\n " + e.ToString());
                }
            }
            else
            {
                Debug.LogWarning("DX4D.PATCHER: FILE NOT FOUND: @" + filePath);
            }
        }

        // 4  P A R A M S - 1 File / 1 Substitution
        /// <summary>
        /// Replaces a line of text in the specified file.
        /// </summary>
        /// <param name="folderPath">The path to the file (starting in the Assets folder).</param>
        /// <param name="fileToModify">The name of the file (including extension)</param>
        /// <param name="toReplace">The string you want replace</param>
        /// <param name="replaceWith">The string to replace it with</param>
        public static void ReplaceTextInFile(string folderPath, string fileToModify, string toReplace, string replaceWith)
        {
            string fullFilePath = Application.dataPath + folderPath + fileToModify;
            string fileText = string.Empty;

            try
            {
                StreamReader r = new StreamReader(fullFilePath);
                fileText = r.ReadToEnd().Replace(toReplace, replaceWith);
                r.Close();
            }
            catch (Exception e) {
                Debug.LogWarning("DX4D.PATCHER: READ ERROR: @" + folderPath + fileToModify + "\n " + e.ToString());
                return;
            }

            if (string.IsNullOrEmpty(fileText)) { Debug.LogWarning("DX4D.PATCHER: FILE NOT FOUND: " + fileToModify + " could not be found at " + folderPath); return; }

            try
            {
                StreamWriter w = new StreamWriter(fullFilePath);
                w.Write(fileText);
                w.Flush();
                w.Close();
            }
            catch(Exception e) {
                Debug.LogWarning("DX4D.PATCHER: WRITE ERROR: @" + folderPath + fileToModify + "\n " + e.ToString());
                return;
            }

            Debug.Log("DX4D.PATCHER: SUCCESS: " + fileToModify + " successfully patched!!"); //DEBUG
            //AssetDatabase.Refresh(); //Reflect the changes right away in the editor
        }

        // 5  P A R A M E T E R S - 2 Files / 1 Substitution
        /// <summary>
        /// Replaces a line of text in two separate files.
        /// </summary>
        /// <param name="folderPath">The path to the files (starting in the Assets folder).</param>
        /// <param name="fileToModify">The name of the first file (including extension)</param>
        /// <param name="toReplace">The string you want replace</param>
        /// <param name="replaceWith">The string to replace it with</param>
        /// <param name="fileToModify2">The name of the second file (including extension)</param>
        public static void ReplaceTextInFile(string folderPath, string fileToModify, string toReplace, string replaceWith, string fileToModify2)
        {
            ReplaceTextInFile(folderPath, fileToModify, toReplace, replaceWith);
            ReplaceTextInFile(folderPath, fileToModify2, toReplace, replaceWith);
        }

        // 6  P A R A M A T E R S - 1 File / 2 Substitutions
        /// <summary>
        /// Replaces two lines of text in a single file.
        /// </summary>
        /// <param name="folderPath">The path to the file (starting in the Assets folder).</param>
        /// <param name="fileToModify">The name of the file (including extension)</param>
        /// <param name="toReplace">The first string you want replace</param>
        /// <param name="replaceWith">The first string to replace it with</param>
        /// <param name="toReplace2">The second string you want replace</param>
        /// <param name="replaceWith2">The second string to replace it with</param>
        public static void ReplaceTextInFile(string folderPath, string fileToModify, string toReplace, string replaceWith, string toReplace2, string replaceWith2)
        {
            ReplaceTextInFile(folderPath, fileToModify, toReplace, replaceWith);
            ReplaceTextInFile(folderPath, fileToModify, toReplace2, replaceWith2);
        }

        // 8  P A R A M A T E R S - 1 File / 3 Substitutions
        /// <summary>
        /// Replaces three lines of text in a single file.
        /// </summary>
        /// <param name="folderPath">The path to the file (starting in the Assets folder).</param>
        /// <param name="fileToModify">The name of the file (including extension)</param>
        /// <param name="toReplace">The first string you want replace</param>
        /// <param name="replaceWith">The first string to replace it with</param>
        /// <param name="toReplace2">The second string you want replace</param>
        /// <param name="replaceWith2">The second string to replace it with</param>
        /// <param name="toReplace3">The third string you want replace</param>
        /// <param name="replaceWith3">The third string to replace it with</param>
        public static void ReplaceTextInFile(string folderPath, string fileToModify, string toReplace, string replaceWith, string toReplace2, string replaceWith2, string toReplace3, string replaceWith3)
        {
            ReplaceTextInFile(folderPath, fileToModify, toReplace, replaceWith);
            ReplaceTextInFile(folderPath, fileToModify, toReplace2, replaceWith2);
            ReplaceTextInFile(folderPath, fileToModify, toReplace3, replaceWith3);
        }
    }
}
