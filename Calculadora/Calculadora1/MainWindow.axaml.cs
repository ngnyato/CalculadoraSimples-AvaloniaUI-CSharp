using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Globalization;

namespace Calculadora1;

public partial class MainWindow : Window
{
    private string input = "";
    private TextBox output; // caxa de txt para meter a expressao

    public MainWindow()
    {
        InitializeComponent();
        if (output == null)  // isto é para caso a txtBox nao estiver atribuida ele atribui a uma txt box que tenha o name de Display
        {
            output = this.FindControl<TextBox>("Display");
        }
    }

    private void Btn_Click(object? sender, RoutedEventArgs e)    // apenas bts com Click="Btn_Click" vao dar trigger nisto
    {
        if (sender is Button b)  // se o sender for um buttao é def como buttao b que vai servir de variavel para qual seja o nosso sender
        {


            // CLEAR
            if (b.Content == "C")
            {
             ClearDisplay();
             return; // evita continuar a concatenar, ou seja, vai fzr com que por tu teres clicado o buttao C ele nao adicione o C ao display
            }

            // IGUAL
            if (b.Content == "=")
            {
                char ch = ' ';
                var s = input.Replace(" ", "");

                // encontra o primeiro operador (ignora '-' no início)
                int pos = -1;
                char op = ' ';
                
                    for (int i = 1; i < s.Length; i++)
                    {
                        ch = s[i];
                        if (ch == '+' || ch == '-' || ch == '*' || ch == '/')
                        {
                            pos = i;
                            op = ch;
                        }
                    }
                    if (pos == -1)
                    {
                        output.Text = "Operador inválido";
                        return;
                    }

                

                string left = s[..pos];
                string right = s[(pos + 1)..];

                if (!double.TryParse(left.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture,
                        out var a) ||
                    !double.TryParse(right.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture,
                        out var bnum))
                {
                    output.Text = "Número inválido";
                    input = "";
                    return;
                }

                
                double result = 0;
                switch (op)
                {
                    case '+':
                        result = a + bnum;
                        break;
                    case '/':
                        result = a / bnum;
                        break;
                    case '*':
                        result = a * bnum;
                        break;
                    case '-':
                        result = a - bnum;
                        break;

                    default:
                        output.Text = "Error";
                        break;
                }
                output.Text = result.ToString();

            }

            if (b.Content.ToString() != "C" && b.Content.ToString() != "=")
            {
                AdicionarChar(b.Content.ToString());
            }
        }
    }
    
    private void AdicionarChar(string text)
    {
        input += text;
        output.Text = input;
    }

        private void  ClearDisplay()
        {
            input = "";
            output.Text = "";
            Debug.WriteLine("clear");
        }
}
           
    
    
    