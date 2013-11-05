using System;
using System.Security.AccessControl;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Context_Menu_Manager
{
    /// <summary>
    /// Contains methods to operate with a system registry
    /// </summary>
    public static class RegistryUtilities
    {
        /// <summary>
        /// Renames a subkey of the passed in registry key since 
        /// the Framework totally forgot to include such a handy feature.
        /// </summary>
        /// <returns>True if succeeds</returns>
        static readonly RegistrySecurity MSec;

        static RegistryUtilities()
        {
            MSec = new RegistrySecurity();
            string user = Environment.UserDomainName +
                          "\\" + Environment.UserName;
            try
            {
                RegistryAccessRule rule = new RegistryAccessRule(user,
                                                  RegistryRights.ReadKey | RegistryRights.WriteKey
                                                  | RegistryRights.Delete | RegistryRights.FullControl | RegistryRights.CreateSubKey,
                                                  InheritanceFlags.ContainerInherit,
                                                  PropagationFlags.None,
                                                  AccessControlType.Allow
                    );
                MSec.AddAccessRule(rule);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Renames <paramref name="subKeyName"/> at the <paramref name="parentKey"/> with a <paramref name="newSubKeyName"/>
        /// </summary>
        /// <param name="parentKey">Parent key</param>
        /// <param name="subKeyName">Old subkey name</param>
        /// <param name="newSubKeyName">New subkey name</param>
        /// <returns>True if renaming was successful</returns>
        public static bool RenameSubKey(this RegistryKey parentKey, string subKeyName, string newSubKeyName)
        {
            try
            {
                CopyKey(parentKey, subKeyName, newSubKeyName);
                parentKey.DeleteSubKeyTree(subKeyName);
            }
            catch (Exception ex)
            {
                // ToDo: send exception details via SmartAssembly bug reporting!
            }
            return true;
        }

        /// <summary>
        /// Copy a registry key. The parentKey must be writeable.
        /// </summary>
        /// <param name="parentKey">Parent key</param>
        /// <param name="keyNameToCopy">Name of the key to copy</param>
        /// <param name="newKeyName">New key name</param>
        /// <returns>True if copying was successful</returns>
        public static bool CopyKey(this RegistryKey parentKey, string keyNameToCopy, string newKeyName)
        {
            //Create new key
            try
            {
                using (RegistryKey destinationKey = parentKey.CreateSubKey(newKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    //Open the sourceKey we are copying from
                    using (RegistryKey sourceKey = parentKey.OpenSubKey(keyNameToCopy))
                    {
                        RecurseCopyKey(sourceKey, destinationKey);
                    }
                }
            }
            catch
            {
            }
            return true;
        }

        /// <summary>
        /// Copy registry key recursively
        /// </summary>
        /// <param name="sourceKey">Sourse key</param>
        /// <param name="destinationKey">Destination key</param>
        static void RecurseCopyKey(RegistryKey sourceKey, RegistryKey destinationKey)
        {
            try
            {
                //copy all the values
                foreach (string valueName in sourceKey.GetValueNames())
                {
                    object objValue = sourceKey.GetValue(valueName);
                    RegistryValueKind valKind = sourceKey.GetValueKind(valueName);
                    destinationKey.SetValue(valueName, objValue, valKind);
                }

                //For Each subKey 
                //Create a new subKey in destinationKey 
                //Call myself 
                foreach (string sourceSubKeyName in sourceKey.GetSubKeyNames())
                {
                    using (RegistryKey sourceSubKey = sourceKey.OpenSubKey(sourceSubKeyName))
                    {
                        using (RegistryKey destSubKey = destinationKey.CreateSubKey(sourceSubKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree,
                                                                             MSec))
                        {
                            RecurseCopyKey(sourceSubKey, destSubKey);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}