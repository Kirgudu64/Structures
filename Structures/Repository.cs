using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Structures
{
    struct Repository
    {
        /// <summary>
        /// База данных сотрудников
        /// </summary>
        private Worker[] workers;

        /// <summary>
        /// Имя файла сотрудников
        /// </summary>
        private string path;

        /// <summary>
        /// Индекс
        /// </summary>
        int index;

        /// <summary>
        /// Титульная строка
        /// </summary>
        string[] titles;

        #region Конструктор
        public Repository(string path)
        {
            this.path = path;
            this.index = 0;
            this.titles = new string[7];
            this.workers = new Worker[10];
        }
        #endregion

        #region Методы

        /// <summary>
        /// Загрузка из файла
        /// </summary>
        /// 
        public void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                titles = sr.ReadLine().Split("#");

                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split("#");
                    Add(new Worker(Convert.ToInt32(args[0]), args[1], args[2], Convert.ToInt32(args[3]), args[4], args[5], args[6]));
                }
            }
        }

        /// <summary>
        /// Метод для добавления строки в массив
        /// </summary>
        ///
        public void Add(Worker CertainWorker)
        {
            this.Resize(index >= this.workers.Length);
            this.workers[index] = CertainWorker;
            this.index++;
        }

        /// <summary>
        /// Увеличение размера массива
        /// </summary>
        ///
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.workers, this.workers.Length * 2);
            }
        }


        /// <summary>
        /// Вывод в консоль
        /// </summary>
        /// 
        public void PrintToConsole()
        {
            Console.WriteLine($"{this.titles[0],-3} {this.titles[1],-15} {this.titles[2],-30} {this.titles[3],6} " +
                $"{this.titles[4],5} {this.titles[5],13} {this.titles[6]}");
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this.workers[i].PrintStr());
            }
            Console.WriteLine($"\nОбщее колличество сотрудников: {Count}");
        }

        /// <summary>
        /// Вывод в консоль одного сотрудника
        /// </summary>
        /// 
        public void PrintToConsoleOneWorker(int indexToWorker)
        {
            //Console.WriteLine($"{this.titles[0],-3} {this.titles[1],-15} {this.titles[2],-30} {this.titles[3],6} " +
            //    $"{this.titles[4],5} {this.titles[5],13} {this.titles[6]}");
            //Console.WriteLine(this.workers[indexToWorker-1].PrintStr());           

            Console.WriteLine($"\n    {this.titles[0],-21}: {this.workers[indexToWorker - 1].ID}");
            //Console.WriteLine($"    {this.titles[1],-21}:{this.workers[indexToWorker - 1].Date}");
            Console.WriteLine($"1 - {this.titles[2],-21}: {this.workers[indexToWorker - 1].Name}");
            Console.WriteLine($"2 - {this.titles[3],-21}: {this.workers[indexToWorker - 1].Age}");
            Console.WriteLine($"3 - {this.titles[4],-21}: {this.workers[indexToWorker - 1].Height}");
            Console.WriteLine($"4 - {this.titles[5],-21}: {this.workers[indexToWorker - 1].Birthday}");
            Console.WriteLine($"5 - {this.titles[6],-21}: {this.workers[indexToWorker - 1].City}");
            Console.Write("\nВведите номер поля для исправления: ");

        }

        /// <summary>
        /// Сохранение данных в файле
        /// </summary>
        public void SaveFile(string path)
        {
            string str;

            for (int i = 0; i < this.index; i++)
            {
                str = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}",
                    this.workers[i].ID, this.workers[i].Date, this.workers[i].Name, this.workers[i].Age,
                    this.workers[i].Height, this.workers[i].Birthday, this.workers[i].City);
                File.AppendAllText(path, $"{str}\n");
            }

        }

        /// <summary>
        /// Изменение записи
        /// </summary>
        /// 
        public void EditWorker(int indexToWorker, int numInd, string editData)
        {
            switch (numInd)
            {
                case 1:
                    {
                        this.workers[indexToWorker - 1].Name = editData;                        
                        break;
                    }
                case 2:
                    {
                        this.workers[indexToWorker - 1].Age = Convert.ToInt32(editData);                        
                        break;
                    }
                case 3:
                    {
                        this.workers[indexToWorker - 1].Height = editData;                        
                        break;
                    }
                case 4:
                    {
                        this.workers[indexToWorker - 1].Birthday = editData;                        
                        break;
                    }
                case 5:
                    {
                        this.workers[indexToWorker - 1].City = editData;                        
                        break;
                    }                               
            }
            this.workers[indexToWorker - 1].Date = DateTime.Today.ToString("dd.MM.yyyy hh:mm");

        }

        /// <summary>
        /// Удаление данных о сотруднике в файле
        /// </summary>
        public void DeleneWorker(string path, int indexToDelete)
        {
            // Проверки, что наш массив не пуст и что указанный индекс существует.
            if (this.workers.Length == 0) return;
            if (this.workers.Length <= indexToDelete) return;

            string[] workersNew = new string[this.workers.Length - 1];
            int counter = 0;

            for (int i = 0; i < this.index; i++)
            {
                if (this.workers[i].ID != indexToDelete)
                {
                    workersNew[counter] = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}",
                        (counter + 1), this.workers[i].Date, this.workers[i].Name, this.workers[i].Age,
                        this.workers[i].Height, this.workers[i].Birthday, this.workers[i].City);
                    File.AppendAllText(path, $"{workersNew[counter]}\n");
                    counter++;
                }
                else continue;
            }
        }

        /// <summary>
        /// Количество сотрудников
        /// </summary>
        /// 
        public int Count { get { return this.index; } }

        /// <summary>
        /// Сортировка по заданному критерию
        /// </summary>
        /// <param name="numPos">Номер позиции в структуре</param>
        public void Sorting(int numPos, bool keySort, DateTime firstDate, DateTime secondDate)
        {           
            string[] arrays = File.ReadAllLines(this.path);

            string title = arrays[0];       // Выделяем титульную строку (она не участвует в сортировке)

            for (int i = 0; i < arrays.Length - 1; i++)
            {
                arrays[i] = arrays[i + 1];
            }
            Array.Resize(ref arrays, arrays.Length - 1);

            if (numPos != 0)
            {
                for (int i = 0; i < arrays.Length; i++)
                {
                    string[] strings = arrays[i].Split("#");
                    if (numPos == 5)
                    {
                        var dateTime = DateTime.Parse(strings[5]);
                        strings[5] = ($"{dateTime:s}");
                    }
                    arrays[i] = strings[numPos];

                    for (int j = 0; j < strings.Length; j++)
                    {
                        if (j != numPos)
                        {
                            arrays[i] = arrays[i] + "#" + strings[j];
                        }
                    }
                }
                Array.Sort(arrays);
                if (!keySort)
                {
                    Array.Reverse(arrays);
                }
            }                                                     
          
            titles = title.Split("#");
            Console.WriteLine($"{this.titles[0],-3} {this.titles[1],-15} {this.titles[2],-30} {this.titles[3],6} " +
                $"{this.titles[4],5} {this.titles[5],13} {this.titles[6]}\n");

            int counter = 0;
            for (int i = 0; i < arrays.Length; i++)
            {
                string[] strings = arrays[i].Split("#");
                
                if (numPos != 0)
                {
                    if (numPos == 5)
                    {
                        var dateTime = DateTime.Parse(strings[0]);
                        strings[0] = dateTime.ToString("d");
                    }
                    arrays[i] = strings[1];

                    for (int j = 2; j < strings.Length; j++)
                    {
                        if (j != (numPos + 1))
                        {
                            arrays[i] = arrays[i] + "#" + strings[j];
                        }
                        else
                        {
                            arrays[i] = arrays[i] + "#" + strings[0] + "#" + strings[j];
                        }
                    }
                    if (numPos == strings.Length - 1)
                    {
                        arrays[i] = arrays[i] + "#" + strings[0];
                    }
                }
            
                string[] args = arrays[i].Split("#");
                
                if (Convert.ToDateTime(args[5]) >= firstDate && Convert.ToDateTime(args[5]) <= secondDate)
                {
                    Console.WriteLine($"{args[0],-3} {args[1],-15} {args[2],-30} {args[3],6} {args[4],5} {args[5],13} {args[6]}");
                    counter++;
                }                
            }
            
            Console.WriteLine($"\nВсего сотрудников: {counter}");
            Console.ReadKey();            
        }

        #endregion

    }
}
