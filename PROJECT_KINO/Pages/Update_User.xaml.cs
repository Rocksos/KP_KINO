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
    /// Логика взаимодействия для Update_User.xaml
    /// </summary>
    public partial class Update_User : Page
    {
        public Update_User()
        {
            InitializeComponent();

            Nick_lbl.Content = Public_Class.Profile;
            Sex_box.Items.Add("м");
            Sex_box.Items.Add("ж");

            //заполнение стран
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
                            Country_box.Items.Add(sqlDataReader["Country_name"]);
                        }
                    }
                }
                conn.Close();
            }

            //заполнение боксов
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Password, Name, Surname FROM [Users] " +
                                                           "WHERE Login = @login");

                    sqlCommand.Parameters.AddWithValue("@login", Public_Class.Profile);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                Password_box.Text = sqlDataReader[0].ToString();
                                Name_box.Text = sqlDataReader[1].ToString();
                                Surname_box.Text = sqlDataReader[2].ToString();
                            }
                        }
                    }
                }

                conn.Close();
            }
        }

        private void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Password_box.Text == "") MessageBox.Show("Введите пароль");
            else if (Name_box.Text == "") MessageBox.Show("Введите имя");
            else if (Surname_box.Text == "") MessageBox.Show("Введите фамилию");
            else if (Sex_box.SelectedIndex == -1) MessageBox.Show("Выберите пол");
            else if (Country_box.SelectedIndex == -1) MessageBox.Show("Выберите страну");
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("UPDATE [Users] SET Password = @pass, Name = @name, Surname = @surn, Sex = @sex, Country_id = @country " +
                                                                   "WHERE Login = @nickname");

                            sqlCommand.Parameters.AddWithValue("@pass", Password_box.Text);
                            sqlCommand.Parameters.AddWithValue("@name", Name_box.Text);
                            sqlCommand.Parameters.AddWithValue("@surn", Surname_box.Text);
                            sqlCommand.Parameters.AddWithValue("@sex", Sex_box.SelectedItem.ToString());
                            sqlCommand.Parameters.AddWithValue("@country", (Country_box.SelectedIndex+1));
                            sqlCommand.Parameters.AddWithValue("nickname", Public_Class.Profile);

                            if (sqlCommand.ExecuteNonQuery() == 1) MessageBox.Show("Данные профиля изменены");
                            else MessageBox.Show("Данные профиля НЕ изменены");
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
