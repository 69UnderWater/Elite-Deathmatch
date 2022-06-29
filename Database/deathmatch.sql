-- --------------------------------------------------------
-- Host:                         173.212.244.116
-- Server Version:               10.4.24-MariaDB - mariadb.org binary distribution
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Exportiere Datenbank Struktur für deathmatch
CREATE DATABASE IF NOT EXISTS `deathmatch` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `deathmatch`;

-- Exportiere Struktur von Tabelle deathmatch.player_accounts
CREATE TABLE IF NOT EXISTS `player_accounts` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) NOT NULL,
  `SocialclubName` varchar(255) NOT NULL,
  `SocialclubId` bigint(20) NOT NULL,
  `HardwareId` varchar(255) NOT NULL,
  `AdminLevel` int(16) NOT NULL,
  `Level` int(16) NOT NULL,
  `Prestige` int(16) NOT NULL,
  `CurrentXP` int(16) NOT NULL,
  `Kills` int(16) NOT NULL,
  `Deaths` int(16) NOT NULL,
  `Money` int(16) NOT NULL,
  `PlayedHours` int(32) NOT NULL,
  `SelectedLanguage` int(32) NOT NULL,
  `PrivateFrakId` int(32) NOT NULL,
  `PrivateFrakRank` int(32) NOT NULL,
  `IsPrivateFrak` tinyint(1) NOT NULL,
  `Weapons` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`Weapons`)),
  `IsBanned` tinyint(1) NOT NULL,
  `IsMuted` tinyint(1) NOT NULL,
  `IsTimeBanned` tinyint(1) NOT NULL,
  `BanDate` datetime NOT NULL,
  `TimeBanUntil` datetime NOT NULL,
  `Warns` int(16) NOT NULL,
  `BanReason` varchar(255) NOT NULL,
  `guildMemberId` varchar(255) NOT NULL,
  `discordSyncCode` varchar(255) NOT NULL,
  `DailyMission` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`DailyMission`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.player_accounts: ~0 rows (ungefähr)
/*!40000 ALTER TABLE `player_accounts` DISABLE KEYS */;
/*!40000 ALTER TABLE `player_accounts` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.player_inventory
CREATE TABLE IF NOT EXISTS `player_inventory` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `SocialClubId` bigint(20) NOT NULL,
  `Inventar` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`Inventar`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.player_inventory: ~9 rows (ungefähr)
/*!40000 ALTER TABLE `player_inventory` DISABLE KEYS */;
/*!40000 ALTER TABLE `player_inventory` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.player_vehicles
CREATE TABLE IF NOT EXISTS `player_vehicles` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `AccountId` int(32) NOT NULL,
  `Vehicles` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`Vehicles`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.player_vehicles: ~0 rows (ungefähr)
/*!40000 ALTER TABLE `player_vehicles` DISABLE KEYS */;
/*!40000 ALTER TABLE `player_vehicles` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.server_adminranks
CREATE TABLE IF NOT EXISTS `server_adminranks` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Permission` int(32) NOT NULL,
  `ChatColor` varchar(255) NOT NULL,
  `AdminClothing` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`AdminClothing`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.server_adminranks: ~7 rows (ungefähr)
/*!40000 ALTER TABLE `server_adminranks` DISABLE KEYS */;
INSERT INTO `server_adminranks` (`Id`, `Name`, `Permission`, `ChatColor`, `AdminClothing`) VALUES
	(1, 'Lead', 100, '~r~', '{"MaskDrawable":0,"MaskTexture":0,"TorsoDrawable":0,"TorsoTexture":0,"LegsDrawable":0,"LegsTexture":0,"ShoeDrawable":0,"ShoeTexture":0,"UndershirtDrawable":0,"UndershirtTexture":0,"TopDrawable":0,"TopTexture":0}'),
	(2, 'Developer', 90, '~b~', '{"MaskDrawable":0,"MaskTexture":0,"TorsoDrawable":0,"TorsoTexture":0,"LegsDrawable":0,"LegsTexture":0,"ShoeDrawable":0,"ShoeTexture":0,"UndershirtDrawable":0,"UndershirtTexture":0,"TopDrawable":0,"TopTexture":0}'),
	(3, 'Staff', 80, '~b~', '{"MaskDrawable":0,"MaskTexture":0,"TorsoDrawable":0,"TorsoTexture":0,"LegsDrawable":0,"LegsTexture":0,"ShoeDrawable":0,"ShoeTexture":0,"UndershirtDrawable":0,"UndershirtTexture":0,"TopDrawable":0,"TopTexture":0}'),
	(4, 'Streamer', 70, '~p~', '{"MaskDrawable":0,"MaskTexture":0,"TorsoDrawable":0,"TorsoTexture":0,"LegsDrawable":0,"LegsTexture":0,"ShoeDrawable":0,"ShoeTexture":0,"UndershirtDrawable":0,"UndershirtTexture":0,"TopDrawable":0,"TopTexture":0}'),
	(5, 'VIP', 20, '~y~', '{"MaskDrawable":0,"MaskTexture":0,"TorsoDrawable":0,"TorsoTexture":0,"LegsDrawable":0,"LegsTexture":0,"ShoeDrawable":0,"ShoeTexture":0,"UndershirtDrawable":0,"UndershirtTexture":0,"TopDrawable":0,"TopTexture":0}'),
	(6, 'Donator', 10, '~o~', '{"MaskDrawable":0,"MaskTexture":0,"TorsoDrawable":0,"TorsoTexture":0,"LegsDrawable":0,"LegsTexture":0,"ShoeDrawable":0,"ShoeTexture":0,"UndershirtDrawable":0,"UndershirtTexture":0,"TopDrawable":0,"TopTexture":0}'),
	(7, 'Player', 0, '~c~', '{"MaskDrawable":0,"MaskTexture":0,"TorsoDrawable":0,"TorsoTexture":0,"LegsDrawable":0,"LegsTexture":0,"ShoeDrawable":0,"ShoeTexture":0,"UndershirtDrawable":0,"UndershirtTexture":0,"TopDrawable":0,"TopTexture":0}');
/*!40000 ALTER TABLE `server_adminranks` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.server_dealer
CREATE TABLE IF NOT EXISTS `server_dealer` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `DealerId` int(32) NOT NULL,
  `LocationName` varchar(255) NOT NULL,
  `DealerPosition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`DealerPosition`)),
  `DealerRotation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`DealerRotation`)),
  `AbgabeDealerPosition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`AbgabeDealerPosition`)),
  `AbgabeDealerRotation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`AbgabeDealerRotation`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.server_dealer: ~5 rows (ungefähr)
/*!40000 ALTER TABLE `server_dealer` DISABLE KEYS */;
INSERT INTO `server_dealer` (`Id`, `DealerId`, `LocationName`, `DealerPosition`, `DealerRotation`, `AbgabeDealerPosition`, `AbgabeDealerRotation`) VALUES
	(1, 1, 'Mirrorpark', '{"x":1135.7942,"y":-434.48648,"z":66.50581}', '{"x":0.0,"y":0.0,"z":38.348476}', '{"x":-949.2695,"y":-1314.8617,"z":13.200036}', '{"x":0.0,"y":0.0,"z":-72.35269}'),
	(2, 2, 'Schrottplatz', '{"x":-473.8966,"y":-1665.5616,"z":18.778889}', '{"x":0.0,"y":0.0,"z":-19.583284}', '{"x":874.46515,"y":-1350.3044,"z":26.30955}', '{"x":0.0,"y":0.0,"z":90.075096}'),
	(3, 3, 'Vespucci', '{"x":-1163.4028,"y":-1240.6703,"z":6.776621}', '{"x":0.0,"y":0.0,"z":-68.23885}', '{"x":0.0,"y":0.0,"z":-68.23885}', '{"x":0.0,"y":0.0,"z":-68.23885}'),
	(4, 4, 'TeQui-La-La Bar', '{"x":-561.0965,"y":281.74786,"z":85.67635}', '{"x":0.0,"y":0.0,"z":80.74735}', '{"x":374.4039,"y":-1442.8224,"z":29.431568}', '{"x":0.0,"y":0.0,"z":-130.60532}'),
	(5, 5, 'Elektrizitätswerk (casino)', '{"x":661.2232,"y":101.22378,"z":80.75458}', '{"x":0.0,"y":0.0,"z":-1.7528313}', '{"x":-438.8985,"y":1593.5597,"z":357.64914}', '{"x":0.0,"y":0.0,"z":-121.80467}');
