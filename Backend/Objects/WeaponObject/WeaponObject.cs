namespace Gangwar.Objects.WeaponObject
{
    public class WeaponObject
    {
        public int WeaponOne { get; set; }
        public int WeaponTwo { get; set; }
        
        public WeaponObject() {}

        public WeaponObject(int WeaponOne, int WeaponTwo)
        {
            this.WeaponOne = WeaponOne;
            this.WeaponTwo = WeaponTwo;
        }
    }
}