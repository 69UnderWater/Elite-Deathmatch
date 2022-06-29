using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Gangwar.Objects
{
    public class GarageObject
    {
        public Vector3 GarageOutParkPosition { get; set; }
        public float GarageOutParkRotation { get; set; }

        public GarageObject() { }

        public GarageObject(Vector3 GarageOutParkPosition, float GarageOutParkRotation)
        {
            this.GarageOutParkPosition = GarageOutParkPosition;
            this.GarageOutParkRotation = GarageOutParkRotation;
        }

        public bool IsParkOutPointFree()
        {
            return NAPI.Pools.GetAllVehicles().Find(x => x.Position.DistanceTo(GarageOutParkPosition) <= 2.5) != null;
        }
    }
}
