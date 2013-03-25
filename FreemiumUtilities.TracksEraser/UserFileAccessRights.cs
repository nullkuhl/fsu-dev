using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FreemiumUtilities.TracksEraser
{
	/// <summary>
	/// User file access rights
	/// </summary>
	public class UserFileAccessRights
	{
		readonly bool _allowAppendData;
		readonly bool _allowChangePermissions;
		readonly bool _allowCreateDirectories;
		readonly bool _allowCreateFiles;
		readonly bool _allowDelete;
		readonly bool _allowDeleteSubdirectoriesAndFiles;
		readonly bool _allowExecuteFile;
		readonly bool _allowFullControl;
		readonly bool _allowListDirectory;
		readonly bool _allowModify;
		readonly bool _allowRead;
		readonly bool _allowReadAndExecute;
		readonly bool _allowReadAttributes;
		readonly bool _allowReadData;
		readonly bool _allowReadExtendedAttributes;
		readonly bool _allowReadPermissions;
		readonly bool _allowSynchronize;
		readonly bool _allowTakeOwnership;
		readonly bool _allowTraverse;
		readonly bool _allowWrite;
		readonly bool _allowWriteAttributes;
		readonly bool _allowWriteData;
		readonly bool _allowWriteExtendedAttributes;
		readonly bool _denyAppendData;
		readonly bool _denyChangePermissions;
		readonly bool _denyCreateDirectories;
		readonly bool _denyCreateFiles;
		readonly bool _denyDelete;
		readonly bool _denyDeleteSubdirectoriesAndFiles;
		readonly bool _denyExecuteFile;
		readonly bool _denyFullControl;
		readonly bool _denyListDirectory;
		readonly bool _denyModify;
		readonly bool _denyRead;
		readonly bool _denyReadAndExecute;
		readonly bool _denyReadAttributes;
		readonly bool _denyReadData;
		readonly bool _denyReadExtendedAttributes;
		readonly bool _denyReadPermissions;
		readonly bool _denySynchronize;
		readonly bool _denyTakeOwnership;
		readonly bool _denyTraverse;
		readonly bool _denyWrite;
		readonly bool _denyWriteAttributes;
		readonly bool _denyWriteData;
		readonly bool _denyWriteExtendedAttributes;
		readonly string _path;
		readonly WindowsIdentity _principal;

		/// <summary>
		/// Convenience constructor assumes the current user
		/// </summary>
		/// <param name="path"></param>
		public UserFileAccessRights(string path) :
			this(path, WindowsIdentity.GetCurrent())
		{
		}


		/// <summary>
		/// Supply the path to the file or directory and a user or group. 
		/// Access checks are done
		/// during instantiation to ensure we always have a valid object
		/// </summary>
		/// <param name="path"></param>
		/// <param name="principal"></param>
		public UserFileAccessRights(string path,
		                            WindowsIdentity principal)
		{
			_path = path;
			_principal = principal;

			var fi = new FileInfo(_path);
			AuthorizationRuleCollection acl = fi.GetAccessControl().GetAccessRules
				(true, true, typeof (SecurityIdentifier));
			for (int i = 0; i < acl.Count; i++)
			{
				var rule =
					(FileSystemAccessRule) acl[i];
				if (rule == null || (_principal.User == null || !_principal.User.Equals(rule.IdentityReference))) continue;
				if (AccessControlType.Deny.Equals
					(rule.AccessControlType))
				{
					if (Contains(FileSystemRights.AppendData, rule))
						_denyAppendData = true;
					if (Contains(FileSystemRights.ChangePermissions, rule))
						_denyChangePermissions = true;
					if (Contains(FileSystemRights.CreateDirectories, rule))
						_denyCreateDirectories = true;
					if (Contains(FileSystemRights.CreateFiles, rule))
						_denyCreateFiles = true;
					if (Contains(FileSystemRights.Delete, rule))
						_denyDelete = true;
					if (Contains(FileSystemRights.DeleteSubdirectoriesAndFiles,
					             rule)) _denyDeleteSubdirectoriesAndFiles = true;
					if (Contains(FileSystemRights.ExecuteFile, rule))
						_denyExecuteFile = true;
					if (Contains(FileSystemRights.FullControl, rule))
						_denyFullControl = true;
					if (Contains(FileSystemRights.ListDirectory, rule))
						_denyListDirectory = true;
					if (Contains(FileSystemRights.Modify, rule))
						_denyModify = true;
					if (Contains(FileSystemRights.Read, rule)) _denyRead = true;
					if (Contains(FileSystemRights.ReadAndExecute, rule))
						_denyReadAndExecute = true;
					if (Contains(FileSystemRights.ReadAttributes, rule))
						_denyReadAttributes = true;
					if (Contains(FileSystemRights.ReadData, rule))
						_denyReadData = true;
					if (Contains(FileSystemRights.ReadExtendedAttributes, rule))
						_denyReadExtendedAttributes = true;
					if (Contains(FileSystemRights.ReadPermissions, rule))
						_denyReadPermissions = true;
					if (Contains(FileSystemRights.Synchronize, rule))
						_denySynchronize = true;
					if (Contains(FileSystemRights.TakeOwnership, rule))
						_denyTakeOwnership = true;
					if (Contains(FileSystemRights.Traverse, rule))
						_denyTraverse = true;
					if (Contains(FileSystemRights.Write, rule)) _denyWrite = true;
					if (Contains(FileSystemRights.WriteAttributes, rule))
						_denyWriteAttributes = true;
					if (Contains(FileSystemRights.WriteData, rule))
						_denyWriteData = true;
					if (Contains(FileSystemRights.WriteExtendedAttributes, rule))
						_denyWriteExtendedAttributes = true;
				}
				else if (AccessControlType.
					Allow.Equals(rule.AccessControlType))
				{
					if (Contains(FileSystemRights.AppendData, rule))
						_allowAppendData = true;
					if (Contains(FileSystemRights.ChangePermissions, rule))
						_allowChangePermissions = true;
					if (Contains(FileSystemRights.CreateDirectories, rule))
						_allowCreateDirectories = true;
					if (Contains(FileSystemRights.CreateFiles, rule))
						_allowCreateFiles = true;
					if (Contains(FileSystemRights.Delete, rule))
						_allowDelete = true;
					if (Contains(FileSystemRights.DeleteSubdirectoriesAndFiles,
					             rule)) _allowDeleteSubdirectoriesAndFiles = true;
					if (Contains(FileSystemRights.ExecuteFile, rule))
						_allowExecuteFile = true;
					if (Contains(FileSystemRights.FullControl, rule))
						_allowFullControl = true;
					if (Contains(FileSystemRights.ListDirectory, rule))
						_allowListDirectory = true;
					if (Contains(FileSystemRights.Modify, rule))
						_allowModify = true;
					if (Contains(FileSystemRights.Read, rule)) _allowRead = true;
					if (Contains(FileSystemRights.ReadAndExecute, rule))
						_allowReadAndExecute = true;
					if (Contains(FileSystemRights.ReadAttributes, rule))
						_allowReadAttributes = true;
					if (Contains(FileSystemRights.ReadData, rule))
						_allowReadData = true;
					if (Contains(FileSystemRights.ReadExtendedAttributes, rule))
						_allowReadExtendedAttributes = true;
					if (Contains(FileSystemRights.ReadPermissions, rule))
						_allowReadPermissions = true;
					if (Contains(FileSystemRights.Synchronize, rule))
						_allowSynchronize = true;
					if (Contains(FileSystemRights.TakeOwnership, rule))
						_allowTakeOwnership = true;
					if (Contains(FileSystemRights.Traverse, rule))
						_allowTraverse = true;
					if (Contains(FileSystemRights.Write, rule))
						_allowWrite = true;
					if (Contains(FileSystemRights.WriteAttributes, rule))
						_allowWriteAttributes = true;
					if (Contains(FileSystemRights.WriteData, rule))
						_allowWriteData = true;
					if (Contains(FileSystemRights.WriteExtendedAttributes, rule))
						_allowWriteExtendedAttributes = true;
				}
			}

			IdentityReferenceCollection groups = _principal.Groups;
			if (groups != null)
				foreach (IdentityReference t in groups)
				{
					for (int i = 0; i < acl.Count; i++)
					{
						var rule =
							(FileSystemAccessRule) acl[i];
						if (rule != null && t.Equals(rule.IdentityReference))
						{
							if (AccessControlType.
								Deny.Equals(rule.AccessControlType))
							{
								if (Contains(FileSystemRights.AppendData, rule))
									_denyAppendData = true;
								if (Contains(FileSystemRights.ChangePermissions, rule))
									_denyChangePermissions = true;
								if (Contains(FileSystemRights.CreateDirectories, rule))
									_denyCreateDirectories = true;
								if (Contains(FileSystemRights.CreateFiles, rule))
									_denyCreateFiles = true;
								if (Contains(FileSystemRights.Delete, rule))
									_denyDelete = true;
								if (Contains(FileSystemRights.
								             	DeleteSubdirectoriesAndFiles, rule))
									_denyDeleteSubdirectoriesAndFiles = true;
								if (Contains(FileSystemRights.ExecuteFile, rule))
									_denyExecuteFile = true;
								if (Contains(FileSystemRights.FullControl, rule))
									_denyFullControl = true;
								if (Contains(FileSystemRights.ListDirectory, rule))
									_denyListDirectory = true;
								if (Contains(FileSystemRights.Modify, rule))
									_denyModify = true;
								if (Contains(FileSystemRights.Read, rule))
									_denyRead = true;
								if (Contains(FileSystemRights.ReadAndExecute, rule))
									_denyReadAndExecute = true;
								if (Contains(FileSystemRights.ReadAttributes, rule))
									_denyReadAttributes = true;
								if (Contains(FileSystemRights.ReadData, rule))
									_denyReadData = true;
								if (Contains(FileSystemRights.
								             	ReadExtendedAttributes, rule))
									_denyReadExtendedAttributes = true;
								if (Contains(FileSystemRights.ReadPermissions, rule))
									_denyReadPermissions = true;
								if (Contains(FileSystemRights.Synchronize, rule))
									_denySynchronize = true;
								if (Contains(FileSystemRights.TakeOwnership, rule))
									_denyTakeOwnership = true;
								if (Contains(FileSystemRights.Traverse, rule))
									_denyTraverse = true;
								if (Contains(FileSystemRights.Write, rule))
									_denyWrite = true;
								if (Contains(FileSystemRights.WriteAttributes, rule))
									_denyWriteAttributes = true;
								if (Contains(FileSystemRights.WriteData, rule))
									_denyWriteData = true;
								if (Contains(FileSystemRights.
								             	WriteExtendedAttributes, rule))
									_denyWriteExtendedAttributes = true;
							}
							else if (AccessControlType.
								Allow.Equals(rule.AccessControlType))
							{
								if (Contains(FileSystemRights.AppendData, rule))
									_allowAppendData = true;
								if (Contains(FileSystemRights.ChangePermissions, rule))
									_allowChangePermissions = true;
								if (Contains(FileSystemRights.CreateDirectories, rule))
									_allowCreateDirectories = true;
								if (Contains(FileSystemRights.CreateFiles, rule))
									_allowCreateFiles = true;
								if (Contains(FileSystemRights.Delete, rule))
									_allowDelete = true;
								if (Contains(FileSystemRights.
								             	DeleteSubdirectoriesAndFiles, rule))
									_allowDeleteSubdirectoriesAndFiles = true;
								if (Contains(FileSystemRights.ExecuteFile, rule))
									_allowExecuteFile = true;
								if (Contains(FileSystemRights.FullControl, rule))
									_allowFullControl = true;
								if (Contains(FileSystemRights.ListDirectory, rule))
									_allowListDirectory = true;
								if (Contains(FileSystemRights.Modify, rule))
									_allowModify = true;
								if (Contains(FileSystemRights.Read, rule))
									_allowRead = true;
								if (Contains(FileSystemRights.ReadAndExecute, rule))
									_allowReadAndExecute = true;
								if (Contains(FileSystemRights.ReadAttributes, rule))
									_allowReadAttributes = true;
								if (Contains(FileSystemRights.ReadData, rule))
									_allowReadData = true;
								if (Contains(FileSystemRights.
								             	ReadExtendedAttributes, rule))
									_allowReadExtendedAttributes = true;
								if (Contains(FileSystemRights.ReadPermissions, rule))
									_allowReadPermissions = true;
								if (Contains(FileSystemRights.Synchronize, rule))
									_allowSynchronize = true;
								if (Contains(FileSystemRights.TakeOwnership, rule))
									_allowTakeOwnership = true;
								if (Contains(FileSystemRights.Traverse, rule))
									_allowTraverse = true;
								if (Contains(FileSystemRights.Write, rule))
									_allowWrite = true;
								if (Contains(FileSystemRights.WriteAttributes, rule))
									_allowWriteAttributes = true;
								if (Contains(FileSystemRights.WriteData, rule))
									_allowWriteData = true;
								if (Contains(FileSystemRights.WriteExtendedAttributes,
								             rule)) _allowWriteExtendedAttributes = true;
							}
						}
					}
				}
		}

		/// <summary>
		/// Can user append data to the file
		/// </summary>
		/// <returns>True if user can append data to the file</returns>
		public bool CanAppendData()
		{
			return !_denyAppendData && _allowAppendData;
		}

		/// <summary>
		/// Can user change the permissions
		/// </summary>
		/// <returns>True if user can change the permissions</returns>
		public bool CanChangePermissions()
		{
			return !_denyChangePermissions && _allowChangePermissions;
		}

		/// <summary>
		/// Can user create directories
		/// </summary>
		/// <returns>True if user can create directories</returns>
		public bool CanCreateDirectories()
		{
			return !_denyCreateDirectories && _allowCreateDirectories;
		}

		/// <summary>
		/// Can user create files
		/// </summary>
		/// <returns>True if user can create files</returns>
		public bool CanCreateFiles()
		{
			return !_denyCreateFiles && _allowCreateFiles;
		}

		/// <summary>
		/// Can user delete items
		/// </summary>
		/// <returns>True if user can delete items</returns>
		public bool CanDelete()
		{
			return !_denyDelete && _allowDelete;
		}

		/// <summary>
		/// Can user delete subdirectories and files
		/// </summary>
		/// <returns>True if user can delete subdirectories and files</returns>
		public bool CanDeleteSubdirectoriesAndFiles()
		{
			return !_denyDeleteSubdirectoriesAndFiles && _allowDeleteSubdirectoriesAndFiles;
		}

		/// <summary>
		/// Can user execute files
		/// </summary>
		/// <returns>True if user can execute files</returns>
		public bool CanExecuteFile()
		{
			return !_denyExecuteFile && _allowExecuteFile;
		}

		/// <summary>
		/// Does user has a full control
		/// </summary>
		/// <returns>True if user has a full control</returns>
		public bool CanFullControl()
		{
			return !_denyFullControl && _allowFullControl;
		}

		/// <summary>
		/// Can user list directory
		/// </summary>
		/// <returns>True if user can list directory</returns>
		public bool CanListDirectory()
		{
			return !_denyListDirectory && _allowListDirectory;
		}

		/// <summary>
		/// Can user modify item
		/// </summary>
		/// <returns>True if user can modify item</returns>
		public bool CanModify()
		{
			return !_denyModify && _allowModify;
		}

		/// <summary>
		/// Can user read item
		/// </summary>
		/// <returns>True if user can read item</returns>
		public bool CanRead()
		{
			return !_denyRead && _allowRead;
		}

		/// <summary>
		/// Can user read and execute item
		/// </summary>
		/// <returns>True if user can read and execute item</returns>
		public bool CanReadAndExecute()
		{
			return !_denyReadAndExecute && _allowReadAndExecute;
		}

		/// <summary>
		/// Can user read attributes
		/// </summary>
		/// <returns>True if user can read attributes</returns>
		public bool CanReadAttributes()
		{
			return !_denyReadAttributes && _allowReadAttributes;
		}

		/// <summary>
		/// Can user read data
		/// </summary>
		/// <returns>True if user can read data</returns>
		public bool CanReadData()
		{
			return !_denyReadData && _allowReadData;
		}

		/// <summary>
		/// Can user read extended attributes
		/// </summary>
		/// <returns>True if user can read extended attributes</returns>
		public bool CanReadExtendedAttributes()
		{
			return !_denyReadExtendedAttributes &&
			       _allowReadExtendedAttributes;
		}

		/// <summary>
		/// Can user read permissions
		/// </summary>
		/// <returns>True if user can read permissions</returns>
		public bool CanReadPermissions()
		{
			return !_denyReadPermissions && _allowReadPermissions;
		}

		/// <summary>
		/// Can user syncronize
		/// </summary>
		/// <returns>True if user can syncronize</returns>
		public bool CanSynchronize()
		{
			return !_denySynchronize && _allowSynchronize;
		}

		/// <summary>
		/// Can user take ownership
		/// </summary>
		/// <returns>True if user can take ownership</returns>
		public bool CanTakeOwnership()
		{
			return !_denyTakeOwnership && _allowTakeOwnership;
		}

		/// <summary>
		/// Can user traverse
		/// </summary>
		/// <returns>True if user can traverse</returns>
		public bool CanTraverse()
		{
			return !_denyTraverse && _allowTraverse;
		}

		/// <summary>
		/// Can user write
		/// </summary>
		/// <returns>True if user can write</returns>
		public bool CanWrite()
		{
			return !_denyWrite && _allowWrite;
		}

		/// <summary>
		/// Can user write attributes
		/// </summary>
		/// <returns>True if user can write attributes</returns>
		public bool CanWriteAttributes()
		{
			return !_denyWriteAttributes && _allowWriteAttributes;
		}

		/// <summary>
		/// Can user write data
		/// </summary>
		/// <returns>True if user can write data</returns>
		public bool CanWriteData()
		{
			return !_denyWriteData && _allowWriteData;
		}

		/// <summary>
		/// Can user write extended attributes
		/// </summary>
		/// <returns>True if user can write extended attributes</returns>
		public bool CanWriteExtendedAttributes()
		{
			return !_denyWriteExtendedAttributes &&
			       _allowWriteExtendedAttributes;
		}

		/// <summary>
		/// Simple accessor
		/// </summary>
		/// <returns></returns>
		public WindowsIdentity GetWindowsIdentity()
		{
			return _principal;
		}

		/// <summary>
		/// Simple accessor
		/// </summary>
		/// <returns></returns>
		public String GetPath()
		{
			return _path;
		}

		/// <summary>
		/// Simply displays all allowed rights
		/// 
		/// Useful if say you want to test for write access and find
		/// it is false;
		/// <xmp>
		/// UserFileAccessRights rights = new UserFileAccessRights(txtLogPath.Text);
		/// System.IO.FileInfo fi = new System.IO.FileInfo(txtLogPath.Text);
		/// if (rights.canWrite() && rights.canRead()) {
		///     lblLogMsg.Text = "R/W access";
		/// } else {
		///     if (rights.canWrite()) {
		///        lblLogMsg.Text = "Only Write access";
		///     } else if (rights.canRead()) {
		///         lblLogMsg.Text = "Only Read access";
		///     } else {
		///         lblLogMsg.CssClass = "error";
		///         lblLogMsg.Text = rights.ToString()
		///     }
		/// }
		/// 
		/// </xmp>
		/// 
		/// </summary>
		/// <returns></returns>
		public override String ToString()
		{
			string str = "";

			if (CanAppendData())
			{
				if (!String.IsNullOrEmpty(str))
					str +=
						",";
				str += "AppendData";
			}
			if (CanChangePermissions())
			{
				if (!String.IsNullOrEmpty(str))
					str +=
						",";
				str += "ChangePermissions";
			}
			if (CanCreateDirectories())
			{
				if (!String.IsNullOrEmpty(str))
					str +=
						",";
				str += "CreateDirectories";
			}
			if (CanCreateFiles())
			{
				if (!String.IsNullOrEmpty(str))
					str +=
						",";
				str += "CreateFiles";
			}
			if (CanDelete())
			{
				if (!String.IsNullOrEmpty(str))
					str +=
						",";
				str += "Delete";
			}
			if (CanDeleteSubdirectoriesAndFiles())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "DeleteSubdirectoriesAndFiles";
			}
			if (CanExecuteFile())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "ExecuteFile";
			}
			if (CanFullControl())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "FullControl";
			}
			if (CanListDirectory())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "ListDirectory";
			}
			if (CanModify())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "Modify";
			}
			if (CanRead())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "Read";
			}
			if (CanReadAndExecute())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "ReadAndExecute";
			}
			if (CanReadAttributes())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "ReadAttributes";
			}
			if (CanReadData())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "ReadData";
			}
			if (CanReadExtendedAttributes())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "ReadExtendedAttributes";
			}
			if (CanReadPermissions())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "ReadPermissions";
			}
			if (CanSynchronize())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "Synchronize";
			}
			if (CanTakeOwnership())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "TakeOwnership";
			}
			if (CanTraverse())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "Traverse";
			}
			if (CanWrite())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "Write";
			}
			if (CanWriteAttributes())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "WriteAttributes";
			}
			if (CanWriteData())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "WriteData";
			}
			if (CanWriteExtendedAttributes())
			{
				if (!String.IsNullOrEmpty(str))
					str += ",";
				str += "WriteExtendedAttributes";
			}
			if (String.IsNullOrEmpty(str))
				str = "None";
			return str;
		}

		/// <summary>
		/// Convenience method to test if the right exists within the given rights
		/// </summary>
		/// <param name="right"></param>
		/// <param name="rule"></param>
		/// <returns></returns>
		public bool Contains(FileSystemRights right,
		                     FileSystemAccessRule rule)
		{
			return (((int) right & (int) rule.FileSystemRights) == (int) right);
		}
	}
}