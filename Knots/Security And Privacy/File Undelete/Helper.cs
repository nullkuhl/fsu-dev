using System.Text.RegularExpressions;

namespace FileUndelete
{
    public class Helper
    {
        /// <summary>
        /// Converts wildcard string <paramref Name="pattern"/> to a valid regex
        /// </summary>
        /// <param Name="pattern">Wildcard string to be converted</param>
        /// <returns>Valid regex from provided wildcard</returns>
        public static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern).
                            Replace("\\*", ".*").
                            Replace("\\?", ".") + "$";
        }

        /// <summary>
        /// Checks that <paramref Name="value"/> contains <paramref Name="pattern"/>
        /// </summary>
        /// <param Name="pattern">Pattern to search in <paramref Name="value"/></param>
        /// <param Name="value">String value</param>
        /// <returns>True if <paramref Name="value"/> contains <paramref Name="pattern"/></returns>
        public static bool PathMatch(string pattern, string value)
        {
            try
            {
                return value.ToLower().Contains(pattern.ToLower());
            }
            catch
            {
                return value.ToUpper().IndexOf(pattern.ToUpper()) == 0;
            }
        }
    }

    #region Helper class definitions

    #region Nested type: Constants

    /// <summary>
    /// Contains string constants for a label texts
    /// </summary>
    public abstract class Constants
    {
        /// <summary>
        /// Filter off
        /// </summary>
        public const string FilterOFF = "Filter: OFF";
        /// <summary>
        /// Filter on
        /// </summary>
        public const string FilterON = "Filter: ON";
        /// <summary>
        /// Found
        /// </summary>
        public const string Found = "Found: {0}";
    }

    #endregion

    #region Nested type: FileToRestore

    /// <summary>
    /// A model class for a FileToRestore logical entity
    /// </summary>
    class FileToRestore
    {
        /// <summary>
        /// Id
        /// </summary>
        public readonly ulong FileId;
        /// <summary>
        /// Path
        /// </summary>
        public readonly string FilePath;
        /// <summary>
        /// Is recoverable
        /// </summary>
        public readonly bool IsRecoverable;
        /// <summary>
        /// Size
        /// </summary>
        public readonly uint Size;

        /// <summary>
        /// <see cref="FileToRestore"/> constructor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <param name="canRecover"></param>
        /// <param name="size"></param>
        public FileToRestore(string path, ulong id, bool canRecover, uint size)
        {
            FilePath = path;
            FileId = id;
            IsRecoverable = canRecover;
            Size = size;
        }
    }

    #endregion

    #endregion
}
