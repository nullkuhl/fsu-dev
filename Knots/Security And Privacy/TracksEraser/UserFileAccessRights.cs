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
		readonly bool allowAppendData;
		readonly bool allowChangePermissions;
		readonly bool allowCreateDirectories;
		readonly bool allowCreateFiles;
		readonly bool allowDelete;
		readonly bool allowDeleteSubdirectoriesAndFiles;
		readonly bool allowExecuteFile;
		readonly bool allowFullControl;
		readonly bool allowListDirectory;
		readonly bool allowModify;
		readonly bool allowRead;
		readonly bool allowReadAndExecute;
		readonly bool allowReadAttributes;
		readonly bool allowReadData;
		readonly bool allowReadExtendedAttributes;
		readonly bool allowReadPermissions;
		readonly bool allowSynchronize;
		readonly bool allowTakeOwnership;
		readonly bool allowTraverse;
		readonly bool allowWrite;
		readonly bool allowWriteAttributes;
		readonly bool allowWriteData;
		readonly bool allowWriteExtendedAttributes;
		readonly bool denyAppendData;
		readonly bool denyChangePermissions;
		readonly bool denyCreateDirectories;
		readonly bool denyCreateFiles;
		readonly bool denyDelete;
		readonly bool denyDeleteSubdirectoriesAndFiles;
		readonly bool denyExecuteFile;
		readonly bool denyFullControl;
		readonly bool denyListDirectory;
		readonly bool denyModify;
		readonly bool denyRead;
		readonly bool denyReadAndExecute;
		readonly bool denyReadAttributes;
		readonly bool denyReadData;
		readonly bool denyReadExtendedAttributes;
		readonly bool denyReadPermissions;
		readonly bool denySynchronize;
		readonly bool denyTakeOwnership;
		readonly bool denyTraverse;
		readonly bool denyWrite;
		readonly bool denyWriteAttributes;
		readonly bool denyWriteData;
		readonly bool denyWriteExtendedAttributes;
		readonly string path;
		readonly WindowsIdentity principal;

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
			this.path = path;
			this.principal = principal;

			var fi = new FileInfo(this.path);
			AuthorizationRuleCollection acl = fi.GetAccessControl().GetAccessRules
				(true, true, typeof (SecurityIdentifier));
			for (int i = 0; i < acl.Count; i++)
			{
				var rule =
					(FileSystemAccessRule) acl[i];
				if (rule == null || (this.principal.User == null || !this.principal.User.Equals(rule.IdentityReference))) continue;
				if (AccessControlType.Deny.Equals
					(rule.AccessControlType))
				{
					if (Contains(FileSystemRights.AppendData, rule))
						denyAppendData = true;
					if (Contains(FileSystemRights.ChangePermissions, rule))
						denyChangePermissions = true;
					if (Contains(FileSystemRights.CreateDirectories, rule))
						denyCreateDirectories = true;
					if (Contains(FileSystemRights.CreateFiles, rule))
						denyCreateFiles = true;
					if (Contains(FileSystemRights.Delete, rule))
						denyDelete = true;
					if (Contains(FileSystemRights.DeleteSubdirectoriesAndFiles,
					             rule)) denyDeleteSubdirectoriesAndFiles = true;
					if (Contains(FileSystemRights.ExecuteFile, rule))
						denyExecuteFile = true;
					if (Contains(FileSystemRights.FullControl, rule))
						denyFullControl = true;
					if (Contains(FileSystemRights.ListDirectory, rule))
						denyListDirectory = true;
					if (Contains(FileSystemRights.Modify, rule))
						denyModify = true;
					if (Contains(FileSystemRights.Read, rule)) denyRead = true;
					if (Contains(FileSystemRights.ReadAndExecute, rule))
						denyReadAndExecute = true;
					if (Contains(FileSystemRights.ReadAttributes, rule))
						denyReadAttributes = true;
					if (Contains(FileSystemRights.ReadData, rule))
						denyReadData = true;
					if (Contains(FileSystemRights.ReadExtendedAttributes, rule))
						denyReadExtendedAttributes = true;
					if (Contains(FileSystemRights.ReadPermissions, rule))
						denyReadPermissions = true;
					if (Contains(FileSystemRights.Synchronize, rule))
						denySynchronize = true;
					if (Contains(FileSystemRights.TakeOwnership, rule))
						denyTakeOwnership = true;
					if (Contains(FileSystemRights.Traverse, rule))
						denyTraverse = true;
					if (Contains(FileSystemRights.Write, rule)) denyWrite = true;
					if (Contains(FileSystemRights.WriteAttributes, rule))
						denyWriteAttributes = true;
					if (Contains(FileSystemRights.WriteData, rule))
						denyWriteData = true;
					if (Contains(FileSystemRights.WriteExtendedAttributes, rule))
						denyWriteExtendedAttributes = true;
				}
				else if (AccessControlType.
					Allow.Equals(rule.AccessControlType))
				{
					if (Contains(FileSystemRights.AppendData, rule))
						allowAppendData = true;
					if (Contains(FileSystemRights.ChangePermissions, rule))
						allowChangePermissions = true;
					if (Contains(FileSystemRights.CreateDirectories, rule))
						allowCreateDirectories = true;
					if (Contains(FileSystemRights.CreateFiles, rule))
						allowCreateFiles = true;
					if (Contains(FileSystemRights.Delete, rule))
						allowDelete = true;
					if (Contains(FileSystemRights.DeleteSubdirectoriesAndFiles,
					             rule)) allowDeleteSubdirectoriesAndFiles = true;
					if (Contains(FileSystemRights.ExecuteFile, rule))
						allowExecuteFile = true;
					if (Contains(FileSystemRights.FullControl, rule))
						allowFullControl = true;
					if (Contains(FileSystemRights.ListDirectory, rule))
						allowListDirectory = true;
					if (Contains(FileSystemRights.Modify, rule))
						allowModify = true;
					if (Contains(FileSystemRights.Read, rule)) allowRead = true;
					if (Contains(FileSystemRights.ReadAndExecute, rule))
						allowReadAndExecute = true;
					if (Contains(FileSystemRights.ReadAttributes, rule))
						allowReadAttributes = true;
					if (Contains(FileSystemRights.ReadData, rule))
						allowReadData = true;
					if (Contains(FileSystemRights.ReadExtendedAttributes, rule))
						allowReadExtendedAttributes = true;
					if (Contains(FileSystemRights.ReadPermissions, rule))
						allowReadPermissions = true;
					if (Contains(FileSystemRights.Synchronize, rule))
						allowSynchronize = true;
					if (Contains(FileSystemRights.TakeOwnership, rule))
						allowTakeOwnership = true;
					if (Contains(FileSystemRights.Traverse, rule))
						allowTraverse = true;
					if (Contains(FileSystemRights.Write, rule))
						allowWrite = true;
					if (Contains(FileSystemRights.WriteAttributes, rule))
						allowWriteAttributes = true;
					if (Contains(FileSystemRights.WriteData, rule))
						allowWriteData = true;
					if (Contains(FileSystemRights.WriteExtendedAttributes, rule))
						allowWriteExtendedAttributes = true;
				}
			}

			IdentityReferenceCollection groups = this.principal.Groups;
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
									denyAppendData = true;
								if (Contains(FileSystemRights.ChangePermissions, rule))
									denyChangePermissions = true;
								if (Contains(FileSystemRights.CreateDirectories, rule))
									denyCreateDirectories = true;
								if (Contains(FileSystemRights.CreateFiles, rule))
									denyCreateFiles = true;
								if (Contains(FileSystemRights.Delete, rule))
									denyDelete = true;
								if (Contains(FileSystemRights.
								             	DeleteSubdirectoriesAndFiles, rule))
									denyDeleteSubdirectoriesAndFiles = true;
								if (Contains(FileSystemRights.ExecuteFile, rule))
									denyExecuteFile = true;
								if (Contains(FileSystemRights.FullControl, rule))
									denyFullControl = true;
								if (Contains(FileSystemRights.ListDirectory, rule))
									denyListDirectory = true;
								if (Contains(FileSystemRights.Modify, rule))
									denyModify = true;
								if (Contains(FileSystemRights.Read, rule))
									denyRead = true;
								if (Contains(FileSystemRights.ReadAndExecute, rule))
									denyReadAndExecute = true;
								if (Contains(FileSystemRights.ReadAttributes, rule))
									denyReadAttributes = true;
								if (Contains(FileSystemRights.ReadData, rule))
									denyReadData = true;
								if (Contains(FileSystemRights.
								             	ReadExtendedAttributes, rule))
									denyReadExtendedAttributes = true;
								if (Contains(FileSystemRights.ReadPermissions, rule))
									denyReadPermissions = true;
								if (Contains(FileSystemRights.Synchronize, rule))
									denySynchronize = true;
								if (Contains(FileSystemRights.TakeOwnership, rule))
									denyTakeOwnership = true;
								if (Contains(FileSystemRights.Traverse, rule))
									denyTraverse = true;
								if (Contains(FileSystemRights.Write, rule))
									denyWrite = true;
								if (Contains(FileSystemRights.WriteAttributes, rule))
									denyWriteAttributes = true;
								if (Contains(FileSystemRights.WriteData, rule))
									denyWriteData = true;
								if (Contains(FileSystemRights.
								             	WriteExtendedAttributes, rule))
									denyWriteExtendedAttributes = true;
							}
							else if (AccessControlType.
								Allow.Equals(rule.AccessControlType))
							{
								if (Contains(FileSystemRights.AppendData, rule))
									allowAppendData = true;
								if (Contains(FileSystemRights.ChangePermissions, rule))
									allowChangePermissions = true;
								if (Contains(FileSystemRights.CreateDirectories, rule))
									allowCreateDirectories = true;
								if (Contains(FileSystemRights.CreateFiles, rule))
									allowCreateFiles = true;
								if (Contains(FileSystemRights.Delete, rule))
									allowDelete = true;
								if (Contains(FileSystemRights.
								             	DeleteSubdirectoriesAndFiles, rule))
									allowDeleteSubdirectoriesAndFiles = true;
								if (Contains(FileSystemRights.ExecuteFile, rule))
									allowExecuteFile = true;
								if (Contains(FileSystemRights.FullControl, rule))
									allowFullControl = true;
								if (Contains(FileSystemRights.ListDirectory, rule))
									allowListDirectory = true;
								if (Contains(FileSystemRights.Modify, rule))
									allowModify = true;
								if (Contains(FileSystemRights.Read, rule))
									allowRead = true;
								if (Contains(FileSystemRights.ReadAndExecute, rule))
									allowReadAndExecute = true;
								if (Contains(FileSystemRights.ReadAttributes, rule))
									allowReadAttributes = true;
								if (Contains(FileSystemRights.ReadData, rule))
									allowReadData = true;
								if (Contains(FileSystemRights.
								             	ReadExtendedAttributes, rule))
									allowReadExtendedAttributes = true;
								if (Contains(FileSystemRights.ReadPermissions, rule))
									allowReadPermissions = true;
								if (Contains(FileSystemRights.Synchronize, rule))
									allowSynchronize = true;
								if (Contains(FileSystemRights.TakeOwnership, rule))
									allowTakeOwnership = true;
								if (Contains(FileSystemRights.Traverse, rule))
									allowTraverse = true;
								if (Contains(FileSystemRights.Write, rule))
									allowWrite = true;
								if (Contains(FileSystemRights.WriteAttributes, rule))
									allowWriteAttributes = true;
								if (Contains(FileSystemRights.WriteData, rule))
									allowWriteData = true;
								if (Contains(FileSystemRights.WriteExtendedAttributes,
								             rule)) allowWriteExtendedAttributes = true;
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
			return !denyAppendData && allowAppendData;
		}

		/// <summary>
		/// Can user change the permissions
		/// </summary>
		/// <returns>True if user can change the permissions</returns>
		public bool CanChangePermissions()
		{
			return !denyChangePermissions && allowChangePermissions;
		}

		/// <summary>
		/// Can user create directories
		/// </summary>
		/// <returns>True if user can create directories</returns>
		public bool CanCreateDirectories()
		{
			return !denyCreateDirectories && allowCreateDirectories;
		}

		/// <summary>
		/// Can user create files
		/// </summary>
		/// <returns>True if user can create files</returns>
		public bool CanCreateFiles()
		{
			return !denyCreateFiles && allowCreateFiles;
		}

		/// <summary>
		/// Can user delete items
		/// </summary>
		/// <returns>True if user can delete items</returns>
		public bool CanDelete()
		{
			return !denyDelete && allowDelete;
		}

		/// <summary>
		/// Can user delete subdirectories and files
		/// </summary>
		/// <returns>True if user can delete subdirectories and files</returns>
		public bool CanDeleteSubdirectoriesAndFiles()
		{
			return !denyDeleteSubdirectoriesAndFiles && allowDeleteSubdirectoriesAndFiles;
		}

		/// <summary>
		/// Can user execute files
		/// </summary>
		/// <returns>True if user can execute files</returns>
		public bool CanExecuteFile()
		{
			return !denyExecuteFile && allowExecuteFile;
		}

		/// <summary>
		/// Does user has a full control
		/// </summary>
		/// <returns>True if user has a full control</returns>
		public bool CanFullControl()
		{
			return !denyFullControl && allowFullControl;
		}

		/// <summary>
		/// Can user list directory
		/// </summary>
		/// <returns>True if user can list directory</returns>
		public bool CanListDirectory()
		{
			return !denyListDirectory && allowListDirectory;
		}

		/// <summary>
		/// Can user modify item
		/// </summary>
		/// <returns>True if user can modify item</returns>
		public bool CanModify()
		{
			return !denyModify && allowModify;
		}

		/// <summary>
		/// Can user read item
		/// </summary>
		/// <returns>True if user can read item</returns>
		public bool CanRead()
		{
			return !denyRead && allowRead;
		}

		/// <summary>
		/// Can user read and execute item
		/// </summary>
		/// <returns>True if user can read and execute item</returns>
		public bool CanReadAndExecute()
		{
			return !denyReadAndExecute && allowReadAndExecute;
		}

		/// <summary>
		/// Can user read attributes
		/// </summary>
		/// <returns>True if user can read attributes</returns>
		public bool CanReadAttributes()
		{
			return !denyReadAttributes && allowReadAttributes;
		}

		/// <summary>
		/// Can user read data
		/// </summary>
		/// <returns>True if user can read data</returns>
		public bool CanReadData()
		{
			return !denyReadData && allowReadData;
		}

		/// <summary>
		/// Can user read extended attributes
		/// </summary>
		/// <returns>True if user can read extended attributes</returns>
		public bool CanReadExtendedAttributes()
		{
			return !denyReadExtendedAttributes &&
			       allowReadExtendedAttributes;
		}

		/// <summary>
		/// Can user read permissions
		/// </summary>
		/// <returns>True if user can read permissions</returns>
		public bool CanReadPermissions()
		{
			return !denyReadPermissions && allowReadPermissions;
		}

		/// <summary>
		/// Can user syncronize
		/// </summary>
		/// <returns>True if user can syncronize</returns>
		public bool CanSynchronize()
		{
			return !denySynchronize && allowSynchronize;
		}

		/// <summary>
		/// Can user take ownership
		/// </summary>
		/// <returns>True if user can take ownership</returns>
		public bool CanTakeOwnership()
		{
			return !denyTakeOwnership && allowTakeOwnership;
		}

		/// <summary>
		/// Can user traverse
		/// </summary>
		/// <returns>True if user can traverse</returns>
		public bool CanTraverse()
		{
			return !denyTraverse && allowTraverse;
		}

		/// <summary>
		/// Can user write
		/// </summary>
		/// <returns>True if user can write</returns>
		public bool CanWrite()
		{
			return !denyWrite && allowWrite;
		}

		/// <summary>
		/// Can user write attributes
		/// </summary>
		/// <returns>True if user can write attributes</returns>
		public bool CanWriteAttributes()
		{
			return !denyWriteAttributes && allowWriteAttributes;
		}

		/// <summary>
		/// Can user write data
		/// </summary>
		/// <returns>True if user can write data</returns>
		public bool CanWriteData()
		{
			return !denyWriteData && allowWriteData;
		}

		/// <summary>
		/// Can user write extended attributes
		/// </summary>
		/// <returns>True if user can write extended attributes</returns>
		public bool CanWriteExtendedAttributes()
		{
			return !denyWriteExtendedAttributes &&
			       allowWriteExtendedAttributes;
		}

		/// <summary>
		/// Simple accessor
		/// </summary>
		/// <returns></returns>
		public WindowsIdentity GetWindowsIdentity()
		{
			return principal;
		}

		/// <summary>
		/// Simple accessor
		/// </summary>
		/// <returns></returns>
		public String GetPath()
		{
			return path;
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