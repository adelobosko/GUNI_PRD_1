using System;
using System.Collections.Generic;

namespace GUNI_PRD_1
{
    public class Menu
    {
        public List<MenuItem> Items { get; set; }

        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public void ShowInConsole()
        {
            var input = "";
            MenuItem menuItem;

            startShowInConsoleLabel:
            Console.Clear();
            Console.WriteLine($"Select option:");
            for (int i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"  {i} - {Items[i].Name}");
            }
            Console.WriteLine($"  e - Close program");
            try
            {
                input = Console.ReadLine();
                if (input == "e")
                {
                    return;
                }
                menuItem = Items[Convert.ToInt32(input)];
                menuItem.Action.Invoke(this, Items[Convert.ToInt32(input)], typeof(MenuItem));
            }
            catch (Exception ex)
            {
                Console.WriteLine($" *** Can not execute {input} operation id.");
                Console.WriteLine($" * Press any key to continue...");
                Console.ReadKey();
            }
            goto startShowInConsoleLabel;
        }

        public override string ToString()
        {
            return "";
        }
    }
}