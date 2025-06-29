using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CarInsuranceApp
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost;Database=CarInsuranceDB;Integrated Security=True;";
        private bool isLoggedIn = false;
        private int currentCustomerId = 0;

        public Form1()
        {
            InitializeComponent();
            SetupGUI();
        }

        private void SetupGUI()
        {
            this.Text = "Car Insurance System";
            this.Size = new Size(800, 650); 
            this.BackColor = Color.WhiteSmoke;

            Panel signInPanel = new Panel
            {
                Location = new Point((this.ClientSize.Width - 350) / 2, (this.ClientSize.Height - 200) / 2),
                Size = new Size(350, 200),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Label lblSignIn = new Label { Text = "Sign In", Location = new Point(20, 15), Size = new Size(100, 25), Font = new Font("Arial", 12, FontStyle.Bold) };
            Label lblCustomerId = new Label { Text = "Customer ID:", Location = new Point(20, 50), Size = new Size(100, 20) };
            TextBox txtCustomerId = new TextBox { Name = "txtCustomerId", Location = new Point(130, 50), Size = new Size(180, 25) };
            Label lblPassword = new Label { Text = "Password:", Location = new Point(20, 85), Size = new Size(100, 20) };
            TextBox txtPassword = new TextBox { Name = "txtPassword", Location = new Point(130, 85), Size = new Size(180, 25), PasswordChar = '*' };

            Button btnSignIn = new Button
            {
                Text = "Sign In",
                Location = new Point(130, 125),
                Size = new Size(100, 35),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSignIn.Click += (s, e) => SignIn(txtCustomerId.Text, txtPassword.Text);

            Button btnSignUp = new Button
            {
                Text = "Sign Up",
                Location = new Point(240, 125),
                Size = new Size(100, 35),
                BackColor = Color.ForestGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSignUp.Click += (s, e) => SignUpNew();

            Button btnSignOut = new Button
            {
                Text = "Sign Out",
                Location = new Point(650, 570),
                Size = new Size(120, 40),
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };
            btnSignOut.Click += (s, e) => SignOut();

            signInPanel.Controls.Add(lblSignIn);
            signInPanel.Controls.Add(lblCustomerId);
            signInPanel.Controls.Add(txtCustomerId);
            signInPanel.Controls.Add(lblPassword);
            signInPanel.Controls.Add(txtPassword);
            signInPanel.Controls.Add(btnSignIn);
            signInPanel.Controls.Add(btnSignUp);

            this.Controls.Add(signInPanel);
            this.Controls.Add(btnSignOut);

            Panel customerPanel = new Panel
            {
                Text = "Customer Management",
                Location = new Point(30, 30), 
                Size = new Size(230, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Visible = false
            };

            Label lblCustomerMgmt = new Label { Text = "Customer Management", Location = new Point(10, 10), Size = new Size(200, 20), Font = new Font("Arial", 10, FontStyle.Bold) };
            customerPanel.Controls.Add(lblCustomerMgmt);

            Button btnAddCustomer = CreateStyledButton("Add Customer", new Point(20, 40), Color.RoyalBlue);
            btnAddCustomer.Click += (s, e) => AddCustomer();
            customerPanel.Controls.Add(btnAddCustomer);

            Button btnUpdateCustomer = CreateStyledButton("Update Customer", new Point(20, 80), Color.RoyalBlue);
            btnUpdateCustomer.Click += (s, e) => UpdateCustomer();
            customerPanel.Controls.Add(btnUpdateCustomer);

            Button btnDeleteCustomer = CreateStyledButton("Delete Customer", new Point(20, 120), Color.RoyalBlue);
            btnDeleteCustomer.Click += (s, e) => DeleteCustomer();
            customerPanel.Controls.Add(btnDeleteCustomer);

            Panel carPanel = new Panel
            {
                Text = "Car Management",
                Location = new Point(280, 30),
                Size = new Size(230, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Visible = false
            };

            Label lblCarMgmt = new Label { Text = "Car Management", Location = new Point(10, 10), Size = new Size(200, 20), Font = new Font("Arial", 10, FontStyle.Bold) };
            carPanel.Controls.Add(lblCarMgmt);

            Button btnAddCar = CreateStyledButton("Add Car", new Point(20, 40), Color.DarkGreen);
            btnAddCar.Click += (s, e) => AddCar();
            carPanel.Controls.Add(btnAddCar);

            Button btnUpdateCar = CreateStyledButton("Update Car", new Point(20, 80), Color.DarkGreen);
            btnUpdateCar.Click += (s, e) => UpdateCar();
            carPanel.Controls.Add(btnUpdateCar);

            Button btnDeleteCar = CreateStyledButton("Delete Car", new Point(20, 120), Color.DarkGreen);
            btnDeleteCar.Click += (s, e) => DeleteCar();
            carPanel.Controls.Add(btnDeleteCar);

            Panel accidentPanel = new Panel
            {
                Text = "Accident Management",
                Location = new Point(530, 30), 
                Size = new Size(230, 190),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Visible = false
            };

            Label lblAccidentMgmt = new Label { Text = "Accident Management", Location = new Point(10, 10), Size = new Size(200, 20), Font = new Font("Arial", 10, FontStyle.Bold) };
            accidentPanel.Controls.Add(lblAccidentMgmt);

            Button btnAddAccident = CreateStyledButton("Add Accident", new Point(20, 40), Color.DarkOrange);
            btnAddAccident.Click += (s, e) => AddAccident();
            accidentPanel.Controls.Add(btnAddAccident);

            Button btnUpdateAccident = CreateStyledButton("Update Accident", new Point(20, 80), Color.DarkOrange);
            btnUpdateAccident.Click += (s, e) => UpdateAccident();
            accidentPanel.Controls.Add(btnUpdateAccident);

            Button btnDeleteAccident = CreateStyledButton("Delete Accident", new Point(20, 120), Color.DarkOrange);
            btnDeleteAccident.Click += (s, e) => DeleteAccident();
            accidentPanel.Controls.Add(btnDeleteAccident);

            Button btnViewAccident = CreateStyledButton("View Accident", new Point(20, 160), Color.DarkOrange);
            btnViewAccident.Click += (s, e) => ViewAccident();
            accidentPanel.Controls.Add(btnViewAccident);

            Panel claimsPanel = new Panel
            {
                Text = "Claims & Reports",
                Location = new Point(30, 200), 
                Size = new Size(230, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Visible = false
            };

            Label lblClaimsMgmt = new Label { Text = "Claims & Reports", Location = new Point(10, 10), Size = new Size(200, 20), Font = new Font("Arial", 10, FontStyle.Bold) };
            claimsPanel.Controls.Add(lblClaimsMgmt);

            Button btnAddClaim = CreateStyledButton("Add Claim", new Point(20, 40), Color.Purple);
            btnAddClaim.Click += (s, e) => AddClaim();
            claimsPanel.Controls.Add(btnAddClaim);

            Button btnViewClaim = CreateStyledButton("View Claim", new Point(20, 80), Color.Purple);
            btnViewClaim.Click += (s, e) => ViewClaim();
            claimsPanel.Controls.Add(btnViewClaim);

            Button btnGenerateReport = CreateStyledButton("Generate Report", new Point(20, 120), Color.Purple);
            btnGenerateReport.Click += (s, e) => GenerateReport();
            claimsPanel.Controls.Add(btnGenerateReport);

            Button btnTotalOwners2023 = CreateStyledButton("Owners in 2023 Accidents", new Point(20, 160), Color.Purple);
            btnTotalOwners2023.Click += (s, e) => TotalOwnersInAccidents2023();
            claimsPanel.Controls.Add(btnTotalOwners2023);

            Button btnAhmedAccidents = CreateStyledButton("Ahmed's Accidents", new Point(20, 200), Color.Purple);
            btnAhmedAccidents.Click += (s, e) => AhmedMohamedAccidents();
            claimsPanel.Controls.Add(btnAhmedAccidents);

            Panel requirementsPanel = new Panel
            {
                Text = "Requirements",
                Location = new Point(280, 200),
                Size = new Size(230, 350), 
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Visible = false
            };

            Label lblRequirements = new Label { Text = "Requirements", Location = new Point(10, 10), Size = new Size(200, 20), Font = new Font("Arial", 10, FontStyle.Bold) };
            requirementsPanel.Controls.Add(lblRequirements);

            Button btnTotalOwners20231 = CreateStyledButton("Owners in 2023 Accidents", new Point(20, 40), Color.Teal);
            btnTotalOwners20231.Click += (s, e) => TotalOwnersInAccidents2023();
            requirementsPanel.Controls.Add(btnTotalOwners20231);

            Button btnAhmedAccidents1 = CreateStyledButton("Ahmed's Accidents", new Point(20, 80), Color.Teal);
            btnAhmedAccidents1.Click += (s, e) => AhmedMohamedAccidents();
            requirementsPanel.Controls.Add(btnAhmedAccidents1);

            Button btnMaxAccidentModel20231 = CreateStyledButton("Top Model 2023 Accidents", new Point(20, 120), Color.Teal);
            btnMaxAccidentModel20231.Click += (s, e) => ModelWithMaxAccidents2023();
            requirementsPanel.Controls.Add(btnMaxAccidentModel20231);

            Button btnZeroAccidentModel2023 = CreateStyledButton("Zero Accidents 2023", new Point(20, 160), Color.Teal);
            btnZeroAccidentModel2023.Click += (s, e) => ModelsWithZeroAccidents2023();
            requirementsPanel.Controls.Add(btnZeroAccidentModel2023);

            Button btnCustomersInAccidents2023 = CreateStyledButton("Customers in 2023", new Point(20, 200), Color.Teal);
            btnCustomersInAccidents2023.Click += (s, e) => CustomersInAccidents2023();
            requirementsPanel.Controls.Add(btnCustomersInAccidents2023);

            Button btnAccidentsByModel = CreateStyledButton("Accidents by Model", new Point(20, 240), Color.Teal);
            btnAccidentsByModel.Click += (s, e) => AccidentsByModel();
            requirementsPanel.Controls.Add(btnAccidentsByModel);

            this.Controls.Add(customerPanel);
            this.Controls.Add(carPanel);
            this.Controls.Add(accidentPanel);
            this.Controls.Add(claimsPanel);
            this.Controls.Add(requirementsPanel);
        }

        private Button CreateStyledButton(string text, Point location, Color color)
        {
            return new Button
            {
                Text = text,
                Location = location,
                Size = new Size(190, 30),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = true
            };
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return Regex.IsMatch(phone, @"^\d{11}$");
        }

        private bool IsValidDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date)) return false;
            return DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _);
        }

        private bool IsValidId(string id)
        {
            return int.TryParse(id, out int result) && result > 0;
        }

        private void SignUpNew()
        {
            Form signUpForm = new Form { Text = "Sign Up", Size = new Size(400, 400) };

            TextBox txtName = new TextBox { Location = new Point(150, 20), Size = new Size(200, 20) };
            TextBox txtEmail = new TextBox { Location = new Point(150, 50), Size = new Size(200, 20) };
            TextBox txtPhone = new TextBox { Location = new Point(150, 80), Size = new Size(200, 20) };
            TextBox txtAddress = new TextBox { Location = new Point(150, 110), Size = new Size(200, 80), Multiline = true, ScrollBars = ScrollBars.Vertical };
            TextBox txtPassword = new TextBox { Location = new Point(150, 200), Size = new Size(200, 20), PasswordChar = '*' };

            Button btnRegister = new Button { Text = "Register", Location = new Point(150, 230), Size = new Size(100, 30) };

            btnRegister.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Name cannot be empty!");
                    return;
                }
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Please enter a valid email address!");
                    return;
                }
                if (!IsValidPhone(txtPhone.Text))
                {
                    MessageBox.Show("Phone number must be 11 digits!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("Address cannot be empty!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password cannot be empty!");
                    return;
                }

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO CUSTOMER (NAME, EMAIL, PHONE, ADDRESS, PASSWORD) VALUES (@name, @email, @phone, @address, @password); SELECT SCOPE_IDENTITY();";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@name", txtName.Text);
                        command.Parameters.AddWithValue("@email", txtEmail.Text);
                        command.Parameters.AddWithValue("@phone", txtPhone.Text);
                        command.Parameters.AddWithValue("@address", txtAddress.Text);
                        command.Parameters.AddWithValue("@password", txtPassword.Text);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            int customerId = Convert.ToInt32(result);
                            MessageBox.Show($"Registration successful! Your Customer ID is: {customerId}. Please use this ID to sign in.");
                            signUpForm.Close();
                        }
                        else
                        {
                            MessageBox.Show("Registration failed. Please check your information and try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during registration: " + ex.Message);
                }
            };

            signUpForm.Controls.Add(new Label { Text = "Name:", Location = new Point(20, 20) });
            signUpForm.Controls.Add(txtName);
            signUpForm.Controls.Add(new Label { Text = "Email:", Location = new Point(20, 50) });
            signUpForm.Controls.Add(txtEmail);
            signUpForm.Controls.Add(new Label { Text = "Phone:", Location = new Point(20, 80) });
            signUpForm.Controls.Add(txtPhone);
            signUpForm.Controls.Add(new Label { Text = "Address:", Location = new Point(20, 110) });
            signUpForm.Controls.Add(txtAddress);
            signUpForm.Controls.Add(new Label { Text = "Password:", Location = new Point(20, 200) });
            signUpForm.Controls.Add(txtPassword);
            signUpForm.Controls.Add(btnRegister);

            signUpForm.ShowDialog();
        }

        private void SignIn(string customerId, string password)
        {
            if (!IsValidId(customerId))
            {
                MessageBox.Show("Customer ID must be a valid positive number!");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty!");
                return;
            }

            string query = "SELECT COUNT(*) FROM CUSTOMER WHERE CUSTOMERID = @id AND PASSWORD = @password";
            string result = ExecuteSelectQuery(query, new[] { ("@id", customerId), ("@password", password) }, false);
            if (result.StartsWith("Error:"))
            {
                MessageBox.Show("Failed to sign in: " + result);
            }
            else if (result == "1")
            {
                isLoggedIn = true;
                currentCustomerId = int.Parse(customerId);
                MessageBox.Show("Signed in successfully!");

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Panel panel)
                    {
                        panel.Visible = true;
                    }
                    else if (ctrl is Button btn && btn.Text == "Sign Out")
                    {
                        btn.Visible = true;
                    }
                }

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Panel panel && panel.Controls.OfType<Label>().Any(l => l.Text == "Sign In"))
                    {
                        panel.Visible = false;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid credentials!");
            }
        }

        private void SignOut()
        {
            isLoggedIn = false;
            currentCustomerId = 0;
            MessageBox.Show("Signed out successfully!");

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel panel)
                {
                    if (panel.Controls.OfType<Label>().Any(l => l.Text == "Sign In"))
                    {
                        panel.Visible = true;
                    }
                    else
                    {
                        panel.Visible = false;
                    }
                }
                else if (ctrl is Button btn && btn.Text == "Sign Out")
                {
                    btn.Visible = false;
                }
            }
        }

        private void AddCustomer()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Add Customer", Size = new Size(400, 400) };
            TextBox txtName = new TextBox { Location = new Point(150, 20), Size = new Size(200, 20) };
            TextBox txtEmail = new TextBox { Location = new Point(150, 50), Size = new Size(200, 20) };
            TextBox txtPhone = new TextBox { Location = new Point(150, 80), Size = new Size(200, 20) };
            TextBox txtAddress = new TextBox { Location = new Point(150, 110), Size = new Size(200, 80), Multiline = true, ScrollBars = ScrollBars.Vertical };
            TextBox txtPassword = new TextBox { Location = new Point(150, 200), Size = new Size(200, 20) };
            Button btnSave = new Button { Text = "Save", Location = new Point(150, 230), Size = new Size(100, 30) };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Name cannot be empty!");
                    return;
                }
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Please enter a valid email address!");
                    return;
                }
                if (!IsValidPhone(txtPhone.Text))
                {
                    MessageBox.Show("Phone number must be 11 digits!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    MessageBox.Show("Address cannot be empty!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password cannot be empty!");
                    return;
                }

                string query = "INSERT INTO CUSTOMER (NAME, EMAIL, PHONE, ADDRESS, PASSWORD) VALUES (@name, @email, @phone, @address, @password)";
                int customerId = ExecuteNonQuery(query, new[] {
                    ("@name", txtName.Text),
                    ("@email", txtEmail.Text),
                    ("@phone", txtPhone.Text),
                    ("@address", txtAddress.Text),
                    ("@password", txtPassword.Text)
                });
                inputForm.Close();
                if (customerId > 0)
                {
                    MessageBox.Show($"Customer added successfully! Customer ID: {customerId}");
                }
                else
                {
                    MessageBox.Show("Failed to add customer. Please check the data and try again.");
                }
            };
            inputForm.Controls.Add(new Label { Text = "Name", Location = new Point(20, 20) });
            inputForm.Controls.Add(txtName);
            inputForm.Controls.Add(new Label { Text = "Email", Location = new Point(20, 50) });
            inputForm.Controls.Add(txtEmail);
            inputForm.Controls.Add(new Label { Text = "Phone", Location = new Point(20, 80) });
            inputForm.Controls.Add(txtPhone);
            inputForm.Controls.Add(new Label { Text = "Address", Location = new Point(20, 110) });
            inputForm.Controls.Add(txtAddress);
            inputForm.Controls.Add(new Label { Text = "Password", Location = new Point(20, 180) });
            inputForm.Controls.Add(txtPassword);
            inputForm.Controls.Add(btnSave);
            inputForm.ShowDialog();
        }

        private void UpdateCustomer()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Update Customer", Size = new Size(400, 400) };
            TextBox txtName = new TextBox { Location = new Point(150, 20), Size = new Size(200, 20) };
            TextBox txtEmail = new TextBox { Location = new Point(150, 50), Size = new Size(200, 20) };
            TextBox txtPhone = new TextBox { Location = new Point(150, 80), Size = new Size(200, 20) };
            TextBox txtAddress = new TextBox { Location = new Point(150, 110), Size = new Size(200, 80), Multiline = true, ScrollBars = ScrollBars.Vertical };
            TextBox txtPassword = new TextBox { Location = new Point(150, 200), Size = new Size(200, 20) };
            txtPassword.PasswordChar = '*';

            string query = "SELECT NAME, EMAIL, PHONE, ADDRESS, PASSWORD FROM CUSTOMER WHERE CUSTOMERID = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", currentCustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["NAME"].ToString();
                            txtEmail.Text = reader["EMAIL"].ToString();
                            txtPhone.Text = reader["PHONE"].ToString();
                            txtAddress.Text = reader["ADDRESS"].ToString();
                            txtPassword.Text = reader["PASSWORD"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Customer not found!");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading customer data: " + ex.Message);
                    return;
                }
            }

            Button btnUpdate = new Button { Text = "Update", Location = new Point(150, 230), Size = new Size(100, 30) };
            btnUpdate.Click += (s, e) =>
            {
                string currentName = txtName.Text;
                string currentEmail = txtEmail.Text;
                string currentPhone = txtPhone.Text;
                string currentAddress = txtAddress.Text;
                string currentPassword = txtPassword.Text;

                string updatedName = string.IsNullOrWhiteSpace(txtName.Text) ? currentName : txtName.Text;
                string updatedEmail = string.IsNullOrWhiteSpace(txtEmail.Text) ? currentEmail : txtEmail.Text;
                string updatedPhone = string.IsNullOrWhiteSpace(txtPhone.Text) ? currentPhone : txtPhone.Text;
                string updatedAddress = string.IsNullOrWhiteSpace(txtAddress.Text) ? currentAddress : txtAddress.Text;
                string updatedPassword = string.IsNullOrWhiteSpace(txtPassword.Text) ? currentPassword : txtPassword.Text;

                if (!IsValidEmail(updatedEmail))
                {
                    MessageBox.Show("Please enter a valid email address!");
                    return;
                }
                if (!IsValidPhone(updatedPhone))
                {
                    MessageBox.Show("Phone number must be 11 digits!");
                    return;
                }

                string updateQuery = "UPDATE CUSTOMER SET NAME = @name, EMAIL = @email, PHONE = @phone, ADDRESS = @address, PASSWORD = @password WHERE CUSTOMERID = @id";
                ExecuteNonQuery(updateQuery, new[] {
                    ("@id", currentCustomerId.ToString()),
                    ("@name", updatedName),
                    ("@email", updatedEmail),
                    ("@phone", updatedPhone),
                    ("@address", updatedAddress),
                    ("@password", updatedPassword)
                });
                inputForm.Close();
                MessageBox.Show("Customer updated!");
            };
            inputForm.Controls.Add(new Label { Text = "Name", Location = new Point(20, 20) });
            inputForm.Controls.Add(txtName);
            inputForm.Controls.Add(new Label { Text = "Email", Location = new Point(20, 50) });
            inputForm.Controls.Add(txtEmail);
            inputForm.Controls.Add(new Label { Text = "Phone", Location = new Point(20, 80) });
            inputForm.Controls.Add(txtPhone);
            inputForm.Controls.Add(new Label { Text = "Address", Location = new Point(20, 110) });
            inputForm.Controls.Add(txtAddress);
            inputForm.Controls.Add(new Label { Text = "Password", Location = new Point(20, 180) });
            inputForm.Controls.Add(txtPassword);
            inputForm.Controls.Add(btnUpdate);
            inputForm.ShowDialog();
        }

        private void DeleteCustomer()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Delete Customer", Size = new Size(400, 400) };
            Label lblConfirm = new Label { Text = "Are you sure you want to delete your account?", Location = new Point(20, 20), Size = new Size(300, 20) };
            Button btnDelete = new Button { Text = "Delete", Location = new Point(150, 50), Size = new Size(100, 30) };
            btnDelete.Click += (s, e) =>
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete your account? This action cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    string query = "DELETE FROM CUSTOMER WHERE CUSTOMERID = @id";
                    ExecuteNonQuery(query, new[] { ("@id", currentCustomerId.ToString()) });
                    inputForm.Close();
                    MessageBox.Show("Customer deleted!");
                    SignOut();
                }
            };
            inputForm.Controls.Add(lblConfirm);
            inputForm.Controls.Add(btnDelete);
            inputForm.ShowDialog();
        }

        private void AddCar()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Add Car", Size = new Size(400, 400) };
            TextBox txtLINCESPLATE = new TextBox { Location = new Point(150, 20), Size = new Size(200, 20) };
            TextBox txtModel = new TextBox { Location = new Point(150, 50), Size = new Size(200, 20) };
            TextBox txtYear = new TextBox { Location = new Point(150, 80), Size = new Size(200, 20) };
            Button btnSave = new Button { Text = "Save", Location = new Point(150, 110), Size = new Size(100, 30) };
            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtLINCESPLATE.Text))
                {
                    MessageBox.Show("License Plate cannot be empty!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtModel.Text))
                {
                    MessageBox.Show("Model cannot be empty!");
                    return;
                }
                if (!int.TryParse(txtYear.Text, out int year) || year > 2025 || year < 1886)
                {
                    MessageBox.Show("Year must be between 1886 and 2025!");
                    return;
                }

                string query = "INSERT INTO CAR (LINCESPLATE, MODEL, YEAR) VALUES (@license, @model, @year)";
                int carId = ExecuteNonQuery(query, new[] {
                    ("@license", txtLINCESPLATE.Text),
                    ("@model", txtModel.Text),
                    ("@year", txtYear.Text)
                });
                if (carId > 0)
                {
                    string ownsQuery = "INSERT INTO OWNS (CUSTOMERID, CARID) VALUES (@customerId, @carId)";
                    ExecuteNonQuery(ownsQuery, new[] { ("@customerId", currentCustomerId.ToString()), ("@carId", carId.ToString()) });
                    inputForm.Close();
                    MessageBox.Show($"Car added successfully! Car ID: {carId}");
                }
                else
                {
                    inputForm.Close();
                    MessageBox.Show("Failed to add car. Please check the data and try again.");
                }
            };
            inputForm.Controls.Add(new Label { Text = "License Plate", Location = new Point(20, 20) });
            inputForm.Controls.Add(txtLINCESPLATE);
            inputForm.Controls.Add(new Label { Text = "Model", Location = new Point(20, 50) });
            inputForm.Controls.Add(txtModel);
            inputForm.Controls.Add(new Label { Text = "Year", Location = new Point(20, 80) });
            inputForm.Controls.Add(txtYear);
            inputForm.Controls.Add(btnSave);
            inputForm.ShowDialog();
        }

        private void UpdateCar()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Update Car", Size = new Size(400, 400) };
            ComboBox cmbCarId = new ComboBox { Location = new Point(150, 20), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };

            string queryCars = "SELECT c.CARID, c.LINCESPLATE FROM CAR c JOIN OWNS o ON c.CARID = o.CARID WHERE o.CUSTOMERID = @customerId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryCars, connection);
                    command.Parameters.AddWithValue("@customerId", currentCustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCarId.Items.Add(new { CarID = reader["CARID"].ToString(), LINCESPLATE = reader["LINCESPLATE"].ToString() });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading cars: " + ex.Message);
                    return;
                }
            }
            cmbCarId.DisplayMember = "LINCESPLATE";
            cmbCarId.ValueMember = "CarID";

            if (cmbCarId.Items.Count == 0)
            {
                MessageBox.Show("No cars found for this customer!");
                return;
            }

            TextBox txtLINCESPLATE = new TextBox { Location = new Point(150, 50), Size = new Size(200, 20) };
            TextBox txtModel = new TextBox { Location = new Point(150, 80), Size = new Size(200, 20) };
            TextBox txtYear = new TextBox { Location = new Point(150, 110), Size = new Size(200, 20) };

            cmbCarId.SelectedIndexChanged += (s, e) =>
            {
                if (cmbCarId.SelectedItem == null) return;
                var selectedCar = (dynamic)cmbCarId.SelectedItem;
                string carId = selectedCar.CarID;

                string query = "SELECT LINCESPLATE, MODEL, YEAR FROM CAR WHERE CARID = @id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", carId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtLINCESPLATE.Text = reader["LINCESPLATE"].ToString();
                                txtModel.Text = reader["MODEL"].ToString();
                                txtYear.Text = reader["YEAR"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading car data: " + ex.Message);
                    }
                }
            };

            Button btnUpdate = new Button { Text = "Update", Location = new Point(150, 140), Size = new Size(100, 30) };
            btnUpdate.Click += (s, e) =>
            {
                if (cmbCarId.SelectedItem == null)
                {
                    MessageBox.Show("Please select a car to update!");
                    return;
                }

                string currentLicensePlate = txtLINCESPLATE.Text;
                string currentModel = txtModel.Text;
                string currentYear = txtYear.Text;

                string updatedLicensePlate = string.IsNullOrWhiteSpace(txtLINCESPLATE.Text) ? currentLicensePlate : txtLINCESPLATE.Text;
                string updatedModel = string.IsNullOrWhiteSpace(txtModel.Text) ? currentModel : txtModel.Text;
                string updatedYear = string.IsNullOrWhiteSpace(txtYear.Text) ? currentYear : txtYear.Text;

                if (!int.TryParse(updatedYear, out int year) || year > 2025 || year < 1886)
                {
                    MessageBox.Show("Year must be between 1886 and 2025!");
                    return;
                }

                var selectedCar = (dynamic)cmbCarId.SelectedItem;
                string carId = selectedCar.CarID;
                string query = "UPDATE CAR SET LINCESPLATE = @license, MODEL = @model, YEAR = @year WHERE CARID = @id";
                ExecuteNonQuery(query, new[] {
                    ("@id", carId),
                    ("@license", updatedLicensePlate),
                    ("@model", updatedModel),
                    ("@year", updatedYear)
                });
                inputForm.Close();
                MessageBox.Show("Car updated!");
            };
            inputForm.Controls.Add(new Label { Text = "Select Car", Location = new Point(20, 20) });
            inputForm.Controls.Add(cmbCarId);
            inputForm.Controls.Add(new Label { Text = "License Plate", Location = new Point(20, 50) });
            inputForm.Controls.Add(txtLINCESPLATE);
            inputForm.Controls.Add(new Label { Text = "Model", Location = new Point(20, 80) });
            inputForm.Controls.Add(txtModel);
            inputForm.Controls.Add(new Label { Text = "Year", Location = new Point(20, 110) });
            inputForm.Controls.Add(txtYear);
            inputForm.Controls.Add(btnUpdate);
            inputForm.ShowDialog();
        }

        private void DeleteCar()
        {
            if (!isLoggedIn) return;

            Form inputForm = new Form { Text = "Delete Car", Size = new Size(400, 400) };
            ComboBox cmbCarId = new ComboBox { Location = new Point(150, 20), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };

            string queryCars = "SELECT c.CARID, c.LINCESPLATE FROM CAR c JOIN OWNS o ON c.CARID = o.CARID WHERE o.CUSTOMERID = @customerId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryCars, connection);
                    command.Parameters.AddWithValue("@customerId", currentCustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCarId.Items.Add(new
                            {
                                CarID = reader["CARID"].ToString(),
                                LINCESPLATE = reader["LINCESPLATE"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading cars: " + ex.Message);
                    return;
                }
            }

            cmbCarId.DisplayMember = "LINCESPLATE";
            cmbCarId.ValueMember = "CarID";

            if (cmbCarId.Items.Count == 0)
            {
                MessageBox.Show("No cars found for this customer!");
                return;
            }

            Button btnDelete = new Button { Text = "Delete", Location = new Point(150, 50), Size = new Size(100, 30) };
            btnDelete.Click += (s, e) =>
            {
                if (cmbCarId.SelectedItem == null)
                {
                    MessageBox.Show("Please select a car to delete!");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show(
                    "Are you sure you want to delete this car? This action cannot be undone.",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    var selectedCar = (dynamic)cmbCarId.SelectedItem;
                    string carId = selectedCar.CarID;

                    try
                    {
                        ExecuteNonQuery("DELETE FROM CAR WHERE CARID = @id", new[] { ("@id", carId) });

                        inputForm.Close();
                        MessageBox.Show("Car and all related data deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting car: " + ex.Message);
                    }
                }
            };

            inputForm.Controls.Add(new Label { Text = "Select Car", Location = new Point(20, 20) });
            inputForm.Controls.Add(cmbCarId);
            inputForm.Controls.Add(btnDelete);
            inputForm.ShowDialog();
        }

        private void AddAccident()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Add Accident", Size = new Size(400, 400) };
            ComboBox cmbCarId = new ComboBox { Location = new Point(150, 20), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };

            string queryCars = "SELECT c.CARID, c.LINCESPLATE FROM CAR c JOIN OWNS o ON c.CARID = o.CARID WHERE o.CUSTOMERID = @customerId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryCars, connection);
                    command.Parameters.AddWithValue("@customerId", currentCustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCarId.Items.Add(new { CarID = reader["CARID"].ToString(), LINCESPLATE = reader["LINCESPLATE"].ToString() });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading cars: " + ex.Message);
                    return;
                }
            }
            cmbCarId.DisplayMember = "LINCESPLATE";
            cmbCarId.ValueMember = "CarID";

            if (cmbCarId.Items.Count == 0)
            {
                MessageBox.Show("No cars found for this customer! Please add a car first.");
                return;
            }

            TextBox txtDate = new TextBox { Location = new Point(150, 50), Size = new Size(200, 20) };
            TextBox txtLocation = new TextBox { Location = new Point(150, 80), Size = new Size(200, 20) };
            TextBox txtDescription = new TextBox { Location = new Point(150, 110), Size = new Size(200, 80), Multiline = true, ScrollBars = ScrollBars.Vertical };
            Button btnSave = new Button { Text = "Save", Location = new Point(150, 200), Size = new Size(100, 30) };
            btnSave.Click += (s, e) =>
            {
                if (cmbCarId.SelectedItem == null)
                {
                    MessageBox.Show("Please select a car!");
                    return;
                }
                if (!IsValidDate(txtDate.Text))
                {
                    MessageBox.Show("Date must be in YYYY-MM-DD format!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    MessageBox.Show("Location cannot be empty!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDescription.Text))
                {
                    MessageBox.Show("Description cannot be empty!");
                    return;
                }

                var selectedCar = (dynamic)cmbCarId.SelectedItem;
                string carId = selectedCar.CarID;
                string accidentQuery = "INSERT INTO ACCIDENT (DATE, LOCATION, DESCRIPTION) VALUES (@date, @location, @desc)";
                int accidentId = ExecuteNonQuery(accidentQuery, new[] {
                    ("@date", txtDate.Text),
                    ("@location", txtLocation.Text),
                    ("@desc", txtDescription.Text)
                });
                if (accidentId > 0)
                {
                    string involvedInQuery = "INSERT INTO INVOLVEDIN (CARID, ACCIDENTID) VALUES (@carId, @accidentId)";
                    ExecuteNonQuery(involvedInQuery, new[] {
                        ("@carId", carId),
                        ("@accidentId", accidentId.ToString())
                    });
                    inputForm.Close();
                    MessageBox.Show($"Accident added successfully! Accident ID: {accidentId}");
                }
                else
                {
                    inputForm.Close();
                    MessageBox.Show("Failed to add accident. Please check the data and try again.");
                }
            };
            inputForm.Controls.Add(new Label { Text = "Select Car", Location = new Point(20, 20) });
            inputForm.Controls.Add(cmbCarId);
            inputForm.Controls.Add(new Label { Text = "Date (YYYY-MM-DD)", Location = new Point(20, 50) });
            inputForm.Controls.Add(txtDate);
            inputForm.Controls.Add(new Label { Text = "Location", Location = new Point(20, 80) });
            inputForm.Controls.Add(txtLocation);
            inputForm.Controls.Add(new Label { Text = "Description", Location = new Point(20, 110) });
            inputForm.Controls.Add(txtDescription);
            inputForm.Controls.Add(btnSave);
            inputForm.ShowDialog();
        }

        private void UpdateAccident()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Update Accident", Size = new Size(400, 400) };
            ComboBox cmbAccidentId = new ComboBox { Location = new Point(150, 20), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };

            string queryAccidents = @"
                SELECT a.ACCIDENTID, a.DATE, c.LINCESPLATE 
                FROM ACCIDENT a 
                JOIN INVOLVEDIN i ON a.ACCIDENTID = i.ACCIDENTID 
                JOIN CAR c ON i.CARID = c.CARID 
                JOIN OWNS o ON c.CARID = o.CARID 
                WHERE o.CUSTOMERID = @customerId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryAccidents, connection);
                    command.Parameters.AddWithValue("@customerId", currentCustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbAccidentId.Items.Add(new { AccidentID = reader["ACCIDENTID"].ToString(), DisplayText = $"Car: {reader["LINCESPLATE"]} - Date: {reader["DATE"]}" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading accidents: " + ex.Message);
                    return;
                }
            }
            cmbAccidentId.DisplayMember = "DisplayText";
            cmbAccidentId.ValueMember = "AccidentID";

            if (cmbAccidentId.Items.Count == 0)
            {
                MessageBox.Show("No accidents found for this customer!");
                return;
            }

            TextBox txtDate = new TextBox { Location = new Point(150, 50), Size = new Size(200, 20) };
            TextBox txtLocation = new TextBox { Location = new Point(150, 80), Size = new Size(200, 20) };
            TextBox txtDescription = new TextBox { Location = new Point(150, 110), Size = new Size(200, 80), Multiline = true, ScrollBars = ScrollBars.Vertical };

            cmbAccidentId.SelectedIndexChanged += (s, e) =>
            {
                if (cmbAccidentId.SelectedItem == null) return;
                var selectedAccident = (dynamic)cmbAccidentId.SelectedItem;
                string accidentId = selectedAccident.AccidentID;

                string query = "SELECT DATE, LOCATION, DESCRIPTION FROM ACCIDENT WHERE ACCIDENTID = @id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", accidentId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtDate.Text = reader["DATE"].ToString();
                                txtLocation.Text = reader["LOCATION"].ToString();
                                txtDescription.Text = reader["DESCRIPTION"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading accident data: " + ex.Message);
                    }
                }
            };

            Button btnUpdate = new Button { Text = "Update", Location = new Point(150, 200), Size = new Size(100, 30) };
            btnUpdate.Click += (s, e) =>
            {
                if (cmbAccidentId.SelectedItem == null)
                {
                    MessageBox.Show("Please select an accident to update!");
                    return;
                }

                string currentDate = txtDate.Text;
                string currentLocation = txtLocation.Text;
                string currentDescription = txtDescription.Text;

                string updatedDate = string.IsNullOrWhiteSpace(txtDate.Text) ? currentDate : txtDate.Text;
                string updatedLocation = string.IsNullOrWhiteSpace(txtLocation.Text) ? currentLocation : txtLocation.Text;
                string updatedDescription = string.IsNullOrWhiteSpace(txtDescription.Text) ? currentDescription : txtDescription.Text;

                if (!IsValidDate(updatedDate))
                {
                    MessageBox.Show("Date must be in YYYY-MM-DD format!");
                    return;
                }

                var selectedAccident = (dynamic)cmbAccidentId.SelectedItem;
                string accidentId = selectedAccident.AccidentID;
                string query = "UPDATE ACCIDENT SET DATE = @date, LOCATION = @location, DESCRIPTION = @desc WHERE ACCIDENTID = @id";
                ExecuteNonQuery(query, new[] {
                    ("@id", accidentId),
                    ("@date", updatedDate),
                    ("@location", updatedLocation),
                    ("@desc", updatedDescription)
                });
                inputForm.Close();
                MessageBox.Show("Accident updated!");
            };
            inputForm.Controls.Add(new Label { Text = "Select Accident", Location = new Point(20, 20) });
            inputForm.Controls.Add(cmbAccidentId);
            inputForm.Controls.Add(new Label { Text = "Date (YYYY-MM-DD)", Location = new Point(20, 50) });
            inputForm.Controls.Add(txtDate);
            inputForm.Controls.Add(new Label { Text = "Location", Location = new Point(20, 80) });
            inputForm.Controls.Add(txtLocation);
            inputForm.Controls.Add(new Label { Text = "Description", Location = new Point(20, 110) });
            inputForm.Controls.Add(txtDescription);
            inputForm.Controls.Add(btnUpdate);
            inputForm.ShowDialog();
        }

        private void DeleteAccident()
        {
            if (!isLoggedIn) return;

            Form inputForm = new Form { Text = "Delete Accident", Size = new Size(400, 400) };
            ComboBox cmbAccidentId = new ComboBox
            {
                Location = new Point(150, 20),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            string queryAccidents = @"
                SELECT a.ACCIDENTID, a.DATE, c.LINCESPLATE 
                FROM ACCIDENT a 
                JOIN INVOLVEDIN i ON a.ACCIDENTID = i.ACCIDENTID 
                JOIN CAR c ON i.CARID = c.CARID 
                JOIN OWNS o ON c.CARID = o.CARID 
                WHERE o.CUSTOMERID = @customerId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryAccidents, connection);
                    command.Parameters.AddWithValue("@customerId", currentCustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbAccidentId.Items.Add(new
                            {
                                AccidentID = reader["ACCIDENTID"].ToString(),
                                DisplayText = $"Car: {reader["LINCESPLATE"]} - Date: {Convert.ToDateTime(reader["DATE"]).ToShortDateString()}"
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading accidents: " + ex.Message);
                    return;
                }
            }

            cmbAccidentId.DisplayMember = "DisplayText";
            cmbAccidentId.ValueMember = "AccidentID";

            if (cmbAccidentId.Items.Count == 0)
            {
                MessageBox.Show("No accidents found for this customer!");
                return;
            }

            Button btnDelete = new Button { Text = "Delete", Location = new Point(150, 50), Size = new Size(100, 30) };
            btnDelete.Click += (s, e) =>
            {
                if (cmbAccidentId.SelectedItem == null)
                {
                    MessageBox.Show("Please select an accident to delete!");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show(
                    "Are you sure you want to delete this accident? This action cannot be undone.",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    var selectedAccident = (dynamic)cmbAccidentId.SelectedItem;
                    string accidentId = selectedAccident.AccidentID;

                    try
                    {
                        string deleteAccident = "DELETE FROM ACCIDENT WHERE ACCIDENTID = @id";
                        ExecuteNonQuery(deleteAccident, new[] { ("@id", accidentId) });

                        inputForm.Close();
                        MessageBox.Show("Accident deleted successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting accident: " + ex.Message);
                    }
                }
            };

            inputForm.Controls.Add(new Label { Text = "Select Accident", Location = new Point(20, 20) });
            inputForm.Controls.Add(cmbAccidentId);
            inputForm.Controls.Add(btnDelete);
            inputForm.ShowDialog();
        }

        private void ViewAccident()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "View Accident", Size = new Size(400, 400) };
            ComboBox cmbAccidentId = new ComboBox { Location = new Point(150, 20), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };

            string queryAccidents = @"
                SELECT a.ACCIDENTID, a.DATE, c.LINCESPLATE 
                FROM ACCIDENT a 
                JOIN INVOLVEDIN i ON a.ACCIDENTID = i.ACCIDENTID 
                JOIN CAR c ON i.CARID = c.CARID 
                JOIN OWNS o ON c.CARID = o.CARID 
                WHERE o.CUSTOMERID = @customerId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryAccidents, connection);
                    command.Parameters.AddWithValue("@customerId", currentCustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbAccidentId.Items.Add(new { AccidentID = reader["ACCIDENTID"].ToString(), DisplayText = $"Car: {reader["LINCESPLATE"]} - Date: {reader["DATE"]}" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading accidents: " + ex.Message);
                    return;
                }
            }

            cmbAccidentId.DisplayMember = "DisplayText";
            cmbAccidentId.ValueMember = "AccidentID";

            if (cmbAccidentId.Items.Count == 0)
            {
                MessageBox.Show("No accidents found for this customer!");
                return;
            }

            Button btnView = new Button { Text = "View", Location = new Point(150, 50), Size = new Size(100, 30) };
            btnView.Click += (s, e) =>
            {
                if (cmbAccidentId.SelectedItem == null)
                {
                    MessageBox.Show("Please select an accident to view!");
                    return;
                }

                var selectedAccident = (dynamic)cmbAccidentId.SelectedItem;
                string accidentId = selectedAccident.AccidentID;

                StringBuilder result = new StringBuilder();
                string query = "SELECT * FROM ACCIDENT WHERE ACCIDENTID = @id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@id", accidentId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    string value = reader[i].ToString();
                                    result.AppendLine($"{columnName}: {value}");
                                }
                            }
                            else
                            {
                                result.Append("No accident found with this ID!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Append("Error: " + ex.Message);
                    }
                }

                inputForm.Close();
                MessageBox.Show(result.ToString());
            };

            inputForm.Controls.Add(new Label { Text = "Select Accident", Location = new Point(20, 20) });
            inputForm.Controls.Add(cmbAccidentId);
            inputForm.Controls.Add(btnView);
            inputForm.ShowDialog();
        }

        private void AddClaim()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Add Claim", Size = new Size(400, 400) };
            ComboBox cmbAccidentId = new ComboBox { Location = new Point(150, 20), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };

            string queryAccidents = @"
                SELECT a.ACCIDENTID, a.DATE, c.LINCESPLATE 
                FROM ACCIDENT a 
                JOIN INVOLVEDIN i ON a.ACCIDENTID = i.ACCIDENTID 
                JOIN CAR c ON i.CARID = c.CARID 
                JOIN OWNS o ON c.CARID = o.CARID 
                WHERE o.CUSTOMERID = @customerId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryAccidents, connection);
                    command.Parameters.AddWithValue("@customerId", currentCustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbAccidentId.Items.Add(new { AccidentID = reader["ACCIDENTID"].ToString(), DisplayText = $"Car: {reader["LINCESPLATE"]} - Date: {reader["DATE"]}" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading accidents: " + ex.Message);
                    return;
                }
            }
            cmbAccidentId.DisplayMember = "DisplayText";
            cmbAccidentId.ValueMember = "AccidentID";

            if (cmbAccidentId.Items.Count == 0)
            {
                MessageBox.Show("No accidents found for this customer!");
                return;
            }

            TextBox txtAmount = new TextBox { Location = new Point(150, 50), Size = new Size(200, 20) };
            TextBox txtStatus = new TextBox { Location = new Point(150, 80), Size = new Size(200, 20) };
            Button btnSave = new Button { Text = "Save", Location = new Point(150, 110), Size = new Size(100, 30) };
            btnSave.Click += (s, e) =>
            {
                if (cmbAccidentId.SelectedItem == null)
                {
                    MessageBox.Show("Please select an accident!");
                    return;
                }
                if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount < 0)
                {
                    MessageBox.Show("Amount must be a positive number!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtStatus.Text) || !new List<string> { "Pending", "Approved", "Rejected" }.Contains(txtStatus.Text))
                {
                    MessageBox.Show("Status must be 'Pending', 'Approved', or 'Rejected'!");
                    return;
                }

                var selectedAccident = (dynamic)cmbAccidentId.SelectedItem;
                string accidentId = selectedAccident.AccidentID;
                string query = "INSERT INTO CLAIM (ACCIDENTID, AMOUNT, STATUS) VALUES (@accidentId, @amount, @status)";
                ExecuteNonQuery(query, new[] {
                    ("@accidentId", accidentId),
                    ("@amount", txtAmount.Text),
                    ("@status", txtStatus.Text)
                });
                inputForm.Close();
                MessageBox.Show("Claim added!");
            };
            inputForm.Controls.Add(new Label { Text = "Select Accident", Location = new Point(20, 20) });
            inputForm.Controls.Add(cmbAccidentId);
            inputForm.Controls.Add(new Label { Text = "Amount", Location = new Point(20, 50) });
            inputForm.Controls.Add(txtAmount);
            inputForm.Controls.Add(new Label { Text = "Status", Location = new Point(20, 80) });
            inputForm.Controls.Add(txtStatus);
            inputForm.Controls.Add(btnSave);
            inputForm.ShowDialog();
        }

        private void ViewClaim()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "View Claim", Size = new Size(400, 400) };
            ComboBox cmbClaimId = new ComboBox { Location = new Point(150, 20), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };

            string queryClaims = @"
                SELECT cl.CLAIMID, cl.AMOUNT, cl.STATUS, a.ACCIDENTID, a.DATE, ca.LINCESPLATE 
                FROM CLAIM cl
                JOIN ACCIDENT a ON cl.ACCIDENTID = a.ACCIDENTID 
                JOIN INVOLVEDIN i ON a.ACCIDENTID = i.ACCIDENTID 
                JOIN CAR ca ON i.CARID = ca.CARID 
                JOIN OWNS o ON ca.CARID = o.CARID 
                WHERE o.CUSTOMERID = @customerId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryClaims, connection);
                    command.Parameters.AddWithValue("@customerId", currentCustomerId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbClaimId.Items.Add(new
                            {
                                ClaimID = reader["CLAIMID"].ToString(),
                                DisplayText = $"Accident: {reader["ACCIDENTID"]} - Date: {reader["DATE"]} - Amount: {reader["AMOUNT"]} - Status: {reader["STATUS"]}"
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading claims: " + ex.Message);
                    return;
                }
            }
            cmbClaimId.DisplayMember = "DisplayText";
            cmbClaimId.ValueMember = "ClaimID";

            if (cmbClaimId.Items.Count == 0)
            {
                MessageBox.Show("No claims found for this customer!");
                return;
            }

            Button btnView = new Button { Text = "View", Location = new Point(150, 50), Size = new Size(100, 30) };
            btnView.Click += (s, e) =>
            {
                if (cmbClaimId.SelectedItem == null)
                {
                    MessageBox.Show("Please select a claim to view!");
                    return;
                }

                var selectedClaim = (dynamic)cmbClaimId.SelectedItem;
                string claimId = selectedClaim.ClaimID;
                string query = "SELECT CLAIMID, AMOUNT, STATUS, ACCIDENTID FROM CLAIM WHERE CLAIMID = @id";
                string result = ExecuteSelectQuery(query, new[] { ("@id", claimId) }, true);
                inputForm.Close();
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("No claim found with this ID!");
                }
                else
                {
                    MessageBox.Show(result);
                }
            };
            inputForm.Controls.Add(new Label { Text = "Select Claim", Location = new Point(20, 20) });
            inputForm.Controls.Add(cmbClaimId);
            inputForm.Controls.Add(btnView);
            inputForm.ShowDialog();
        }

        private void GenerateReport()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Generate Monthly Accident Report", Size = new Size(400, 400) };
            TextBox txtYear = new TextBox { Location = new Point(150, 20), Size = new Size(200, 20) };
            ComboBox cmbMonth = new ComboBox { Location = new Point(150, 50), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };

            var months = new[] {
                new { Name = "January", Value = 1 },
                new { Name = "February", Value = 2 },
                new { Name = "March", Value = 3 },
                new { Name = "April", Value = 4 },
                new { Name = "May", Value = 5 },
                new { Name = "June", Value = 6 },
                new { Name = "July", Value = 7 },
                new { Name = "August", Value = 8 },
                new { Name = "September", Value = 9 },
                new { Name = "October", Value = 10 },
                new { Name = "November", Value = 11 },
                new { Name = "December", Value = 12 }
            };
            cmbMonth.Items.AddRange(months);
            cmbMonth.DisplayMember = "Name";
            cmbMonth.ValueMember = "Value";

            Button btnGenerate = new Button { Text = "Generate Report", Location = new Point(150, 80), Size = new Size(100, 30) };

            btnGenerate.Click += (s, e) =>
            {
                if (!int.TryParse(txtYear.Text, out int year) || year > 2025 || year < 0)
                {
                    MessageBox.Show("Please enter a valid year between 0 and 2025!");
                    return;
                }
                if (cmbMonth.SelectedItem == null)
                {
                    MessageBox.Show("Please select a month!");
                    return;
                }

                var selectedMonth = (dynamic)cmbMonth.SelectedItem;
                int month = selectedMonth.Value;

                StringBuilder reportResult = new StringBuilder();
                string query = "SELECT * FROM ACCIDENT WHERE YEAR(DATE) = @year AND MONTH(DATE) = @month";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@year", year);
                        command.Parameters.AddWithValue("@month", month);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                reportResult.Append($"No accidents found for {cmbMonth.Text} {year}!");
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnName = reader.GetName(i);
                                        string value = reader[i].ToString();
                                        reportResult.AppendLine($"{columnName}: {value}");
                                    }
                                    reportResult.AppendLine("-------------------");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        reportResult.Append("Error: " + ex.Message);
                    }
                }

                inputForm.Close();

                Form resultForm = new Form { Text = $"Accidents in {cmbMonth.Text} {year}", Size = new Size(400, 400) };
                TextBox txtResults = new TextBox
                {
                    Location = new Point(20, 20),
                    Size = new Size(340, 320),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    ReadOnly = true,
                    Text = reportResult.ToString()
                };

                Button btnClose = new Button { Text = "Close", Location = new Point(150, 350), Size = new Size(100, 30) };
                btnClose.Click += (s2, e2) => resultForm.Close();

                resultForm.Controls.Add(txtResults);
                resultForm.Controls.Add(btnClose);
                resultForm.ShowDialog();
            };

            inputForm.Controls.Add(new Label { Text = "Year", Location = new Point(20, 20) });
            inputForm.Controls.Add(txtYear);
            inputForm.Controls.Add(new Label { Text = "Month", Location = new Point(20, 50) });
            inputForm.Controls.Add(cmbMonth);
            inputForm.Controls.Add(btnGenerate);
            inputForm.ShowDialog();
        }

        // Requirement a: Total number of people who owned cars involved in accidents in 2017
        private void TotalOwnersInAccidents2023()
        {
            if (!isLoggedIn) return;
            Form resultForm = new Form { Text = "Total Owners in 2023 Accidents", Size = new Size(400, 400) };
            TextBox txtResults = new TextBox
            {
                Location = new Point(20, 20),
                Size = new Size(340, 320),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };

            string query = @"
                SELECT COUNT(DISTINCT c.CUSTOMERID) AS TotalOwners
                FROM CUSTOMER c
                JOIN OWNS o ON c.CUSTOMERID = o.CUSTOMERID
                JOIN CAR ca ON o.CARID = ca.CARID
                JOIN INVOLVEDIN i ON ca.CARID = i.CARID
                JOIN ACCIDENT a ON i.ACCIDENTID = a.ACCIDENTID
                WHERE YEAR(a.DATE) = 2023";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    object result = command.ExecuteScalar();
                    txtResults.Text = result != null
                        ? $"Total number of owners in 2023 accidents: {result}"
                        : "No data found for 2023 accidents.";
                }
                catch (Exception ex)
                {
                    txtResults.Text = "Error: " + ex.Message;
                }
            }

            Button btnClose = new Button { Text = "Close", Location = new Point(150, 350), Size = new Size(100, 30) };
            btnClose.Click += (s, e) => resultForm.Close();

            resultForm.Controls.Add(txtResults);
            resultForm.Controls.Add(btnClose);
            resultForm.ShowDialog();
        }

        // Requirement b: Number of accidents involving Ahmed Mohamed's cars
        private void AhmedMohamedAccidents()
        {
            if (!isLoggedIn) return;
            Form resultForm = new Form { Text = "Ahmed Mohamed's Accidents", Size = new Size(400, 400) };
            TextBox txtResults = new TextBox
            {
                Location = new Point(20, 20),
                Size = new Size(340, 320),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };

            string query = @"
                SELECT COUNT(*) AS AccidentCount
                FROM CUSTOMER c
                JOIN OWNS o ON c.CUSTOMERID = o.CUSTOMERID
                JOIN CAR ca ON o.CARID = ca.CARID
                JOIN INVOLVEDIN i ON ca.CARID = i.CARID
                JOIN ACCIDENT a ON i.ACCIDENTID = a.ACCIDENTID
                WHERE c.NAME = 'Ahmed Mohamed'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    object result = command.ExecuteScalar();
                    txtResults.Text = result != null
                        ? $"Number of accidents involving Ahmed Mohamed's cars: {result}"
                        : "No accidents found for Ahmed Mohamed.";
                }
                catch (Exception ex)
                {
                    txtResults.Text = "Error: " + ex.Message;
                }
            }

            Button btnClose = new Button { Text = "Close", Location = new Point(150, 350), Size = new Size(100, 30) };
            btnClose.Click += (s, e) => resultForm.Close();

            resultForm.Controls.Add(txtResults);
            resultForm.Controls.Add(btnClose);
            resultForm.ShowDialog();
        }

        // Requirement c: Model with maximum number of accidents in 2017
        private void ModelWithMaxAccidents2023()
        {
            if (!isLoggedIn) return;
            Form resultForm = new Form { Text = "Model with Most Accidents in 2023", Size = new Size(400, 400) };
            TextBox txtResults = new TextBox
            {
                Location = new Point(20, 20),
                Size = new Size(340, 320),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };

            string query = @"
                SELECT TOP 1 ca.MODEL, COUNT(*) AS AccidentCount
                FROM CAR ca
                JOIN INVOLVEDIN i ON ca.CARID = i.CARID
                JOIN ACCIDENT a ON i.ACCIDENTID = a.ACCIDENTID
                WHERE YEAR(a.DATE) = 2023
                GROUP BY ca.MODEL
                ORDER BY AccidentCount DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtResults.Text = $"Model with most accidents in 2023: {reader["MODEL"]}\nAccident Count: {reader["AccidentCount"]}";
                        }
                        else
                        {
                            txtResults.Text = "No accidents found for 2023.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtResults.Text = "Error: " + ex.Message;
                }
            }

            Button btnClose = new Button { Text = "Close", Location = new Point(150, 350), Size = new Size(100, 30) };
            btnClose.Click += (s, e) => resultForm.Close();

            resultForm.Controls.Add(txtResults);
            resultForm.Controls.Add(btnClose);
            resultForm.ShowDialog();
        }

        // Requirement d: Models with zero accidents in 2017
        private void ModelsWithZeroAccidents2023()
        {
            if (!isLoggedIn) return;
            Form resultForm = new Form { Text = "Models with Zero Accidents in 2023", Size = new Size(400, 400) };
            TextBox txtResults = new TextBox
            {
                Location = new Point(20, 20),
                Size = new Size(340, 320),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };

            string query = @"
                SELECT DISTINCT ca.MODEL
                FROM CAR ca
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM INVOLVEDIN i
                    JOIN ACCIDENT a ON i.ACCIDENTID = a.ACCIDENTID
                    WHERE i.CARID = ca.CARID AND YEAR(a.DATE) = 2023
                )";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder result = new StringBuilder();
                        if (!reader.HasRows)
                        {
                            result.Append("No models found with zero accidents in 2023.");
                        }
                        else
                        {
                            result.AppendLine("Models with zero accidents in 2023:");
                            while (reader.Read())
                            {
                                result.AppendLine(reader["MODEL"].ToString());
                            }
                        }
                        txtResults.Text = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    txtResults.Text = "Error: " + ex.Message;
                }
            }

            Button btnClose = new Button { Text = "Close", Location = new Point(150, 350), Size = new Size(100, 30) };
            btnClose.Click += (s, e) => resultForm.Close();

            resultForm.Controls.Add(txtResults);
            resultForm.Controls.Add(btnClose);
            resultForm.ShowDialog();
        }

        // Requirement e: All information of customers who owned cars involved in accidents in 2017
        private void CustomersInAccidents2023()
        {
            if (!isLoggedIn) return;
            Form resultForm = new Form { Text = "Customers in 2023 Accidents", Size = new Size(600, 400) };
            TextBox txtResults = new TextBox
            {
                Location = new Point(20, 20),
                Size = new Size(540, 320),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };

            string query = @"
                SELECT DISTINCT c.CUSTOMERID, c.NAME, c.EMAIL, c.PHONE, c.ADDRESS
                FROM CUSTOMER c
                JOIN OWNS o ON c.CUSTOMERID = o.CUSTOMERID
                JOIN CAR ca ON o.CARID = ca.CARID
                JOIN INVOLVEDIN i ON ca.CARID = i.CARID
                JOIN ACCIDENT a ON i.ACCIDENTID = a.ACCIDENTID
                WHERE YEAR(a.DATE) = 2023";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        StringBuilder result = new StringBuilder();
                        if (!reader.HasRows)
                        {
                            result.Append("No customers found in 2023 accidents.");
                        }
                        else
                        {
                            result.AppendLine("Customers involved in 2023 accidents:");
                            while (reader.Read())
                            {
                                result.AppendLine($"Customer ID: {reader["CUSTOMERID"]}, Name: {reader["NAME"]}, Email: {reader["EMAIL"]}, Phone: {reader["PHONE"]}, Address: {reader["ADDRESS"]}");
                            }
                        }
                        txtResults.Text = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    txtResults.Text = "Error: " + ex.Message;
                }
            }

            Button btnClose = new Button { Text = "Close", Location = new Point(250, 350), Size = new Size(100, 30) };
            btnClose.Click += (s, e) => resultForm.Close();

            resultForm.Controls.Add(txtResults);
            resultForm.Controls.Add(btnClose);
            resultForm.ShowDialog();
        }

        // Requirement f: Number of accidents involving a specific model
        private void AccidentsByModel()
        {
            if (!isLoggedIn) return;
            Form inputForm = new Form { Text = "Accidents by Model", Size = new Size(400, 400) };
            ComboBox cmbModel = new ComboBox { Location = new Point(150, 20), Size = new Size(200, 20), DropDownStyle = ComboBoxStyle.DropDownList };

            string queryModels = "SELECT DISTINCT MODEL FROM CAR";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryModels, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbModel.Items.Add(reader["MODEL"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading models: " + ex.Message);
                    return;
                }
            }

            Button btnView = new Button { Text = "View", Location = new Point(150, 50), Size = new Size(100, 30) };
            btnView.Click += (s, e) =>
            {
                if (cmbModel.SelectedItem == null)
                {
                    MessageBox.Show("Please select a model to view!");
                    return;
                }

                string selectedModel = cmbModel.SelectedItem.ToString();
                string query = @"
                    SELECT COUNT(*) AS AccidentCount
                    FROM CAR ca
                    JOIN INVOLVEDIN i ON ca.CARID = i.CARID
                    JOIN ACCIDENT a ON i.ACCIDENTID = a.ACCIDENTID
                    WHERE ca.MODEL = @model";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@model", selectedModel);
                        int accidentCount = (int)command.ExecuteScalar();
                        MessageBox.Show($"Number of accidents involving {selectedModel}: {accidentCount}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            };

            inputForm.Controls.Add(new Label { Text = "Select Model", Location = new Point(20, 20) });
            inputForm.Controls.Add(cmbModel);
            inputForm.Controls.Add(btnView);
            inputForm.ShowDialog();
        }

        private string ExecuteSelectQuery(string query, (string name, string value)[] parameters, bool returnAllColumns)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.name, param.value);
                        }

                        if (!returnAllColumns)
                        {
                            object scalarResult = command.ExecuteScalar();
                            return scalarResult?.ToString() ?? "0";
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    string value = reader[i].ToString();
                                    result.AppendLine($"{columnName}: {value}");
                                }
                                result.AppendLine("-------------------");
                            }
                        }
                    }
                }
                return result.Length > 0 ? result.ToString() : "No data found.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }



        private int ExecuteNonQuery(string query, (string name, string value)[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.name, param.value);
                        }

                        if (query.Trim().ToUpper().StartsWith("INSERT"))
                        {
                            command.CommandText += "; SELECT SCOPE_IDENTITY();";
                            object result = command.ExecuteScalar();
                            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        }

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0 ? 1 : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing query: " + ex.Message);
                return -1;
            }
        }
    }
}    
