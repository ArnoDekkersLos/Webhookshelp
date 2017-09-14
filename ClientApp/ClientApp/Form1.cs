using ClientApp.Models;
using ClientApp.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        IServerConnector server;

        public Form1()
        {
            InitializeComponent();
            //server = new RESTServer();
            server = new CustomRESTServer();
            //server = new SocketServer();
            //server.ExecuteComplex1();
            //server.ExecuteComplex2();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text != "" && txtPassword.Text != "")
            {
                if (!server.SignIn(txtUserName.Text, txtPassword.Text))
                {
                    MessageBox.Show("Login failed");
                }
                else
                {
                    MessageBox.Show("Login successfull");
                }
            }
            else
            {
                MessageBox.Show("Supply the request with a user name and password");
            }
        }

        private void btnSearchEmployees_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(txtId.Text, out id) && id >= 0)
            {
                Employee foundEmployee = server.GetEmployeeWithTeammembers(id);
                if (foundEmployee != null)
                {
                    dgEmployees.Rows.Clear();
                    dgEmployees.Rows.Add(foundEmployee.Id, foundEmployee.FirstName, foundEmployee.Tussenvoegsel, foundEmployee.LastName);
                    foreach (Employee emp in foundEmployee.TeamMembers)
                    {
                        if (emp != null)
                        {
                            dgEmployees.Rows.Add(emp.Id, emp.FirstName, emp.Tussenvoegsel, emp.LastName);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No employee found or not authorized see output");
                }
            }
            else
            {
                MessageBox.Show("voer een geldig ID in(een rond nummer groter dan 0)");
            }
        }

        private void dgEmployees_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow changedRow = dgEmployees.Rows[e.RowIndex];
                Employee changedEmployee = new Employee(Int32.Parse(changedRow.Cells[0].Value.ToString()), (changedRow.Cells[1].Value ?? "").ToString(), (changedRow.Cells[2].Value ?? "").ToString(), (changedRow.Cells[3].Value ?? "").ToString());
                server.UpdateEmployee(changedEmployee);
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            RefreshListBox();
        }

        private void RefreshListBox()
        {
            List<FileModel> files = server.FetchFiles();
            lbFiles.Items.Clear();
            foreach (FileModel f in files)
            {
                lbFiles.Items.Add(f);
            }
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex >= 0)
            {
                FileModel selected = (FileModel)lbFiles.SelectedItem;
                server.DownloadFile(selected);
            }
            else
            {
                MessageBox.Show("Selecteer eerst de file die u wilt downloaden.");
            }
            RefreshListBox();
        }

        private void btnUpdateFile_Click(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex >= 0)
            {
                FileModel selected = (FileModel)lbFiles.SelectedItem;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK && openFileDialog1.CheckFileExists)
                {
                    Stream fileStream = openFileDialog1.OpenFile();
                    server.UpdateFile(fileStream, selected);
                }
                else
                {
                    MessageBox.Show("No file selected");
                }
                //server.UpdateFile(fileStream, selected);
            }
            else
            {
                MessageBox.Show("select the file that you want to update");
            }
            RefreshListBox();
        }

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK && openFileDialog1.CheckFileExists)
            {
                Stream fileStream = openFileDialog1.OpenFile();
                FileForm fileForm = new FileForm();
                fileForm.ShowDialog();

                FileModel newFileModel = fileForm.NewFile;
                //Add the file extension from the selected file
                newFileModel.FileName = newFileModel.FileName + openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("."));
                server.UpdateFile(fileStream, newFileModel);
            }
            else
            {
                MessageBox.Show("No file selected");
            }
            RefreshListBox();
        }
    }
}
