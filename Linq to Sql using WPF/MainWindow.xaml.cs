using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Linq_to_Sql_using_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LinqToSqlDataClassDataContext dataContext;
        public MainWindow()
        {
            InitializeComponent();
            //Add configuration references + Linq to SQL class
            string connectionString = ConfigurationManager
                .ConnectionStrings["LinqtoSqlusingWPF.Properties.Settings.UniversityManagementConnectionString"]
                .ConnectionString;
            dataContext = new LinqToSqlDataClassDataContext(connectionString);
        }
    }
}
