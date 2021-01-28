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
using System.Configuration;
using System.Data.SqlClient;

namespace PROJECT_KINO.Pages
{
    /// <summary>
    /// Логика взаимодействия для Create_film.xaml
    /// </summary>
    public partial class Create_film : Page
    {
        public Create_film()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (film_name.Text == "") MessageBox.Show("Введите название фильма");
            else if (year_box.Text == "") MessageBox.Show("Введите год выхода");
            else if (duration_box.Text == "") MessageBox.Show("Введите длину фильма");
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("INSERT INTO [Films] (Film_name, Premiere_year, Duration) " +
                                                                    "VALUES (@Fname, @Year, @Duration)");

                            sqlCommand.Parameters.AddWithValue("@Fname", film_name.Text);
                            sqlCommand.Parameters.AddWithValue("@Year", year_box.Text);
                            sqlCommand.Parameters.AddWithValue("@Duration", duration_box.Text);

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Фильм добавлен");
                                film_name.Text = "";
                                year_box.Text = "";
                                duration_box.Text = "";
                            }
                            else
                                MessageBox.Show("Фильм НЕ добавлен");
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
