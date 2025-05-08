using UserManagementApp.Managers;
using UserManagementApp.Models;

namespace UserManagementApp.Forms
{
    public class AdminForm : Form
    {
        private readonly UserManager _userManager;
        private ListView _usersListView;

        public AdminForm(UserManager userManager)
        {
            InitializeComponent();
            _userManager = userManager;
            LoadUsers();
        }

        private void InitializeComponent()
        {
            this._usersListView = new ListView();
            this.btnAddUser = new Button();
            this.btnBlockUser = new Button();
            this.btnToggleRestrictions = new Button();
            this.btnChangePassword = new Button();
            this.btnExit = new Button();
            this.SuspendLayout();

            // _usersListView
            this._usersListView.FullRowSelect = true;
            this._usersListView.GridLines = true;
            this._usersListView.Location = new System.Drawing.Point(12, 12);
            this._usersListView.Name = "_usersListView";
            this._usersListView.Size = new System.Drawing.Size(560, 200);
            this._usersListView.TabIndex = 0;
            this._usersListView.UseCompatibleStateImageBehavior = false;
            this._usersListView.View = View.Details;
            this._usersListView.Columns.Add("Username", 150);
            this._usersListView.Columns.Add("Status", 100);
            this._usersListView.Columns.Add("Password Restrictions", 150);

            // btnAddUser
            this.btnAddUser.Location = new System.Drawing.Point(12, 218);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(100, 23);
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.Click += new EventHandler(this.btnAddUser_Click);

            // btnBlockUser
            this.btnBlockUser.Location = new System.Drawing.Point(118, 218);
            this.btnBlockUser.Name = "btnBlockUser";
            this.btnBlockUser.Size = new System.Drawing.Size(100, 23);
            this.btnBlockUser.Text = "Block User";
            this.btnBlockUser.Click += new EventHandler(this.btnBlockUser_Click);

            // btnToggleRestrictions
            this.btnToggleRestrictions.Location = new System.Drawing.Point(224, 218);
            this.btnToggleRestrictions.Name = "btnToggleRestrictions";
            this.btnToggleRestrictions.Size = new System.Drawing.Size(150, 23);
            this.btnToggleRestrictions.Text = "Toggle Restrictions";
            this.btnToggleRestrictions.Click += new EventHandler(this.btnToggleRestrictions_Click);

            // btnChangePassword
            this.btnChangePassword.Location = new System.Drawing.Point(380, 218);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(100, 23);
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.Click += new EventHandler(this.btnChangePassword_Click);

            // btnExit
            this.btnExit.Location = new System.Drawing.Point(486, 218);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(86, 23);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);

            // AdminForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 251);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.btnToggleRestrictions);
            this.Controls.Add(this.btnBlockUser);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this._usersListView);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AdminForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Admin Panel";
            this.ResumeLayout(false);
        }

        private void LoadUsers()
        {
            _usersListView.Items.Clear();
            var users = _userManager.GetAllUsers();
            foreach (var user in users)
            {
                var item = new ListViewItem(user.Username);
                item.SubItems.Add(user.IsBlocked ? "Blocked" : "Active");
                item.SubItems.Add(user.PasswordRestricted ? "Enabled" : "Disabled");
                _usersListView.Items.Add(item);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using (var form = new AddUserForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var newUser = new User
                    {
                        Username = form.Username,
                        Password = form.Password,
                        IsBlocked = false,
                        PasswordRestricted = form.PasswordRestricted
                    };
                    _userManager.AddUser(newUser);
                    LoadUsers();
                }
            }
        }

        private void btnBlockUser_Click(object sender, EventArgs e)
        {
            if (_usersListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedUser = _usersListView.SelectedItems[0];
            var username = selectedUser.Text;
            var users = _userManager.GetAllUsers();
            var user = users.Find(u => u.Username == username);

            if (user != null)
            {
                user.IsBlocked = !user.IsBlocked;
                _userManager.UpdateUser(user);
                LoadUsers();
            }
        }

        private void btnToggleRestrictions_Click(object sender, EventArgs e)
        {
            if (_usersListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedUser = _usersListView.SelectedItems[0];
            var username = selectedUser.Text;
            var users = _userManager.GetAllUsers();
            var user = users.Find(u => u.Username == username);

            if (user != null)
            {
                user.PasswordRestricted = !user.PasswordRestricted;
                _userManager.UpdateUser(user);
                LoadUsers();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (_usersListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedUser = _usersListView.SelectedItems[0];
            var username = selectedUser.Text;

            using (var form = new ChangePasswordForm(username))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _userManager.ChangePassword(username, form.NewPassword);
                    MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Button btnAddUser;
        private Button btnBlockUser;
        private Button btnToggleRestrictions;
        private Button btnChangePassword;
        private Button btnExit;
    }
} 