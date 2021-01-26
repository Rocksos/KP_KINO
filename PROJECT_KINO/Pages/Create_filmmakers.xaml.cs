using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace PROJECT_KINO.Pages
{
    /// <summary>
    /// Логика взаимодействия для Create_filmmakers.xaml
    /// </summary>
    public partial class Create_filmmakers : Page
    {
        public Create_filmmakers()
        {
            InitializeComponent();

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

                    sqlCommand.CommandText = string.Format("SELECT Profession_name FROM [Professions_list]");

                    using (SqlDataReader sqlDataReader1 = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader1.Read())
                        {
                            pro_box.Items.Add(sqlDataReader1["Profession_name"]);
                        }
                    }
                }   
                conn.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (maker_name.Text == "") MessageBox.Show("Введите имя");
            else if (country_box.SelectedIndex == -1) MessageBox.Show("Выберите страну");
            else if (pro_box.SelectedIndex == -1) MessageBox.Show("Выберите профессию");
            else
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("INSERT INTO [Filmmakers] (Maker_name, Maker_Birth, Country_id) " +
                                                                    "VALUES (@name, @date, @country)");

                            sqlCommand.Parameters.AddWithValue("@name", maker_name.Text);
                            sqlCommand.Parameters.AddWithValue("@date", birth_date.SelectedDate.ToString());
                            sqlCommand.Parameters.AddWithValue("@country", (country_box.SelectedIndex + 1).ToString());

                            if (sqlCommand.ExecuteNonQuery() == 1)
                                MessageBox.Show("Кинодеятель добавлен");
                            else
                                MessageBox.Show("Кинодеятель НЕ добавлен");
                        }

                        using (SqlCommand command = conn.CreateCommand())
                        {
                            command.CommandText = string.Format("INSERT INTO [Prifessions] (Maker_id, Profession_id) VALUES ((SELECT Maker_id FROM [Filmmakers] WHERE Maker_name = @name), @idpro)");

                            command.Parameters.AddWithValue("@name", maker_name.Text);
                            command.Parameters.AddWithValue("@idpro", (pro_box.SelectedIndex + 1).ToString());

                            if (command.ExecuteNonQuery() == 1)
                            {
                                maker_name.Text = "";
                                birth_date.SelectedDate = null;
                                country_box.SelectedIndex = -1;
                                pro_box.SelectedIndex = -1;
                            }
                            else
                                MessageBox.Show("Профессия НЕ добавлена");
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
