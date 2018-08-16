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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string number1 = "", number2 = "", flag = null;
        List<string> expressions=new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {if(flag==null)
            { number1 = number1 + "7";
                label1.Content = number1;
            }
            else {
                number2 = number2 + "7";
                label1.Content = number2;
            }
        }
        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + "8";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + "8";
                label1.Content = number2;
            }
        }
        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + "9";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + "9";
                label1.Content = number2;
            }
        }
        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + "0";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + "0";
                label1.Content = number2;
            }
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + "1";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + "1";
                label1.Content = number2;
            }
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + "2";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + "2";
                label1.Content = number2;
            }
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + "3";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + "3";
                label1.Content = number2;
            }
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + "4";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + "4";
                label1.Content = number2;
            }
        }
        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + "5";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + "5";
                label1.Content = number2;
            }
        }
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + "6";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + "6";
                label1.Content = number2;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                number1 = number1 + ".";
                label1.Content = number1;
            }
            else
            {
                number2 = number2 + ".";
                label1.Content = number2;
            }
        }
        private void Button_add_Click(object sender, RoutedEventArgs e)
        {
            flag = "+";
        }
        private void Button_sub_Click(object sender, RoutedEventArgs e)
        {
            flag = "-";
        }
        private void Button_mul_Click(object sender, RoutedEventArgs e)
        {
            flag = "*";
        }
        private void Button_div_Click(object sender, RoutedEventArgs e)
        {
            flag = "/";
        }
        private void Button_del_Click(object sender, RoutedEventArgs e)
        {
            if (flag == null)
            {
                if (number1.Length > 0)
                {
                    number1 = number1.Remove(number1.Length - 1);
                    label1.Content = number1;
                }
            }
            else
            {
                if (number2.Length > 0)
                {
                    number2 = number2.Remove(number2.Length - 1);
                    label1.Content = number2;
                }
            }
        }

        private void MenuItem_save_Click(object sender, RoutedEventArgs e)
        {
            FileStream resultfile = new FileStream("result.txt", FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(resultfile);
            foreach(string a in expressions)
            { streamWriter.WriteLine(a); }
            streamWriter.Close();
        }

        private void Button_c_Click(object sender, RoutedEventArgs e)
        {
            number1="";

            number2 = "";
            flag = null;
            label1.Content = "0";

        }
        private void Button_equ_Click(object sender, RoutedEventArgs e)
        {if(number1==""||number2=="")
            { number1 = "";
                number2 = "";
                    flag = null;
                return;
            }
          switch(flag)
            {
                case "+":
                    label1.Content =Convert.ToString(Convert.ToDouble(number1) + Convert.ToDouble(number2));
                    expressions.Add(number1 + "+"+number2 + "=" + label1.Content);
                    break;
                case "-":
                    label1.Content = Convert.ToString(Convert.ToDouble(number1) - Convert.ToDouble(number2));
                    expressions.Add(number1 + "-" + number2 + "=" + label1.Content);
                    break;
                case "*":
                    label1.Content = Convert.ToString(Convert.ToDouble(number1) * Convert.ToDouble(number2));
                    expressions.Add(number1 + "*" + number2 + "=" + label1.Content);
                    break;
                case "/":
                    label1.Content = Convert.ToString(Convert.ToDouble(number1) / Convert.ToDouble(number2));
                    expressions.Add(number1 + "/" + number2 + "=" + label1.Content);
                    break;
            }
            number1 = ""; number2 = ""; flag = null;
        }
    }
}
