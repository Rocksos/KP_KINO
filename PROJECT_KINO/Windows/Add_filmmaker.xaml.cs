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
    /// Логика взаимодействия для Add_filmmaker.xaml
    /// </summary>
    public partial class Add_filmmaker : Window
    {
        public Add_filmmaker(string prof)
        {
            InitializeComponent();

            #region Изменение окна под профессию
            if (prof == "actor")
            {
                Prof_label.Content = "Имя актера/актрисы";

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand sqlCommand = conn.CreateCommand())
                    {
                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id " +
                                                               "WHERE Professions_list.Profession_id = 6");

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                maker_box.Items.Add(sqlDataReader["Maker_name"]);
                            }
                        }
                    }

                    conn.Close();
                }
            }

            if (prof == "composer")
            {
                Prof_label.Content = "Имя композитора";

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand sqlCommand = conn.CreateCommand())
                    {
                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id " +
                                                               "WHERE Professions_list.Profession_id = 5");

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                maker_box.Items.Add(sqlDataReader["Maker_name"]);
                            }
                        }
                    }

                    conn.Close();
                }
            }

            if (prof == "operator")
            {
                Prof_label.Content = "Имя оператора";

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand sqlCommand = conn.CreateCommand())
                    {
                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id " +
                                                               "WHERE Professions_list.Profession_id = 4");

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                maker_box.Items.Add(sqlDataReader["Maker_name"]);
                            }
                        }
                    }

                    conn.Close();
                }
            }

            if (prof == "prod")
            {
                Prof_label.Content = "Имя продюссера";

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand sqlCommand = conn.CreateCommand())
                    {
                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id " +
                                                               "WHERE Professions_list.Profession_id = 3");

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                maker_box.Items.Add(sqlDataReader["Maker_name"]);
                            }
                        }
                    }

                    conn.Close();
                }
            }

            if (prof == "writer")
            {
                Prof_label.Content = "Имя сценариста";

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand sqlCommand = conn.CreateCommand())
                    {
                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id " +
                                                               "WHERE Professions_list.Profession_id = 2");

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                maker_box.Items.Add(sqlDataReader["Maker_name"]);
                            }
                        }
                    }

                    conn.Close();
                }
            }

            if (prof == "director")
            {
                Prof_label.Content = "Имя режиссера";

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand sqlCommand = conn.CreateCommand())
                    {
                        sqlCommand.CommandText = string.Format("SELECT Maker_name FROM Filmmakers INNER JOIN Prifessions ON Filmmakers.Maker_id = Prifessions.Maker_id " +
                                                               "INNER JOIN Professions_list ON Prifessions.Profession_id = Professions_list.Profession_id " +
                                                               "WHERE Professions_list.Profession_id = 1");

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                maker_box.Items.Add(sqlDataReader["Maker_name"]);
                            }
                        }
                    }

                    conn.Close();
                }
            }
            #endregion
        }

        //добавление кинодеятеля
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (maker_box.SelectedIndex == -1) MessageBox.Show("Выберите кинодеятеля");
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("INSERT INTO [Film_info] (Film_id, Maker_id) " +
                                                                    "VALUES ((SELECT Film_id FROM [Films] WHERE Film_name = @film), (SELECT Maker_id FROM [Filmmakers] WHERE Maker_name = @maker))");

                            sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);
                            sqlCommand.Parameters.AddWithValue("@maker", maker_box.SelectedItem.ToString());

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBoxResult result = MessageBox.Show("Продолжить добавлять?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                                if (result == MessageBoxResult.Yes)
                                    maker_box.SelectedIndex = -1;
                                else
                                    this.Close();
                            }

                            else MessageBox.Show("Кинодеятель НЕ добавлен");
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