/*!40000 ALTER TABLE `server_dealer` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.server_deathmatch
CREATE TABLE IF NOT EXISTS `server_deathmatch` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `DeathmatchId` int(32) NOT NULL,
  `TeamOneSpawnPosition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`TeamOneSpawnPosition`)),
  `TeamTwoSpawnPosition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`TeamTwoSpawnPosition`)),
  `MaxPlayTime` int(32) NOT NULL,
  `MaxDeaths` int(32) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.server_deathmatch: ~0 rows (ungefähr)
/*!40000 ALTER TABLE `server_deathmatch` DISABLE KEYS */;
/*!40000 ALTER TABLE `server_deathmatch` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.server_factory
CREATE TABLE IF NOT EXISTS `server_factory` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `OwnerId` int(32) NOT NULL,
  `FactoryPosition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`FactoryPosition`)),
  `FactoryRotation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`FactoryRotation`)),
  `FactoryRobPosition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`FactoryRobPosition`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.server_factory: ~6 rows (ungefähr)
/*!40000 ALTER TABLE `server_factory` DISABLE KEYS */;
INSERT INTO `server_factory` (`Id`, `OwnerId`, `FactoryPosition`, `FactoryRotation`, `FactoryRobPosition`) VALUES
	(1, 1, '{"x":634.7189,"y":-2913.9053,"z":5.9639435}', '{"x":0.0,"y":0.0,"z":35.139915}', '{"x":634.7189,"y":-2913.9053,"z":5.9639435}'),
	(2, 2, '{"x":449.64246,"y":-1496.4886,"z":29.29201}', '{"x":0.0,"y":0.0,"z":173.6238}', '{"x":449.64246,"y":-1496.4886,"z":29.29201}'),
	(3, 3, '{"x":-736.8627,"y":-2103.2754,"z":9.02275}', '{"x":0.0,"y":0.0,"z":-128.45099}', '{"x":-736.8627,"y":-2103.2754,"z":9.02275}'),
	(4, 4, '{"x":-445.46588,"y":-972.02435,"z":25.90324}', '{"x":0.0,"y":0.0,"z":81.09113}', '{"x":-445.46588,"y":-972.02435,"z":25.90324}'),
	(5, 5, '{"x":-317.02432,"y":-1340.4222,"z":31.364296}', '{"x":0.0,"y":0.0,"z":90.72254}', '{"x":-317.02432,"y":-1340.4222,"z":31.364296}'),
	(6, 6, '{"x":1409.9896,"y":-2055.1372,"z":52.196877}', '{"x":0.0,"y":0.0,"z":102.00981}', '{"x":1409.9896,"y":-2055.1372,"z":52.196877}');
