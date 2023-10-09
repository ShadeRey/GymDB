using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Gym;

public partial class ghoul : Window
{
    
    private readonly Client _client;

    public ghoul(Client client)
    {
        _client = client;
        InitializeComponent();
    }

    private void Yes_OnClick(object? sender, RoutedEventArgs e)
    {
        string _connString = "server=10.10.1.24;database=pro1_23;User Id=user_01;password=user01pro";
        MySqlConnection _connection;
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "DELETE from Client WHERE Client_id = @id LIMIT 1";
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32);
        command.Parameters["@id"].Value = _client.Client_id;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }

    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }

    public bool Result { get; private set; }

    private void No_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}