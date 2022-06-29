using System;
using System.Collections.Generic;
using System.Text;

namespace Gangwar.Objects
{
    public class FfaWeaponObject
    {
        public uint WeaponHashOne { get; set; }
        public uint WeaponHashTwo { get; set; }
        public uint WeaponHashThree { get; set; }

        public FfaWeaponObject() { }

        public FfaWeaponObject(uint WeaponHashOne, uint WeaponHashTwo, uint WeaponHashThree)
        {
            this.WeaponHashOne = WeaponHashOne;
            this.WeaponHashTwo = WeaponHashTwo;
            this.WeaponHashThree = WeaponHashThree;
        }
    }
}
