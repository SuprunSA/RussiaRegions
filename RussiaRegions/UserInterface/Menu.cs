using System;
using System.Collections.Generic;
using System.Text;

namespace RussiaRegions
{
    class Menu
    {
        public IEnumerable<MenuItem> Items { get; }

        public Menu(IEnumerable<MenuItem> items)
        {
            Items = items;
        }

        public void Print()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            foreach (var item in Items)
            {
                if (!item.Hidden)
                {
                    item.Print();
                    Console.Write("  ");
                }
            }
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
            Console.ResetColor();
        }

        public bool Action(ConsoleKey key)
        {
            foreach (var menuItem in Items)
            {
                if (menuItem.Key == key)
                {
                    if (menuItem is MenuClose)
                    {
                        return false;
                    }
                    if (menuItem is MenuAction)
                    {
                        (menuItem as MenuAction).Action();
                    }
                }
            }
            return true;
        }
    }
}