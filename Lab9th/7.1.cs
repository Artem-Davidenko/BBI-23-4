using System;
using LastLangVersion.Serializers;
public class Seven_1
{
    [ProtoBuf.ProtoContract()]
    [ProtoBuf.ProtoInclude(1, typeof(InformaticStudent))]
    [ProtoBuf.ProtoInclude(2, typeof(MathStudent))]
    public abstract class Student
    {
        protected int mark;
        protected int misses;
        [ProtoBuf.ProtoMember(3)]
        public int Mark { get { return mark; } set { mark = value; } }
        [ProtoBuf.ProtoMember(4)]
        public int Misses { get { return misses; } set { misses = value; } }
        public Student(int Mark, int Misses)
        {
            mark = Mark;
            misses = Misses;
        }
        public Student() { }
        public virtual void write() { }
    }
    [ProtoBuf.ProtoContract()]
    public class InformaticStudent : Student
    {
        public InformaticStudent(int mark, int misses) : base(mark, misses) { }
        public InformaticStudent() : base()  { }
        public override void write()
        {
            if (mark <= 2) { Console.WriteLine($"Пропустил {misses} по Информатике"); }
        }
    }
    [ProtoBuf.ProtoContract()]
    public class MathStudent : Student
    {
        public MathStudent(int mark, int misses) : base(mark, misses) { }
        public MathStudent() : base() { }
        public override void write()
        {
            if (mark <= 2) { Console.WriteLine($"Пропустил {misses} по Математике"); }
        }
    }
    static void Main(string[] args)
    {
        void QuickSort(Student[] array, int leftIndex, int rightIndex)
        {
            int i = leftIndex;
            int j = rightIndex;
            int pivot = array[leftIndex].Misses;
            while (i <= j)
            {
                while (array[i].Misses < pivot)
                {
                    i++;
                }

                while (array[j].Misses > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    Student temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                QuickSort(array, leftIndex, j);
            if (i < rightIndex)
                QuickSort(array, i, rightIndex);
        }
        int Number = 20;
        MathStudent[] MathList = new MathStudent[Number];
        InformaticStudent[] InfList = new InformaticStudent[Number];
        int[] studentsMarksList = new int[Number];
        for (int i = 0; i < studentsMarksList.Length; i++)
        {
            MathList[i] = new MathStudent(new Random().Next(0, 5), new Random().Next(0, 20));
            InfList[i] = new InformaticStudent(new Random().Next(0, 5), new Random().Next(0, 20));
        }
        QuickSort(InfList, 0, InfList.Length - 1);
        QuickSort(MathList, 0, MathList.Length - 1);
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        path = Path.Combine(path, "Lab9");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string path1 = Path.Combine(path, "Inf");
        string path2 = Path.Combine(path, "Math");
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
            serializers[i].Write(InfList, Path.Combine(path1, files[i]));
        }
        Console.WriteLine("Список неуспевающих по Информатике");
        for (int i = 0; i < files.Length; i++)
        {
            InfList = serializers[i].Read<InformaticStudent[]>(Path.Combine(path1, files[i]));
            foreach (InformaticStudent house in InfList)
            {
                house.write();
            }
        }
        for (int i = 0; i < files.Length; i++)
        {
            serializers[i].Write(MathList, Path.Combine(path2, files[i]));
        }
        Console.WriteLine("Список неуспевающих по Математике");
        for (int i = 0; i < files.Length; i++)
        {
            MathList = serializers[i].Read<MathStudent[]>(Path.Combine(path2, files[i]));
            foreach (MathStudent house in MathList)
            {
                house.write();
            }
        }
        Seven_2.Main2();
        Seven_3.Main3();
        Console.ReadKey();
    }
}