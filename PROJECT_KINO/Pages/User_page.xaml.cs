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
    /// Логика взаимодействия для User_page.xaml
    /// </summary>
    public partial class User_page : Page
    {
        public User_page()
        {
            InitializeComponent();

            #region Заполнение страницы
            User_nickname.Content = Public_Class.User_name;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                //заполнение личных данных
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Users.Login, Users.Sex, Countries.Country_name " +
                                                           "FROM Countries INNER JOIN Users ON Countries.Country_id = Users.Country_id " +
                                                           "WHERE Login = @login");

                    sqlCommand.Parameters.AddWithValue("@login", Public_Class.User_name);

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                User_country.Content = sqlDataReader[2];
                                string user_sex = sqlDataReader[1].ToString();

                                if (user_sex == "м") User_sex.Content = "Мужской";
                                else User_sex.Content = "Женский";
                            }
                        }
                    }
                }

                //заполнение грида с оценками
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Films.Film_name, Films.Premiere_year, Marks.Mark " +
                                                           "FROM Users INNER JOIN Marks ON Users.Login = Marks.Login " +
                                                           "INNER JOIN Films ON dbo.Marks.Film_id = dbo.Films.Film_id " +
                                                           "WHERE Users.Login = @login");

                    sqlCommand.Parameters.AddWithValue("@login", Public_Class.User_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable marks = new DataTable();
                        adpt.Fill(marks);
                        UserMarks_grid.ItemsSource = marks.DefaultView;
                    }
                }

                conn.Close();
            }
            #endregion
        }
    }
}