/*!40000 ALTER TABLE `server_factory` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.server_ffa_spawns
CREATE TABLE IF NOT EXISTS `server_ffa_spawns` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `ArenaId` int(32) NOT NULL,
  `ArenaName` varchar(255) NOT NULL,
  `maxPlayers` int(32) NOT NULL,
  `Spawns` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`Spawns`)),
  `Weapons` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`Weapons`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.server_ffa_spawns: ~7 rows (ungefähr)
/*!40000 ALTER TABLE `server_ffa_spawns` DISABLE KEYS */;
INSERT INTO `server_ffa_spawns` (`Id`, `ArenaId`, `ArenaName`, `maxPlayers`, `Spawns`, `Weapons`) VALUES
	(1, 1, 'Pool-Party [Headshot]', 6, '[{"SpawnId":1,"Position":{"x":514.3741,"y":227.69258,"z":104.74485},"Rotation":{"x":0.0,"y":0.0,"z":164.14708}},{"SpawnId":2,"Position":{"x":529.3454,"y":223.3649,"z":104.74486},"Rotation":{"x":0.0,"y":0.0,"z":120.98618}},{"SpawnId":3,"Position":{"x":521.78894,"y":203.90341,"z":104.74486},"Rotation":{"x":0.0,"y":0.0,"z":68.382935}},{"SpawnId":4,"Position":{"x":518.24774,"y":192.43744,"z":104.74486},"Rotation":{"x":0.0,"y":0.0,"z":23.709402}},{"SpawnId":5,"Position":{"x":484.0799,"y":203.93063,"z":104.744896},"Rotation":{"x":0.0,"y":0.0,"z":-51.806423}},{"SpawnId":6,"Position":{"x":488.44232,"y":215.7728,"z":104.7449},"Rotation":{"x":0.0,"y":0.0,"z":-83.215965}},{"SpawnId":7,"Position":{"x":495.2417,"y":235.7075,"z":104.74509},"Rotation":{"x":0.0,"y":0.0,"z":-140.78104}},{"SpawnId":8,"Position":{"x":515.9382,"y":191.12994,"z":108.309494},"Rotation":{"x":0.0,"y":0.0,"z":67.12815}}]', '{"WeaponHashOne":0,"WeaponHashTwo":0,"WeaponHashThree":3219281620}'),
	(2, 2, 'Humane Labs', 25, '[{"SpawnId":1,"Position":{"x":3637.6482,"y":3771.6294,"z":28.54283},"Rotation":{"x":0.0,"y":0.0,"z":81.93911}},{"SpawnId":2,"Position":{"x":3600.8394,"y":3755.115,"z":30.072607},"Rotation":{"x":0.0,"y":0.0,"z":53.457386}},{"SpawnId":3,"Position":{"x":3558.7317,"y":3814.904,"z":30.396585},"Rotation":{"x":0.0,"y":0.0,"z":142.12833}},{"SpawnId":4,"Position":{"x":3514.134,"y":3757.9465,"z":29.984425},"Rotation":{"x":0.0,"y":0.0,"z":-12.845304}},{"SpawnId":5,"Position":{"x":3466.8801,"y":3789.832,"z":30.460695},"Rotation":{"x":0.0,"y":0.0,"z":-122.515144}},{"SpawnId":6,"Position":{"x":3458.9229,"y":3742.754,"z":36.642696},"Rotation":{"x":0.0,"y":0.0,"z":-102.85123}},{"SpawnId":7,"Position":{"x":3510.6694,"y":3746.129,"z":36.242416},"Rotation":{"x":0.0,"y":0.0,"z":151.61649}},{"SpawnId":8,"Position":{"x":3503.2725,"y":3713.0793,"z":36.64267},"Rotation":{"x":0.0,"y":0.0,"z":-100.26719}},{"SpawnId":9,"Position":{"x":3595.9744,"y":3731.8975,"z":36.3106},"Rotation":{"x":0.0,"y":0.0,"z":171.4695}},{"SpawnId":10,"Position":{"x":3618.0042,"y":3708.3074,"z":35.79016},"Rotation":{"x":0.0,"y":0.0,"z":56.975346}},{"SpawnId":11,"Position":{"x":3508.4927,"y":3682.296,"z":33.83702},"Rotation":{"x":0.0,"y":0.0,"z":173.95613}},{"SpawnId":12,"Position":{"x":3491.211,"y":3698.6274,"z":34.00157},"Rotation":{"x":0.0,"y":0.0,"z":147.41672}},{"SpawnId":13,"Position":{"x":3583.3848,"y":3673.013,"z":33.888664},"Rotation":{"x":0.0,"y":0.0,"z":127.93614}},{"SpawnId":14,"Position":{"x":3550.2817,"y":3686.2283,"z":33.888706},"Rotation":{"x":0.0,"y":0.0,"z":-108.661026}},{"SpawnId":15,"Position":{"x":3525.7942,"y":3659.1353,"z":33.888744},"Rotation":{"x":0.0,"y":0.0,"z":-5.878129}},{"SpawnId":16,"Position":{"x":3592.4707,"y":3652.3547,"z":36.959053},"Rotation":{"x":0.0,"y":0.0,"z":73.339005}},{"SpawnId":17,"Position":{"x":3608.6614,"y":3630.5898,"z":41.34041},"Rotation":{"x":0.0,"y":0.0,"z":76.64497}},{"SpawnId":18,"Position":{"x":3591.7354,"y":3613.3176,"z":39.57127},"Rotation":{"x":0.0,"y":0.0,"z":86.91239}},{"SpawnId":19,"Position":{"x":3576.9124,"y":3619.1448,"z":42.84849},"Rotation":{"x":0.0,"y":0.0,"z":-11.189447}},{"SpawnId":20,"Position":{"x":3560.9246,"y":3647.998,"z":41.340336},"Rotation":{"x":0.0,"y":0.0,"z":-109.13658}},{"SpawnId":21,"Position":{"x":3513.3271,"y":3654.063,"z":41.34029},"Rotation":{"x":0.0,"y":0.0,"z":80.26033}},{"SpawnId":22,"Position":{"x":3466.4827,"y":3637.164,"z":41.187412},"Rotation":{"x":0.0,"y":0.0,"z":176.32817}},{"SpawnId":23,"Position":{"x":3441.1738,"y":3646.3086,"z":42.595642},"Rotation":{"x":0.0,"y":0.0,"z":-8.198604}},{"SpawnId":24,"Position":{"x":3413.2087,"y":3652.692,"z":41.340233},"Rotation":{"x":0.0,"y":0.0,"z":-29.306374}},{"SpawnId":25,"Position":{"x":3428.0276,"y":3680.6624,"z":41.340286},"Rotation":{"x":0.0,"y":0.0,"z":-94.11363}}]', '{"WeaponHashOne":2937143193,"WeaponHashTwo":1627465347,"WeaponHashThree":3523564046}'),
	(3, 3, 'Casino Dach', 10, '[{"SpawnId":1,"Position":{"x":902.1734,"y":37.913685,"z":111.32614},"Rotation":{"x":0.0,"y":0.0,"z":-96.995605}},{"SpawnId":2,"Position":{"x":904.7638,"y":2.8629816,"z":111.27586},"Rotation":{"x":0.0,"y":0.0,"z":-88.08038}},{"SpawnId":3,"Position":{"x":933.4497,"y":2.0323555,"z":111.29516},"Rotation":{"x":0.0,"y":0.0,"z":-161.92891}},{"SpawnId":4,"Position":{"x":955.22266,"y":3.5979116,"z":111.260864},"Rotation":{"x":0.0,"y":0.0,"z":-39.539402}},{"SpawnId":5,"Position":{"x":1004.13074,"y":67.79161,"z":111.32016},"Rotation":{"x":0.0,"y":0.0,"z":139.17186}},{"SpawnId":6,"Position":{"x":965.7351,"y":64.47914,"z":112.55309},"Rotation":{"x":0.0,"y":0.0,"z":114.99878}},{"SpawnId":7,"Position":{"x":951.0651,"y":76.78004,"z":113.547676},"Rotation":{"x":0.0,"y":0.0,"z":144.62642}},{"SpawnId":8,"Position":{"x":948.18506,"y":39.422466,"z":112.55285},"Rotation":{"x":0.0,"y":0.0,"z":54.437138}},{"SpawnId":9,"Position":{"x":926.3607,"y":46.63555,"z":111.66145},"Rotation":{"x":0.0,"y":0.0,"z":61.192303}},{"SpawnId":10,"Position":{"x":935.7595,"y":14.692245,"z":112.553154},"Rotation":{"x":0.0,"y":0.0,"z":34.693016}}]', '{"WeaponHashOne":2937143193,"WeaponHashTwo":1627465347,"WeaponHashThree":3523564046}'),
	(4, 4, 'Sandy Schrottplatz', 15, '[{"SpawnId":1,"Position":{"x":2432.4841,"y":3112.3962,"z":48.153046},"Rotation":{"x":0.0,"y":0.0,"z":94.82492}},{"SpawnId":2,"Position":{"x":2420.9148,"y":3096.243,"z":48.152897},"Rotation":{"x":0.0,"y":0.0,"z":77.17781}},{"SpawnId":3,"Position":{"x":2410.4768,"y":3032.3572,"z":48.152462},"Rotation":{"x":0.0,"y":0.0,"z":3.690269}},{"SpawnId":4,"Position":{"x":2382.3025,"y":3036.1936,"z":48.152714},"Rotation":{"x":0.0,"y":0.0,"z":0.95152724}},{"SpawnId":5,"Position":{"x":2352.1785,"y":3035.1257,"z":48.157158},"Rotation":{"x":0.0,"y":0.0,"z":8.961481}},{"SpawnId":6,"Position":{"x":2362.8103,"y":3074.12,"z":48.17885},"Rotation":{"x":0.0,"y":0.0,"z":-87.35272}},{"SpawnId":7,"Position":{"x":2356.1765,"y":3086.0305,"z":48.12707},"Rotation":{"x":0.0,"y":0.0,"z":-13.985481}},{"SpawnId":8,"Position":{"x":2332.6687,"y":3125.3801,"z":48.184837},"Rotation":{"x":0.0,"y":0.0,"z":-14.541253}},{"SpawnId":9,"Position":{"x":2344.9702,"y":3143.3079,"z":48.208755},"Rotation":{"x":0.0,"y":0.0,"z":-98.21027}},{"SpawnId":10,"Position":{"x":2358.3416,"y":3119.6719,"z":48.208748},"Rotation":{"x":0.0,"y":0.0,"z":78.59877}},{"SpawnId":11,"Position":{"x":2363.1912,"y":3146.981,"z":48.20896},"Rotation":{"x":0.0,"y":0.0,"z":-132.14656}},{"SpawnId":12,"Position":{"x":2403.887,"y":3141.5366,"z":48.151726},"Rotation":{"x":0.0,"y":0.0,"z":-120.35509}},{"SpawnId":13,"Position":{"x":2429.659,"y":3153.837,"z":48.195187},"Rotation":{"x":0.0,"y":0.0,"z":127.0939}},{"SpawnId":14,"Position":{"x":2428.4097,"y":3128.9355,"z":48.143078},"Rotation":{"x":0.0,"y":0.0,"z":62.899715}},{"SpawnId":15,"Position":{"x":2392.4163,"y":3164.6833,"z":46.927074},"Rotation":{"x":0.0,"y":0.0,"z":158.89307}}]', '{"WeaponHashOne":2937143193,"WeaponHashTwo":1627465347,"WeaponHashThree":3523564046}'),
	(5, 5, 'Container Schiff', 10, '[{"SpawnId":1,"Position":{"x":1240.2695,"y":-2883.122,"z":9.319258},"Rotation":{"x":0.0,"y":0.0,"z":-149.14049}},{"SpawnId":2,"Position":{"x":1252.6019,"y":-2923.3643,"z":9.319266},"Rotation":{"x":0.0,"y":0.0,"z":89.92614}},{"SpawnId":3,"Position":{"x":1228.0608,"y":-2947.756,"z":9.319261},"Rotation":{"x":0.0,"y":0.0,"z":-90.34356}},{"SpawnId":4,"Position":{"x":1252.1616,"y":-2969.6763,"z":9.319257},"Rotation":{"x":0.0,"y":0.0,"z":92.556015}},{"SpawnId":5,"Position":{"x":1251.7034,"y":-3010.8386,"z":9.319249},"Rotation":{"x":0.0,"y":0.0,"z":96.57243}},{"SpawnId":6,"Position":{"x":1240.8584,"y":-3043.846,"z":14.29769},"Rotation":{"x":0.0,"y":0.0,"z":-0.83609706}},{"SpawnId":7,"Position":{"x":1240.788,"y":-3017.1118,"z":13.739018},"Rotation":{"x":0.0,"y":0.0,"z":-2.1481764}},{"SpawnId":8,"Position":{"x":1238.6962,"y":-2996.1067,"z":12.159412},"Rotation":{"x":0.0,"y":0.0,"z":-25.255566}},{"SpawnId":9,"Position":{"x":1242.1006,"y":-2974.2195,"z":14.979977},"Rotation":{"x":0.0,"y":0.0,"z":-0.7386986}},{"SpawnId":10,"Position":{"x":1244.8113,"y":-2905.989,"z":25.330185},"Rotation":{"x":0.0,"y":0.0,"z":-1.7986755}},{"SpawnId":11,"Position":{"x":1240.9235,"y":-2915.8672,"z":29.685448},"Rotation":{"x":0.0,"y":0.0,"z":-170.99309}},{"SpawnId":12,"Position":{"x":1235.4368,"y":-2893.0544,"z":17.332453},"Rotation":{"x":0.0,"y":0.0,"z":4.2746873}}]', '{"WeaponHashOne":2937143193,"WeaponHashTwo":1627465347,"WeaponHashThree":3523564046}'),
	(6, 6, 'Fischer Dorf', 7, '[{"SpawnId":1,"Position":{"x":1288.777,"y":4333.177,"z":38.561283},"Rotation":{"x":0.0,"y":0.0,"z":-100.65108}},{"SpawnId":2,"Position":{"x":1305.9038,"y":4308.2197,"z":37.35765},"Rotation":{"x":0.0,"y":0.0,"z":-30.377811}},{"SpawnId":3,"Position":{"x":1351.3215,"y":4356.1426,"z":43.731903},"Rotation":{"x":0.0,"y":0.0,"z":-11.453011}},{"SpawnId":4,"Position":{"x":1420.1543,"y":4393.7124,"z":44.014965},"Rotation":{"x":0.0,"y":0.0,"z":132.03854}},{"SpawnId":5,"Position":{"x":1374.2499,"y":4306.9736,"z":37.585484},"Rotation":{"x":0.0,"y":0.0,"z":-19.445766}},{"SpawnId":6,"Position":{"x":1343.1454,"y":4312.15,"z":37.98171},"Rotation":{"x":0.0,"y":0.0,"z":-14.24332}},{"SpawnId":7,"Position":{"x":1326.5347,"y":4390.295,"z":44.345894},"Rotation":{"x":0.0,"y":0.0,"z":166.5707}}]', '{"WeaponHashOne":2937143193,"WeaponHashTwo":1627465347,"WeaponHashThree":3523564046}'),
	(7, 7, 'Weinberge', 8, '[{"SpawnId":1,"Position":{"x":-1897.9105,"y":1995.9432,"z":141.87735},"Rotation":{"x":0.0,"y":0.0,"z":-0.33762988}},{"SpawnId":2,"Position":{"x":-1925.3083,"y":2032.9258,"z":140.73804},"Rotation":{"x":0.0,"y":0.0,"z":-100.101036}},{"SpawnId":3,"Position":{"x":-1869.0967,"y":2051.8848,"z":140.98373},"Rotation":{"x":0.0,"y":0.0,"z":-92.06817}},{"SpawnId":4,"Position":{"x":-1860.0573,"y":2074.1013,"z":140.99521},"Rotation":{"x":0.0,"y":0.0,"z":69.28247}},{"SpawnId":5,"Position":{"x":-1873.062,"y":2093.797,"z":140.04},"Rotation":{"x":0.0,"y":0.0,"z":153.40411}},{"SpawnId":6,"Position":{"x":-1901.7042,"y":2092.2727,"z":140.38814},"Rotation":{"x":0.0,"y":0.0,"z":-111.021614}},{"SpawnId":7,"Position":{"x":-1922.0405,"y":2078.7488,"z":139.0923},"Rotation":{"x":0.0,"y":0.0,"z":175.19943}},{"SpawnId":8,"Position":{"x":-1898.3999,"y":2059.8845,"z":140.90024},"Rotation":{"x":0.0,"y":0.0,"z":118.61987}}]', '{"WeaponHashOne":2937143193,"WeaponHashTwo":1627465347,"WeaponHashThree":3523564046}');
