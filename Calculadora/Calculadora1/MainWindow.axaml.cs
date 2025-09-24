using System.Diagnostics;
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

        if (output == null){
         output = this.FindControl<TextBox>("Display");
        }


    }

    private void Btn_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button b)
        {
            if (b.Content == "C")
            {
                input = "";
                Debug.WriteLine("igual");
            }
            else if (b.Content == "=")
            {
                 // Aqui vao os calculos
                 Debug.WriteLine("resultados");
                 
            }
            else
            {
                input += b.Content?.ToString();
            }
            output.Text = input;
        }
        
    }
}
           
    
    
    