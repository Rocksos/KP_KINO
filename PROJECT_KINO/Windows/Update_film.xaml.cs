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
using System.Data;

namespace PROJECT_KINO.Windows
{
    /// <summary>
    /// Логика взаимодействия для Update_film.xaml
    /// </summary>
    public partial class Update_film : Window
    {
        public Update_film()
        {
            InitializeComponent();

            //заполнение боксов
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Film_name, Premiere_year, Duration FROM [Films] " +
                                                           "WHERE Film_name = @film AND Premiere_year = @year AND Duration = @time");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);
                    sqlCommand.Parameters.AddWithValue("@year", Public_Class.Film_year);
                    sqlCommand.Parameters.AddWithValue("@time", Public_Class.Film_time);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                film_box.Text = sqlDataReader[0].ToString();
                                year_box.Text = sqlDataReader[1].ToString();
                                time_box.Text = sqlDataReader[2].ToString();
                            }
                        }
                    }
                }

                conn.Close();
            }
        }

        //кнопка изменения
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (film_box.Text == "") MessageBox.Show("Введите название фильма");
            else if (year_box.Text == "") MessageBox.Show("Введите год выхода");
            else if (time_box.Text == "") MessageBox.Show("Введите длительность фильма");
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("UPDATE [Films] SET Film_name = @film, Premiere_year = @year, Duration = @time " +
                                                                   "WHERE Film_id = (SELECT Film_id FROM [Films] WHERE Film_name = @film2 AND Premiere_year = @year2 AND Duration = @time2)");

                            sqlCommand.Parameters.AddWithValue("@film", film_box.Text);
                            sqlCommand.Parameters.AddWithValue("@year", Convert.ToInt32(year_box.Text));
                            sqlCommand.Parameters.AddWithValue("@time", time_box.Text);
                            sqlCommand.Parameters.AddWithValue("@film2", Public_Class.Film_name);
                            sqlCommand.Parameters.AddWithValue("@year2", Public_Class.Film_year);
                            sqlCommand.Parameters.AddWithValue("@time2", Public_Class.Film_time);

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Фильм изменен");
                                this.Close();
                            }

                            else
                                MessageBox.Show("Фильм НЕ изменен");
                           
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