/*!40000 ALTER TABLE `server_ffa_spawns` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.server_teams
CREATE TABLE IF NOT EXISTS `server_teams` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `TeamId` int(32) NOT NULL,
  `TeamName` varchar(255) NOT NULL,
  `ShortName` varchar(255) NOT NULL,
  `TeamSpawnPoint` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`TeamSpawnPoint`)),
  `TeamSpawnRotation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`TeamSpawnRotation`)),
  `BlipColor` int(32) NOT NULL,
  `PrimaryColor` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`PrimaryColor`)),
  `SecondaryColor` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`SecondaryColor`)),
  `IsPrivate` tinyint(1) NOT NULL,
  `TeamPedHash` varchar(255) NOT NULL,
  `TeamPedSpawnPoint` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`TeamPedSpawnPoint`)),
  `TeamPedSpawnRotation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`TeamPedSpawnRotation`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.server_teams: ~6 rows (ungefähr)
/*!40000 ALTER TABLE `server_teams` DISABLE KEYS */;
INSERT INTO `server_teams` (`Id`, `TeamId`, `TeamName`, `ShortName`, `TeamSpawnPoint`, `TeamSpawnRotation`, `BlipColor`, `PrimaryColor`, `SecondaryColor`, `IsPrivate`, `TeamPedHash`, `TeamPedSpawnPoint`, `TeamPedSpawnRotation`) VALUES
	(1, 1, 'VAGOS', 'lsv', '{"x":341.60983,"y":-2045.7433,"z":21.346794}', '{"x":0.0,"y":0.0,"z":47.263706}', 46, '{"r":255,"g":255,"b":0}', '{"r":255,"g":255,"b":0}', 0, 'u_m_y_mani', '{"x":337.96378,"y":-2035.7744,"z":21.401379}', '{"x":0.0,"y":0.0,"z":141.79028}'),
	(2, 2, 'CRIPS', 'lsc', '{"x":476.04358,"y":-1777.7373,"z":28.682812}', '{"x":0.0,"y":0.0,"z":-84.321785}', 3, '{"r":31,"g":81,"b":255}', '{"r":31,"g":81,"b":255}', 0, 'a_m_y_epsilon_02', '{"x":479.7014,"y":-1792.745,"z":28.533134}', '{"x":0.0,"y":0.0,"z":-128.05565}'),
	(3, 3, 'GROVE', 'gf', '{"x":88.779724,"y":-1953.5029,"z":20.742727}', '{"x":0.0,"y":0.0,"z":-42.971424}', 2, '{"r":50,"g":205,"b":50}', '{"r":50,"g":205,"b":50}', 0, 'g_m_y_famdnf_01', '{"x":102.894455,"y":-1959.372,"z":20.811855}', '{"x":0.0,"y":0.0,"z":-9.281503}'),
	(4, 4, 'BALLAS', 'fyb', '{"x":-197.9937,"y":-1672.3535,"z":33.965824}', '{"x":0.0,"y":0.0,"z":-93.145325}', 27, '{"r":148,"g":0,"b":211}', '{"r":148,"g":0,"b":211}', 0, 'g_m_y_ballasout_01', '{"x":-198.20285,"y":-1699.8331,"z":33.475254}', '{"x":0.0,"y":0.0,"z":44.09525}'),
	(5, 5, 'BLOODS', 'csb', '{"x":-14.495639,"y":-1446.6832,"z":30.646147}', '{"x":0.0,"y":0.0,"z":-177.63942}', 1, '{"r":255,"g":0,"b":0}', '{"r":255,"g":0,"b":0}', 0, 'ig_claypain', '{"x":-20.662457,"y":-1437.4407,"z":30.653227}', '{"x":0.0,"y":0.0,"z":95.59843}'),
	(6, 6, 'SIERRA FAMILY', 'srf', '{"x":844.00793,"y":-2118.291,"z":30.521059}', '{"x":0.0,"y":0.0,"z":94.13737}', 76, '{"r":73,"g":17,"b":29}', '{"r":73,"g":17,"b":29}', 1, 'a_c_hen', '{"x":843.84033,"y":-2124.3022,"z":29.831795}', '{"x":0.0,"y":0.0,"z":85.46281}');
