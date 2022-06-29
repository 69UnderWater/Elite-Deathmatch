using System.Collections.Generic;
using GTANetworkAPI;
using Microsoft.EntityFrameworkCore;
using Gangwar.DbModels.Models;
using Gangwar.Objects;
using Gangwar.Objects.TeamObject;
using Gangwar.Objects.WeaponObject;

namespace Gangwar.DbModels
{
    public partial class gtaContext : DbContext
    {
        public gtaContext() { }
        public gtaContext(DbContextOptions<gtaContext> options) : base(options) { }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<FFASpawns> FFASpawns { get; set; }
        public virtual DbSet<AdminRanks> AdminRanks { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<TeamClothing> TeamClothing { get; set; }
        public virtual DbSet<Factorys> Factorys { get; set; }
        public virtual DbSet<WeaponTruck> WeaponTruck { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Dealer> Dealer { get; set; }
        public virtual DbSet<Garage> Garage { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }
        public virtual DbSet<Deathmatch> Deathmatch { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    $"server={Core.DatabaseConfig.Host};port={Core.DatabaseConfig.Port};user={Core.DatabaseConfig.Username};password={Core.DatabaseConfig.Password};database={Core.DatabaseConfig.Database}");
                    optionsBuilder.EnableSensitiveDataLogging(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("player_accounts", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(16)");
                entity.Property(x => x.Username).HasColumnName("Username").HasColumnType("varchar(255)");
                entity.Property(x => x.SocialclubName).HasColumnName("SocialclubName").HasColumnType("varchar(255)");
                entity.Property(x => x.SocialclubId).HasColumnName("SocialclubId").HasColumnType("bigint(20)");
                entity.Property(x => x.HardwareId).HasColumnName("HardwareId").HasColumnType("varchar(255)");
                entity.Property(x => x.AdminLevel).HasColumnName("AdminLevel").HasColumnType("int(16)");
                entity.Property(x => x.Level).HasColumnName("Level").HasColumnType("int(16)");
                entity.Property(x => x.Prestige).HasColumnName("Prestige").HasColumnType("int(16)");
                entity.Property(x => x.CurrentXP).HasColumnName("CurrentXP").HasColumnType("int(16)");
                entity.Property(x => x.Kills).HasColumnName("Kills").HasColumnType("int(16)");
                entity.Property(x => x.Deaths).HasColumnName("Deaths").HasColumnType("int(16)");
                entity.Property(x => x.Money).HasColumnName("Money").HasColumnType("int(16)");
                entity.Property(x => x.PlayedHours).HasColumnName("PlayedHours").HasColumnType("int(32)");
                entity.Property(x => x.SelectedLanguage).HasColumnName("SelectedLanguage").HasColumnType("int(32)");
                entity.Property(x => x.PrivateFrakId).HasColumnName("PrivateFrakId").HasColumnType("int(32)");
                entity.Property(x => x.PrivateFrakRank).HasColumnName("PrivateFrakRank").HasColumnType("int(32)");
                entity.Property(x => x.IsPrivateFrak).HasColumnName("IsPrivateFrak");
                entity.Property(x => x.Weapons).HasColumnName("Weapons").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<WeaponObject>(v)
                );

                entity.Property(x => x.IsBanned).HasColumnName("IsBanned");
                entity.Property(x => x.IsMuted).HasColumnName("IsMuted");
                entity.Property(x => x.IsTimeBanned).HasColumnName("IsTimeBanned");
                entity.Property(x => x.BanDate).HasColumnName("BanDate");
                entity.Property(x => x.TimeBanUntil).HasColumnName("TimeBanUntil");
                entity.Property(x => x.Warns).HasColumnName("Warns").HasColumnType("int(16)");
                entity.Property(x => x.BanReason).HasColumnName("BanReason").HasColumnType("varchar(255)");

                entity.Property(x => x.guildMemberId).HasColumnName("guildMemberId").HasColumnType("varchar(255)");
                entity.Property(x => x.discordSyncCode).HasColumnName("discordSyncCode").HasColumnType("varchar(255)");
                entity.Property(x => x.DailyMission).HasColumnName("DailyMission").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<DailyMissionObject>(v)
                );
            });

