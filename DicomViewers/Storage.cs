using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DicomViewers
{
    class Storage
    {
        private class myNode : Object
        {
            public bool is_root;
            public bool is_leaf;
            public bool find_child;
            public int type;
            public int level;
            public string name;
            public myNode():base()
            {
                type = -1;
                level = -1;
                find_child = false;
                name = null;
                is_root = false;
                is_leaf = false;
            }
        }

        private int state = -1;

        public TreeNode GetObject(TreeNode root, TreeNode selectd)
        {
            if (selectd.Tag == null)
                return root;

            if (((myNode)selectd.Tag).type == 0)
            {
                if (((myNode)selectd.Tag).level == 1)
                { 
                StringBuilder volumeName = new StringBuilder(256);
                int rslt = GetVolumeInformation(((myNode)selectd.Tag).name, volumeName, 256, new int(), new int(), new int(), "", 256);
                if (rslt != 0)
                {
                    string[] strs= Directory.GetDirectories(((myNode)selectd.Tag).name);
                    for (int i = 0; i < strs.Length; i++)
                    {
                        TreeNode node = new TreeNode(Path.GetFileName(strs[i]));
                        node.Tag = new myNode();
                        ((myNode)node.Tag).level = 2;
                        ((myNode)node.Tag).name = strs[i];
                        ((myNode)node.Tag).type = 0;
                        selectd.Nodes.Add(node);
                    }
                    selectd.Expand();
                }
                else
                {
                    selectd.Text = ((myNode)selectd.Tag).name + "  is unavailable";
                }
        
                   
                }
            }
            if (((myNode)selectd.Tag).level == 2)
            {
                if (!((myNode)selectd.Tag).find_child)
                {
                    selectd.Nodes.Clear();
                    string[] strs = Directory.GetDirectories(((myNode)selectd.Tag).name);
                    for (int i = 0; i < strs.Length; i++)
                    {
                        TreeNode node = new TreeNode(Path.GetFileName(strs[i]));
                        node.Tag = new myNode();
                        ((myNode)node.Tag).level = 2;
                        ((myNode)node.Tag).name = strs[i];
                        ((myNode)node.Tag).type = 0;
                        if (Directory.GetDirectories(strs[i]).Length > 0 || Directory.GetFiles(strs[i]).Length > 0)
                        {
                            node.Nodes.Add("");
                        }
                        selectd.Nodes.Add(node);
                    }
                    ((myNode)selectd.Tag).find_child = true;
                    selectd.Expand();
                }
            }
            return root;
        }
        
        public TreeNode GetCDs()
        {
            state = 0;
            TreeNode root = new TreeNode("Cd drives");
            root.Tag = new myNode();
            ((myNode)root.Tag).type = 0;
            ((myNode)root.Tag).is_root = true;
            ((myNode)root.Tag).name = "Cd drives";
            string[] drves = getCdDrives();
            for (int i = 0; i < drves.Length; i++)
            {
                TreeNode tn = new TreeNode(drves[i]);
                root.Nodes.Add(tn);
                tn.Tag =new myNode();
                ((myNode)tn.Tag).type = 0;
                ((myNode)tn.Tag).level = 1;
                ((myNode)tn.Tag).name = drves[i];

            }
            root.Expand();
            return root;
        }

        string[] getCdDrives()
        {
            List<string> list = new List<string>();
            string[] strs = Directory.GetLogicalDrives();

            foreach (string str in strs)
            {
                if (GetDriveType(str) == 5)
                {
                    list.Add(str);
                }
            }
            return list.ToArray();  
        }

        [DllImport("kernel32.dll", EntryPoint = "GetDriveTypeA")]
        static extern int GetDriveType(string nDrive);

        [DllImport("kernel32.dll", EntryPoint = "GetVolumeInformationA")]
        static extern int GetVolumeInformation(string lpRootPathName,
            StringBuilder lpVolumeNameBuffer, int nVolumeNameSize,
            int lpVolumeSerialNumber, int lpMaximumComponentLength,
            int lpFileSystemFlags, string lpFileSystemNameBuffer,
            int nFileSystemNameSize);
    }
}