/*!40000 ALTER TABLE `server_teams` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.server_teams_clothing
CREATE TABLE IF NOT EXISTS `server_teams_clothing` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `TeamId` int(32) NOT NULL,
  `Gender` int(32) NOT NULL,
  `TeamClothingId` int(32) NOT NULL,
  `MaskDrawable` int(32) NOT NULL,
  `MaskTexture` int(32) NOT NULL,
  `TorsoDrawable` int(32) NOT NULL,
  `TorsoTexture` int(32) NOT NULL,
  `LegsDrawable` int(32) NOT NULL,
  `LegsTexture` int(32) NOT NULL,
  `BagsNParachuteDrawable` int(32) NOT NULL,
  `BagsNParachuteTexture` int(32) NOT NULL,
  `ShoeDrawable` int(32) NOT NULL,
  `ShoeTexture` int(32) NOT NULL,
  `AccessiorDrawable` int(32) NOT NULL,
  `AccessiorTexture` int(32) NOT NULL,
  `UndershirtDrawable` int(32) NOT NULL,
  `UndershirtTexture` int(32) NOT NULL,
  `BodyArmorDrawable` int(32) NOT NULL,
  `BodyArmorTexture` int(32) NOT NULL,
  `TopDrawable` int(32) NOT NULL,
  `TopTexture` int(32) NOT NULL,
  `HatsDrawable` int(32) NOT NULL,
  `HatsTexture` int(32) NOT NULL,
  `GlassesDrawable` int(32) NOT NULL,
  `GlassesTexture` int(32) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.server_teams_clothing: ~37 rows (ungefähr)
/*!40000 ALTER TABLE `server_teams_clothing` DISABLE KEYS */;
INSERT INTO `server_teams_clothing` (`Id`, `TeamId`, `Gender`, `TeamClothingId`, `MaskDrawable`, `MaskTexture`, `TorsoDrawable`, `TorsoTexture`, `LegsDrawable`, `LegsTexture`, `BagsNParachuteDrawable`, `BagsNParachuteTexture`, `ShoeDrawable`, `ShoeTexture`, `AccessiorDrawable`, `AccessiorTexture`, `UndershirtDrawable`, `UndershirtTexture`, `BodyArmorDrawable`, `BodyArmorTexture`, `TopDrawable`, `TopTexture`, `HatsDrawable`, `HatsTexture`, `GlassesDrawable`, `GlassesTexture`) VALUES
	(1, 1, 0, 1, 51, 1, 1, 0, 1, 0, 1, 1, 8, 0, 1, 1, 15, 0, 1, 1, 14, 1, 1806, 0, 2, 0),
	(2, 1, 0, 2, 51, 8, 1, 0, 16, 2, 2, 2, 6, 0, 1, 1, 15, 0, 0, 0, 182, 0, 83, 0, 2, 0),
	(3, 1, 0, 3, 51, 0, 1, 0, 117, 11, 0, 0, 26, 7, 1, 1, 15, 0, 15, 2, 262, 13, 83, 0, 2, 0),
	(4, 1, 1, 4, 51, 8, 15, 0, 16, 0, 0, 0, 35, 0, 0, 0, 15, 0, 0, 0, 170, 0, 109, 4, 10, 0),
	(5, 1, 1, 5, 51, 8, 18, 0, 16, 0, 0, 0, 35, 0, 0, 0, 15, 0, 0, 0, 317, 13, 108, 1, 6, 0),
	(6, 1, 1, 6, 51, 8, 18, 0, 102, 5, 0, 0, 35, 0, 0, 0, 15, 0, 0, 0, 103, 3, 57, 0, 20, 0),
	(7, 2, 0, 1, 54, 0, 5, 0, 59, 5, 0, 0, 7, 0, 1, 1, 15, 0, 15, 2, 5, 2, 83, 0, 9, 0),
	(8, 2, 0, 2, 51, 9, 1, 0, 6, 1, 0, 0, 6, 0, 1, 1, 15, 0, 15, 2, 14, 0, 83, 0, 17, 0),
	(9, 2, 0, 3, 51, 1, 4, 0, 6, 0, 0, 0, 7, 0, 1, 1, 15, 0, 15, 2, 305, 6, 2125, 1, 9, 0),
	(10, 2, 1, 4, 51, 9, 3, 0, 66, 0, 0, 0, 35, 0, 0, 0, 14, 0, 0, 0, 136, 3, 142, 0, 20, 0),
	(11, 2, 1, 5, 51, 9, 3, 0, 54, 2, 0, 0, 35, 0, 0, 0, 15, 0, 0, 0, 316, 6, 142, 0, 20, 0),
	(12, 2, 1, 6, 51, 9, 0, 0, 134, 14, 0, 0, 35, 0, 0, 0, 15, 0, 0, 0, 0, 2, 142, 0, 20, 0),
	(13, 3, 0, 1, 51, 0, 11, 0, 6, 1, 0, 0, 7, 0, 0, 0, 15, 0, 15, 2, 82, 4, 20, 0, 7, 0),
	(14, 3, 0, 2, 51, 5, 11, 0, 16, 4, 0, 0, 6, 0, 0, 0, 15, 0, 15, 2, 171, 1, 83, 0, 7, 0),
	(15, 3, 0, 3, 111, 0, 8, 0, 6, 1, 0, 0, 42, 0, 0, 0, 15, 0, 15, 2, 128, 0, 120, 0, 7, 0),
	(16, 3, 1, 4, 51, 5, 1, 0, 102, 17, 0, 0, 11, 2, 0, 0, 15, 0, 17, 2, 140, 0, 3181, 0, 12, 0),
	(17, 3, 1, 5, 51, 5, 1, 0, 43, 0, 0, 0, 11, 2, 0, 0, 15, 0, 17, 2, 76, 4, 1946, 0, 12, 0),
	(18, 3, 1, 6, 111, 0, 4, 0, 66, 9, 0, 0, 1, 0, 14, 0, 14, 0, 17, 2, 74, 0, 151, 0, 12, 0),
	(19, 4, 0, 1, 111, 2, 4, 0, 6, 1, 0, 0, 42, 1, 0, 0, 15, 0, 15, 2, 305, 9, 83, 0, 0, 0),
	(20, 4, 0, 2, 51, 6, 4, 0, 5, 9, 0, 0, 8, 0, 0, 0, 15, 0, 15, 2, 84, 0, 2, 0, 5, 0),
	(21, 4, 0, 3, 51, 0, 4, 0, 16, 5, 0, 0, 6, 0, 0, 0, 15, 0, 15, 2, 84, 5, 3, 1, 2, 0),
	(22, 4, 1, 4, 51, 6, 9, 0, 134, 3, 0, 0, 35, 0, 0, 0, 2, 0, 0, 0, 347, 3, 57, 0, 12, 0),
	(23, 4, 1, 5, 51, 6, 1, 0, 12, 15, 0, 0, 35, 0, 0, 0, 14, 0, 0, 0, 350, 5, 57, 0, 12, 0),
	(24, 4, 1, 6, 51, 6, 4, 0, 134, 3, 0, 0, 35, 0, 0, 0, 14, 0, 0, 0, 74, 1, 57, 0, 12, 0),
	(25, 5, 0, 1, 111, 9, 4, 0, 15, 3, 0, 0, 7, 0, 0, 0, 0, 2, 0, 0, 7, 5, 2367, 0, 5, 0),
	(26, 5, 0, 2, 111, 9, 6, 0, 33, 0, 0, 0, 7, 0, 0, 0, 15, 0, 0, 0, 113, 0, 96, 0, 5, 0),
	(27, 5, 0, 3, 95, 0, 5, 0, 5, 5, 0, 0, 7, 0, 0, 0, 15, 0, 0, 0, 5, 2, 365, 0, 5, 0),
	(28, 5, 1, 28, 111, 9, 4, 0, 16, 7, 0, 0, 35, 0, 0, 0, 3, 0, 0, 0, 5, 0, 57, 0, 12, 0),
	(29, 5, 1, 29, 111, 9, 5, 0, 87, 3, 0, 0, 35, 0, 0, 0, 60, 1, 0, 0, 8, 2, 57, 0, 12, 0),
	(30, 5, 1, 30, 0, 0, 4, 0, 125, 1, 0, 0, 35, 0, 0, 0, 14, 0, 17, 2, 321, 1, 57, 0, 12, 0),
	(31, 4, 0, 3, 51, 0, 4, 0, 16, 5, 0, 0, 6, 0, 0, 0, 15, 0, 15, 2, 84, 5, 94, 0, 2, 0),
	(32, 6, 0, 1, 111, 5, 31, 0, 24, 4, 0, 0, 7, 0, 0, 0, 15, 0, 15, 2, 306, 1, 365, 0, 1296, 0),
	(33, 6, 0, 2, 111, 5, 4, 0, 24, 4, 0, 0, 34, 0, 0, 0, 15, 0, 15, 2, 200, 2, 15, 1, 16, 5),
	(34, 6, 0, 3, 111, 5, 0, 0, 6, 2, 0, 0, 34, 0, 0, 0, 15, 0, 0, 0, 235, 7, 109, 1, 16, 5),
	(35, 6, 1, 4, 111, 5, 0, 0, 54, 2, 0, 0, 35, 0, 0, 0, 15, 0, 0, 0, 281, 12, 57, 0, 0, 0),
	(36, 6, 1, 5, 118, 11, 3, 0, 87, 3, 0, 0, 35, 0, 0, 0, 14, 0, 0, 0, 103, 3, 57, 0, 0, 0),
	(37, 6, 1, 6, 111, 5, 0, 0, 15, 0, 0, 0, 35, 0, 0, 0, 14, 0, 0, 0, 161, 1, 75, 9, 0, 0);
