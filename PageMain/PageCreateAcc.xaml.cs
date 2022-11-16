using ISRPO_Palashicheva_PR12.ApplicationData;
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

namespace ISRPO_Palashicheva_PR12.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageCreateAcc.xaml
    /// </summary>
    public partial class PageCreateAcc : Page
    {
        public PageCreateAcc()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            // проверяем есть ли такой же пользователь
            if(AppConnect.modelOdb.User.Count(x=>x.login==txbLogin.Text)>0)
            {
                MessageBox.Show("Пользователь с таким логином есть!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //если прошли проверку логина, записываем нового пользователя с ролью 2
            try 
            {
                User userObj = new User()
                {
                    login = txbLogin.Text, // получаем данные логина
                    name = txbName.Text, // получаем данные имени пользователя
                    password = txbPass.Text, // получаем роль
                    IdRole = 2 // присваиваем роль 2, т.к. администратор 1                 
                };

                AppConnect.modelOdb.User.Add(userObj);//добавление объекта
                AppConnect.modelOdb.SaveChanges(); //сохранение
                MessageBox.Show("Данные успешно добавлены!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                  "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void psbPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (psbPass.Password !=txbPass.Text)
            {
                btnCreate.IsEnabled = false;
                psbPass.Background = Brushes.LightCoral;
                psbPass.BorderBrush = Brushes.Red;
            }
            else
            {
                btnCreate.IsEnabled = true;
                psbPass.Background = Brushes.LightGreen;
                psbPass.BorderBrush = Brushes.Green;
            }
        }
    }
}
