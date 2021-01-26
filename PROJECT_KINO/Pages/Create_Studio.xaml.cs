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
    /// Логика взаимодействия для Create_Studio.xaml
    /// </summary>
    public partial class Create_Studio : Page
    {
        public Create_Studio()
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
                        {country_box.Items.Add(sqlDataReader["Country_name"]);}
                    }

                }
                conn.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (St_name.Text == "") MessageBox.Show("Введите название студии");
            else if (Dr_name.Text == "") MessageBox.Show("Введите имя директора");
            else if (country_box.SelectedIndex == -1) MessageBox.Show("Выберите страну");
            else
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("INSERT INTO [Studios] (Studio_name, Director_name, Country_id) " +
                                                                    "VALUES (@Sname, @Dname, @country)");

                            sqlCommand.Parameters.AddWithValue("@Sname", St_name.Text);
                            sqlCommand.Parameters.AddWithValue("@Dname", Dr_name.Text);
                            sqlCommand.Parameters.AddWithValue("@country", (country_box.SelectedIndex + 1).ToString());

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Студия добавлена");
                                St_name.Text = "";
                                Dr_name.Text = "";
                                country_box.SelectedIndex = -1;
                            }
                            else
                                MessageBox.Show("Студия НЕ добавлена");
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
