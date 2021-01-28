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
    /// Логика взаимодействия для Add_mark.xaml
    /// </summary>
    public partial class Add_mark : Window
    {
        public Add_mark(string mark)
        {
            InitializeComponent();

            if (mark == "add") Update_mark.Visibility = Visibility.Hidden;
            if (mark == "update") Add_mark1.Visibility = Visibility.Hidden;
        }

        #region Смена цветов оценок при нажатии
        private void mark_1_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 1;

            mark_1.Background = System.Windows.Media.Brushes.DarkRed;

            mark_2.Background = System.Windows.Media.Brushes.White;
            mark_3.Background = System.Windows.Media.Brushes.White;
            mark_4.Background = System.Windows.Media.Brushes.White;
            mark_5.Background = System.Windows.Media.Brushes.White;
            mark_6.Background = System.Windows.Media.Brushes.White;
            mark_7.Background = System.Windows.Media.Brushes.White;
            mark_8.Background = System.Windows.Media.Brushes.White;
            mark_9.Background = System.Windows.Media.Brushes.White;
            mark_10.Background = System.Windows.Media.Brushes.White;
        }

        private void mark_2_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 2;

            mark_2.Background = System.Windows.Media.Brushes.DarkRed;

            mark_1.Background = System.Windows.Media.Brushes.White;
            mark_3.Background = System.Windows.Media.Brushes.White;
            mark_4.Background = System.Windows.Media.Brushes.White;
            mark_5.Background = System.Windows.Media.Brushes.White;
            mark_6.Background = System.Windows.Media.Brushes.White;
            mark_7.Background = System.Windows.Media.Brushes.White;
            mark_8.Background = System.Windows.Media.Brushes.White;
            mark_9.Background = System.Windows.Media.Brushes.White;
            mark_10.Background = System.Windows.Media.Brushes.White;

        }

        private void mark_3_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 3;

            mark_3.Background = System.Windows.Media.Brushes.DarkRed;

            mark_2.Background = System.Windows.Media.Brushes.White;
            mark_1.Background = System.Windows.Media.Brushes.White;
            mark_4.Background = System.Windows.Media.Brushes.White;
            mark_5.Background = System.Windows.Media.Brushes.White;
            mark_6.Background = System.Windows.Media.Brushes.White;
            mark_7.Background = System.Windows.Media.Brushes.White;
            mark_8.Background = System.Windows.Media.Brushes.White;
            mark_9.Background = System.Windows.Media.Brushes.White;
            mark_10.Background = System.Windows.Media.Brushes.White;
        }

        private void mark_4_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 4;

            mark_4.Background = System.Windows.Media.Brushes.DarkRed;

            mark_2.Background = System.Windows.Media.Brushes.White;
            mark_3.Background = System.Windows.Media.Brushes.White;
            mark_1.Background = System.Windows.Media.Brushes.White;
            mark_5.Background = System.Windows.Media.Brushes.White;
            mark_6.Background = System.Windows.Media.Brushes.White;
            mark_7.Background = System.Windows.Media.Brushes.White;
            mark_8.Background = System.Windows.Media.Brushes.White;
            mark_9.Background = System.Windows.Media.Brushes.White;
            mark_10.Background = System.Windows.Media.Brushes.White;
        }

        private void mark_5_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 5;

            mark_5.Background = System.Windows.Media.Brushes.DarkGray;

            mark_2.Background = System.Windows.Media.Brushes.White;
            mark_3.Background = System.Windows.Media.Brushes.White;
            mark_4.Background = System.Windows.Media.Brushes.White;
            mark_1.Background = System.Windows.Media.Brushes.White;
            mark_6.Background = System.Windows.Media.Brushes.White;
            mark_7.Background = System.Windows.Media.Brushes.White;
            mark_8.Background = System.Windows.Media.Brushes.White;
            mark_9.Background = System.Windows.Media.Brushes.White;
            mark_10.Background = System.Windows.Media.Brushes.White;
        }

        private void mark_6_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 6;

            mark_6.Background = System.Windows.Media.Brushes.DarkGray;

            mark_2.Background = System.Windows.Media.Brushes.White;
            mark_3.Background = System.Windows.Media.Brushes.White;
            mark_4.Background = System.Windows.Media.Brushes.White;
            mark_1.Background = System.Windows.Media.Brushes.White;
            mark_5.Background = System.Windows.Media.Brushes.White;
            mark_7.Background = System.Windows.Media.Brushes.White;
            mark_8.Background = System.Windows.Media.Brushes.White;
            mark_9.Background = System.Windows.Media.Brushes.White;
            mark_10.Background = System.Windows.Media.Brushes.White;
        }

        private void mark_7_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 7;

            mark_7.Background = System.Windows.Media.Brushes.Green;

            mark_2.Background = System.Windows.Media.Brushes.White;
            mark_3.Background = System.Windows.Media.Brushes.White;
            mark_4.Background = System.Windows.Media.Brushes.White;
            mark_1.Background = System.Windows.Media.Brushes.White;
            mark_6.Background = System.Windows.Media.Brushes.White;
            mark_5.Background = System.Windows.Media.Brushes.White;
            mark_8.Background = System.Windows.Media.Brushes.White;
            mark_9.Background = System.Windows.Media.Brushes.White;
            mark_10.Background = System.Windows.Media.Brushes.White;
        }

        private void mark_8_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 8;

            mark_8.Background = System.Windows.Media.Brushes.Green;

            mark_2.Background = System.Windows.Media.Brushes.White;
            mark_3.Background = System.Windows.Media.Brushes.White;
            mark_4.Background = System.Windows.Media.Brushes.White;
            mark_1.Background = System.Windows.Media.Brushes.White;
            mark_6.Background = System.Windows.Media.Brushes.White;
            mark_5.Background = System.Windows.Media.Brushes.White;
            mark_7.Background = System.Windows.Media.Brushes.White;
            mark_9.Background = System.Windows.Media.Brushes.White;
            mark_10.Background = System.Windows.Media.Brushes.White;
        }

        private void mark_9_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 9;

            mark_9.Background = System.Windows.Media.Brushes.Green;

            mark_2.Background = System.Windows.Media.Brushes.White;
            mark_3.Background = System.Windows.Media.Brushes.White;
            mark_4.Background = System.Windows.Media.Brushes.White;
            mark_1.Background = System.Windows.Media.Brushes.White;
            mark_6.Background = System.Windows.Media.Brushes.White;
            mark_5.Background = System.Windows.Media.Brushes.White;
            mark_8.Background = System.Windows.Media.Brushes.White;
            mark_7.Background = System.Windows.Media.Brushes.White;
            mark_10.Background = System.Windows.Media.Brushes.White;
        }

        private void mark_10_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.Mark = 10;

            mark_10.Background = System.Windows.Media.Brushes.Green;

            mark_2.Background = System.Windows.Media.Brushes.White;
            mark_3.Background = System.Windows.Media.Brushes.White;
            mark_4.Background = System.Windows.Media.Brushes.White;
            mark_1.Background = System.Windows.Media.Brushes.White;
            mark_6.Background = System.Windows.Media.Brushes.White;
            mark_5.Background = System.Windows.Media.Brushes.White;
            mark_8.Background = System.Windows.Media.Brushes.White;
            mark_9.Background = System.Windows.Media.Brushes.White;
            mark_7.Background = System.Windows.Media.Brushes.White;
        }
        #endregion

        //добавление оценки
        private void Add_mark1_Click(object sender, RoutedEventArgs e)
        {
            if (Public_Class.Mark > 0 && Public_Class.Mark < 11)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("INSERT INTO [Marks] (Mark, Login, Film_id) " +
                                                                    "VALUES (@mark, @login, (SELECT Film_id FROM [Films] WHERE Film_name = @film))");

                            sqlCommand.Parameters.AddWithValue("@mark", Public_Class.Mark);
                            sqlCommand.Parameters.AddWithValue("@login", Public_Class.Profile);
                            sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Оценка поставлена");
                                this.Close();
                            }

                            else
                                MessageBox.Show("Оценка НЕ поставлена");
                        }

                    }
                    catch (SqlException ex)
                    { MessageBox.Show("Оценка уже стоит"); }
                    finally
                    { conn.Close(); }
                }
            }
            else MessageBox.Show("Выберите оценку");
        }

        //изменение оценки
        private void Update_mark_Click(object sender, RoutedEventArgs e)
        {
            if (Public_Class.Mark > 0 && Public_Class.Mark < 11)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        using (SqlCommand sqlCommand = conn.CreateCommand())
                        {
                            sqlCommand.CommandText = string.Format("UPDATE [Marks] SET Mark = @mark " +
                                                                    "WHERE Login = @login AND Film_id = (SELECT Film_id FROM [Films] WHERE Film_name = @film)");

                            sqlCommand.Parameters.AddWithValue("@mark", Public_Class.Mark);
                            sqlCommand.Parameters.AddWithValue("@login", Public_Class.Profile);
                            sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Оценка изменена");
                                this.Close();
                            }

                            else
                                MessageBox.Show("Оценка НЕ изменена");
                        }
                    }
                    catch (SqlException ex)
                    { MessageBox.Show(ex.Message); }
                    finally
                    { conn.Close(); }
                }
            }
            else MessageBox.Show("Выберите оценку");
        }
    }
}
