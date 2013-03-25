using System;
using System.IO;
using System.Text;

namespace FreemiumUtilities.TracksEraser
{
    /// <summary>
    /// Cookie view
    /// </summary>
    public class cCookieView
    {
        static string domain;
        static string secure;

        /// <summary>
        /// constructor for cCookieView
        /// </summary>
        /// <param name="localname"></param>
        public cCookieView(string localname)
        {
            //Get file data
            string fData = GetfData(localname);
            string[] lines = fData.Split(Environment.NewLine.ToCharArray());
            //Get domain name
            domain = lines[2];

            if (domain.EndsWith("/"))
            {
                //Get last / position
                int sPos = domain.LastIndexOf("/");
                //Rip of last /
                domain = domain.Substring(0, sPos);
            }

            //Check if cookie is secure
            secure = lines[3];

            secure = secure == "0" ? "NO" : "YES";
        }

        /// <summary>
        /// Secure
        /// </summary>
        public string Secure
        {
            get { return secure; }
        }

        //Domain
        public string Domain
        {
            get { return domain; }
        }

        /// <summary>
        /// get string representation of cookie file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetfData(string filename)
        {
            string sData = string.Empty;
            try
            {
                using (BinaryReader br = new BinaryReader(File.OpenRead(filename)))
                {
                    byte[] Bytes = br.ReadBytes((int)br.BaseStream.Length);
                    sData = Encoding.Default.GetString(Bytes);
                }
            }
            catch
            {
                throw new BadFile("Cookie Not Found");
            }
            return sData;
        }
    }

    internal class BadFile : ApplicationException
    {
        /// <summary>
        /// Bad file exception
        /// </summary>
        public BadFile()
        {
        }

        /// <summary>
        /// Bad file exception
        /// </summary>
        public BadFile(string s)
            : base(s)
        {
        }

        /// <summary>
        /// Bad file exception
        /// </summary>
        public BadFile(string s, Exception ex)
            : base(s, ex)
        {
        }
    }
}