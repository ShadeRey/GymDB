using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace Gym;

public partial class InstructorWindow : Window
{
    private string _connString = "server=10.10.1.24;database=pro1_23;User Id=user_01;password=user01pro";

    private MySqlConnectionStringBuilder _connectionStringBuilder = new MySqlConnectionStringBuilder() {
        Server = "10.10.1.24",
        Database = "pro1_23",
        UserID = "user_01",
        Password = "user01pro"
    };
    private List<Instructor> _instructors;
    private MySqlConnection _connection;
    public InstructorWindow()
    {
        InitializeComponent();
        string fullTable = "select * from Instructor;";
        _connection = new MySqlConnection(_connectionStringBuilder.ConnectionString);
        _connection.Open();
        ShowTable(fullTable);
    }
    public void ShowTable(string sql)
    {
        _instructors = new List<Instructor>();
        using MySqlCommand command = new MySqlCommand(sql, _connection);
        using MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows) {
            var id = reader.GetInt32("Instructor_id");
            var name = reader.GetString("Name");
            var surname = reader.GetString("Surname");
            var patronymic = reader.GetString("Patronymic");
            var currentInstructor = new Instructor()
            {
                Instructor_id = id,
                Name = name,
                Surname = surname,
                Patronymic = patronymic
            };
            _instructors.Add(currentInstructor);
        }
        _connection.Close();
        InstructorGrid.ItemsSource = _instructors;
    }

    private void PreviousWindow_OnClick(object? sender, RoutedEventArgs e) {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void NextWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        Working_week_days_window days = new Working_week_days_window();
        days.Show();
        Close();
    }
}