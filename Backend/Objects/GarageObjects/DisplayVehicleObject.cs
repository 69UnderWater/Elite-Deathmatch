using GTANetworkAPI;

namespace Gangwar.Objects
{
    public class DisplayVehicleObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public int Level { get; set; }
        
        public DisplayVehicleObject(){}

        public DisplayVehicleObject(int Id, string Name, string Hash, int Level)
        {
            this.Id = Id;
            this.Name = Name;
            this.Hash = Hash;
            this.Level = Level;
        }
    }
}