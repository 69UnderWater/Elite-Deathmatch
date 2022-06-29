using System;
using System.Collections.Generic;
using System.Text;

namespace Gangwar.Objects
{
    public class InventoryObject
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public InventoryObject() { }

        public InventoryObject(string Name, int Amount)
        {
            this.Name = Name;
            this.Amount = Amount;
        }
    }
}
