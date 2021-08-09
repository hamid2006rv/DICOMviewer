using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace DicomViewers
{
    
    class Global
    {
        static public ImageList ImageList = new ImageList();
        //**************************************************
        [DllImport("kernel32.dll", EntryPoint = "GetDriveTypeA")]
        public static extern int GetDriveType(string nDrive);

        static public string[] getCDRomDrives()
        {
            string rslt = "";
            string[] drvs = Directory.GetLogicalDrives();
            foreach (string s in drvs)
            {
                if (GetDriveType(s) == 5)
                    rslt += s + "#";
            }
            return rslt.Split("#".ToCharArray());
        }
        //**************************************************
    }
}
