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
using System.IO;
using MahApps.Metro.Controls;

namespace WpfApp5
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        string formula = "";
        List<string> expressions = new List<string>();

        private int GetSignPriority(char sign) 
        {
            switch (sign)
            {
                case '(':                       
                    return 0;
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
            }
            return -1;
        }
        private int GetTheTypeOfObj(string obj)
        {
            switch(obj)
            {
                case "+":
                    return 1;
                case "-":
                    return 2;
                case "*":
                    return 3;
                case "/":
                    return 4;
                default:
                    return 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            formula += Convert.ToString((sender as Button).Content);
            expressions.Add(Convert.ToString((sender as Button).Content));
            label1.Content += Convert.ToString((sender as Button).Content);
        }
        private void Button_del_Click(object sender, RoutedEventArgs e)
        {
            if (formula.Length > 0)
            {
                formula = formula.Remove(formula.Length - 1);
                label1.Content = formula;
            }
        }
        private void Button_clear_Click(object sender, RoutedEventArgs e)
        {
            formula = "";
            label1.Content = "";
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FileStream resultfile = new FileStream("result.txt", FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(resultfile);
            foreach (string a in expressions)
            {
                streamWriter.Write(a);
            }
            streamWriter.Close();
        }
        private void Button_equ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stack<double> GetTheConquenceStack = new Stack<double>();
                Stack<char> signStack = new Stack<char>();
                Queue<string> postfixExpressionQueue = new Queue<string>();
                string tempStr = "";
                int objType;
                double tempDouble;
                for (int i = 0; i < formula.Length; i++)
                {
                    if (formula[i] <= '9' && formula[i] >= '0' || formula[i] == '.')
                    {
                        tempStr += formula[i];
                    }

                    else
                    {
                        if (tempStr.Length > 0)
                        {
                            postfixExpressionQueue.Enqueue(tempStr);
                            tempStr = "";
                        }
                        if (signStack.Count == 0)
                        {
                            signStack.Push(formula[i]);
                        }
                        else
                        {
                            if (formula[i] == '(')
                            {
                                signStack.Push('(');
                            }
                            else if (formula[i] == ')')
                            {
                                
                                char tempSign;
                                while (true)
                                {
                                    tempSign = signStack.Pop();
                                    if (tempSign != '(')
                                    {
                                        postfixExpressionQueue.Enqueue(Convert.ToString(tempSign));
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (GetSignPriority(formula[i]) > GetSignPriority(signStack.Peek()))
                                {
                                    signStack.Push(formula[i]);
                                }
                                else
                                {
                                    while (true)
                                    {
                                        postfixExpressionQueue.Enqueue(Convert.ToString(signStack.Pop()));
                                        if (signStack.Count == 0)
                                            break;
                                        else if (GetSignPriority(formula[i]) > GetSignPriority(signStack.Peek()))
                                            break;
                                    }
                                    signStack.Push(formula[i]);
                                }
                            }

                        }
                    }
                }
                if (tempStr.Length > 0)
                {
                    postfixExpressionQueue.Enqueue(tempStr);
                    tempStr = "";
                }
                while (signStack.Count > 0)
                {
                    postfixExpressionQueue.Enqueue(Convert.ToString(signStack.Pop()));
                }
                signStack.Clear();
                tempStr = "";
                while (postfixExpressionQueue.Count > 0)
                {
                    objType = GetTheTypeOfObj(postfixExpressionQueue.Peek());
                    switch (objType)
                    {
                        case 0:                
                            GetTheConquenceStack.Push(Convert.ToDouble(postfixExpressionQueue.Dequeue()));
                            break;
                        case 1:
                            postfixExpressionQueue.Dequeue();
                            GetTheConquenceStack.Push(GetTheConquenceStack.Pop() + GetTheConquenceStack.Pop());
                            break;
                        case 2:
                            postfixExpressionQueue.Dequeue();
                            GetTheConquenceStack.Push(-GetTheConquenceStack.Pop() + GetTheConquenceStack.Pop());
                            break;
                        case 3:
                            postfixExpressionQueue.Dequeue();
                            GetTheConquenceStack.Push(GetTheConquenceStack.Pop() * GetTheConquenceStack.Pop());
                            break;
                        case 4:
                            postfixExpressionQueue.Dequeue();
                            tempDouble = GetTheConquenceStack.Pop();
                            if (tempDouble != 0.0)
                                GetTheConquenceStack.Push(GetTheConquenceStack.Pop() / tempDouble);
                            else
                            {
                                MessageBox.Show("Error: zero divisor.");
                            }
                            break;
                        default:
                            MessageBox.Show("Unknown Error.");
                            break;
                    }
                }
                formula = Convert.ToString(GetTheConquenceStack.Pop());
                label1.Content = formula;
                expressions.Add("=" + formula);
            }
            catch
            { };
        }
        public MainWindow()
        {
                InitializeComponent();
        }
    }
}
