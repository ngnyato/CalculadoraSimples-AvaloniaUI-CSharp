using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Globalization;
using Avalonia.Input;

namespace Calculadora1;

public partial class MainWindow : Window
{
    private string input = "";
    private TextBox output; // caxa de txt para meter a expressao

    public MainWindow()
    {
        InitializeComponent();
        this.Focusable = true;
        this.Focus();
        this.KeyDown += MainWindow_KeyDown;

        if (output == null) // isto é para caso a txtBox nao estiver atribuida ele atribui a uma txt box que tenha o name de Display
        {
            output = this.FindControl<TextBox>("Display");
        }
    }

    #region Input Buttons

    private void Btn_Click(object? sender, RoutedEventArgs e) // apenas bts com Click="Btn_Click" vao dar trigger nisto
    {
        if (sender is Button b) // se o sender for um buttao é def como buttao b que vai servir de variavel para qual seja o nosso sender
        {

            string btContent = b.Content.ToString();

            // CLEAR
            if (b.Name == "btC")
            {
                ClearDisplay();
                return; // evita continuar a concatenar, ou seja, vai fzr com que por tu teres clicado o buttao C ele nao adicione o C ao display
            }
            if (b.Name == "btEq")
            {
                Calcular();
            }

            if (b.Name == "btBack")
            {
                ApagarUltimo();
            }
            if  (b.Name != "btBack" && b.Name != "btC" && b.Name != "btEq")
            {
            AdicionarChar(btContent);
            }
        }
    }

    #endregion

    #region Input Teclado

    private void MainWindow_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.D0:
            case Key.NumPad0:
                AdicionarChar("0");
                break;

            case Key.D1:
            case Key.NumPad1:
                AdicionarChar("1");
                break;

            case Key.D2:
            case Key.NumPad2:
                AdicionarChar("2");
                break;

            case Key.D3:
            case Key.NumPad3:
                AdicionarChar("3");
                break;

            case Key.D4:
            case Key.NumPad4:
                AdicionarChar("4");
                break;

            case Key.D5:
            case Key.NumPad5:
                AdicionarChar("5");
                break;

            case Key.D6:
            case Key.NumPad6:
                AdicionarChar("6");
                break;

            case Key.D7:
            case Key.NumPad7:
                AdicionarChar("7");
                break;

            case Key.D8:
            case Key.NumPad8:
                AdicionarChar("8");
                break;

            case Key.D9:
            case Key.NumPad9:
                AdicionarChar("9");
                break;

            case Key.OemPlus:
            case Key.Add:
                AdicionarChar("+");
                break;

            case Key.OemMinus:
            case Key.Subtract:
                AdicionarChar("-");
                break;

            case Key.X:
            case Key.Multiply:
                AdicionarChar("*");
                break;

            case Key.Divide:
            case Key.Oem2: // "/"
                AdicionarChar("/");
                break;

            case Key.Decimal:
            case Key.OemPeriod:
                AdicionarChar(".");
                break;

            case Key.Enter:
                Calcular();
                break;

            case Key.Back:
                ApagarUltimo();
                break;

            case Key.Escape:
            case Key.C:
                ClearDisplay();
                break;
        }
    }

    #endregion


    private void AdicionarChar(string text)
    {
        input += text;
        output.Text = input;
    }

    private void ClearDisplay()
    {
        input = "";
        output.Text = "";
        Debug.WriteLine("clear");
    }

    private void ApagarUltimo()
    {
        if (input.Length > 0) // verificar se string nao esta vazia
        {
            input = input[..^1]; // apagar ultimo
            output.Text = input; // mostrar no display
        }
    }

    private void Calcular()
    {
        char ch = ' ';
        var s = input.Replace(" ", "");

        // encontra o primeiro operador (ignora '-' no início)
        int pos = -1;
        char op = ' ';

        for (int i = 1; i < s.Length; i++) // for percorre a expressao
        {
            ch = s[i]; //guarda o char local
            if (ch == '+' || ch == '-' || ch == '*' || ch == '/') // verifica se é Oper.
            {
                pos = i; // aqui guarda a Posicao do OP para dividir a expressao depois
                op = ch; // aqui guarda qual é o OPq
            }
        }

        if (pos == -1)
        {
            output.Text = "Operador inválido";
            return;
        }

        string left = s[..pos]; // guarda a esquerda do op
        string right = s[(pos + 1)..]; //== a direita do op

        if (op == '/' && right == "0")
        {
            output.Text = "Impossible";
        }
        else
        {
            if (!double.TryParse(left.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture,
                    out var a) || // isto server para converter os . em , para o pc entender
                !double.TryParse(right.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture,
                    out var bnum))
            {
                output.Text = "Número inválido";
                input = "";
                return;
            }

            if (op == '/' && right == "0")
            {
                output.Text = "Impossible Operation";
            }
            else
            {
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
        }


    }

    private void  History_onClick(object? sender, RoutedEventArgs e)
    {
        // aqui temos de meter a logica de guardar as expressoes !!
    }
}
           
    
    
    