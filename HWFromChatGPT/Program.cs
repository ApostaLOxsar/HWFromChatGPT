using System.ComponentModel.Design.Serialization;
using System.IO;

string path = "test.txt";// название файла с сохраненными значение
string pathTemp = "temp.txt";//название файла с актуальным значение
var humanList = new List<Human> { }; // создание колекции из людей

//using (StreamWriter test = new StreamWriter(path, true, System.Text.Encoding.Default))
//{
//    await test.WriteLineAsync("этот текст написан для теста");
//}
void printHuman() 
{
    int count = 0;
    Console.WriteLine("П/н\tФИО\tВозраст\tДолжность\tЗарплата");
    foreach (Human human in humanList) // вывод людей в колекции
    {
        Console.Write(count + 1 + "\t");
        Console.Write(human.FIO + "\t");
        Console.Write(human.Age + "\t");
        Console.Write(human.Post + "\t\t");
        Console.Write(human.Salary + "\n");
        count++;
    }
    Console.WriteLine();
    Action();
}

void Action()
{
    { //
        Console.WriteLine("Что вы хотите сделать?");
        Console.WriteLine("1 - Посмотреть перечень сотрудников");
        Console.WriteLine("2 - Добавить запись");
        Console.WriteLine("3 - Редактировать запись");
        Console.WriteLine("4 - Удалить запись");
        Console.WriteLine("5 - Сохранить изменение");
        Console.WriteLine("6 - Отменить изменение");
        Console.WriteLine("7 - Очистить консоль");
        Console.WriteLine("ESC или 8 - Выйти"); // Дописать
    }
    ConsoleKeyInfo buton = Console.ReadKey(true);
    Console.WriteLine(buton.Key.ToString());
    switch (buton.KeyChar)
    {
        case '1':
            {
                Console.WriteLine("Вы выбрали посмотреть перечень сотрудников \nВот он:");
                printHuman();
                break;
            }
        case '2':
            {
                Console.WriteLine("Вы выбрали добавить запись");
                WriteTest();
                break;
            }
        case '3':
            {
                Console.WriteLine("Вы выбрали редактировать запись");
                Console.WriteLine("Введите номер сотрудника который хотите отредактировать");
                break;
            }
        case '4':
            {
                Console.WriteLine("Вы выбрали удалить запись");
                break;
            }
        case '5':
            {
                Console.WriteLine("Вы выбрали cохранить изменение");
                break;
            }
        case '6':
            {
                Console.WriteLine("Вы выбрали отменить изменение");
                break;
            }
        case '7':
            {
                Console.WriteLine("Вы выбрали очистить консоль");
                Console.Clear();
                Action();
                break;
            }
        case '8':
            {
                break;
            }
        default:
            {
                Console.WriteLine("Вы ввели неверную команду :( \nПопробуйте снова, вот варианты которые доступны\n");
                Action();
                break;
            }

    }
}


async void WriteTest() // переписать через прямую запись в класс а в конце проги запись в файл
{
    using (StreamWriter WriteTest = new StreamWriter(path, true, System.Text.Encoding.Default))
    {
        /*for (int i = 0; i < 5; i++)
        {
            await WriteTest.WriteLineAsync("Максим, 12, медик, 1250");
        }*/
    }
}

Reader();
Action();
//WriteTest();
//Reader(); //чтение и присвоение в класс

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