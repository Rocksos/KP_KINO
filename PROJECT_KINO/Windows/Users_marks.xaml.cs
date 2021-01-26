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
    /// Логика взаимодействия для Users_marks.xaml
    /// </summary>
    public partial class Users_marks : Window
    {
        public Users_marks()
        {
            InitializeComponent();

            Film_lbl.Content = "«" + Public_Class.Film_name + "»";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandText = string.Format("SELECT Login, Mark FROM [Marks] WHERE Film_id = (SELECT Film_id FROM [Films] WHERE Film_name = @film)");
                    sqlCommand.Parameters.AddWithValue("@film", Public_Class.Film_name);

                    using (SqlDataAdapter adpt = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable marks = new DataTable();
                        adpt.Fill(marks);
                        Marks_grid.ItemsSource = marks.DefaultView;
                    }
                }
                conn.Close();
            }
        }
    }
}
