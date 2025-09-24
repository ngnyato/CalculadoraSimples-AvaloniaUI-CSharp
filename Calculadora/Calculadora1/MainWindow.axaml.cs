using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Calculadora1;

public partial class MainWindow : Window
{
    public string input = "";
    public TextBox output;

    public MainWindow()
    {
        InitializeComponent();


         output = this.FindControl<TextBox>("Display");


    }

    private void Btn_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button b)
        {
            input += b.Content?.ToString();
            output.Text = input;
        }
    }
}
           
    
    
    