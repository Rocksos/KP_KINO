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

namespace PROJECT_KINO.Pages
{
    /// <summary>
    /// Логика взаимодействия для Search_films.xaml
    /// </summary>
    public partial class Search_films : Page
    {
        public Search_films()
        {
            InitializeComponent();

            //заполнение страницы
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                //заполнение бокса года
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Premiere_year FROM [Films]");

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            Year_box.Items.Add(sqlDataReader["Premiere_year"]);
                        }
                    }
                }

                //заполнение бокса жанра
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Genre_name FROM Film_info INNER JOIN Genres ON dbo.Film_info.Genre_id = dbo.Genres.Genre_id "+
                                                           "GROUP by Genre_name");

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            Genre_box.Items.Add(sqlDataReader["Genre_name"]);
                        }
                    }
                }

                //заполнение бокса поиска
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Film_name FROM [Films]");

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            search_box.Items.Add(sqlDataReader["Film_name"]);
                        }
                    }
                }

                //заполнение грида фильмов
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Film_name, Premiere_year, Duration FROM [Films]");

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable films = new DataTable();
                        adpt.Fill(films);
                        Films_grid.ItemsSource = films.DefaultView;
                    }

                }

                conn.Close();
            }

            //скрытие функций изменения и удаления
            if (Public_Class.Role_num < 3)
            {
                //удаление фильма
                delete.Click += (s, e) =>
                {
                    if (Films_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить фильм?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    connection.Open();

                                    using (SqlCommand command = connection.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Films WHERE Film_name = @fname AND Premiere_year = @year";

                                        command.Parameters.AddWithValue("@fname", ((DataRowView)Films_grid.SelectedItem)[0]);
                                        command.Parameters.AddWithValue("@year", ((DataRowView)Films_grid.SelectedItem)[1]);

                                        command.ExecuteNonQuery();

                                    }

                                    using (SqlCommand command = connection.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Film_id = (SELECT Film_id FROM Films WHERE (Film_name = @fname) " +
                                                              "AND (Premiere_year = @year)";

                                        command.Parameters.AddWithValue("@fname", ((DataRowView)Films_grid.SelectedItem)[0]);
                                        command.Parameters.AddWithValue("@year", ((DataRowView)Films_grid.SelectedItem)[1]);

                                        command.ExecuteNonQuery();
                                    }
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                                    return;
                                }
                                finally
                                {
                                    using (SqlCommand sqlCommand = connection.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Film_name, Premiere_year, Duration FROM [Films]");

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable films = new DataTable();
                                            adpt.Fill(films);
                                            Films_grid.ItemsSource = films.DefaultView;
                                        }

                                    }
                                    connection.Close();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Запись не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                        return;
                    }
                };

                //Изменение фильма
                update.Click += (s, e) =>
                {
                    if (Films_grid.SelectedItem != null)
                    {
                        Public_Class.Film_name = ((DataRowView)Films_grid.SelectedItem)[0].ToString();
                        Public_Class.Film_year = ((DataRowView)Films_grid.SelectedItem)[1].ToString();
                        Public_Class.Film_time = ((DataRowView)Films_grid.SelectedItem)[2].ToString();

                        Windows.Update_film update_film = new Windows.Update_film();
                        update_film.ShowDialog();
                    }
                };
            }
            else
            {
                delete.Visibility = Visibility.Hidden;
                update.Visibility = Visibility.Hidden;
            }

            //переход на стринцу фильма
            gotofilm.Click += (s, e) =>
            {
                if (Films_grid.SelectedItem != null)
                {
                    Public_Class.Film_name = ((DataRowView)Films_grid.SelectedItem)[0].ToString();
                    Public_Class.Film_year = ((DataRowView)Films_grid.SelectedItem)[1].ToString();
                    Public_Class.Film_time = ((DataRowView)Films_grid.SelectedItem)[2].ToString();

                    NavigationService?.Navigate(new Film_page());
                }
            };
           
        }

        //кнопка поиска по названию
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (search_box.SelectedIndex == -1) MessageBox.Show("Введите название фильма");
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand sqlCommand = conn.CreateCommand())
                    {
                        sqlCommand.CommandText = string.Format("SELECT Film_name, Premiere_year, Duration FROM [Films] WHERE Film_name = @fname");

                        sqlCommand.Parameters.AddWithValue("@fname", search_box.SelectedItem.ToString());

                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                        {
                            DataTable films = new DataTable();
                            adpt.Fill(films);
                            Films_grid.ItemsSource = films.DefaultView;
                        }

                    }
                    conn.Close();
                }
            }
        }

        //сортировка по году
        private void Year_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Film_name, Premiere_year, Duration FROM [Films] WHERE Premiere_year = @year");

                    sqlCommand.Parameters.AddWithValue("@year", Year_box.SelectedItem.ToString());

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable films = new DataTable();
                        adpt.Fill(films);
                        Films_grid.ItemsSource = films.DefaultView;
                    }

                }
                conn.Close();
            }
        }

        //сортировка по жанру
        private void Genre_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Film_name, Premiere_year, Duration "+
                                                           "FROM Film_info INNER JOIN Films ON Film_info.Film_id = Films.Film_id "+
                                                           "WHERE Genre_id = (SELECT GEnre_id FROM Genres WHERE Genre_name = @genre)");

                    sqlCommand.Parameters.AddWithValue("@genre", Genre_box.SelectedItem.ToString());

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable films = new DataTable();
                        adpt.Fill(films);
                        Films_grid.ItemsSource = films.DefaultView;
                    }

                }
                conn.Close();
            }

        }
    }
}
