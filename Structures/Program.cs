//
//  Файлы
//
//  Практическая работа
//
//  Цель: Научиться работать с файловой системой. Читать данные из файла и записывать их туда
//

using Structures;
/// <summary>
/// Метод, позволяющий вывести заголовок модуля в консоль
/// </summary>
/// <param name="Text">Текст заголовка</param>
static void Heading(string Text)
{
    Console.Clear();
    Console.WriteLine($"\n\t{Text}\n");
}

/// <summary>
/// Метод создания файла
/// </summary>
/// /// <param name="fileName">Имя файла</param>
static bool CreatingFile(string fileName)
{
    File.AppendAllText(fileName, "ID#Дата и время#Фамилия Имя Отчество#Возраст#Рост#Дата рождения#Место рождения\n");
    return true;
}

/// <summary>
/// Ввод данных: Дата и время добавления записи
/// </summary>
/// 
static string InputDate()
{
    string dataDate = DateTime.Today.ToString("dd.MM.yyyy hh:mm");
    Console.WriteLine($"Дата и время добавления записи: {dataDate}");    
    return dataDate;
}

/// <summary>
/// Ввод данных: Фамиоия Имя Отчество
/// </summary>
/// 
static string InputName()
{
    Console.Write("Фамилия Имя Отчество сотрудника (полностью): ");    
    string dataName = Console.ReadLine();
    return dataName;
}

/// <summary>
/// Ввод данных: дата рождения
/// </summary>
/// 
static string InputBirthday()
{
    Console.Write("Дата рождения: ");    
    string dataBirthday = Console.ReadLine();
    return dataBirthday;
}

/// <summary>
/// Ввод данных: Возраст
/// </summary>
/// 
static int InputAge(string birthday)
{
    int dataAge = DateTime.Now.Year - Convert.ToDateTime(birthday).Year;
    Console.WriteLine($"Возраст: {dataAge}");    
    return dataAge;
}

/// <summary>
/// Ввод данных: рост
/// </summary>
/// 
static string InputHeigh()
{
    Console.Write("Рост: ");
    string dataHeight = Console.ReadLine();
    return dataHeight;
}

/// <summary>
/// Ввод данных: место рождения
/// </summary>
/// 
static string InputCity()
{
    Console.Write("Место рождения: ");
    string dataCity = Console.ReadLine();
    return dataCity;
}

