using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace excel
{
    public static class Parser //класс парсер
    {
        public class ErrorCell
        {

        }

        public static ErrorCell error = new ErrorCell();

        public static void remove_spaces(ref string x)
        {
            x = String.Concat(x.Where(c => !Char.IsWhiteSpace(c)));
        }

        public static List<dynamic> merge(dynamic x, dynamic y)
        {
            if (x is List<dynamic> && y is List<dynamic>)
            {
                x.AddRange(y);
                return x;
            }
            else if (x is List<dynamic>)
            {
                x.Add(y);
                return x;
            }
            else if (y is List<dynamic>)
            {
                y.Add(x);
                return y;
            }
            else
            {
                return new List<dynamic> { x, y };
            }
        }   

        public static int find_bracket(string x, int i)
        {
            char y = x[i];

            if (y == '(' || y == '[')
            {
                y = y == '(' ? ')' : ']';
                ++i;

                while (i < x.Length)
                {
                    if (x[i] == y)
                        return i;

                    if (x[i] == '(' || x[i] == '[')
                    {
                        i = find_bracket(x, i);

                        if (i == -1)
                            return -1;
                    }

                    ++i;
                }
            }

            if (y == ')' || y == ']')
            {
                y = y == ')' ? '(' : '[';
                --i;

                while (i >= 0)
                {
                    if (x[i] == y)
                        return i;

                    if (x[i] == ')' || x[i] == ']')
                    {
                        i = find_bracket(x, i);

                        if (i == -1)
                            return -1;
                    }

                    --i;
                }
            }

            return -1;
        }

        public static int findleft(string x, char y)
        {
            int i = 0;

            while (i < x.Length)
            {
                if (x[i] == y && i != 0 && x[i - 1] != '+' && x[i - 1] != '-' && x[i - 1] != '(' && x[i - 1] != '*' && x[i - 1] != '/')
                    return i;

                if (x[i] == ')' || x[i] == ']')
                    return -1;

                if (x[i] == '(' || x[i] == '[')
                {
                    i = find_bracket(x, i);

                    if (i == -1)
                        throw new ArgumentException("Brackets");
                }

                ++i;
            }

            return -1;
        }

        public static int findright(string x, char y)
        {
            int i = x.Length - 1;

            while (i >= 0)
            {
                if (x[i] == y && i != 0 && x[i - 1] != '+' && x[i - 1] != '-' && x[i - 1] != '(' && x[i - 1] != '*' && x[i - 1] != '/')
                    return i;

                if (x[i] == '(' || x[i] == '[')
                    return -1;

                if (x[i] == ')' || x[i] == ']')
                {
                    i = find_bracket(x, i);

                    if (i == -1)
                        throw new ArgumentException("Дужки");
                }

                --i;
            }

            return -1;
        }

        public static dynamic parse(string x)
        {
            if (x == null || x == "")
                return 0;

            if (x == "True" || x == "False")
                return x == "True";

            if (x[0] == '"' || x[0] == '\'')
            {
                if (x[x.Length - 1] == x[0] || x.Length < 2)
                    return x;
                else
                    return x.Substring(1, x.Length - 1);
            }

            x = x.Replace(" ", "");
            x = x.Replace("mod", "%");
            x = x.Replace("div", "\\");
            x = x.Replace(">=", "}");
            x = x.Replace("<=", "{");
            x = x.Replace("<>", "!");
            x = x.Replace("and", "&");
            x = x.Replace("or", "|");
            x = x.Replace("eqv", "~");

            if (x[0] == '+')
                return parse(x.Substring(1, x.Length - 1));

            List<char> NoBegin = new List<char> { '~', '|', '&', '=', '!', '>', '<', '}', '{', '%', '\\', '*', '/', '^', ',' }, NoEnd = new List<char> { '-', '+' };

            if (NoBegin.Contains(x[0]) || NoBegin.Contains(x[x.Length - 1]) || NoEnd.Contains(x[x.Length - 1]))
                throw new ArgumentException("Оператори");

            Tuple<char, Func<dynamic, dynamic, dynamic>, bool>[] ops = new Tuple<char, Func<dynamic, dynamic, dynamic>, bool>[]
            {
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('~', (dynamic a, dynamic b) => a == b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('|', (dynamic a, dynamic b) => a || b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('&', (dynamic a, dynamic b) => a && b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('=', (dynamic a, dynamic b) => a == b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('!', (dynamic a, dynamic b) => a != b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('>', (dynamic a, dynamic b) => a > b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('<', (dynamic a, dynamic b) => a < b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('}', (dynamic a, dynamic b) => a >= b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('{', (dynamic a, dynamic b) => a <= b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('%', (dynamic a, dynamic b) => a - b * Math.Floor(a / b), true),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('\\', (dynamic a, dynamic b) => Math.Floor(a / b), true),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('+', (dynamic a, dynamic b) => a + b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('-', (dynamic a, dynamic b) => a - b, true),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('*', (dynamic a, dynamic b) => a * b, false),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('/', (dynamic a, dynamic b) => a / b, true),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>('^', (dynamic a, dynamic b) => Math.Pow(a, b), true),
                new Tuple<char, Func<dynamic,dynamic,dynamic>, bool>(',', (dynamic a, dynamic b) => merge(a, b), false),
            };

            foreach (var i in ops)
            {
                int j;

                if (i.Item3)
                    j = findright(x, i.Item1);
                else
                    j = findleft(x, i.Item1);

                if (j > 0)
                    return i.Item2(parse(x.Substring(0, j)), parse(x.Substring(j + 1, x.Length - j - 1)));
            }

            if (x[0] == '-')
            {
                return -parse(x.Substring(1, x.Length - 1));
            }
            else if (x.Length > 3 && x.Substring(0, 3) == "min")
            {
                List<dynamic> f = parse(x.Substring(3, x.Length - 3));
                return f.Min();
            }
            else if (x.Length > 3 && x.Substring(0, 3) == "max")
            {
                List<dynamic> f = parse(x.Substring(3, x.Length - 3));
                return f.Max();
            }
            else if (x.Length > 3 && x.Substring(0, 3) == "not")
            {
                return !parse(x.Substring(3, x.Length - 3));
            }
            else if (x[0] == '(' && x[x.Length - 1] == ')')
                return parse(x.Substring(1, x.Length - 2));
            else if (x[0] == '[' && x[x.Length - 1] == ']')
                return parse(x.Substring(1, x.Length - 2));
            else if (x[0] == 'R')
            {
                if (x.Contains('C'))
                {
                    int a = Convert.ToInt32(x.Substring(1, x.IndexOf('C') - 1));
                    int b = Convert.ToInt32(x.Substring(x.IndexOf('C') + 1, x.Length - x.IndexOf('C') - 1));
                    Cell cell = Manage_Cell.GetCellbyindex(b - 1, a - 1);
                    cell.process();
                    return cell.Value;
                }
                else
                {
                    return x;
                }
            }
            else
            {
                try
                {
                    double res = Convert.ToDouble(x, CultureInfo.InvariantCulture);
                    return res;
                }
                catch (Exception e)
                {
                    return x;
                }
            }
        }
    };

    public class Cell //класс комірки
    {
        public dynamic Value;
        public string Formula;
        public bool Busy;
        public bool Counted;
        public DataGridViewCell Example;
        public Cell(DataGridViewCell example, string exp)
        {
            Value = 0;
            Formula = exp;
            Example = example;
            Busy = false;
            Counted = true;
        }

        public void process() // метод який обраховує значення у комірці
        {
            if (Busy)
            {
                throw new ArgumentException("Busy" + Formula);
            }
            else if (!Counted)
            {
                Busy = true;
                dynamic temp = "";

                try
                {
                    temp = Parser.parse(Formula);
                }
                catch (Exception e)
                {
                    temp = Parser.error;
                    throw e;
                }
                finally
                {
                    if (Formula == null || Formula == "")
                        Value = 0;
                    else
                        Value = temp;

                    Counted = true;
                    Busy = false;
                }
            }
        }

    };

    public static class Manage_Cell
    {
        private static Cell[,] list_cells; //масив клітинок

        public static Cell GetCellbyindex(int column, int row) //метод, що повертає комірку по її індексам
        {
            return list_cells[column, row];
        }
        public static void Rebuildtable(DataGridView dgv) // метод оновлення таблиці після внесення у неї змін
        {
            list_cells = new Cell[dgv.ColumnCount, dgv.RowCount];
            for (int i = 0; i < dgv.ColumnCount; ++i)
                for (int j = 0; j < dgv.RowCount; ++j)
                {
                    Cell cell = new Cell(dgv[i, j], Convert.ToString(dgv[i, j].Value));
                    list_cells[i, j] = cell;
                }
        }
        public static dynamic GetValue(int rownumber, int columnnumber)
        {
            Cell cell = GetCellbyindex(columnnumber, rownumber);
            cell.process();
            return cell.Value;
        }
    };

}
