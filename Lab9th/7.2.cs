using LastLangVersion.Serializers;
using System;
using System.Xml.Linq;
using static Seven_1;

public class Seven_2
{
    [ProtoBuf.ProtoContract()]
    [ProtoBuf.ProtoInclude(1, typeof(Jump120))]
    [ProtoBuf.ProtoInclude(2, typeof(Jump180))]
    public abstract class Jump
    {
        protected int _points = 0;
        protected string _surname = "";
        protected string _name = "Jump";
        [ProtoBuf.ProtoMember(3)]
        public int Points { get { return _points; } set { _points += value; } }
        [ProtoBuf.ProtoMember(4)]
        public string Surname { get { return _surname; } set { _surname = value; } }
        [ProtoBuf.ProtoMember(5)]
        public string Name { get { return _name; } set { _name = value; } }
        public Jump(string surname) { _surname = surname; CalculateJumpPoints(); CalculateJuryPoints(); }
        public Jump() { }
        public void Write()
        {
            Console.WriteLine($"Дисциплина {_name}, Участник {_surname}, Счёт: {_points}");
        }
        void CalculateJuryPoints()
        {
            Random random = new Random();
            int max = 0;
            int min = 21;
            for (int i = 0; i < 5; i++)
            {
                int point = random.Next(0, 20);
                if (point < min) min = point;
                if (point > max) max = point;
                Points = point;
            }
            Points = -1 * (min + max);
        }
        void CalculateJumpPoints()
        {
            Random random = new Random();
            int jump = random.Next(100, 140);
            Points = (jump - 120) * 2;
        }
    }
    [ProtoBuf.ProtoContract()]
    public class Jump120 : Jump
    {
        public Jump120() : base() { }
        public Jump120(string surname) : base(surname) { _name = "Jump120"; }
    }
    [ProtoBuf.ProtoContract()]
    public class Jump180 : Jump
    {
        public Jump180() : base() { }
        public Jump180(string surname) : base(surname) { _name = "Jump180"; }
    }
    public static void Main2()
    {
        void ShellSort(Jump[] array)
        {
            int size = array.Length;
            for (int interval = size / 2; interval > 0; interval /= 2)
            {
                for (int i = interval; i < size; i++)
                {
                    Jump currentKey = array[i];
                    int k = i;
                    while (k >= interval && array[k - interval].Points < currentKey.Points)
                    {
                        array[k] = array[k - interval];
                        k -= interval;
                    }
                    array[k] = currentKey;
                }
            }
        }
        string[] Surnames = { "Хвойный", "Вертюк", "Пенаст", "Гаврилов", "Тернюк" };
        Jump180[] Competitors = new Jump180[Surnames.Length];
        for (int i = 0; i < Surnames.Length; i++)
        {
            Competitors[i] = new Jump180(Surnames[i]);
        }
        ShellSort(Competitors);
        Jump120[] Competitors2 = new Jump120[Surnames.Length];
        for (int i = 0; i < Surnames.Length; i++)
        {
            Competitors2[i] = new Jump120(Surnames[i]);
        }
        ShellSort(Competitors2);
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        path = Path.Combine(path, "Lab9");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string path1 = Path.Combine(path, "180");
        string path2 = Path.Combine(path, "120");
        if (!Directory.Exists(path1)) Directory.CreateDirectory(path1);
        if (!Directory.Exists(path2)) Directory.CreateDirectory(path2);
        Serial[] serializers = new Serial[3]
        {
            new JsonManager(),
            new XmlManager(),
            new BinManager()
        };
        string[] files = new string[3]
        {
            "task.json",
            "task.xml",
            "task.bin"
        };
        for (int i = 0; i < files.Length; i++)
        {
            serializers[i].Write(Competitors, Path.Combine(path1, files[i]));
        }
        Console.WriteLine("Прыжки 180");
        for (int i = 0; i < files.Length; i++)
        {
            Competitors = serializers[i].Read<Jump180[]>(Path.Combine(path1, files[i]));
            foreach (Jump180 house in Competitors)
            {
                house.Write();
            }
        }
        for (int i = 0; i < files.Length; i++)
        {
            serializers[i].Write(Competitors2, Path.Combine(path2, files[i]));
        }
        Console.WriteLine("Прыжки120");
        for (int i = 0; i < files.Length; i++)
        {
            Competitors2 = serializers[i].Read<Jump120[]>(Path.Combine(path2, files[i]));
            foreach (Jump120 house in Competitors2)
            {
                house.Write();
            }
        }
    }
}