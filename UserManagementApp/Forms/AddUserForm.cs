namespace UserManagementApp.Forms
{
    public class AddUserForm : Form
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool PasswordRestricted { get; private set; }

        public AddUserForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.txtConfirmPassword = new TextBox();
            this.chkPasswordRestricted = new CheckBox();
            this.btnAdd = new Button();
            this.btnCancel = new Button();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblConfirmPassword = new Label();
            this.SuspendLayout();

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(12, 15);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.Text = "Username:";

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(12, 31);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(260, 20);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 54);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.Text = "Password:";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(12, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(260, 20);

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

            // chkPasswordRestricted
            this.chkPasswordRestricted.AutoSize = true;
            this.chkPasswordRestricted.Location = new System.Drawing.Point(12, 135);
            this.chkPasswordRestricted.Name = "chkPasswordRestricted";
            this.chkPasswordRestricted.Size = new System.Drawing.Size(114, 17);
            this.chkPasswordRestricted.Text = "Password Restricted";
            this.chkPasswordRestricted.Checked = true;

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(116, 158);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(197, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // AddUserForm
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 193);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.chkPasswordRestricted);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUserForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Add New User";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chkPasswordRestricted.Checked)
            {
                if (!ValidatePassword(txtPassword.Text))
                {
                    MessageBox.Show("Пароль не відповідає вимогам: \n- рядкові і прописні букви \n- цифри\n- знаки арифметичних операцій", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Username = txtUsername.Text;
            Password = txtPassword.Text;
            PasswordRestricted = chkPasswordRestricted.Checked;

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
            if (password == null || password.Length < 6)
                return false;

            bool hasLowerLatin = false;
            bool hasUpperLatin = false;
            bool hasDigit = false;
            bool hasCyrillic = false;

            foreach (char c in password)
            {
                if (char.IsLower(c) && c >= 'a' && c <= 'z') // Lowercase Latin (a–z)
                    hasLowerLatin = true;
                else if (char.IsUpper(c) && c >= 'A' && c <= 'Z') // Uppercase Latin (A–Z)
                    hasUpperLatin = true;
                else if (char.IsDigit(c)) // Digits (0–9)
                    hasDigit = true;
                else if ((c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я') || c == 'ё' || c == 'Ё') // Cyrillic characters
                    hasCyrillic = true;
            }

            return hasLowerLatin && hasUpperLatin && hasDigit && hasCyrillic;
        }

        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private CheckBox chkPasswordRestricted;
        private Button btnAdd;
        private Button btnCancel;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblConfirmPassword;
    }
} 