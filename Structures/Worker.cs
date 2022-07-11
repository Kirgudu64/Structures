using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    /// <summary>
    /// Структура, описывающая сотрудника
    /// </summary>
    public struct Worker
    {
        #region Поля

        /// <summary>
        /// ID сострудника
        /// </summary>
        int id;

        /// <summary>
        /// Дата и время добавления записи о сотруднике
        /// </summary>
        string date;

        /// <summary>
        /// Фамилия Имя Отчество сотрудника
        /// </summary>
        string name;

        /// <summary>
        /// Возраст сострудника
        /// </summary>
        int age;


        /// <summary>
        /// Рост сострудника
        /// </summary>
        string height;

        /// <summary>
        /// Дата рождения сострудника
        /// </summary>
        string birthday;

        /// <summary>
        /// Место раждения сострудника
        /// </summary>
        string city;

        #endregion

        #region Свойства

        /// <summary>
        /// ID сострудника
        /// </summary>
        public int ID { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// Дата и время добавления записи о сотруднике
        /// </summary>
        public string Date { get { return this.date; } set { this.date = value; } }

        /// <summary>
        /// Фамилия Имя Отчество сотрудника
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// Возраст сострудника
        /// </summary>
        public int Age { get { return this.age; } set { this.age = value; } }


        /// <summary>
        /// Рост сострудника
        /// </summary>
        public string Height { get { return this.height; } set { this.height = value; } }

        /// <summary>
        /// Дата рождения сострудника
        /// </summary>
        public string Birthday { get { return this.birthday; } set { this.birthday = value; } }

        /// <summary>
        /// Место раждения сострудника
        /// </summary>
        public string City { get { return this.city; } set { this.city = value; } }

        ///// <summary>
        ///// Место раждения сострудника
        ///// </summary>
        //public string City 
        //{ 
        //    get { return this.city;  } 
        //    set 
        //    {
        //        Console.Write("Место рождения: ");
        //        //this.City = Console.ReadLine();
        //        this.city = value; 
        //    } 
        //}

        #endregion

        #region Конструктор

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Date">Дата и время записи</param>
        /// <param name="Name">Фамилия Имя Отчество</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Height">Рост</param>
        /// <param name="Birthday">Дата рождения</param>
        /// <param name="City">Место рождения</param>
        public Worker(int ID, string Date, string Name, int Age, string Height, string Birthday, string City)
        {
            this.id = ID;
            this.date = Date;
            this.name = Name;
            this.age = Age;
            this.height = Height;
            this.birthday = Birthday;
            this.city = City;
        }


        #endregion

        #region Методы

        /// <summary>
        /// Формирование строки для вывода в консоль
        /// </summary>
        /// <returns></returns>
        public string PrintStr()
        {
            return $"{this.id,-3} {this.date,-15} {this.name,-30} {this.age,6} {this.height,5} " +
                $"{this.birthday,13} {this.city}";
        }

        /// <summary>
        /// Формирование строки с разделителями
        /// </summary>
        /// <returns></returns>
        public string WriteStr()
        {
            return $"{this.id}#{this.date}#{this.name}#{this.age}#{this.height}#{this.birthday}#{this.city}";
        }


        #endregion
    }
}
