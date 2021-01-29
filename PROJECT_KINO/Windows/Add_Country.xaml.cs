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
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Navigation;
using PROJECT_KINO.Pages;

namespace PROJECT_KINO.Windows
{
    /// <summary>
    /// Логика взаимодействия для Add_Country.xaml
    /// </summary>
    public partial class Add_Country : Window
    {
        public Add_Country()
        {
            InitializeComponent();

            //заполнение бокса стран
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Country_name FROM[Countries]");

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

        //добавление страны
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (country_box.SelectedIndex == -1) MessageBox.Show("Выберите страну");
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("INSERT INTO [Film_info] (Film_id, Country_id) " +
                                                                    "VALUES ((SELECT Film_id FROM [Films] WHERE Film_name = @film), @country)");

                            sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);
                            sqlCommand.Parameters.AddWithValue("@country", (country_box.SelectedIndex + 1).ToString());

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBoxResult result = MessageBox.Show("Продолжить добавлять?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                                if (result == MessageBoxResult.Yes)
                                    country_box.SelectedIndex = -1;
                                else
                                    this.Close();
                            }

                            else MessageBox.Show("Страна НЕ добавлена");
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