/*!40000 ALTER TABLE `server_teams_clothing` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.server_team_garages
CREATE TABLE IF NOT EXISTS `server_team_garages` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `TeamId` int(32) NOT NULL,
  `GaragePosition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`GaragePosition`)),
  `GarageOutParkPoint` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `ParkOutRotation` float NOT NULL,
  `ParkoutPoints` longtext NOT NULL,
  `Vehicles` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`Vehicles`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.server_team_garages: ~6 rows (ungefähr)
/*!40000 ALTER TABLE `server_team_garages` DISABLE KEYS */;
INSERT INTO `server_team_garages` (`Id`, `TeamId`, `GaragePosition`, `GarageOutParkPoint`, `ParkOutRotation`, `ParkoutPoints`, `Vehicles`) VALUES
	(1, 1, '{"x":337.96378,"y":-2035.7744,"z":21.401379}', '{"x":320.13907,"y":-2027.4854,"z":20.717003}', 51.515, '[{"Position":{"x":320.13907,"y":-2027.4854,"z":20.717003},"Rotation":{"x":0.0,"y":0.0,"z":51.515}}]', '[{"Id":1,"Name":"Kanjo","Hash":"kanjo","Level":1},{"Id":2,"Name":"Oracle","Hash":"oracle","Level":2},{"Id":3,"Name":"Avarus","Hash":"avarus","Level":3},{"Id":4,"Name":"Sanchez","Hash":"sanchez2","Level":4},{"Id":5,"Name":"Slamvan","Hash":"slamvan3","Level":5},{"Id":6,"Name":"Rebla","Hash":"rebla","Level":6},{"Id":7,"Name":"Buffalo","Hash":"buffalo4","Level":7},{"Id":8,"Name":"Blazer","Hash":"blazer4","Level":8},{"Id":9,"Name":"BF400","Hash":"bf400","Level":9},{"Id":10,"Name":"Schafter","Hash":"Schafter4","Level":10},{"Id":11,"Name":"Hellion","Hash":"hellion","Level":11},{"Id":12,"Name":"Caracara","Hash":"caracara2","Level":12},{"Id":13,"Name":"Drafter","Hash":"drafter","Level":13},{"Id":14,"Name":"Speedo","Hash":"speedo2","Level":14},{"Id":15,"Name":"SultanRS","Hash":"sultanrs","Level":15},{"Id":16,"Name":"Bati","Hash":"bati","Level":16},{"Id":17,"Name":"Paragon","Hash":"paragon","Level":17},{"Id":18,"Name":"Iwagen","Hash":"iwagen","Level":18},{"Id":19,"Name":"Kuruma","Hash":"kuruma","Level":19},{"Id":20,"Name":"Shinobi","Hash":"shinobi","Level":20},{"Id":21,"Name":"Tyrus","Hash":"tyrus","Level":21},{"Id":22,"Name":"Jubilee","Hash":"Jubilee","Level":22},{"Id":23,"Name":"Ignus","Hash":"ignus","Level":23},{"Id":24,"Name":"Vacca","Hash":"vacca","Level":24},{"Id":25,"Name":"Penetrator","Hash":"penetrator","Level":25},{"Id":26,"Name":"Reaper","Hash":"reaper","Level":26},{"Id":27,"Name":"Sheava","Hash":"sheava","Level":27},{"Id":28,"Name":"Neon","Hash":"neon","Level":28},{"Id":29,"Name":"Bullet","Hash":"bullet","Level":29},{"Id":30,"Name":"SultanClassic","Hash":"sultan2","Level":30},{"Id":31,"Name":"Growler","Hash":"growler","Level":31},{"Id":32,"Name":"Raiden","Hash":"raiden","Level":32},{"Id":33,"Name":"T20","Hash":"t20","Level":33},{"Id":34,"Name":"Osiris","Hash":"osiris","Level":34},{"Id":35,"Name":"XA21","Hash":"xa21","Level":35},{"Id":37,"Name":"X80","Hash":"prototipo","Level":37},{"Id":38,"Name":"Zentorno","Hash":"zentorno","Level":38},{"Id":39,"Name":"Nero","Hash":"nero2","Level":39},{"Id":40,"Name":"Patriot","Hash":"patriot3","Level":40}]'),
	(2, 2, '{"x":479.7014,"y":-1792.745,"z":28.533134}', '{"x":488.11038,"y":-1802.4801,"z":28.542372}', 136.525, '[{"Position":{"x":488.11038,"y":-1802.4801,"z":28.542372},"Rotation":{"x":0.0,"y":0.0,"z":136.525}}]', '[{"Id":1,"Name":"Kanjo","Hash":"kanjo","Level":1},{"Id":2,"Name":"Oracle","Hash":"oracle","Level":2},{"Id":3,"Name":"Avarus","Hash":"avarus","Level":3},{"Id":4,"Name":"Sanchez","Hash":"sanchez2","Level":4},{"Id":5,"Name":"Slamvan","Hash":"slamvan3","Level":5},{"Id":6,"Name":"Rebla","Hash":"rebla","Level":6},{"Id":7,"Name":"Buffalo","Hash":"buffalo4","Level":7},{"Id":8,"Name":"Blazer","Hash":"blazer4","Level":8},{"Id":9,"Name":"BF400","Hash":"bf400","Level":9},{"Id":10,"Name":"Schafter","Hash":"Schafter4","Level":10},{"Id":11,"Name":"Hellion","Hash":"hellion","Level":11},{"Id":12,"Name":"Caracara","Hash":"caracara2","Level":12},{"Id":13,"Name":"Drafter","Hash":"drafter","Level":13},{"Id":14,"Name":"Speedo","Hash":"speedo2","Level":14},{"Id":15,"Name":"SultanRS","Hash":"sultanrs","Level":15},{"Id":16,"Name":"Bati","Hash":"bati","Level":16},{"Id":17,"Name":"Paragon","Hash":"paragon","Level":17},{"Id":18,"Name":"Iwagen","Hash":"iwagen","Level":18},{"Id":19,"Name":"Kuruma","Hash":"kuruma","Level":19},{"Id":20,"Name":"Shinobi","Hash":"shinobi","Level":20},{"Id":21,"Name":"Tyrus","Hash":"tyrus","Level":21},{"Id":22,"Name":"Jubilee","Hash":"Jubilee","Level":22},{"Id":23,"Name":"Ignus","Hash":"ignus","Level":23},{"Id":24,"Name":"Vacca","Hash":"vacca","Level":24},{"Id":25,"Name":"Penetrator","Hash":"penetrator","Level":25},{"Id":26,"Name":"Reaper","Hash":"reaper","Level":26},{"Id":27,"Name":"Sheava","Hash":"sheava","Level":27},{"Id":28,"Name":"Neon","Hash":"neon","Level":28},{"Id":29,"Name":"Bullet","Hash":"bullet","Level":29},{"Id":30,"Name":"SultanClassic","Hash":"sultan2","Level":30},{"Id":31,"Name":"Growler","Hash":"growler","Level":31},{"Id":32,"Name":"Raiden","Hash":"raiden","Level":32},{"Id":33,"Name":"T20","Hash":"t20","Level":33},{"Id":34,"Name":"Osiris","Hash":"osiris","Level":34},{"Id":35,"Name":"XA21","Hash":"xa21","Level":35},{"Id":37,"Name":"X80","Hash":"prototipo","Level":37},{"Id":38,"Name":"Zentorno","Hash":"zentorno","Level":38},{"Id":39,"Name":"Nero","Hash":"nero2","Level":39},{"Id":40,"Name":"Patriot","Hash":"patriot3","Level":40}]'),
	(3, 3, '{"x":102.894455,"y":-1959.372,"z":20.811855}', '{"x":104.54284,"y":-1938.952,"z":20.803717}', 49.8363, '[{"Position":{"x":104.54284,"y":-1938.952,"z":20.803717},"Rotation":{"x":0.0,"y":0.0,"z":49.8363}}]', '[{"Id":1,"Name":"Kanjo","Hash":"kanjo","Level":1},{"Id":2,"Name":"Oracle","Hash":"oracle","Level":2},{"Id":3,"Name":"Avarus","Hash":"avarus","Level":3},{"Id":4,"Name":"Sanchez","Hash":"sanchez2","Level":4},{"Id":5,"Name":"Slamvan","Hash":"slamvan3","Level":5},{"Id":6,"Name":"Rebla","Hash":"rebla","Level":6},{"Id":7,"Name":"Buffalo","Hash":"buffalo4","Level":7},{"Id":8,"Name":"Blazer","Hash":"blazer4","Level":8},{"Id":9,"Name":"BF400","Hash":"bf400","Level":9},{"Id":10,"Name":"Schafter","Hash":"Schafter4","Level":10},{"Id":11,"Name":"Hellion","Hash":"hellion","Level":11},{"Id":12,"Name":"Caracara","Hash":"caracara2","Level":12},{"Id":13,"Name":"Drafter","Hash":"drafter","Level":13},{"Id":14,"Name":"Speedo","Hash":"speedo2","Level":14},{"Id":15,"Name":"SultanRS","Hash":"sultanrs","Level":15},{"Id":16,"Name":"Bati","Hash":"bati","Level":16},{"Id":17,"Name":"Paragon","Hash":"paragon","Level":17},{"Id":18,"Name":"Iwagen","Hash":"iwagen","Level":18},{"Id":19,"Name":"Kuruma","Hash":"kuruma","Level":19},{"Id":20,"Name":"Shinobi","Hash":"shinobi","Level":20},{"Id":21,"Name":"Tyrus","Hash":"tyrus","Level":21},{"Id":22,"Name":"Jubilee","Hash":"Jubilee","Level":22},{"Id":23,"Name":"Ignus","Hash":"ignus","Level":23},{"Id":24,"Name":"Vacca","Hash":"vacca","Level":24},{"Id":25,"Name":"Penetrator","Hash":"penetrator","Level":25},{"Id":26,"Name":"Reaper","Hash":"reaper","Level":26},{"Id":27,"Name":"Sheava","Hash":"sheava","Level":27},{"Id":28,"Name":"Neon","Hash":"neon","Level":28},{"Id":29,"Name":"Bullet","Hash":"bullet","Level":29},{"Id":30,"Name":"SultanClassic","Hash":"sultan2","Level":30},{"Id":31,"Name":"Growler","Hash":"growler","Level":31},{"Id":32,"Name":"Raiden","Hash":"raiden","Level":32},{"Id":33,"Name":"T20","Hash":"t20","Level":33},{"Id":34,"Name":"Osiris","Hash":"osiris","Level":34},{"Id":35,"Name":"XA21","Hash":"xa21","Level":35},{"Id":37,"Name":"X80","Hash":"prototipo","Level":37},{"Id":38,"Name":"Zentorno","Hash":"zentorno","Level":38},{"Id":39,"Name":"Nero","Hash":"nero2","Level":39},{"Id":40,"Name":"Patriot","Hash":"patriot3","Level":40}]'),
	(4, 4, '{"x":-198.20285,"y":-1699.8331,"z":33.475254}', '{"x":-194.91827,"y":-1693.3992,"z":33.371445}', -53.8404, '[{"Position":{"x":-194.91827,"y":-1693.3992,"z":33.371445},"Rotation":{"x":0.0,"y":0.0,"z":-53.8404}}]', '[{"Id":1,"Name":"Kanjo","Hash":"kanjo","Level":1},{"Id":2,"Name":"Oracle","Hash":"oracle","Level":2},{"Id":3,"Name":"Avarus","Hash":"avarus","Level":3},{"Id":4,"Name":"Sanchez","Hash":"sanchez2","Level":4},{"Id":5,"Name":"Slamvan","Hash":"slamvan3","Level":5},{"Id":6,"Name":"Rebla","Hash":"rebla","Level":6},{"Id":7,"Name":"Buffalo","Hash":"buffalo4","Level":7},{"Id":8,"Name":"Blazer","Hash":"blazer4","Level":8},{"Id":9,"Name":"BF400","Hash":"bf400","Level":9},{"Id":10,"Name":"Schafter","Hash":"Schafter4","Level":10},{"Id":11,"Name":"Hellion","Hash":"hellion","Level":11},{"Id":12,"Name":"Caracara","Hash":"caracara2","Level":12},{"Id":13,"Name":"Drafter","Hash":"drafter","Level":13},{"Id":14,"Name":"Speedo","Hash":"speedo2","Level":14},{"Id":15,"Name":"SultanRS","Hash":"sultanrs","Level":15},{"Id":16,"Name":"Bati","Hash":"bati","Level":16},{"Id":17,"Name":"Paragon","Hash":"paragon","Level":17},{"Id":18,"Name":"Iwagen","Hash":"iwagen","Level":18},{"Id":19,"Name":"Kuruma","Hash":"kuruma","Level":19},{"Id":20,"Name":"Shinobi","Hash":"shinobi","Level":20},{"Id":21,"Name":"Tyrus","Hash":"tyrus","Level":21},{"Id":22,"Name":"Jubilee","Hash":"Jubilee","Level":22},{"Id":23,"Name":"Ignus","Hash":"ignus","Level":23},{"Id":24,"Name":"Vacca","Hash":"vacca","Level":24},{"Id":25,"Name":"Penetrator","Hash":"penetrator","Level":25},{"Id":26,"Name":"Reaper","Hash":"reaper","Level":26},{"Id":27,"Name":"Sheava","Hash":"sheava","Level":27},{"Id":28,"Name":"Neon","Hash":"neon","Level":28},{"Id":29,"Name":"Bullet","Hash":"bullet","Level":29},{"Id":30,"Name":"SultanClassic","Hash":"sultan2","Level":30},{"Id":31,"Name":"Growler","Hash":"growler","Level":31},{"Id":32,"Name":"Raiden","Hash":"raiden","Level":32},{"Id":33,"Name":"T20","Hash":"t20","Level":33},{"Id":34,"Name":"Osiris","Hash":"osiris","Level":34},{"Id":35,"Name":"XA21","Hash":"xa21","Level":35},{"Id":37,"Name":"X80","Hash":"prototipo","Level":37},{"Id":38,"Name":"Zentorno","Hash":"zentorno","Level":38},{"Id":39,"Name":"Nero","Hash":"nero2","Level":39},{"Id":40,"Name":"Patriot","Hash":"patriot3","Level":40}]'),
	(5, 5, '{"x":-20.662457,"y":-1437.4407,"z":30.653227}', '{"x":-25.12488,"y":-1462.1171,"z":30.85625}', -93.3459, '[{"Position":{"x":-25.12488,"y":-1462.1171,"z":30.85625},"Rotation":{"x":0.0,"y":0.0,"z":-93.34586}}]', '[{"Id":1,"Name":"Kanjo","Hash":"kanjo","Level":1},{"Id":2,"Name":"Oracle","Hash":"oracle","Level":2},{"Id":3,"Name":"Avarus","Hash":"avarus","Level":3},{"Id":4,"Name":"Sanchez","Hash":"sanchez2","Level":4},{"Id":5,"Name":"Slamvan","Hash":"slamvan3","Level":5},{"Id":6,"Name":"Rebla","Hash":"rebla","Level":6},{"Id":7,"Name":"Buffalo","Hash":"buffalo4","Level":7},{"Id":8,"Name":"Blazer","Hash":"blazer4","Level":8},{"Id":9,"Name":"BF400","Hash":"bf400","Level":9},{"Id":10,"Name":"Schafter","Hash":"Schafter4","Level":10},{"Id":11,"Name":"Hellion","Hash":"hellion","Level":11},{"Id":12,"Name":"Caracara","Hash":"caracara2","Level":12},{"Id":13,"Name":"Drafter","Hash":"drafter","Level":13},{"Id":14,"Name":"Speedo","Hash":"speedo2","Level":14},{"Id":15,"Name":"SultanRS","Hash":"sultanrs","Level":15},{"Id":16,"Name":"Bati","Hash":"bati","Level":16},{"Id":17,"Name":"Paragon","Hash":"paragon","Level":17},{"Id":18,"Name":"Iwagen","Hash":"iwagen","Level":18},{"Id":19,"Name":"Kuruma","Hash":"kuruma","Level":19},{"Id":20,"Name":"Shinobi","Hash":"shinobi","Level":20},{"Id":21,"Name":"Tyrus","Hash":"tyrus","Level":21},{"Id":22,"Name":"Jubilee","Hash":"Jubilee","Level":22},{"Id":23,"Name":"Ignus","Hash":"ignus","Level":23},{"Id":24,"Name":"Vacca","Hash":"vacca","Level":24},{"Id":25,"Name":"Penetrator","Hash":"penetrator","Level":25},{"Id":26,"Name":"Reaper","Hash":"reaper","Level":26},{"Id":27,"Name":"Sheava","Hash":"sheava","Level":27},{"Id":28,"Name":"Neon","Hash":"neon","Level":28},{"Id":29,"Name":"Bullet","Hash":"bullet","Level":29},{"Id":30,"Name":"SultanClassic","Hash":"sultan2","Level":30},{"Id":31,"Name":"Growler","Hash":"growler","Level":31},{"Id":32,"Name":"Raiden","Hash":"raiden","Level":32},{"Id":33,"Name":"T20","Hash":"t20","Level":33},{"Id":34,"Name":"Osiris","Hash":"osiris","Level":34},{"Id":35,"Name":"XA21","Hash":"xa21","Level":35},{"Id":37,"Name":"X80","Hash":"prototipo","Level":37},{"Id":38,"Name":"Zentorno","Hash":"zentorno","Level":38},{"Id":39,"Name":"Nero","Hash":"nero2","Level":39},{"Id":40,"Name":"Patriot","Hash":"patriot3","Level":40}]'),
	(6, 6, '{"x":843.84033,"y":-2124.3022,"z":29.831795}', '{"x":819.8869,"y":-2118.0225,"z":29.342323}', 179.107, '[{"Position":{"x":819.8869,"y":-2118.0225,"z":29.342323},"Rotation":{"x":0.0,"y":0.0,"z":179.107}}]', '[{"Id":1,"Name":"Kanjo","Hash":"kanjo","Level":1},{"Id":2,"Name":"Oracle","Hash":"oracle","Level":2},{"Id":3,"Name":"Avarus","Hash":"avarus","Level":3},{"Id":4,"Name":"Sanchez","Hash":"sanchez2","Level":4},{"Id":5,"Name":"Slamvan","Hash":"slamvan3","Level":5},{"Id":6,"Name":"Rebla","Hash":"rebla","Level":6},{"Id":7,"Name":"Buffalo","Hash":"buffalo4","Level":7},{"Id":8,"Name":"Blazer","Hash":"blazer4","Level":8},{"Id":9,"Name":"BF400","Hash":"bf400","Level":9},{"Id":10,"Name":"Schafter","Hash":"Schafter4","Level":10},{"Id":11,"Name":"Hellion","Hash":"hellion","Level":11},{"Id":12,"Name":"Caracara","Hash":"caracara2","Level":12},{"Id":13,"Name":"Drafter","Hash":"drafter","Level":13},{"Id":14,"Name":"Speedo","Hash":"speedo2","Level":14},{"Id":15,"Name":"SultanRS","Hash":"sultanrs","Level":15},{"Id":16,"Name":"Bati","Hash":"bati","Level":16},{"Id":17,"Name":"Paragon","Hash":"paragon","Level":17},{"Id":18,"Name":"Iwagen","Hash":"iwagen","Level":18},{"Id":19,"Name":"Kuruma","Hash":"kuruma","Level":19},{"Id":20,"Name":"Shinobi","Hash":"shinobi","Level":20},{"Id":21,"Name":"Tyrus","Hash":"tyrus","Level":21},{"Id":22,"Name":"Jubilee","Hash":"Jubilee","Level":22},{"Id":23,"Name":"Ignus","Hash":"ignus","Level":23},{"Id":24,"Name":"Vacca","Hash":"vacca","Level":24},{"Id":25,"Name":"Penetrator","Hash":"penetrator","Level":25},{"Id":26,"Name":"Reaper","Hash":"reaper","Level":26},{"Id":27,"Name":"Sheava","Hash":"sheava","Level":27},{"Id":28,"Name":"Neon","Hash":"neon","Level":28},{"Id":29,"Name":"Bullet","Hash":"bullet","Level":29},{"Id":30,"Name":"SultanClassic","Hash":"sultan2","Level":30},{"Id":31,"Name":"Growler","Hash":"growler","Level":31},{"Id":32,"Name":"Raiden","Hash":"raiden","Level":32},{"Id":33,"Name":"T20","Hash":"t20","Level":33},{"Id":34,"Name":"Osiris","Hash":"osiris","Level":34},{"Id":35,"Name":"XA21","Hash":"xa21","Level":35},{"Id":37,"Name":"X80","Hash":"prototipo","Level":37},{"Id":38,"Name":"Zentorno","Hash":"zentorno","Level":38},{"Id":39,"Name":"Nero","Hash":"nero2","Level":39},{"Id":40,"Name":"Patriot","Hash":"patriot3","Level":40}]');
