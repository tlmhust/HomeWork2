# 计算器
## 1.版本更新
### (1)1.0版本
#### 1.能够进行二元计算.&nbsp;&nbsp;2.存在历史纪录功能.
### (2)2.0版本
#### 1.能够进行多元计算,但是没有优先级.&nbsp;&nbsp;2.BUG非常多,容易出现异常.&nbsp;&nbsp;3.更改了图形界面.nbsp;4.实现了代码重构,简化了代码.
### (3)3.0版本
#### 1.能够进行多元计算且存在优先级和括号关系.&nbsp;2.BUG较少.  
### (4)4.0版本
#### 1.删除保存历史纪录功能,自动保存历史纪录.&nbsp;2.新加入删除历史纪录功能  
## 2.主要功能
### 主要功能就是一个基本的计算器,具有如下功能:  
#### 1.能完成包括括号任意输入的表达式的计算.
#### 2.具有保存历史纪录和删除历史纪录的功能.
#### 3.利用图形界面通过鼠标点击按键输入表达式.
## 3.测试方法
### 1.打开程序并执行,得到如下的图形界面:
![avatar](https://raw.githubusercontent.com/tlmhust/HomeWork2/master/picture/1234.png)
### 2.输入任意表达式按等号键,就可以得出结果.
![avatar](https://raw.githubusercontent.com/tlmhust/HomeWork2/master/picture/4567.png)
### 3.所有的历史结果保存在  
HomeWork2\calculator 3.0\WpfApp5\bin\Debug\result.txt中.  
保存结果如下图所示
![avatar](https://raw.githubusercontent.com/tlmhust/HomeWork2/master/picture/789.png)
### 4.点击如图所示清空历史纪录按钮,可以看到历史纪录txt文件被清空.
## 4.代码分析
```
    private void Button_Click(object sender, RoutedEventArgs e)
        {
            formula += Convert.ToString((sender as Button).Content);
            expressions.Add(Convert.ToString((sender as Button).Content));
            label1.Content += Convert.ToString((sender as Button).Content);
        }
```
由方法名称Button_Click可知该函数是点击按钮后的操作,将算术表达式赋给formula字符串并由label1所显示,同时存储在expressions方便输入历史纪录.    
```
 private void Button_del_Click(object sender, RoutedEventArgs e)
        {
            if (formula.Length > 0)
            {
                formula = formula.Remove(formula.Length - 1);
                label1.Content = formula;
            }
        }
```
由方法名称Button_del_Click可知是点击del按钮后的操作,利用remove函数删除最后输入的字符.  
```  
 private void Button_clear_Click(object sender, RoutedEventArgs e)
        {
            formula = "";
            label1.Content = "";
        }
```  
由方法名称Button_clear_Click可知是点击C按钮后的操作,将formula和显示都清空.
```  
private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            List<string> lines = new List<string>(File.ReadAllLines("result.txt"));
            lines.RemoveAt(0);
            File.WriteAllLines("result.txt", lines.ToArray());
        }  
```    
该方法是文件清空操作,按下清空时可以将txt文件清空.
```
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
                expressions.Add("=" + formula+"   ");
                FileStream resultfile = new FileStream("result.txt", FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(resultfile);
                foreach (string a in expressions)
                {
                    streamWriter.Write(a);
                }
                streamWriter.Close();
            }
            catch
            { };
        }
```
该函数是按下=号键的操作,参考了晚上的相关教程,大致原理就是通过两个栈和一个队列来进行计算.计算步骤如下:  
1.首先通过其中signStack栈和postfixExpressionQueue队列相互配合将输入的中缀表达式转换为后缀表达式存储在队列中.
2.将后缀表达式利用GetTheConquenceStack栈将后缀表达式计算出来,就可以得到相应结果.
3.整个方法的实现都由try包围,同时一个空catch,可以保证程序不出现异常.  
## 5.程序参与者  
软件工程营4班第二组全体成员,包括  
队长:谭力铭  
队员:曹善康,陈致利,李嘉诚,相雨.  