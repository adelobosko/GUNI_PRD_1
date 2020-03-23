using System;
using System.Collections.Generic;

namespace GUNI_PRD_1
{
    public class MenuItem
    {
        public List<MenuItem> Items { get; set; }
        public string Name { get; set; }
        public Action<Menu, object, Type> Action { get; set; }

        public MenuObject MenuObject { get; set; }

        public MenuItem()
        {
            Items = new List<MenuItem>();
            MenuObject = new MenuObject();
        }
    }
}