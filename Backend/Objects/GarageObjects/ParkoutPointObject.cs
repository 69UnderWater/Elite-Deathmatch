using GTANetworkAPI;

namespace Gangwar.Objects
{
    public class ParkoutPointObject
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        
        public ParkoutPointObject() {}
        public ParkoutPointObject(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;
        }
    }
}