
using Microsoft.VisualBasic;

class Program
{
    public struct Pair
    {
        private string pai;
        private int count = 0;
        public string code;
        public string Pai { get { return pai; } }
        public int Count { get { return count; } }
        public Pair(string t, int u)
        {
            pai = t;
            count = u;
        }
    }
    class Task
    {
        protected string text;
        public Task(string t)
        {
            text = t;
        }
    }
    class Num8 : Task
    {
        public Num8(string t) : base(t) { }
        private string Do(List<String> list, int n)
        {
            string h = list[0];
            if (list.Count == 0) { }
            else if (list.Count == 1)
            {
                while (h.Length < 50) { h += " "; }
            }
            else
            {
                string[] g = new string[list.Count - 1];
                n -= list.Count;
                int k = 0;
                while (n < 50)
                {
                    g[k] += " ";
                    n++;
                    k++;
                    if (k == g.Length)
                    {
                        k = 0;
                    }
                }
                for (int i = 0; i < g.Length; i++)
                {
                    h += g[i];
                    h += list[i + 1];
                }
            }
            return h;
        }
        public override string ToString()
        {
            string[] s = text.Split(' ');
            List<string> l = new List<string>();
            List<string> res = new List<string>();
            int n = 0;
            foreach (string c in s)
            {
                if (n + c.Length <= 50)
                {
                    l.Add(c);
                    n += c.Length + 1;
                }
                else
                {
                    res.Add(Do(l, n));
                    n = c.Length + 1;
                    l.Clear();
                    l.Add(c);
                }
            }
            res.Add(Do(l, n));
            return string.Join("\n", res.ToArray());
        }
    }
    class Num9 : Task
    {
        private int n;
        private Pair[] list;
        public Pair[] List { get { return list; } }
        public Num9(string t, int u) : base(t) { n = u; list = new Pair[n]; }
        public override string ToString()
        {
            string z = "!@#$%^&*/?-=+_<>";
            List<string> l = new List<string>();
            List<string> pairs = new List<string>();
            List<Pair> r = new List<Pair>();
            bool f = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    if (f)
                    {
                        pairs.Add(char.ToLower(text[i - 1]).ToString() + char.ToLower(text[i]).ToString());
                    }
                    else { f = true; }
                }
                else { f = false; }
            }
            foreach (string s in pairs)
            {
                if (!l.Contains(s))
                {
                    r.Add(new Pair(s, pairs.Count(x => x == s)));
                    l.Add(s);
                }
            }
            
            foreach (Pair pair in r)
            {
                if (pair.Count > list[0].Count)
                {
                    list[0] = pair;
                    for (int i = 1; i < list.Length; i++)
                    {
                        if (list[i].Count < list[i - 1].Count)
                        {
                            Pair a = list[i];
                            list[i] = list[i - 1];
                            list[i - 1] = a;
                        }
                        else { break; }
                    }
                }
            }
            int k = 0;
            for (int i = 0; i < list.Length; i++)
            {
                while (text.Contains(z[k]))
                {
                    k++;
                }
                list[i].code = z[k].ToString();
                k++;
            }
            string h = "";
            f = false;
            bool ff = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    if (f)
                    {
                        
                        string y = char.ToLower(text[i - 1]).ToString() + char.ToLower(text[i]).ToString();
                        for (int j = 0; j < list.Length; j++)
                        {
                            if (list[j].Pai == y)
                            {
                                h += list[j].code;
                                ff = false;
                                f = false; break;
                            }
                        }
                        if (ff) { h += text[i - 1].ToString(); }
                    }
                    else { f = true; if (ff) { h += text[i - 1].ToString(); } else { ff = true; } }
                }
                else
                {
                    if (ff) { h += text[i - 1].ToString(); } else { ff = true; }
                    f = false;
                }
            }
            return h;
        }
        public void CodeTable()
        {
            for (int i = 0; i < list.Length ; i++)
            {
                Console.WriteLine($"{list[i].Pai} - {list[i].code}");
            }
        }
    }
    class Num10 : Task
    {
        private Pair[] pairs;
        public Num10(string t, Pair[] c) : base(t) { pairs = c; }
        public override string ToString()
        {
            string h = "";
            for (int i = 0; i < text.Length; i++)
            {
                string y = text[i].ToString();
                bool f = false;
                int id = 0;
                for (int j = 0; j < pairs.Length;j++)
                {
                    if (pairs[j].code == y) {h += pairs[j].Pai; f = true; break; }
                }
                if (!f)
                {
                    h += y;
                }
            }
            return h;
        }
    }
    class Num12 : Task
    {
        private string[] words = {"показал", "деятельность", "показателей"};
        private string[] codes = { "KK", "GG", "db" };
        public Num12(string t) : base(t) { }
        public override string ToString()
        {
            string[] s = text.Split(' ');
            string h = "";
            foreach (string c in s)
            {
                string pre = "";
                string outr = "";
                bool f = false;
                string u = "";
                for (int i = 0; i < c.Length; i++)
                {
                    if (char.IsPunctuation(c[i]))
                    {
                        if (f)
                        {
                            outr += c[i];
                        }
                        else { pre += c[i]; }
                    }
                    else
                    {
                        f = true;
                        u += c[i];
                    }
                }
                f = false;
                int id = 0;
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j] == u) { f = true; id = j; break; }
                }
                if (f)
                {
                    h += pre + codes[id] + outr + " ";
                }
                else { h += c + " "; }
            }
            return h;
        }
    }
    class Num13 : Task
    {
        public Num13(string t) : base(t) { }
        public override string ToString()
        {
            List<char> letters = new List<char>();
            List<int> count = new List<int>();
            string[] s = text.Split(' ');
            foreach (string c in s)
            {
                foreach(char g in c)
                {
                    if (char.IsLetter(g))
                    {
                        bool f = true;
                        for (int i = 0; i < count.Count; i++)
                        {
                            if (letters[i] == g) { count[i]++; f = false; break;  }
                        }
                        if (f)
                        {
                            letters.Add(g);
                            count.Add(1);
                        }
                        break;
                    }
                }
            }
            string h = "";
            for (int i = 0;i < count.Count;i++)
            {
                h += letters[i].ToString() + " - " + (count[i] / (float)s.Length).ToString() + "%\n";
            }
            return h;
        }
    }
    class Num15 : Task
    {
        public Num15(string t) : base(t) { }
        public override string ToString()
        {
            int x = 0;
            int k = 0;
            bool f = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                {
                    if (f)
                    {
                        k *= 10;
                    }
                    k += int.Parse(text[i].ToString());
                }
                else
                {
                    x += k;
                    k = 0;
                }
            }
            return x.ToString();
        }
    }
    static void Main()
    {
        string ex = "После многолетних исследований ученые обнаружили тревожную тенденцию в вырубк2е лесов Амазонии. Анализ данных показал, что 33основной участник разрушен40ия лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей.";
        Num8 x = new Num8(ex);
        Console.WriteLine(x);
        Num9 y = new Num9(ex, 10);
        Console.WriteLine(y);
        y.CodeTable();
        Num10 z = new Num10(ex, y.List);
        Console.WriteLine(z);
        Num12 w = new Num12(ex);
        Console.WriteLine(w);
        Num13 q = new Num13(ex);
        Console.WriteLine(q);
        Num15 m = new Num15(ex);
        Console.WriteLine(m);
        Console.ReadLine();
    }
}