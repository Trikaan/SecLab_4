using UserManagementApp.Managers;

namespace UserManagementApp.Forms
{
    public class ChangePasswordForm : Form
    {
        private readonly string _username;
        private readonly UserManager _userManager;

        public string NewPassword { get; private set; }

        public ChangePasswordForm(string username)
        {
            InitializeComponent();
            _username = username;
            _userManager = new UserManager();
        }

        private void InitializeComponent()
        {
            this.txtOldPassword = new TextBox();
            this.txtNewPassword = new TextBox();
            this.txtConfirmPassword = new TextBox();
            this.btnChange = new Button();
            this.btnCancel = new Button();
            this.lblOldPassword = new Label();
            this.lblNewPassword = new Label();
            this.lblConfirmPassword = new Label();
            this.SuspendLayout();

            // lblOldPassword
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(12, 15);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(75, 13);
            this.lblOldPassword.Text = "Old Password:";

            // txtOldPassword
            this.txtOldPassword.Location = new System.Drawing.Point(12, 31);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(260, 20);

            // lblNewPassword
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(12, 54);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(81, 13);
            this.lblNewPassword.Text = "New Password:";

            // txtNewPassword
            this.txtNewPassword.Location = new System.Drawing.Point(12, 70);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(260, 20);

            // lblConfirmPassword
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(12, 93);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(91, 13);
            this.lblConfirmPassword.Text = "Confirm Password:";

            // txtConfirmPassword
            this.txtConfirmPassword.Location = new System.Drawing.Point(12, 109);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(260, 20);

            // btnChange
            this.btnChange.Location = new System.Drawing.Point(116, 135);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.Text = "Change";
            this.btnChange.Click += new EventHandler(this.btnChange_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(197, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // ChangePasswordForm
            this.AcceptButton = this.btnChange;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 170);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.lblOldPassword);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Change Password";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            var users = _userManager.GetAllUsers();
            var user = users.Find(u => u.Username == _username);

            if (user == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (user.Password != txtOldPassword.Text)
            {
                MessageBox.Show("Old password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                MessageBox.Show("Please enter a new password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("New passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (user.PasswordRestricted)
            {
                if (!ValidatePassword(txtNewPassword.Text))
                {
                    MessageBox.Show("Password does not meet the requirements:\n- Minimum 6 characters\n- At least one uppercase letter\n- At least one number\n- At least one special character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            NewPassword = txtNewPassword.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidatePassword(string password)
        {
            if (password.Length < 6)
                return false;

            bool hasUpper = false;
            bool hasNumber = false;
            bool hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUpper = true;
                else if (char.IsDigit(c))
                    hasNumber = true;
                else if (!char.IsLetterOrDigit(c))
                    hasSpecial = true;
            }

            return hasUpper && hasNumber && hasSpecial;
        }

        private TextBox txtOldPassword;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Button btnChange;
        private Button btnCancel;
        private Label lblOldPassword;
        private Label lblNewPassword;
        private Label lblConfirmPassword;
    }
} 