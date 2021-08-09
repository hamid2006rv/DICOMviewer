using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DicomViewers
{
    public partial class Form1 : Form
    {
        #region properties that affect on this form 's controls
        private string _root_directory;
        private string _curent_directory;

        public string Root_Directory
        {
            set {
                if (_root_directory != value)
                {
                    treeListView1.Clear();
                    switch (value)
                    { 
                    case "cd"
                    }
                    treeView1.Nodes.Add();
                }

                _root_directory = value;

            }
            get { return _root_directory; }
        }//CD Folder  MyDicom Network
        #endregion
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Bounds = Screen.PrimaryScreen.Bounds;
            menuStrip1.Visible = false;
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y > 0 && e.Y<5 )
                menuStrip1.Visible = true;
            else
                menuStrip1.Visible = false;
        }

        private void bToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:

                    break;
                case 1: break;
                case 2: break;
                case 3: break;

            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        
    }
}