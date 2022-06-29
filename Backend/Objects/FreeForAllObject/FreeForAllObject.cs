using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Gangwar.Objects
{
    public class FreeForAllObject
    {
        public int SpawnId { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public FreeForAllObject() { }

        public FreeForAllObject(int SpawnId, Vector3 Position, Vector3 Rotation)
        {
            this.SpawnId = SpawnId;
            this.Position = Position;
            this.Rotation = Rotation;
        }
    }
}
