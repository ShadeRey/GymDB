using System;
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
    private readonly Action<Window, Client?> _action;

    public sss(Client client, Action<Window, Client?> action)
    {
        _client = client;
        _action = action;
        InitializeComponent();
        DataContext = _client;
    }

    private void AccBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        _action?.Invoke(this, DataContext as Client);
        this.Close(true);
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