            modelBuilder.Entity<FFASpawns>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("server_ffa_spawns", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.ArenaId).HasColumnName("ArenaId").HasColumnType("int(32)");
                entity.Property(x => x.ArenaName).HasColumnName("ArenaName").HasColumnType("varchar(255)");
                entity.Property(x => x.maxPlayers).HasColumnName("maxPlayers").HasColumnType("int(32)");
                entity.Property(x => x.Spawns).HasColumnName("Spawns").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<List<FreeForAllObject>>(v)
                );
                entity.Property(x => x.Weapons).HasColumnName("Weapons").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<FfaWeaponObject>(v)
                );
            });

            modelBuilder.Entity<AdminRanks>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("server_adminranks", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar(255)");
                entity.Property(x => x.Permission).HasColumnName("Permission").HasColumnType("int(32)");
                entity.Property(x => x.ChatColor).HasColumnName("ChatColor").HasColumnType("varchar(255)");
                entity.Property(x => x.AdminClothing).HasColumnName("AdminClothing").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<AdminClothingObject>(v)
                );
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("server_teams", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.TeamId).HasColumnName("TeamId").HasColumnType("int(32)");
                entity.Property(x => x.TeamName).HasColumnName("TeamName").HasColumnType("varchar(255)");
                entity.Property(x => x.ShortName).HasColumnName("ShortName").HasColumnType("varchar(255)");
                entity.Property(x => x.TeamSpawnPoint).HasColumnName("TeamSpawnPoint").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.TeamSpawnRotation).HasColumnName("TeamSpawnRotation").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.BlipColor).HasColumnName("BlipColor").HasColumnType("int(32)");
                entity.Property(x => x.PrimaryColor).HasColumnName("PrimaryColor").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<ColorObjects>(v)
                );
                entity.Property(x => x.SecondaryColor).HasColumnName("SecondaryColor").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<ColorObjects>(v)
                );
                entity.Property(x => x.IsPrivate).HasColumnName("IsPrivate");
                entity.Property(x => x.TeamPedHash).HasColumnName("TeamPedHash").HasColumnType("varchar(255)");
                entity.Property(x => x.TeamPedSpawnPoint).HasColumnName("TeamPedSpawnPoint").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.TeamPedSpawnRotation).HasColumnName("TeamPedSpawnRotation").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
            });

            modelBuilder.Entity<TeamClothing>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("server_teams_clothing", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.TeamId).HasColumnName("TeamId").HasColumnType("int(32)");
                entity.Property(x => x.Gender).HasColumnName("Gender").HasColumnType("int(32)");
                entity.Property(x => x.TeamClothingId).HasColumnName("TeamClothingId").HasColumnType("int(32)");

                entity.Property(x => x.MaskDrawable).HasColumnName("MaskDrawable").HasColumnType("int(32)");
                entity.Property(x => x.MaskTexture).HasColumnName("MaskTexture").HasColumnType("int(32)");
                entity.Property(x => x.TorsoDrawable).HasColumnName("TorsoDrawable").HasColumnType("int(32)");
                entity.Property(x => x.TorsoTexture).HasColumnName("TorsoTexture").HasColumnType("int(32)");
                entity.Property(x => x.LegsDrawable).HasColumnName("LegsDrawable").HasColumnType("int(32)");
                entity.Property(x => x.LegsTexture).HasColumnName("LegsTexture").HasColumnType("int(32)");
                entity.Property(x => x.BagsNParachuteDrawable).HasColumnName("BagsNParachuteDrawable").HasColumnType("int(32)");
                entity.Property(x => x.BagsNParachuteTexture).HasColumnName("BagsNParachuteTexture").HasColumnType("int(32)");
                entity.Property(x => x.ShoeDrawable).HasColumnName("ShoeDrawable").HasColumnType("int(32)");
                entity.Property(x => x.ShoeTexture).HasColumnName("ShoeTexture").HasColumnType("int(32)");
                entity.Property(x => x.AccessiorDrawable).HasColumnName("AccessiorDrawable").HasColumnType("int(32)");
                entity.Property(x => x.AccessiorTexture).HasColumnName("AccessiorTexture").HasColumnType("int(32)");
                entity.Property(x => x.UndershirtDrawable).HasColumnName("UndershirtDrawable").HasColumnType("int(32)");
                entity.Property(x => x.UndershirtTexture).HasColumnName("UndershirtTexture").HasColumnType("int(32)");
                entity.Property(x => x.BodyArmorDrawable).HasColumnName("BodyArmorDrawable").HasColumnType("int(32)");
                entity.Property(x => x.BodyArmorTexture).HasColumnName("BodyArmorTexture").HasColumnType("int(32)");
                entity.Property(x => x.TopDrawable).HasColumnName("TopDrawable").HasColumnType("int(32)");
                entity.Property(x => x.TopTexture).HasColumnName("TopTexture").HasColumnType("int(32)");

                entity.Property(x => x.HatsDrawable).HasColumnName("HatsDrawable").HasColumnType("int(32)");
                entity.Property(x => x.HatsTexture).HasColumnName("HatsTexture").HasColumnType("int(32)");
                entity.Property(x => x.GlassesDrawable).HasColumnName("GlassesDrawable").HasColumnType("int(32)");
                entity.Property(x => x.GlassesTexture).HasColumnName("GlassesTexture").HasColumnType("int(32)");
            });

            modelBuilder.Entity<Factorys>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("server_factory", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.OwnerId).HasColumnName("OwnerId").HasColumnType("int(32)");
                entity.Property(x => x.FactoryPosition).HasColumnName("FactoryPosition").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.FactoryRotation).HasColumnName("FactoryRotation").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.FactoryRobPosition).HasColumnName("FactoryRobPosition").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                    );
            });

            modelBuilder.Entity<WeaponTruck>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("server_weapontruck", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.SpawnPosition).HasColumnName("SpawnPosition").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<WeaponTruckPositionObject>(v)
                );
                entity.Property(x => x.ReturnPosition).HasColumnName("ReturnPosition").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("player_inventory", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.SocialClubId).HasColumnName("SocialClubId").HasColumnType("bigint(20)");
                entity.Property(x => x.Inventar).HasColumnName("Inventar").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<List<InventoryObject>>(v)
                );
            });

            modelBuilder.Entity<Dealer>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("server_dealer", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.DealerId).HasColumnName("DealerId").HasColumnType("int(32)");
                entity.Property(x => x.LocationName).HasColumnName("LocationName").HasColumnType("varchar(255)");
                entity.Property(x => x.DealerPosition).HasColumnName("DealerPosition").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.DealerRotation).HasColumnName("DealerRotation").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.AbgabeDealerPosition).HasColumnName("AbgabeDealerPosition").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.AbgabeDealerRotation).HasColumnName("AbgabeDealerRotation").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
            });

            modelBuilder.Entity<Garage>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("server_team_garages", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.TeamId).HasColumnName("TeamId").HasColumnType("int(32)");
                entity.Property(x => x.GaragePosition).HasColumnName("GaragePosition").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.GarageOutParkPoint).HasColumnName("GarageOutParkPoint").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<Vector3>(v)
                );
                entity.Property(x => x.ParkoutPoints).HasColumnName("ParkoutPoints").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<List<ParkoutPointObject>>(v)
                );
                entity.Property(x => x.Vehicles).HasColumnName("Vehicles").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<List<GarageVehicleObject>>(v)
                );
            });

            modelBuilder.Entity<Vehicles>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("player_vehicles", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.AccountId).HasColumnName("AccountId").HasColumnType("int(32)");
                entity.Property(x => x.PrivateVehicles).HasColumnName("Vehicles").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<List<GarageVehicleObject>>(v)
                );
            });

            modelBuilder.Entity<Deathmatch>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("server_deathmatch", Core.DatabaseConfig.Database);
                entity.HasIndex(x => x.Id).HasName("Id");
                entity.Property(x => x.Id).HasColumnName("Id").HasColumnType("int(32)");
                entity.Property(x => x.DeathmatchId).HasColumnName("DeathmatchId").HasColumnType("int(32)");
                entity.Property(x => x.TeamOneSpawnPosition).HasColumnName("TeamOneSpawnPosition").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<DeathmatchTeamObject>(v)
                );
                entity.Property(x => x.TeamTwoSpawnPosition).HasColumnName("TeamTwoSpawnPosition").HasConversion(
                    v => NAPI.Util.ToJson(v),
                    v => NAPI.Util.FromJson<DeathmatchTeamObject>(v)
                );
                entity.Property(x => x.MaxPlayTime).HasColumnName("MaxPlayTime").HasColumnType("int(32)");
                entity.Property(x => x.MaxDeaths).HasColumnName("MaxDeaths").HasColumnType("int(32)");
            });
        }
    }
}