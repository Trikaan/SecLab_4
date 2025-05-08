using UserManagementApp.Managers;

namespace UserManagementApp.Forms
{
    public class LoginForm : Form
    {
        private readonly UserManager _userManager;
        private int _loginAttempts;

        public LoginForm()
        {
            InitializeComponent();
            _userManager = new UserManager();
            _loginAttempts = 0;
        }

        private void InitializeComponent()
        {
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.SuspendLayout();
            
            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(12, 15);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(60, 13);
            this.lblUsername.Text = "Username:";
            
            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(78, 12);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(194, 20);
            
            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 41);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.Text = "Password:";
            
            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(78, 38);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(194, 20);
            
            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(197, 64);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            
            // LoginForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            var user = _userManager.AuthenticateUser(username, password);

            if (user != null)
            {
                if (user.Username.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
                {
                    var adminForm = new AdminForm(_userManager);
                    adminForm.Show();
                }
                else
                {
                    var userForm = new UserForm(_userManager, user);
                    userForm.Show();
                }
                this.Hide();
            }
            else
            {
                _loginAttempts++;
                if (_loginAttempts >= 3)
                {
                    MessageBox.Show("Too many failed login attempts. The application will now close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show($"Invalid username or password. Attempts remaining: {3 - _loginAttempts}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblUsername;
        private Label lblPassword;
    }
} 