/*!40000 ALTER TABLE `server_team_garages` ENABLE KEYS */;

-- Exportiere Struktur von Tabelle deathmatch.server_weapontruck
CREATE TABLE IF NOT EXISTS `server_weapontruck` (
  `Id` int(32) NOT NULL AUTO_INCREMENT,
  `SpawnPosition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`SpawnPosition`)),
  `ReturnPosition` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`ReturnPosition`)),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

-- Exportiere Daten aus Tabelle deathmatch.server_weapontruck: ~5 rows (ungefähr)
/*!40000 ALTER TABLE `server_weapontruck` DISABLE KEYS */;
INSERT INTO `server_weapontruck` (`Id`, `SpawnPosition`, `ReturnPosition`) VALUES
	(1, '{"Position":{"x":451.17566,"y":-1020.25555,"z":28.41457},"Rotation":91.65648}', '{"x":-351.60544,"y":-79.83147,"z":45.664154}'),
	(2, '{"Position":{"x":855.3027,"y":-1279.939,"z":26.51525},"Rotation":20.191444}', '{"x":1001.0165,"y":-55.628246,"z":74.95917}'),
	(3, '{"Position":{"x":537.1836,"y":-39.43465,"z":70.763916},"Rotation":-144.9579}', '{"x":-1316.679,"y":-1260.2374,"z":4.5797954}'),
	(4, '{"Position":{"x":-1045.6445,"y":-850.2134,"z":4.8681593},"Rotation":132.67451}', '{"x":-447.3665,"y":291.1052,"z":83.23401}'),
	(5, '{"Position":{"x":79.76157,"y":169.8347,"z":104.54968},"Rotation":-109.59494}', '{"x":1130.2502,"y":-1299.567,"z":34.735928}');
/*!40000 ALTER TABLE `server_weapontruck` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
