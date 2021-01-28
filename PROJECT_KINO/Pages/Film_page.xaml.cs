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
    /// Логика взаимодействия для Film_page.xaml
    /// </summary>
    public partial class Film_page : Page
    {
        public Film_page()
        {
            InitializeComponent();

            //скрытие кнопок для юзера
            if (Public_Class.Role_num == 3)
            {
                Add_actors_btn.Visibility = Visibility.Hidden;
                Add_composer_btn.Visibility = Visibility.Hidden;
                Add_country_btn.Visibility = Visibility.Hidden;
                Add_directors_btn.Visibility = Visibility.Hidden;
                Add_genre_btn.Visibility = Visibility.Hidden;
                Add_operator_btn.Visibility = Visibility.Hidden;
                Add_prod_btn.Visibility = Visibility.Hidden;
                Add_studio_btn.Visibility = Visibility.Hidden;
                Add_writers_btn.Visibility = Visibility.Hidden;
                Add_maker_btn.Visibility = Visibility.Hidden;
                
            }

            Film_lbl.Content = Public_Class.Film_name;
            Year_lbl.Content = Public_Class.Film_year;
            Time_lbl.Content = Public_Class.Film_time;

            //заполнения гридов
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                bool mark_exist = false;

                //заполнение жанров
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Genre_name FROM  Genres INNER JOIN Film_info ON Genres.Genre_id = Film_info.Genre_id " +
                                                           "WHERE Film_id = (SELECT Film_id FROM [Films] WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable genre = new DataTable();
                        adpt.Fill(genre);
                        Genres_grid.ItemsSource = genre.DefaultView;
                    }

                }

                //заполнение студий
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Studio_name FROM Studios INNER JOIN Film_info ON Studios.Studio_id = Film_info.Studio_id " +
                                                           "WHERE Film_id = (SELECT Film_id FROM [Films] WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable std = new DataTable();
                        adpt.Fill(std);
                        Studio_grid.ItemsSource = std.DefaultView;
                    }

                }

                //заполнение стран
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Country_name FROM Countries INNER JOIN Film_info ON Countries.Country_id = Film_info.Country_id " +
                                                           "WHERE Film_id = (SELECT Film_id FROM [Films] WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable cntry = new DataTable();
                        adpt.Fill(cntry);
                        Country_grid.ItemsSource = cntry.DefaultView;
                    }

                }
                
                //заполнение актеров
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " + 
                                                           "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                           "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 6" +
                                                           "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable actors = new DataTable();
                        adpt.Fill(actors);
                        Actors_grid.ItemsSource = actors.DefaultView;
                    }

                }

                //заполнение режиссеров
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                           "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                           "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 1" +
                                                           "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable dir = new DataTable();
                        adpt.Fill(dir);
                        Dir_grid.ItemsSource = dir.DefaultView;
                    }

                }

                //заполнение сценаристов
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                           "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                           "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 2" +
                                                           "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable scrin = new DataTable();
                        adpt.Fill(scrin);
                        Scrin_grid.ItemsSource = scrin.DefaultView;
                    }

                }

                //заполнение продюссеров
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                           "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                           "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 3" +
                                                           "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable prod = new DataTable();
                        adpt.Fill(prod);
                        Prod_grid.ItemsSource = prod.DefaultView;
                    }

                }

                //заполнение композиторов
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                           "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                           "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 5" +
                                                           "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable comp = new DataTable();
                        adpt.Fill(comp);
                        Comp_grid.ItemsSource = comp.DefaultView;
                    }

                }

                //заполнение операторов
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                           "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                           "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 4" +
                                                           "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable oper = new DataTable();
                        adpt.Fill(oper);
                        Oper_grid.ItemsSource = oper.DefaultView;
                    }

                }

                //вывод оценки пользователя
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Mark FROM Marks WHERE Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film) AND Login = @login");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);
                    sqlCommand.Parameters.AddWithValue("@login", Public_Class.Profile);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                my_mark_lbl.Content = sqlDataReader[0];
                                

                                if (sqlDataReader[0] != DBNull.Value)
                                {
                                    if (Convert.ToInt32(my_mark_lbl.Content) < 5) my_mark_lbl.Foreground = System.Windows.Media.Brushes.Red;
                                    else if (Convert.ToInt32(my_mark_lbl.Content) < 7) my_mark_lbl.Foreground = System.Windows.Media.Brushes.DarkSlateGray;
                                    else my_mark_lbl.Foreground = System.Windows.Media.Brushes.Green;

                                    mark_exist = true;
                                }
                            }
                        }
                    }
                }

                //убирается кнопка изменения оценки
                if (mark_exist == false) update_mark.Visibility = Visibility.Hidden;

                //вывод средней оценки
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT round(AVG(Mark), 3) FROM Marks WHERE Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                mark_lbl.Content = sqlDataReader[0].ToString();

                                if (sqlDataReader[0] != DBNull.Value)
                                {
                                    if (Convert.ToDouble(mark_lbl.Content) < 5) mark_lbl.Foreground = System.Windows.Media.Brushes.Red;
                                    else if (Convert.ToDouble(mark_lbl.Content) < 7) mark_lbl.Foreground = System.Windows.Media.Brushes.DarkSlateGray;
                                    else mark_lbl.Foreground = System.Windows.Media.Brushes.Green;
                                }
                            }
                        }
                    }
                }

                conn.Close();
            }

            //скрытие функции удаление для юзера
            if (Public_Class.Role_num < 3)
            {
                //удаление жанров
                delete_genre.Click += (s, e) =>
                {
                    if (Genres_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить жанр?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    using (SqlCommand command = conn.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Genre_id = (SELECT Genre_id FROM Genres WHERE Genre_name = @name)";

                                        command.Parameters.AddWithValue("@name", ((DataRowView)Genres_grid.SelectedItem)[0]);

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
                                    using (SqlCommand sqlCommand = conn.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Genre_name FROM  Genres INNER JOIN Film_info ON Genres.Genre_id = Film_info.Genre_id " +
                                                                               "WHERE Film_id = (SELECT Film_id FROM [Films] WHERE Film_name = @film)");

                                        sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable genre = new DataTable();
                                            adpt.Fill(genre);
                                            Genres_grid.ItemsSource = genre.DefaultView;
                                        }

                                    }
                                    conn.Close();
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

                //удаление стран
                delete_country.Click += (s, e) =>
                {
                    if (Country_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить страну?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    using (SqlCommand command = conn.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Country_id = (SELECT Country_id FROM Countries WHERE Country_name = @name)";

                                        command.Parameters.AddWithValue("@name", ((DataRowView)Country_grid.SelectedItem)[0]);

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
                                    using (SqlCommand sqlCommand = conn.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Country_name FROM Countries INNER JOIN Film_info ON Countries.Country_id = Film_info.Country_id " +
                                                                               "WHERE Film_id = (SELECT Film_id FROM [Films] WHERE Film_name = @film)");

                                        sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable cntry = new DataTable();
                                            adpt.Fill(cntry);
                                            Country_grid.ItemsSource = cntry.DefaultView;
                                        }

                                    }
                                    conn.Close();
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

                //удаление студий
                delete_studio.Click += (s, e) =>
                {
                    if (Studio_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить продюссера?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    using (SqlCommand command = conn.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Studio_id = (SELECT Studio_id FROM Studios WHERE Studio_name = @name)";

                                        command.Parameters.AddWithValue("@name", ((DataRowView)Studio_grid.SelectedItem)[0]);

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
                                    using (SqlCommand sqlCommand = conn.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Studio_name FROM Studios INNER JOIN Film_info ON Studios.Studio_id = Film_info.Studio_id " +
                                                                               "WHERE Film_id = (SELECT Film_id FROM [Films] WHERE Film_name = @film)");

                                        sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable std = new DataTable();
                                            adpt.Fill(std);
                                            Studio_grid.ItemsSource = std.DefaultView;
                                        }

                                    }
                                    conn.Close();
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

                //удаление режиссера
                delete_maker.Click += (s, e) =>
                {
                    if (Dir_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить режиссера?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    using (SqlCommand command = conn.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Maker_id = (SELECT Maker_id FROM Filmmakers WHERE Maker_name = @name)";

                                        command.Parameters.AddWithValue("@name", ((DataRowView)Dir_grid.SelectedItem)[0]);

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
                                    using (SqlCommand sqlCommand = conn.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                                               "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 1" +
                                                                               "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                                        sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable dir = new DataTable();
                                            adpt.Fill(dir);
                                            Dir_grid.ItemsSource = dir.DefaultView;
                                        }

                                    }
                                    conn.Close();
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

                //удаление сценариста
                delete_maker1.Click += (s, e) =>
                {
                    if (Scrin_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить сценариста?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    using (SqlCommand command = conn.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Maker_id = (SELECT Maker_id FROM Filmmakers WHERE Maker_name = @name)";

                                        command.Parameters.AddWithValue("@name", ((DataRowView)Scrin_grid.SelectedItem)[0]);

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
                                    using (SqlCommand sqlCommand = conn.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                                               "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 2" +
                                                                               "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                                        sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable scrin = new DataTable();
                                            adpt.Fill(scrin);
                                            Scrin_grid.ItemsSource = scrin.DefaultView;
                                        }

                                    }
                                    conn.Close();
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

                //удаление продюссера
                delete_maker2.Click += (s, e) =>
                {
                    if (Prod_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить продюссера?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    using (SqlCommand command = conn.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Maker_id = (SELECT Maker_id FROM Filmmakers WHERE Maker_name = @name)";

                                        command.Parameters.AddWithValue("@name", ((DataRowView)Prod_grid.SelectedItem)[0]);

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
                                    using (SqlCommand sqlCommand = conn.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                                               "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 3" +
                                                                               "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                                        sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable prod = new DataTable();
                                            adpt.Fill(prod);
                                            Prod_grid.ItemsSource = prod.DefaultView;
                                        }

                                    }
                                    conn.Close();
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

                //Удаление композитора
                delete_maker3.Click += (s, e) =>
                {
                    if (Comp_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить композитора?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    using (SqlCommand command = conn.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Maker_id = (SELECT Maker_id FROM Filmmakers WHERE Maker_name = @name)";

                                        command.Parameters.AddWithValue("@name", ((DataRowView)Comp_grid.SelectedItem)[0]);

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
                                    using (SqlCommand sqlCommand = conn.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                                               "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 5" +
                                                                               "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                                        sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable comp = new DataTable();
                                            adpt.Fill(comp);
                                            Comp_grid.ItemsSource = comp.DefaultView;
                                        }

                                    }
                                    conn.Close();
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

                //удаление оператора
                delete_maker4.Click += (s, e) =>
                {
                    if (Oper_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить оператора?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    using (SqlCommand command = conn.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Maker_id = (SELECT Maker_id FROM Filmmakers WHERE Maker_name = @name)";

                                        command.Parameters.AddWithValue("@name", ((DataRowView)Oper_grid.SelectedItem)[0]);

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
                                    using (SqlCommand sqlCommand = conn.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                                               "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 4" +
                                                                               "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                                        sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable oper = new DataTable();
                                            adpt.Fill(oper);
                                            Oper_grid.ItemsSource = oper.DefaultView;
                                        }

                                    }
                                    conn.Close();
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

                //удаление актеров
                delete_maker5.Click += (s, e) =>
                {
                    if (Actors_grid.SelectedItem != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить актера/актриссу?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    using (SqlCommand command = conn.CreateCommand())
                                    {
                                        command.CommandText = "DELETE FROM Film_info WHERE Maker_id = (SELECT Maker_id FROM Filmmakers WHERE Maker_name = @name)";

                                        command.Parameters.AddWithValue("@name", ((DataRowView)Actors_grid.SelectedItem)[0]);

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
                                    using (SqlCommand sqlCommand = conn.CreateCommand())
                                    {
                                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id INNER JOIN " +
                                                                               "Film_info ON Filmmakers.Maker_id = Film_info.Maker_id WHERE Professions_list.Profession_id = 6" +
                                                                               "AND Film_info.Film_id = (SELECT Film_id FROM Films WHERE Film_name = @film)");

                                        sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                                        using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                                        {
                                            DataTable actors = new DataTable();
                                            adpt.Fill(actors);
                                            Actors_grid.ItemsSource = actors.DefaultView;
                                        }

                                    }
                                    conn.Close();
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
            }
            else 
            { 
                Genres_grid.ContextMenu.Visibility = Visibility.Hidden;
                Country_grid.ContextMenu.Visibility = Visibility.Hidden;
                Studio_grid.ContextMenu.Visibility = Visibility.Hidden;
                Dir_grid.ContextMenu.Visibility = Visibility.Hidden;
                Scrin_grid.ContextMenu.Visibility = Visibility.Hidden;
                Prod_grid.ContextMenu.Visibility = Visibility.Hidden;
                Comp_grid.ContextMenu.Visibility = Visibility.Hidden;
                Oper_grid.ContextMenu.Visibility = Visibility.Hidden;
                Actors_grid.ContextMenu.Visibility = Visibility.Hidden;
            }

        }
        
        private void Add_genre_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_Genre add_Genre = new Windows.Add_Genre();
            add_Genre.ShowDialog();
        }

        private void Add_country_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_Country add_Country = new Windows.Add_Country();
            add_Country.ShowDialog();
        }

        private void Add_studio_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_Studio add_Studio = new Windows.Add_Studio();
            add_Studio.ShowDialog();
        }

        private void Add_actors_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_filmmaker add_maker = new Windows.Add_filmmaker("actor");
            add_maker.ShowDialog();
        }

        private void Add_directors_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_filmmaker add_maker = new Windows.Add_filmmaker("director");
            add_maker.ShowDialog();
        }

        private void Add_writers_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_filmmaker add_maker = new Windows.Add_filmmaker("writer");
            add_maker.ShowDialog();
        }

        private void Add_prod_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_filmmaker add_maker = new Windows.Add_filmmaker("prod");
            add_maker.ShowDialog();
        }

        private void Add_composer_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_filmmaker add_maker = new Windows.Add_filmmaker("composer");
            add_maker.ShowDialog();
        }

        private void Add_operator_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_filmmaker add_maker = new Windows.Add_filmmaker("operator");
            add_maker.ShowDialog();
        }

        private void Add_maker_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Create_filmmakers());
        }

        private void add_mark_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_mark add_mark = new Windows.Add_mark("add");
            add_mark.ShowDialog();
        }

        private void users_marks_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new User_marks());
        }

        private void update_mark_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Add_mark add_mark = new Windows.Add_mark("update");
            add_mark.ShowDialog();
        }
    }
}
