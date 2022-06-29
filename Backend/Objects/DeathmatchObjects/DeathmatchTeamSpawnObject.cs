using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace Gangwar.Objects
{
    public class DeathmatchTeamSpawnObject
    {
        public Vector3 SpawnPosition { get; set; }
        public Vector3 SpawnRotation { get; set; }

        public DeathmatchTeamSpawnObject(Vector3 SpawnPosition, Vector3 SpawnRotation)
        {
            this.SpawnPosition = SpawnPosition;
            this.SpawnRotation = SpawnRotation;
        }

        public DeathmatchTeamSpawnObject() { }
    }
}
