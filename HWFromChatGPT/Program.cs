using System;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Reflection.PortableExecutable;

string path = "test.txt";// название файла с сохраненными значение
string pathTemp = "temp.txt";//название файла с актуальным значение
var humanList = new List<Human> { }; // создание колекции из людей

initialization();
Action();

void initialization() //пересмотреть(возможно упростить) 
{
    using StreamWriter writeTest = new StreamWriter(path, true, System.Text.Encoding.Default);
    writeTest.Close();
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
    reader.Close();

    FileInfo copyTemp = new FileInfo(path);
    copyTemp.CopyTo(pathTemp, true);
}

void Action()
{
    {
        Console.WriteLine("Что вы хотите сделать?");
        Console.WriteLine("1 - Посмотреть перечень сотрудников"); //функционал готов
        Console.WriteLine("2 - Добавить запись"); //функционал готов
        Console.WriteLine("3 - Редактировать запись");
        Console.WriteLine("4 - Удалить запись"); //функционал готов
        Console.WriteLine("5 - Удалить все записи"); //функционал готов
        Console.WriteLine("6 - Сохранить изменение"); //функционал готов
        Console.WriteLine("7 - Отменить изменение"); //функционал готов
        Console.WriteLine("8 - Очистить консоль"); //функционал готов
        Console.WriteLine("9 или ESC - Выйти"); //функционал готов
    }
    ConsoleKeyInfo button = Console.ReadKey(true);
    if (button.Key.ToString() == "9" || button.Key.ToString() == "D9" || button.Key.ToString() == "NumPad9" || button.Key.ToString() == "Escape")
    {
        FileInfo deleteTemp = new FileInfo(pathTemp);
        deleteTemp.Delete();
        Console.WriteLine("Пока-пока :(");
    }
    else
    {
        switch (button.KeyChar)
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
                    AddNewHuman();
                    break;
                }
            case '3':
                {
                    Console.WriteLine("Вы выбрали редактировать запись");
                    CorectHuman();
                    break;
                }
            case '4':
                {
                    Console.WriteLine("Вы выбрали удалить запись");
                    DeleteHuman();
                    break;
                }
            case '5':
                {
                    Console.WriteLine("Вы выбрали удалить все записи");
                    DeleteAllHuman();
                    break;
                }
            case '6':
                {
                    Console.WriteLine("Вы выбрали cохранить изменение");
                    SaveChange();
                    break;
                }
            case '7':
                {
                    Console.WriteLine("Вы выбрали отменить изменение");
                    CanselChange();
                    break;
                }
            case '8':
                {
                    Console.WriteLine("Вы выбрали очистить консоль");
                    Console.Clear();
                    Action();
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
}

void printHuman()
{
    int count = 0;
    Console.WriteLine("П/н\tФИО\t\t\t\tВозраст\t\tДолжность\t\t\tЗарплата");
    foreach (Human human in humanList) // вывод людей в колекции
    {
        Console.Write(count + 1);
        Console.Write("\t" + human.FIO);
        Console.Write("\t\t\t\t" + human.Age);
        Console.Write("\t\t" + human.Post);
        Console.Write("\t\t\t\t" + human.Salary + "\n");
        count++;
    }
    Console.WriteLine();
    Action();
}

async void AddNewHuman() // переписать через прямую запись в класс а в конце проги запись в файл
{
    using (StreamWriter WriteTest = new StreamWriter(pathTemp, true, System.Text.Encoding.Default))
    {
        Console.Write("Введите ФИО : ");
        string? fioTemp = Console.ReadLine();
        fioTemp = (string.IsNullOrEmpty(fioTemp)) ? "-" : fioTemp;

        Console.Write("Введите возраст : ");
        string? ageTemp = Console.ReadLine();
        ageTemp = (string.IsNullOrEmpty(ageTemp)) ? "0" : ageTemp;

        Console.Write("Введите должность : ");
        string? postTemp = Console.ReadLine();
        postTemp = (string.IsNullOrEmpty(postTemp)) ? "-" : postTemp;

        Console.Write("Введите зарплату : ");
        string? salaryTemp = Console.ReadLine();
        salaryTemp = (string.IsNullOrEmpty(salaryTemp) ? "0" : salaryTemp);

        await WriteTest.WriteLineAsync($"{fioTemp}, "
                                     + $"{ageTemp}, "
                                     + $"{postTemp}, "
                                     + $"{salaryTemp}");

        humanList.Add(new Human());

        humanList[humanList.Count - 1].FIO = fioTemp;
        humanList[humanList.Count - 1].Age = (int.Parse(ageTemp));
        humanList[humanList.Count - 1].Post = postTemp;
        humanList[humanList.Count - 1].Salary = (int.Parse(salaryTemp));
        WriteTest.Close();
    }
    Action();
}

void CorectHuman()
{
    Console.WriteLine("Введите п/н сотрудника которого хотите отредактировать?");
    int numberCorectHuman = int.Parse(Console.ReadLine());

    if (numberCorectHuman - 1 < humanList.Count)
    {
        Console.WriteLine("Введите п/н графы который хотите отредактировать");

        int numberCorectChapt = int.Parse(Console.ReadLine());
        switch (numberCorectChapt)
        {
            case 1:
                {
                    Console.WriteLine($"Введите новое ФИО cотрудника с п/н {numberCorectHuman}");
                    string? NewFio = Console.ReadLine();
                    NewFio = (string.IsNullOrEmpty(NewFio)) ? "-" : NewFio;
                    humanList[numberCorectHuman - 1].FIO = NewFio;
                    break;
                }
            case 2:
                {
                    Console.WriteLine($"Введите новый воозраст cотрудника с п/н {numberCorectHuman}");
                    string? NewAge = Console.ReadLine();
                    NewAge = (string.IsNullOrEmpty(NewAge)) ? "0" : NewAge;
                    humanList[numberCorectHuman - 1].Age = int.Parse(NewAge);
                    break;
                }
            case 3:
                {
                    Console.WriteLine($"Введите новую должность cотрудника с п/н {numberCorectHuman}");
                    string? NewPost = Console.ReadLine();
                    NewPost = (string.IsNullOrEmpty(NewPost)) ? "-" : NewPost;
                    humanList[numberCorectHuman - 1].Post = NewPost;
                    break;
                }
            case 4:
                {
                    Console.WriteLine($"Введите новую заработную плату cотрудника с п/н {numberCorectHuman}");
                    string? NewSalary = Console.ReadLine();
                    NewSalary = (string.IsNullOrEmpty(NewSalary)) ? "0" : NewSalary;
                    humanList[numberCorectHuman - 1].Salary = int.Parse(NewSalary);
                    break;
                }
            default:
                {
                    Console.WriteLine("Такой графы нет :(");
                    break;
                }
        }
    }

    else Console.WriteLine("Сотрудника с таким п/п нет :(");
    Action();
}

void DeleteHuman()
{
    Console.Write("\nВведите п/п сотрудника которого хотите удалить : ");
    int numberDeletHuman = int.Parse(Console.ReadLine());
    if (numberDeletHuman < humanList.Count)
        humanList.RemoveAt(numberDeletHuman - 1);
    else Console.WriteLine("Сотрудника с таким п/п нет :(");
    Action();
}

void DeleteAllHuman()
{
    Console.WriteLine("\nВы все удалили.\n");
    humanList.Clear();
    using StreamWriter zeroingTemp = new StreamWriter(pathTemp, false);
    zeroingTemp.Close();
    Action();
}

void SaveChange() //сохранение путем перезаписи файла
{
    FileInfo copyTempToFile = new FileInfo(pathTemp);
    copyTempToFile.CopyTo(path, true);
    Console.WriteLine("\nВы все сохранили\n");
    Action();
}

void CanselChange()
{
    FileInfo copyFileToTemp = new FileInfo(path);
    copyFileToTemp.CopyTo(pathTemp, true);
    Console.WriteLine("\nВы все отменили\n");
    humanList.Clear();
    RecordInClass();
    Action();
}

void RecordInClass()
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
    reader.Close();
}






class Human // люди с их данными
{
    private string fio = "-";
    private int age = 0;
    private string post = "-";
    private int salary = 0;

    public string FIO
    {
        get => fio;
        set => fio = (value == null) ? "-" : value;
    }

    public int Age
    {
        get => age;
        set => age = value;
    }

    public string Post
    {
        get => post;
        set => post = (value == null) ? "-" : value;
    }

    public int Salary
    {
        get => salary;
        set => salary = value;
    }

}