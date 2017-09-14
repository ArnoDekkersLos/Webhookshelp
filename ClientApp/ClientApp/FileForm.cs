using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class FileForm : Form
    {
        public FileModel NewFile { get; private set; }
        private bool ExceptionThrown = false;

        public FileForm()
        {
            InitializeComponent();
            NewFile = new FileModel();
            NewFile.Version = 0;
        }

        private void btnConfirmNewFileDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewFileName.Text == "" || txtNewFileName.Text.Contains(@"\") || txtNewFileName.Text.Contains("."))
                {
                    throw new Exception("the File name was empty or contained an invallid character");
                }else if(txtNewFilePath.Text == ""){

                }
                else if (txtNewFilePath.Text.Contains(".") || txtNewFilePath.Text.Substring(txtNewFilePath.Text.Length - 1, 1) != "\\")
                {
                    throw new Exception("The path contained at least 1 invallid character or didn't end with \\");
                }
                NewFile.FolderPath = txtNewFilePath.Text;
                NewFile.FileName = txtNewFileName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invallid input: " + ex.Message);
                ExceptionThrown = true;
            }
        }

        private void FileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ExceptionThrown)
            {
                e.Cancel = true;
                ExceptionThrown = false;
            }
        }
    }
}
