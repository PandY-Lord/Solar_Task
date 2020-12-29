using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ForSolar
{
    class Database : DbContext
    {

        public DbSet<Table> Elements { get; set; }

        public Database()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ForSolar;Trusted_Connection=True;");
        }
        private void PrintList(List<Table> tables) //Вывод
        {
            Console.WriteLine("Планировщик:\n Id\t|\t Дата выполнения\t|\t     Задача     \t|\t       Описание\n" +
                                           " ___\t \t _______________\t \t _______________\t \t ______________________\t");
            foreach (Table i in tables)
            {
                Console.WriteLine(
                    $"{i.Id,3}\t|\t " +
                    $"{i.Date,15}\t|\t " +
                    $"{i.Name,15}\t|\t " +
                    $"{i.Note,22}\t|\t   "
                    );
            }

        }

        // Создать элемент
        public Table CreateAndFill(Table caseEvent = null)
        {
            if (caseEvent == null)
                caseEvent = new Table();

            for (int i = 0; i < 3; i++)
            {
                string eventMenu = $"Событие:\n" +
                    $" 1 Название: \t\t{caseEvent.Name}\n" +
                    $" 2 Описание: \t\t{caseEvent.Note}\n" +
                    $" 3 Дата начала события: \t{caseEvent.Date}\n";
                Console.Clear();
                Console.Write(eventMenu);
                string input = null;
                Console.Write($" \n\n Ввод {i + 1}: ");
                input = Console.ReadLine().ToString();

                switch (i)
                {
                    case 0:
                        caseEvent.Name = input;
                        break;

                    case 1:
                        caseEvent.Note = input;
                        break;

                    case 2:
                        caseEvent.Date = input;
                        break;
                }
            }
            return caseEvent;
        }
        // Вывод
        public void PrintCase()
        {
            PrintList(Elements.ToList());
        }

        // Сортировка названий
        public void PrintCaseOrderByNameAsc() // Сортировка названий по возрастанию
        {
            PrintList(Elements.OrderBy(p => p.Name).ToList());
        }

        public void PrintCaseOrderByNameDesc() // Сортировка названий по убыванию
        {
            PrintList(Elements.OrderByDescending(p => p.Name).ToList());
        }

        // Сортировка по Описанию
        public void PrintCaseOrderByDescriptionAsc() // Сортировка описаний по возрастанию
        {
            PrintList(Elements.OrderBy(p => p.Note).ToList());
        }

        public void PrintCaseOrderByDescriptionDesc() // Сортировка описаний по убыванию
        {
            PrintList(Elements.OrderByDescending(p => p.Note).ToList());
        }

        public void PrintCaseOrderByDateAsc() // Сортировка дат по возрастанию
        {
            PrintList(Elements.OrderBy(p => p.Date).ToList());
        }

        public void PrintCaseOrderByDateDesc() // Сортировка дат по убыванию
        {
            PrintList(Elements.OrderByDescending(p => p.Date).ToList());
        }

        public Table GetCaseById(int id) // Копия экземпляра по ID
        {
            return Elements.Where(p => p.Id == id).ToList()[0];
        }
    }
}
