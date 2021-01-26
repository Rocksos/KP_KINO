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
    /// Логика взаимодействия для Edit_profile.xaml
    /// </summary>
    public partial class Edit_profile : Page
    {
        public Edit_profile()
        {
            InitializeComponent();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Role_name FROM [Roles]");

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            role_box.Items.Add(sqlDataReader["Role_name"]);
                        }
                    }

                    sqlCommand.CommandText = string.Format("SELECT Login FROM [Users]" +
                                                            "WHERE Role_id <> '1'");

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            Name_box.Items.Add(sqlDataReader["Login"]);
                        }
                    }

                }
                conn.Close();
            }
        }

        #region Изменение роли
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (Name_box.SelectedIndex == -1) MessageBox.Show("Выберите пользователя");
            else if (role_box.SelectedIndex == -1) MessageBox.Show("Выберите роль");
            else
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("UPDATE [Users] " +
                                                                   "SET Role_id = @role " +
                                                                   "WHERE Login = @name");

                            sqlCommand.Parameters.AddWithValue("@name", Name_box.SelectedItem.ToString());
                            sqlCommand.Parameters.AddWithValue("@role", role_box.SelectedIndex + 1);

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Роль пользователя изменена");
                                Name_box.SelectedIndex = -1;
                                role_box.SelectedIndex = -1;
                            }
                            else
                                MessageBox.Show("Не удалось изменить роль");

                        }
                    }
                    catch (SqlException ex)
                    { MessageBox.Show(ex.Message); }
                    finally
                    { conn.Close(); }
                }
            }
        }
        #endregion

        #region Удаление пользователя
        private void Delete_button_Click(object sender, RoutedEventArgs e)
        {
            if (Name_box.SelectedIndex == -1) MessageBox.Show("Выберите пользователя");
            else
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {

                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("DELETE Users WHERE Login = @name ");

                            sqlCommand.Parameters.AddWithValue("@name", Name_box.SelectedItem.ToString());

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Пользователь удален");
                                Name_box.SelectedIndex = -1;
                                role_box.SelectedIndex = -1;
                            }
                            else
                                MessageBox.Show("Не удалось удалить пользователя");

                        }
                    }
                    catch (SqlException ex)
                    { MessageBox.Show(ex.Message); }
                    finally
                    { conn.Close(); }
                }
            }  
        }
        #endregion
    }
}