byte exit = 0;
while (exit != 1)
{
    Console.Clear();
    Console.Write("\tМЕНЮ:\n 1 - Вывести на экран список сотрудников\n");
    Console.Write(" 2 - Добавить данные о новых сотрудниках и сохранить в файл\n");
    Console.Write(" 3 - Удалить данные о сотруднике\n 4 - Изменить данные о сотруднике\n 0 - Выход\n\n =====");
    Console.Write("\n Выберите пункт меню: ");
    byte numMenu = byte.Parse(Console.ReadLine());
    Worker worker = new Worker();
    int idNum;

    if (numMenu >= 0 && numMenu <= 2) ;
    {
        string path = "staff.txt";
        Repository rep = new Repository(path);
        string ind = "д";

        switch (numMenu)
        {
            case 0: exit = 1; break;    // Выход из программы
            case 1:                     // Выводим список сотрудников в консоль
                Heading("Вывод списка сотрудников");

                if (File.Exists(path))
                {                    
                    Console.Write("Нужна сортировка? (д/н): ");
                    string srt = Console.ReadLine();
                    Console.Write("\nНужен фильтр по дате рождения? (д/н): ");
                    string filter = Console.ReadLine();
                    DateTime firstDate = new DateTime();
                    DateTime secondDate;
                    if (filter == "д")
                    {
                        Console.Write("\n   Введите дату начала периода: ");
                        firstDate = Convert.ToDateTime(Console.ReadLine());
                        Console.Write("\n   Введите дату окончания периода: ");
                        secondDate = Convert.ToDateTime(Console.ReadLine());

                        if (srt != "д")
                        {
                            rep.Sorting(0, true, firstDate, secondDate);
                        }
                    }
                    if (srt == "д")
                    {
                        Console.WriteLine($"\nКак сортировать?\n    1 - по возрастанию\n    2 - по убыванию");
                        bool key = true;
                        string k = Console.ReadLine();
                        if (k == "2")
                        {
                            key = false;
                        }
                        Console.Write($"\n 1 - Дата формирования записи\n 2 - Фамилия Имя Отчество\n" +
                            $" 3 - Возраст\n 4 - Рост\n 5 - Дата рождения\n 6 - Место рождения\n" +
                            $" Введите номер критерия для сортировки ===> ");
                        int num = Convert.ToInt16(Console.ReadLine());
                        Console.Clear();
                                                
                        rep.Sorting(num, key, firstDate, DateTime.Today);
                        break;
                    }
                    else if (filter != "д")
                    {
                        rep.Load();
                        rep.PrintToConsole();
                    }
                }
                else
                {
                    Console.Write("\n Файл отсутствует. Создать файл? (д/н): ");
                    ind = Console.ReadLine();
                    if (ind == "д")
                    {
                        if (CreatingFile(path))
                        {
                            Console.WriteLine(" Файл создан");
                            Console.ReadKey();
                        }
                    }
                    break;
                }
                Console.ReadKey();
                break;
            case 2:                     // Добавляем данные о новых сотрудниках и сохраняем в файл
                Heading("Добавляем данные о новых сотрудниках и сохраняем в файл");
                rep.Load();
                do
                {
                    Console.WriteLine("\nВведите данные о новом сотруднике:");
                    worker.ID = rep.Count + 1;
                    Console.WriteLine($"\n\nID сотрудника: {worker.ID}");                    
                    worker.Date = InputDate();
                    worker.Name = InputName();                    
                    worker.Birthday = InputBirthday();                    
                    worker.Age = InputAge(worker.Birthday);
                    worker.Height = InputHeigh();                    
                    worker.City = InputCity();
                    rep.Add(new Worker(worker.ID, worker.Date, worker.Name, worker.Age, worker.Height, worker.Birthday, worker.City));
                    Console.Write("\nПродолжить в ввод? (д/н): ");
                    ind = Console.ReadLine();
                    Console.Clear();
                } while (ind == "д");
                rep.PrintToConsole();
                Console.ReadKey();
                File.Delete(path);
                CreatingFile(path);
                rep.SaveFile(path);
                break;
            case 3:                     // Удаляем данные о сотруднике
                Heading("Удалить данные о сотруднике\n");
                rep.Load();
                Console.Write($"Введите ID сотрудника: ");
                idNum = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine(idNum);
                Console.ReadKey();
                File.Delete(path);
                CreatingFile(path);
                rep.DeleneWorker(path, idNum);
                break;
            case 4:                     // Изменяем данные о сотруднике
                Heading("Изменить данные о сотруднике\n");
                rep.Load();
                Console.Write($"Введите ID сотрудника: ");
                idNum = Convert.ToInt16(Console.ReadLine());
                rep.PrintToConsoleOneWorker(idNum);

                int numPar = Convert.ToInt16(Console.ReadLine());

                switch (numPar)
                {
                    default: break;
                    case 1:
                        {
                            worker.Name = InputName();
                            rep.EditWorker(idNum, numPar, worker.Name);
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введите возраст: ");
                            string age = Console.ReadLine();
                            rep.EditWorker(idNum, numPar, age);
                            break;
                        }
                    case 3:
                        {
                            worker.Height = InputHeigh();
                            rep.EditWorker(idNum, numPar, worker.Height);
                            break;
                        }
                    case 4:
                        {
                            worker.Birthday = InputBirthday();
                            rep.EditWorker(idNum, numPar, worker.Birthday);
                            break;
                        }
                    case 5:
                        {
                            worker.City = InputCity();
                            rep.EditWorker(idNum, numPar, worker.City);
                            break;
                        }
                    
                }
                File.Delete(path);
                CreatingFile(path);
                rep.SaveFile(path);




                //worker.Date = InputDate();
                //worker.Name = InputName();
                //worker.Birthday = InputBirthday();
                //worker.Age = InputAge(worker.Birthday);
                //worker.Height = InputHeigh();
                //worker.City = InputCity();
                Console.ReadKey();
                break;
        }
    }
}