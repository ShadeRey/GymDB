using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Gym;

public partial class Working_week_days_window : Window
{
    private string _connString = "server=10.10.1.24;database=pro1_23;User Id=user_01;password=user01pro";
    private List<Working_week_days> _working;
    private MySqlConnection _connection;

    public Working_week_days_window()
    {
        InitializeComponent();
        string fullTable = "select * from Working_week_days";
        ShowTable(fullTable);
    }

    public void ShowTable(string sql)
    {
        _working = new List<Working_week_days>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentDays = new Working_week_days()
            {
                Working_week_days_id = reader.GetInt32("Working_week_days_id"),
                Working_days = reader.GetString("Working_days")
            };
            _working.Add(currentDays);
        }
        _connection.Close();
        ClientGrid.ItemsSource = _working;
    }
}