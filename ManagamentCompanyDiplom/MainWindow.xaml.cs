using ManagamentCompanyDiplom.Classes;
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
using ManagamentCompanyDiplom.Pages;
using ManagamentCompanyDiplom.Pages.UserPages;
using ManagamentCompanyDiplom.Pages.AdminPages;

namespace ManagamentCompanyDiplom
{
    public partial class MainWindow : Window
    {
        //public static List<Page> AllPages = new List<Page>();
        public MainWindow()
        {
            InitializeComponent();
            FrameApp.entObj = Frm;
            Frm.Navigate(new Autorization());
            //AllPages.Add(new UserProfilePage());
            //AllPages.Add(new UserServicesPage());
            //AllPages.Add(new UserPaymentPage());
            //AllPages.Add(new UserMyServicesPage());
            //AllPages.Add(new AdminApplicationPage());
            //AllPages.Add(new AdminApplicationServicesPage());
            //AllPages.Add(new AdminCreateUsersPage());
            //AllPages.Add(new AdminProfilePage());
            //AllPages.Add(new AdminServicesPage());
            //AllPages.Add(new AdminUsersPage());

        }
    }
}
