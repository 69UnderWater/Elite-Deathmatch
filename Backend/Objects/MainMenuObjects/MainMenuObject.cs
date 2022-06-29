namespace Gangwar.Objects.MainMenuObjects
{
    public class PlayerDataObject
    {
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Money { get; set; }
        public int Level { get; set; }
        public int PlayedHours { get; set; }
        public bool HasAccount { get; set; }
        public int Language { get; set; }

        public PlayerDataObject()
        {
        }

        public PlayerDataObject(int Kills, int Deaths, int Money, int Level, int PlayedHours, bool HasAccount,
            int Language)
        {
            this.Kills = Kills;
            this.Deaths = Deaths;
            this.Money = Money;
            this.Level = Level;
            this.PlayedHours = PlayedHours;
            this.HasAccount = HasAccount;
            this.Language = Language;
        }
    }

    public class TeamDataObject
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string ShortName { get; set; }
        public int Count { get; set; }
        public bool IsPrivate { get; set; }
        
        public TeamDataObject() {}

        public TeamDataObject(int Id, string TeamName, string ShortName, int Count, bool IsPrivate)
        {
            this.Id = Id;
            this.TeamName = TeamName;
            this.ShortName = ShortName;
            this.Count = Count;
            this.IsPrivate = IsPrivate;
        }
    }

    public class FfaDataObject
    {
        public int Id { get; set; }
        public string ArenaName { get; set; }
        public int MaxPlayers { get; set; }
        public int Count { get; set; }
        
        public FfaDataObject() {}

        public FfaDataObject(int Id, string ArenaName, int Count, int MaxPlayers)
        {
            this.Id = Id;
            this.ArenaName = ArenaName;
            this.MaxPlayers = MaxPlayers;
            this.Count = Count;
        }
    }
}
