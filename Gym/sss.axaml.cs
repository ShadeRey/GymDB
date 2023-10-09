using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Gym;

public partial class sss : Window
{
    private readonly Client _client;

    public sss(Client client)
    {
        _client = client;
        InitializeComponent();
        DataContext = _client;
    }

    private void AccBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        string _connString = "server=10.10.1.24;database=pro1_23;User Id=user_01;password=user01pro";
        List<Client> _clients;
        MySqlConnection _connection;
        string sql="UPDATE Client SET Name = @namebox, Surname = @surnamebox, Patronymic = @patronymicbox, Phone_number = @phonebox WHERE Client_id = @id";
        _clients = new List<Client>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@id", MySqlDbType.Int32);
        command.Parameters["@id"].Value = _client.Client_id;
        command.Parameters.Add("@namebox", MySqlDbType.VarChar);
        command.Parameters["@namebox"].Value = namebox.Text;
        command.Parameters.Add("@surnamebox", MySqlDbType.VarChar);
        command.Parameters["@surnamebox"].Value = surnamebox.Text;
        command.Parameters.Add("@patronymicbox", MySqlDbType.VarChar);
        command.Parameters["@patronymicbox"].Value = patronymicbox.Text;
        command.Parameters.Add("@phonebox", MySqlDbType.Int32);
        command.Parameters["@phonebox"].Value = phonebox.Text;
        command.ExecuteNonQuery();
        _connection.Close();
        Close(true);
    }

    public void Close(bool result) {
        Result = result;
        base.Close(result);
    }
    public bool Result { get; private set; }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}