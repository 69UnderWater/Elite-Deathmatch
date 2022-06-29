using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Gangwar.Objects
{
    public class ItemObject : Script
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ItemObject() { }

        public virtual bool getItemFunction(Player player) { return false; }
    }
}
