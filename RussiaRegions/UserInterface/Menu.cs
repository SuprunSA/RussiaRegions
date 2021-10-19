using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    class Menu
    {
        IEnumerable<MenuItem> Items { get; }

        public Menu(IEnumerable<MenuItem> items)
        {
            Items = items;
        }

        public void Print()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            foreach (var item in Items)
            {
                item.Print();
                Console.Write("  ");
            }
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
            Console.ResetColor();
        }
    }
}