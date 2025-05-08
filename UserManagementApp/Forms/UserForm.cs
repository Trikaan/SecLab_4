using UserManagementApp.Managers;
using UserManagementApp.Models;

namespace UserManagementApp.Forms
{
    public class UserForm : Form
    {
        private readonly UserManager _userManager;
        private readonly User _currentUser;
        private Label lblWelcome;
        private Button btnChangePassword;
        private Button btnExit;

        public UserForm(UserManager userManager, User currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            InitializeComponent();
            lblWelcome.Text = $"Welcome, {_currentUser.Username}!";
        }

        private void InitializeComponent()
        {
            this.btnChangePassword = new Button();
            this.btnExit = new Button();
            this.lblWelcome = new Label();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(12, 15);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(200, 13);
            this.lblWelcome.Text = "Welcome!";

            // btnChangePassword
            this.btnChangePassword.Location = new System.Drawing.Point(12, 41);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(100, 23);
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.Click += new EventHandler(this.btnChangePassword_Click);

            // btnExit
            this.btnExit.Location = new System.Drawing.Point(118, 41);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);

            // UserForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 76);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.lblWelcome);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UserForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "User Panel";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            using (var form = new ChangePasswordForm(_currentUser.Username))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _userManager.ChangePassword(_currentUser.Username, form.NewPassword);
                    MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
} 