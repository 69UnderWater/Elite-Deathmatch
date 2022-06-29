namespace Gangwar.Objects
{
    public class GarageVehicleObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public int Level { get; set; }
        
        public GarageVehicleObject() {}

        public GarageVehicleObject(int Id, string name, string hash, int level)
        {
            this.Id = Id;
            this.Name = name;
            this.Hash = hash;
            this.Level = level;
        }
    }
}