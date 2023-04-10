using System.IO;

string path = "test.txt";

//using (StreamWriter test = new StreamWriter(path, true, System.Text.Encoding.Default))
//{
//    await test.WriteLineAsync("этот текст написан для теста");
//}

async void WriteTest() // переписать через прямую запись в класс а в конце проги запись в файл
{
    using (StreamWriter WriteTest = new StreamWriter(path, true, System.Text.Encoding.Default))
    {
        for (int i = 0; i < 2; i++)
        {
            await WriteTest.WriteLineAsync("Максим, 12, медик, 1250");
        }
    }
}


var humanList = new List<Human> { }; // создание колекции из людей


//WriteTest();
Reader(); //чтение и присвоение в класс

void Reader() //переписать с не глобальными переменными 
{
    using StreamReader reader = new StreamReader(path);
    int count = 0;
    string[] date = new string[4];
    string? line;
    while ((line = reader.ReadLine()) != null)
    {
        humanList.Add(new Human());
        date = line.Replace(" ", "")
                   .Split(",");

        humanList[count].FIO = (date[0]);
        humanList[count].Age = (int.Parse(date[1]));
        humanList[count].Post = (date[2]);
        humanList[count].Salary = (int.Parse(date[3]));

        count++;
    }
}




foreach( Human human in humanList) // вывод людей в колекции
{
    Console.Write(human.FIO + "\t");
    Console.Write(human.Age + "\t");
    Console.Write(human.Post + "\t");
    Console.Write(human.Salary);
    Console.WriteLine();
}

class Human // люди с их данными
{
    private string fio = "under";
    private int age = 0;
    private string post = "under";
    private int salary = 0;

    public string FIO
    {
        get => fio;
        set => fio = value;
    }

    public int Age
    {
        get => age;
        set => age = value;
    }

    public string Post
    {
        get => post;
        set => post = value;
    }

    public int Salary
    {
        get => salary;
        set => salary = value;
    }


}