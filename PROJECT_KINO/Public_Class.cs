using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_KINO
{
    static class Public_Class
    {
        public static string Profile { get; set; } //Имя текущего пользователя
        public static string User_name { get; set; } //имя просматриваемого пользователя

        public static string Film_name { get; set; } //название фильма

        public static string Film_year { get; set; } //год выхода фильма

        public static string Film_time { get; set; } //длительность фильма

        public static int Role_num { get; set; } //номер роли для авторизации

        public static int Mark { get; set; } //выбранная оценка для выставления
    }
}
