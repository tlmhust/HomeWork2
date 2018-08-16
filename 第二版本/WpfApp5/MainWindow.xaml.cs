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
       
        string Number1 = "", Number2 = "";
        Operator flag = Operator.none;
        enum Operator { add, sub, mul, div, none };
        List<string> expressions = new List<string>();
        private void Button_number_Click(object sender, RoutedEventArgs e)
        {
            Numberinput(Convert.ToString((sender as Button).Content));
        }
        private void Button_flag_Click(object sender, RoutedEventArgs e)
        {
            Flaginput(Convert.ToString((sender as Button).Content));
        }
        private void Button_del_Click(object sender, RoutedEventArgs e)
        {
            if (flag == Operator.none)
            {
                if (Number1 != null)
                {
                    if (Number1.Length > 0)
                        Number1 = Number1.Remove(Number1.Length - 1);
                    label1.Content = Number1;
                }
            }
            else
            {
                if (Number2 != null)
                {
                    if (Number2.Length > 0)
                        Number2 = Number2.Remove(Number2.Length - 1);
                    string NewFlag="";
                    switch(flag)
                    {
                        case Operator.add:
                            NewFlag= "+";
                            break;
                        case Operator.sub:
                            NewFlag= "-";
                            break;
                        case Operator.div:
                            NewFlag= "/";
                            break;
                        case Operator.mul:
                            NewFlag= "*";
                            break;

                    }
                    label1.Content = Number1+NewFlag+Number2;
                }
            }
        }
        private void Button_clear_Click(object sender, RoutedEventArgs e)
        {
            Number1 = "";
            Number2 = "";
            flag = Operator.none;
            label1.Content = "0";
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
                switch (flag)
                {
                    case Operator.add:
                        label1.Content = Convert.ToString(Convert.ToDouble(Number1) + Convert.ToDouble(Number2));
                        expressions.Add("=" + label1.Content + "\n");
                        break;
                    case Operator.sub:
                        label1.Content = Convert.ToString(Convert.ToDouble(Number1) - Convert.ToDouble(Number2));
                        expressions.Add("=" + label1.Content + "\n");
                        break;
                    case Operator.mul:
                        label1.Content = Convert.ToString(Convert.ToDouble(Number1) * Convert.ToDouble(Number2));
                        expressions.Add("=" + label1.Content + "\n");
                        break;
                    case Operator.div:
                        label1.Content = Convert.ToString(Convert.ToDouble(Number1) / Convert.ToDouble(Number2));
                        expressions.Add("=" + label1.Content + "\n");
                        break;
                }
                Number1 = Convert.ToString(label1.Content);
                Number2 = "";
                flag = Operator.none;
            }
            catch
            { }
        }
        private void Numberinput(string content)
        {
            if (flag == Operator.none)
            {
                if (Number1.Contains(".") && content == ".")
                {
                    return;
                }
                Number1 += content;
                label1.Content = Number1;
                expressions.Add(content);
            }
            else
            {
                if (Number2.Contains(".") && content == ".")
                {
                    return;
                }
                Number2 += content;
                label1.Content = label1.Content + Convert.ToString(content);
                expressions.Add(content);
            }
        }
        private void Flaginput(string content)
        {
            try
            {
                if (flag == Operator.none)
                {
                    switch (content)
                    {
                        case "+":
                            flag = Operator.add;
                            label1.Content = label1.Content + "+";
                            expressions.Add("+");
                            break;
                        case "-":
                            flag = Operator.sub;
                            label1.Content = label1.Content + "-";
                            expressions.Add("-");
                            break;
                        case "*":
                            flag = Operator.mul;
                            label1.Content = label1.Content + "*";
                            expressions.Add("*");
                            break;
                        case "/":
                            flag = Operator.div;
                            label1.Content = label1.Content + "/";
                            expressions.Add("/");
                            break;
                    }
                }
                else if (flag == Operator.add)
                {
                    Number1 = Convert.ToString(Convert.ToDouble(Number1) + Convert.ToDouble(Number2));
                    Number2 = "";
                    switch (content)
                    {
                        case "+":
                            flag = Operator.add;
                            label1.Content = label1.Content + "+";
                            expressions.Add("+");
                            break;
                        case "-":
                            flag = Operator.sub;
                            label1.Content = label1.Content + "-";
                            expressions.Add("-");
                            break;
                        case "*":
                            flag = Operator.mul;
                            label1.Content = label1.Content + "*";
                            expressions.Add("*");
                            break;
                        case "/":
                            flag = Operator.div;
                            label1.Content = label1.Content + "/";
                            expressions.Add("/");
                            break;
                    }
                }
                else if (flag == Operator.sub)
                {
                    Number1 = Convert.ToString(Convert.ToDouble(Number1) - Convert.ToDouble(Number2));
                    Number2 = "";
                    switch (content)
                    {
                        case "+":
                            flag = Operator.add;
                            label1.Content = label1.Content + "+";
                            expressions.Add("+");
                            break;
                        case "-":
                            flag = Operator.sub;
                            label1.Content = label1.Content + "-";
                            expressions.Add("-");
                            break;
                        case "*":
                            flag = Operator.mul;
                            label1.Content = label1.Content + "*";
                            expressions.Add("*");
                            break;
                        case "/":
                            flag = Operator.div;
                            label1.Content = label1.Content + "/";
                            expressions.Add("/");
                            break;
                    }
                }
                else if (flag == Operator.mul)
                {
                    Number1 = Convert.ToString(Convert.ToDouble(Number1) * Convert.ToDouble(Number2));
                    Number2 = "";
                    switch (content)
                    {
                        case "+":
                            flag = Operator.add;
                            label1.Content = label1.Content + "+";
                            expressions.Add("+");
                            break;
                        case "-":
                            flag = Operator.sub;
                            label1.Content = label1.Content + "-";
                            expressions.Add("/");
                            break;
                        case "*":
                            flag = Operator.mul;
                            label1.Content = label1.Content + "*";
                            expressions.Add("*");
                            break;
                        case "/":
                            flag = Operator.div;
                            label1.Content = label1.Content + "/";
                            expressions.Add("/");
                            break;
                    }
                }
                else if (flag == Operator.div)
                {
                    Number1 = Convert.ToString(Convert.ToDouble(Number1) / Convert.ToDouble(Number2));
                    Number2 = "";
                    switch (content)
                    {
                        case "+":
                            flag = Operator.add;
                            label1.Content = label1.Content + "+";
                            expressions.Add("+");
                            break;
                        case "-":
                            flag = Operator.sub;
                            label1.Content = label1.Content + "-";
                            expressions.Add("/");
                            break;
                        case "*":
                            flag = Operator.mul;
                            label1.Content = label1.Content + "*";
                            expressions.Add("*");
                            break;
                        case "/":
                            flag = Operator.div;
                            label1.Content = label1.Content + "/";
                            expressions.Add("/");
                            break;
                    }
                }
            }
            catch
            { }
        }
        public MainWindow()
        {
                InitializeComponent();
        }
    
    }
}
