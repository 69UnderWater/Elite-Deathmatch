using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Gangwar.Objects
{
    public class WeaponTruckPositionObject
    {
        public Vector3 Position { get; set; }
        public float Rotation { get; set; }

        public WeaponTruckPositionObject() { }

        public WeaponTruckPositionObject(Vector3 Position, float Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;
        }
    }
}
