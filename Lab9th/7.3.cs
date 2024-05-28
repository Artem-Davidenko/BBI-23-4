using System;
using System.Xml.Serialization;
using LastLangVersion.Serializers;
using static Seven_1;
public class Seven_3
{
    [ProtoBuf.ProtoContract()]
    [ProtoBuf.ProtoInclude(1, typeof(MCommand))]
    [ProtoBuf.ProtoInclude(2, typeof(WCommand))]
    [XmlInclude(typeof(MCommand))]
    [XmlInclude(typeof(WCommand))]
    public class Command
    {
        protected int _points;
        protected int _difference;
        protected string _name;
        protected string _gender;
        [ProtoBuf.ProtoMember(3)]
        public int Points { get { return _points; } set { _points = value; } }
        [ProtoBuf.ProtoMember(4)]
        public int Difference { get { return _difference; } set { _difference = value; } }
        [ProtoBuf.ProtoMember(5)]
        public string Name { get { return _name; } set { _name = value; } }
        [ProtoBuf.ProtoMember(6)]
        public string Gender { get { return _gender; } set { _gender = value; } }
        public void Win()
        {
            Points += 3;
        }
        public void Tie()
        {
            Points++;
        }
        public void AddDifference(int a)
        {
            Difference += a;
        }
        public Command() { }
        public Command(string name)
        {
            _name = name;
            _points = 0;
            _difference = 0;
        }
        public void Write(int n)
        {
            Console.WriteLine($"{n}-е Место заняла {_gender} команда {_name}, их счёт: {_points}; Разница: {_difference}");
        }
    }
    [ProtoBuf.ProtoContract()]
    public class MCommand : Command
    {
        public MCommand() { }
        public MCommand(string name) : base(name) { _gender = "мужская"; }

    }
    [ProtoBuf.ProtoContract()]
    public class WCommand : Command
    {
        public WCommand() { }
        public WCommand(string name) : base(name) { _gender = "женская"; }

    }
    public static void Main3()
    {
        void GnomeSort(Command[] commands)
        {
            int n = commands.Length;
            int i = 1;
            int j = 2;
            while (i < n)
            {
                if (commands[i].Points < commands[i - 1].Points | (commands[i].Points == commands[i - 1].Points & commands[i].Difference <= commands[i - 1].Difference)) { i = j; j += 1; }
                else { Command a = commands[i]; commands[i] = commands[i - 1]; commands[i - 1] = a; i--; if (i == 0) { i = j; j += 1; } }
            }
        }

        Random random = new Random();
        string[] WNames = { "Алмаз", "Изумруд", "Рубин", "Топаз", "Яшма" };
        string[] MNames = { "Свинец", "Титан", "Медь", "Сталь", "Чугун" };
        int[] MatchesResults = new int[30];
        int[] MatchesCommandsId = new int[30];
        for (int i = 0; i < 30; i++) { MatchesResults[i] = random.Next(0, 5); }
        int a = random.Next(0, 4);
        MatchesCommandsId[0] = a;
        for (int i = 1; i < 30; i++)
        {
            if (i % 2 == 1) { int n = random.Next(0, 4); while (n == a) { n = random.Next(0, 4); } MatchesCommandsId[i] = n; }
            else { a = random.Next(0, 4); MatchesCommandsId[i] = a; }
        }
        int[] MatchesResults2 = new int[30];
        int[] MatchesCommandsId2 = new int[30];
        for (int i = 0; i < 30; i++) { MatchesResults2[i] = random.Next(0, 5); }
        a = random.Next(0, 4);
        MatchesCommandsId2[0] = a;
        for (int i = 1; i < 30; i++)
        {
            if (i % 2 == 1) { int n = random.Next(0, 4); while (n == a) { n = random.Next(0, 4); } MatchesCommandsId2[i] = n; }
            else { a = random.Next(0, 4); MatchesCommandsId2[i] = a; }
        }
        MCommand[] MCommandsList = new MCommand[MNames.Length];
        for (int i = 0; i < MNames.Length; i++)
        {
            MCommandsList[i] = new MCommand(MNames[i]);
        }
        WCommand[] WCommandsList = new WCommand[WNames.Length];
        for (int i = 0; i < WNames.Length; i++)
        {
            WCommandsList[i] = new WCommand(WNames[i]);
        }
        for (int i = 0; i < 30; i += 2)
        {
            if (MatchesResults[i] == MatchesResults[i + 1]) { MCommandsList[MatchesCommandsId[i]].Tie(); MCommandsList[MatchesCommandsId[i + 1]].Tie(); }
            else if (MatchesResults[i] > MatchesResults[i + 1]) { MCommandsList[MatchesCommandsId[i]].Win(); }
            else { MCommandsList[MatchesCommandsId[i + 1]].Win(); }
            MCommandsList[MatchesCommandsId[i]].AddDifference(Math.Abs(MatchesResults[i] - MatchesResults[i + 1]));
            MCommandsList[MatchesCommandsId[i + 1]].AddDifference(Math.Abs(MatchesResults[i] - MatchesResults[i + 1]));
        }
        for (int i = 0; i < 30; i += 2)
        {
            if (MatchesResults2[i] == MatchesResults2[i + 1]) { WCommandsList[MatchesCommandsId2[i]].Tie(); WCommandsList[MatchesCommandsId2[i + 1]].Tie(); }
            else if (MatchesResults2[i] > MatchesResults2[i + 1]) { WCommandsList[MatchesCommandsId2[i]].Win(); }
            else { WCommandsList[MatchesCommandsId2[i + 1]].Win(); }
            WCommandsList[MatchesCommandsId2[i]].AddDifference(Math.Abs(MatchesResults2[i] - MatchesResults2[i + 1]));
            WCommandsList[MatchesCommandsId2[i + 1]].AddDifference(Math.Abs(MatchesResults2[i] - MatchesResults2[i + 1]));
        }
        Command[] Results = new Command[WCommandsList.Length + MCommandsList.Length];
        for (int i = 0; i < WCommandsList.Length; i++)
        {
            Results[i] = WCommandsList[i];
        }
        int index = WCommandsList.Length;
        for (int i = 0; i < MCommandsList.Length; i++)
        {
            Results[index] = MCommandsList[i];
            index++;
        }
        GnomeSort(Results);
        for (int i = 0; i < Results.Length; i++)
        {
            Results[i].Write(i + 1);
        }
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        path = Path.Combine(path, "Lab9");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string path1 = Path.Combine(path, "Commands");
        if (!Directory.Exists(path1)) Directory.CreateDirectory(path1);
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
            serializers[i].Write(Results, Path.Combine(path1, files[i]));
        }
        Console.WriteLine("Прыжки 180");
        for (int i = 0; i < files.Length; i++)
        {
            Results = serializers[i].Read<Command[]>(Path.Combine(path1, files[i]));
            i = 0;
            foreach (Command house in Results)
            {
                i++;
                house.Write(i);
            }
        }

    }
}