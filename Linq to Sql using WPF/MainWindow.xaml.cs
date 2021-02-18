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
            string connectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UniversityManagement.mdf;Integrated Security=True";
            dataContext = new LinqToSqlDataClassDataContext(connectionString);
            //AddUniversity();
            //AddStudent();
            AddLecture();
        }

        public void AddUniversity()
        {
            dataContext.Universities.InsertOnSubmit(new University() { Name = "Politecnico"});
            dataContext.Universities.InsertOnSubmit(new University(){Name = "Oxford"});
            dataContext.Universities.InsertOnSubmit(new University(){Name = "Stanford" });
            dataContext.SubmitChanges();
            //MainDataGrid.ItemsSource = dataContext.Universities;
        }

        public void AddStudent()
        {
            var universities = dataContext.Universities.ToList();
            List<Student> students = new List<Student>();
            students.Add(new Student(){Name = "Mina",Gender = "Male",UniversityId = universities[0].Id});
            students.Add(new Student(){Name = "Sara",Gender = "Female",University = universities[1]});
            students.Add(new Student(){Name = "Luca",Gender = "Male",University = universities[2]});

            dataContext.Students.InsertAllOnSubmit(students);
            dataContext.SubmitChanges();
            MainDataGrid.ItemsSource = dataContext.Students;
        }

        public void AddLecture()
        {
            dataContext.Lectures.InsertOnSubmit(new Lecture() { Name = "Math" });
            dataContext.Lectures.InsertOnSubmit(new Lecture() { Name = "English" });
            dataContext.Lectures.InsertOnSubmit(new Lecture() { Name = "Physics" });
            dataContext.Lectures.InsertOnSubmit(new Lecture() { Name = "Arabic" });

            dataContext.SubmitChanges();
            MainDataGrid.ItemsSource = dataContext.Lectures;
        }
    }
}
