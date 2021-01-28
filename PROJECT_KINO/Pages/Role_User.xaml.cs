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

namespace PROJECT_KINO.Pages
{
    /// <summary>
    /// Логика взаимодействия для Role_User.xaml
    /// </summary>
    public partial class Role_User : Page
    {
        public Role_User()
        {
            InitializeComponent();

            Nik_lbl.Content = Public_Class.Profile;
        }

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Search_films());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Public_Class.User_name = Public_Class.Profile;
            NavigationService?.Navigate(new User_page());
        }
    }
}
