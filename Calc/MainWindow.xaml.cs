using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static double numberOne; // число в верхнем текстблоке (expressionTxt)
        static double numberTwo; // число в нижнем текстблоке (resultTxt)
        static string oper; // символ операции (+ - * /)
        static bool flag = false; // флаг для определения возможности ввода текста в нижнем текстблоке (resultTxt)
        static string lastChar; // последний символ введенного числа

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // обработка нажатий на кнопки в окне программы
        {
            Button button = sender as Button;
            string content = button.Content.ToString();

            switch (content)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    EnterNumber(content);
                    break;

                case "+":
                    FinalizeDecimalNumber();
                    Operation(content);
                    oper = "+";
                    flag = false;
                    break;

                case "-":
                    FinalizeDecimalNumber();
                    Operation(content);
                    oper = "-";
                    flag = false;
                    break;

                case "*":
                    FinalizeDecimalNumber();
                    Operation(content);
                    oper = "*";
                    flag = false;
                    break;

                case "/":
                    FinalizeDecimalNumber();
                    Operation(content);
                    oper = "/";
                    flag = false;
                    break;

                case ",":
                    EnterDecimal();
                    break;

                case "C":
                    ClearAllTextBox();
                    break;

                case "CE":
                    ClearResultTextBox();
                    break;

                case "←":
                    Backspace();
                    break;

                case "+/-":
                    if (resultTxt.Text != "0")
                    {
                        if (expressionTxt.Text.Contains("=") || expressionTxt.Text.Contains("("))
                        {
                            if (expressionTxt.Text.Contains("="))
                            {
                                expressionTxt.Text = "negate(" + resultTxt.Text + ")";
                            }
                            else
                                expressionTxt.Text = "negate(" + expressionTxt.Text + ")";
                        }
                        resultTxt.Text = ((-1) * Convert.ToDouble(resultTxt.Text)).ToString();
                    }
                    flag = false;
                    break;

                case "x²":
                    if (expressionTxt.Text != "")
                    {
                        if (expressionTxt.Text.Contains("=") || expressionTxt.Text.Contains("("))
                        {
                            if (expressionTxt.Text.Contains("="))
                            {
                                expressionTxt.Text = "sqr(" + resultTxt.Text + ")";
                            }
                            else
                            {
                                expressionTxt.Text = "sqr(" + expressionTxt.Text + ")";
                            }
                        }
                    }
                    else
                    {
                        expressionTxt.Text = "sqr(" + resultTxt.Text + ")";
                    }
                    flag = false;
                    resultTxt.Text = Math.Pow(Convert.ToDouble(resultTxt.Text), 2).ToString();
                    break;


                case "√x":
                    if (expressionTxt.Text != "")
                    {
                        if (expressionTxt.Text.Contains("=") || expressionTxt.Text.Contains("("))
                        {
                            if (expressionTxt.Text.Contains("="))
                            {
                                expressionTxt.Text = "√(" + resultTxt.Text + ")";
                            }
                            else
                            {
                                expressionTxt.Text = "√(" + expressionTxt.Text + ")";
                            }
                        }
                    }
                    else
                    {
                        expressionTxt.Text = "√(" + resultTxt.Text + ")";
                    }
                    flag = false;
                    resultTxt.Text = Math.Pow(Convert.ToDouble(resultTxt.Text), 0.5).ToString();
                    break;

                case "1/x":
                    if (expressionTxt.Text != "")
                    {
                        if (expressionTxt.Text.Contains("=") || expressionTxt.Text.Contains("("))
                        {
                            if (expressionTxt.Text.Contains("="))
                            {
                                expressionTxt.Text = "1/(" + resultTxt.Text + ")";
                            }
                            else
                            {
                                expressionTxt.Text = "1/(" + expressionTxt.Text + ")";
                            }
                        }
                    }
                    else
                    {
                        expressionTxt.Text = "1/(" + resultTxt.Text + ")";
                    }
                    flag = false;
                    resultTxt.Text = Math.Pow(Convert.ToDouble(resultTxt.Text), -1).ToString();
                    break;

                case "%":
                    if (expressionTxt.Text == "" || expressionTxt.Text == "0")
                    {
                        expressionTxt.Text = "";
                        resultTxt.Text = "0";
                    }
                    else if (expressionTxt.Text.Contains("+") || expressionTxt.Text.Contains("-") || expressionTxt.Text.Contains("*") || expressionTxt.Text.Contains("/"))
                    {
                        int x = resultTxt.Text.Length - 1;
                        resultTxt.Text = (Convert.ToDouble(resultTxt.Text) * numberOne / 100).ToString();
                    }
                    break;

                case "=":
                    FinalizeDecimalNumber();
                    Equal();
                    flag = false;

                    break;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) // обработка нажатий на клавиши клавиатуры
        {
            string content;

            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                    content = ((int)e.Key - 34).ToString();
                    EnterNumber(content);
                    break;

                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    content = ((int)e.Key - 74).ToString();
                    EnterNumber(content);
                    break;

                case Key.OemMinus:
                    FinalizeDecimalNumber();
                    Operation("-");
                    oper = "-";
                    flag = false;
                    break;

                case Key.OemPlus:
                    FinalizeDecimalNumber();
                    Operation("+");
                    oper = "+";
                    flag = false;
                    break;

                case Key.Add:
                    FinalizeDecimalNumber();
                    Operation("+");
                    oper = "+";
                    flag = false;
                    break;

                case Key.Subtract:
                    FinalizeDecimalNumber();
                    Operation("-");
                    oper = "-";
                    flag = false;
                    break;

                case Key.Multiply:
                    FinalizeDecimalNumber();
                    Operation("*");
                    oper = "*";
                    flag = false;
                    break;

                case Key.Divide:
                    FinalizeDecimalNumber();
                    Operation("/");
                    oper = "/";
                    flag = false;
                    break;

                case Key.Decimal:
                    EnterDecimal();
                    break;

                case Key.Back:
                    Backspace();
                    break;

                case Key.Escape:
                    ClearAllTextBox();
                    break;

                case Key.Delete:
                    ClearResultTextBox();
                    break;

                case Key.Return:
                    FinalizeDecimalNumber();
                    Equal();
                    flag = false;

                    break;
            }
        }

        private void EnterNumber(string content) // ввод чисел в нижнем текстблоке (resultTxt)
        {
            if (resultTxt.Text == "0")
            {
                resultTxt.Text = null;
                resultTxt.Text += content;
                flag = true;
            }
            else if (resultTxt.Text != "")
            {
                if (expressionTxt.Text.Contains("(") || expressionTxt.Text.Contains("="))
                {
                    expressionTxt.Text = "";
                    resultTxt.Text = content;
                    flag = true;
                    return;
                }

                if (flag == false)
                {
                    if (!expressionTxt.Text.Contains("="))
                    {
                        resultTxt.Text = null;
                        resultTxt.Text += content;
                        flag = true;
                    }
                    else
                    {
                        if (!expressionTxt.Text.Contains(","))
                        {
                            ClearAllTextBox();
                            resultTxt.Text = null;
                            resultTxt.Text += content;
                            flag = true;
                        }
                        else
                        {
                            resultTxt.Text += content;
                            flag = true;

                        }
                    }
                }
                else
                {
                    resultTxt.Text += content;
                }
            }
        }

        private void Operation(string operation) // операции сложения, вычитания, умножения, деления
        {
            if (expressionTxt.Text != "" && expressionTxt.Text.Last().ToString() != "=")
            {
                string oper = expressionTxt.Text.Last().ToString();
                int x = expressionTxt.Text.Length - 1;
                expressionTxt.Text = expressionTxt.Text.Remove(x);
                if (!expressionTxt.Text.Contains("("))
                {
                    numberOne = Convert.ToDouble(expressionTxt.Text);
                    numberTwo = Convert.ToDouble(resultTxt.Text);
                }
                else
                {
                    numberOne = Convert.ToDouble(resultTxt.Text);
                    numberTwo = Convert.ToDouble(resultTxt.Text);
                }


                switch (oper)
                {
                    case "+":
                        resultTxt.Text = Addition(numberOne, numberTwo).ToString();
                        break;
                    case "-":
                        resultTxt.Text = Subtraction(numberOne, numberTwo).ToString();
                        break;
                    case "*":
                        resultTxt.Text = Multiplication(numberOne, numberTwo).ToString();
                        break;
                    case "/":
                        resultTxt.Text = Dividing(numberOne, numberTwo).ToString();
                        break;
                }
            }
            numberOne = Convert.ToDouble(resultTxt.Text);
            expressionTxt.Text = resultTxt.Text + operation;
        }

        private double Addition(double a, double b)
        {
            return a + b;
        }

        private double Subtraction(double a, double b)
        {
            return a - b;
        }

        private double Multiplication(double a, double b)
        {
            return a * b;
        }

        private double Dividing(double a, double b)
        {
            return a / b;
        }

        private void EnterDecimal() // ввод запятой в нижнем текстблоке (resultTxt)
        {
            if (!resultTxt.Text.Contains(","))
            {
                if (resultTxt.Text == "")
                {
                    resultTxt.Text = "0,";
                    flag = true;
                }
                else
                {
                    if (!expressionTxt.Text.Contains("(") && !expressionTxt.Text.Contains("="))
                    {
                        resultTxt.Text += ",";
                        flag = true;
                    }
                    else
                    {
                        ClearAllTextBox();
                        resultTxt.Text = "0,";
                        flag = true;
                    }
                }
            }
            else
            {
                if (expressionTxt.Text.Contains("=") || expressionTxt.Text.Contains("("))
                {
                    ClearAllTextBox();
                    resultTxt.Text = "0,";
                    flag = true;
                }
                else
                {
                    resultTxt.Text = "0,";
                    flag = true;
                }
            }
        }

        private void FinalizeDecimalNumber() // добавление нуля после запятой в нижнем текстблоке (resultTxt), если пользователь не ввел цифру после запятой
        {
            lastChar = resultTxt.Text.Substring(resultTxt.Text.Length - 1);
            if (lastChar == ",")
                resultTxt.Text = resultTxt.Text + "0";
        }

        private void Backspace() // удаление символа перед курсором в нижнем текстблоке (resultTxt) - нажатие клавиши BackSpaсe или кнопки ←
        {
            if (!expressionTxt.Text.Contains("="))
            {
                if (!expressionTxt.Text.Contains("("))
                {
                    if (resultTxt.Text.Length > 1)
                    {
                        int x = resultTxt.Text.Length - 1;
                        resultTxt.Text = resultTxt.Text.Remove(x);
                        if (resultTxt.Text.Contains("-") && resultTxt.Text.Length == 1)
                            resultTxt.Text = "0";
                    }
                    else
                    {
                        resultTxt.Text = "0";
                    }
                }
                else
                {
                    ClearAllTextBox();
                }
            }
            else
            {
                ClearAllTextBox();
            }
        }


        private void ClearAllTextBox() // нажатие клавиши Eskape или кнопки С
        {
            expressionTxt.Text = "";
            resultTxt.Text = "0";
            numberOne = 0;
            numberTwo = 0;

        }

        private void ClearResultTextBox() // нажатие клавиши Delite или кнопки СE
        {
            resultTxt.Text = "0";
            numberTwo = 0;
            if (expressionTxt.Text.Contains("=") || expressionTxt.Text.Contains("("))
            {
                expressionTxt.Text = "";
                numberOne = 0;
            }
        }

        private void Equal() // нажатие клавиши Enter или кнопки =
        {
            if (!expressionTxt.Text.Contains("=") && !expressionTxt.Text.Contains("("))
            {
                if (resultTxt.Text != "")
                {
                    numberTwo = Convert.ToDouble(resultTxt.Text);
                }
                else
                {
                    numberTwo = 0;
                    expressionTxt.Text += "0";
                }
                expressionTxt.Text += resultTxt.Text + "=";

                switch (oper)
                {
                    case "+":
                        resultTxt.Text = Addition(numberOne, numberTwo).ToString();
                        break;
                    case "-":
                        resultTxt.Text = Subtraction(numberOne, numberTwo).ToString();
                        break;
                    case "*":
                        resultTxt.Text = Multiplication(numberOne, numberTwo).ToString();
                        break;
                    case "/":
                        resultTxt.Text = Dividing(numberOne, numberTwo).ToString();
                        break;
                }
            }
        }

        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e) // копирование содержимого нижнего текстблока (resultTxt)
        {
            Clipboard.SetText(resultTxt.Text);
        }
    }
}
