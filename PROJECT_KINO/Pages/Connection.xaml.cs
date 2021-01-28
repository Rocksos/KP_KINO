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
    /// Логика взаимодействия для Connection.xaml
    /// </summary>
    public partial class Connection : Page
    {
        
        public Connection()
        {
            InitializeComponent();
        }

        private void btn_Enter_Click(object sender, RoutedEventArgs e)
        {
            if (login_box.Text == "") MessageBox.Show("Введите Логин");
            else if (password_box.Text == "") MessageBox.Show("Введите Пароль");
            else
            {
                Public_Class.Profile = login_box.Text;

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {

                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("SELECT Login, Password, Role_id FROM [Users] WHERE Login=@login AND Password=@password");

                            sqlCommand.Parameters.AddWithValue("@login", login_box.Text);
                            sqlCommand.Parameters.AddWithValue("@password", password_box.Text);

                            if (!string.IsNullOrEmpty(login_box.Text) &&
                                !string.IsNullOrEmpty(password_box.Text))
                            {
                                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                                {
                                    if (sqlDataReader.HasRows)
                                    {
                                        while (sqlDataReader.Read())
                                        {
                                            switch (sqlDataReader[2])
                                            {
                                                case 1:
                                                    NavigationService?.Navigate(new Role_Administrator());
                                                    Public_Class.Role_num = 1;
                                                    break;
                                                case 2:
                                                    NavigationService?.Navigate(new Role_Editor());
                                                    Public_Class.Role_num = 2;
                                                    break;
                                                case 3:
                                                    NavigationService?.Navigate(new Role_User());
                                                    Public_Class.Role_num = 3;
                                                    break;
                                                default:
                                                    MessageBox.Show("Переход не выполнен!", "Ошибка",
                                                        MessageBoxButton.OK, MessageBoxImage.Error);
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            else MessageBox.Show("Введите логин и пароль!",
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void btn_Registry_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Registration());
        }
    }
}
