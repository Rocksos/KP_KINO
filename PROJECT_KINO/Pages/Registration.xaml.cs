using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;

namespace PROJECT_KINO.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();

            Rsex_box.Items.Add("м");
            Rsex_box.Items.Add("ж");

            //заполнение бокса стран
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Country_name FROM [Countries]");

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            country_box.Items.Add(sqlDataReader["Country_name"]);
                        }
                    }
                }
                conn.Close();
            }
        }

        private void Reg_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Rlogin_box.Text == "") MessageBox.Show("Введите Логин");
            else if (Rpassword_box.Text == "") MessageBox.Show("Введите Пароль");
            else if (Rsurname_box.Text == "") MessageBox.Show("Введите Фамилию");
            else if (Rname_box.Text == "") MessageBox.Show("Введите Имя");
            else if (Rsex_box.SelectedIndex == -1) MessageBox.Show("Выберите пол");
            else if (country_box.SelectedIndex == -1) MessageBox.Show("Ввыберите страну");
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("INSERT INTO [Users] (Login, Password, Surname, Name, Sex, Country_id, Role_id)" +
                                                            "VALUES (@login, @password, @surname, @name, @sex, @country, 3) ");

                            sqlCommand.Parameters.AddWithValue("@login", Rlogin_box.Text);
                            sqlCommand.Parameters.AddWithValue("@password", Rpassword_box.Text);
                            sqlCommand.Parameters.AddWithValue("@surname", Rsurname_box.Text);
                            sqlCommand.Parameters.AddWithValue("@name", Rname_box.Text);
                            sqlCommand.Parameters.AddWithValue("@sex", Rsex_box.SelectedItem.ToString());
                            sqlCommand.Parameters.AddWithValue("@country", (country_box.SelectedIndex + 1));

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Аккаунт создан");
                                NavigationService?.Navigate(new Connection());
                            }
                            else MessageBox.Show("Аккаунт не создан");
                        }
                    }
                    catch (SqlException ex)
                    { MessageBox.Show(ex.Message); }
                    finally
                    { conn.Close(); }
                }
            }
        }
    }
}
