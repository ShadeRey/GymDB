using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace Gym;

public partial class MainWindow : Window
{
    private string _connString = "server=10.10.1.24;database=pro1_23;User Id=user_01;password=user01pro";
    private List<Client> _clients;
    private MySqlConnection _connection;

    public MainWindow()
    {
        InitializeComponent();
        string fullTable = "select * from Client";
        ShowTable(fullTable);
        
    }

    public void ShowTable(string sql)
    {
        _clients = new List<Client>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentClient = new Client()
            {
                Client_id = reader.GetInt32("Client_id"),
                Name = reader.GetString("Name"),
                Surname = reader.GetString("Surname"),
                Patronymic = reader.GetString("Patronymic"),
                Phone_number = reader.GetInt32("Phone_number")
            };
            _clients.Add(currentClient);
        }

        _connection.Close();
        ClientGrid.ItemsSource = _clients;
    }

    private List<Client> _clientsPreSearch;
    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        // // string searchSql = $"select * from Client where Client.Name = '{txtSearch.Text}%';";
        // // ShowTable();
        if (_clientsPreSearch is null)
        {
            _clientsPreSearch = _clients;
        }
        
        if (txtSearch.Text is null)
        {
            return;
        }
        
        if (string.IsNullOrWhiteSpace(txtSearch.Text))
        {
            ClientGrid.ItemsSource = _clientsPreSearch;
            return;
        }
        //
        // ClientGrid.ItemsSource = _clientsPreSearch.Where(
        //     it => it.Name.ToLower().Contains(txtSearch.Text.ToLower())
        //           || it.Patronymic.ToLower().Contains(txtSearch.Text.ToLower())
        //           || it.Surname.ToLower().Contains(txtSearch.Text.ToLower())
        //           || it.Phone_number.ToString().Contains(txtSearch.Text.ToLower())
        // ).ToList();
        Filter();
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e) => Filter();

    private void Filter() {
        if (txtFilter.SelectedIndex == 0) {
            var filteredd = _clientsPreSearch.Where(
                it => it.Client_id.ToString().Contains(txtSearch.Text)
                      || it.Name.ToLower().Contains(txtSearch.Text)
                      || it.Surname.ToLower().Contains(txtSearch.Text)
                      || it.Patronymic.ToLower().Contains(txtSearch.Text)
                      || it.Phone_number.ToString().Contains(txtSearch.Text)
            ).ToList();
            filteredd = filteredd.OrderBy(id => id.Client_id).ToList();
            ClientGrid.ItemsSource = filteredd;
        }
        else if (txtFilter.SelectedIndex == 1) {
            var filteredd = _clientsPreSearch.Where(it => it.Client_id.ToString().Contains(txtSearch.Text)).ToList();
            filteredd = filteredd.OrderBy(id => id.Client_id).ToList();
            ClientGrid.ItemsSource = filteredd;
        }
        else if (txtFilter.SelectedIndex == 2) {
            var filtereddd = _clientsPreSearch.Where(it => it.Name.ToLower().Contains(txtSearch.Text)).ToList();
            filtereddd = filtereddd.OrderBy(nam => nam.Name).ToList();
            ClientGrid.ItemsSource = filtereddd;
        }
        else if (txtFilter.SelectedIndex == 3) {
            var filtered = _clientsPreSearch.Where(it => it.Surname.ToLower().Contains(txtSearch.Text)).ToList();
            filtered = filtered.OrderBy(surnam => surnam.Surname).ToList();
            ClientGrid.ItemsSource = filtered;
        }
        else if (txtFilter.SelectedIndex == 4) {
            var filteredddd = _clientsPreSearch.Where(it => it.Patronymic.ToLower().Contains(txtSearch.Text)).ToList();
            filteredddd = filteredddd.OrderBy(patronym => patronym.Patronymic).ToList();
            ClientGrid.ItemsSource = filteredddd;
        }
        else if (txtFilter.SelectedIndex == 5) {
            var filtereddddd = _clientsPreSearch.Where(it => it.Phone_number.ToString().Contains(txtSearch.Text)).ToList();
            filtereddddd = filtereddddd.OrderBy(phonenumb => phonenumb.Phone_number).ToList();
            ClientGrid.ItemsSource = filtereddddd;
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = ((Button)sender).Tag as Client;
        sss s = new sss(myValue);
        s.ShowDialog(this);
        s.Closed += (o, args) => {
            if (s.Result) {
                ShowTable("select * from Client");
            }
        };
    }

    private void ButtonDelete_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValuee = ((Button)sender).Tag as Client;
        ghoul g = new ghoul(myValuee);
        g.ShowDialog(this);
        g.Closed += (o, args) => {
            if (g.Result) {
                ShowTable("select * from Client");
            }
        };
    }

    private void NextWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        InstructorWindow instructor = new InstructorWindow();
        instructor.Show();
        Close();
    }
}
