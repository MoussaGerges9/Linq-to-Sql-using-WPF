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
            AddUniversity();
            AddStudent();
            AddLecture();
            AddStudentLectures();
            GetUniversitiesWithFemale();
        }

        public void AddUniversity()
        {
            dataContext.Universities.InsertOnSubmit(new University() { Name = "Politecnico" });
            dataContext.Universities.InsertOnSubmit(new University() { Name = "Oxford" });
            dataContext.Universities.InsertOnSubmit(new University() { Name = "Stanford" });
            dataContext.SubmitChanges();
            //MainDataGrid.ItemsSource = dataContext.Universities;
        }

        public void AddStudent()
        {
            var universities = dataContext.Universities.ToList();
            List<Student> students = new List<Student>();
            students.Add(new Student() { Name = "Mina", Gender = "Male", UniversityId = universities[0].Id });
            students.Add(new Student() { Name = "Sara", Gender = "Female", University = universities[1] });
            students.Add(new Student() { Name = "Nicol", Gender = "Female", University = universities[2] });
            students.Add(new Student() { Name = "Luca", Gender = "Male", University = universities[2] });

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
            //MainDataGrid.ItemsSource = dataContext.Lectures;
        }

        public void AddStudentLectures()
        {
            var students = dataContext.Students.ToList();
            var lectures = dataContext.Lectures.ToList();
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture() { StudentId = students[0].Id, LectureId = lectures[0].Id });
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture() { StudentId = students[1].Id, LectureId = lectures[1].Id });
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture() { StudentId = students[2].Id, LectureId = lectures[2].Id });

            var allDataStudents = from studLec in dataContext.StudentLectures
                select new { studLec.Student.Name, LectureName = studLec.Lecture.Name, studLec.Student.Gender,
                    UniversityName = studLec.Student.University.Name };

            dataContext.SubmitChanges();
            //MainDataGrid.ItemsSource = allDataStudents;
        }

        public void GetUniversitiesWithFemale()
        {
            var universities = from student in dataContext.Students
                where student.Gender == "Female"
                select student.University;
            MainDataGrid.ItemsSource = universities;

        }
    }
}
