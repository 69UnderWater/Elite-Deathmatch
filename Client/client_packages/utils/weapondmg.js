let isrespawning = false,
hotkeyinaction = false,
lastInteraction = 0;

function checkCanInteract(){
    return lastInteraction + 200 < Date.now()
}

const weapondamages = {
	//pistolen
	453432689: {
		"name": "weapon_pistol",
		"wdmg": 9,
		"headdmg": 12
	},
	3219281620: {
		"name": "weapon_pistol_mk2",
		"wdmg": 10,
		"headdmg": 300
	},
	3523564046: {
		"name": "weapon_heavypistol",
		"wdmg": 10,
		"headdmg": 0
	},
	2578377531: {
		"name": "weapon_pistol50",
		"wdmg": 13,
		"headdmg": 15
	},
	137902532: {
		"name": "weapon_vintagepistol",
		"wdmg": 12,
		"headdmg": 14
	},
	727643628: {
		"name": "weapon_ceramicpistol",
		"wdmg": 12,
		"headdmg": 14
	},
	3249783761: {
		"name": "weapon_revolver",
		"wdmg": 60,
		"headdmg": 75
	},
	2441047180: {
		"name": "weapon_navyrevolver",
		"wdmg": 10,
		"headdmg": 300
	},
	//Sturmgewehr
	3220176749: {
		"name": "weapon_assaultrifle",
		"wdmg": 12,
		"headdmg": 14
	},
	2210333304: {
		"name": "weapon_carbinerifle",
		"wdmg": 12,
		"headdmg": 0
	},
	2937143193: {
		"name": "weapon_advancedrifle",
		"wdmg": 13,
		"headdmg": 15
	},
	2132975508: {
		"name": "weapon_bullpuprifle",
		"wdmg": 12,
		"headdmg": 0
	},
	1649403952: {
		"name": "weapon_compactrifle",
		"wdmg": 12,
		"headdmg": 0
	},
	3231910285: {
		"name": "weapon_specialcarbine",
		"wdmg": 13,
		"headdmg": 15
	},

	//SMG
	736523883: {
		"name": "weapon_smg",
		"wdmg": 10,
		"headdmg": 0
	},
	171789620: {
		"name": "weapon_combatpdw",
		"wdmg": 10,
		"headdmg": 0
	},

	//LMG & MG
	1627465347: {
		"name": "weapon_gusenberg",
		"wdmg": 11,
		"headdmg": 0
	},

	//Sniper
	3342088282: {
		"name": "weapon_marksmanrifle",
		"wdmg": 35,
		"headdmg": 45
	},

	//Pumpguns ( wdmg / 8)
	487013001: {
		"name": "weapon_pumpshotgun",
		"wdmg": 3,
		"headdmg": 0
	},
	2017895192: {
		"name": "weapon_sawnoffshotgun",
		"wdmg": 4,
		"headdmg": 0
	},
	2640438543: {
		"name": "weapon_bullpupshotgun",
		"wdmg": 3,
		"headdmg": 0
	}
}

class AntiCheat {
    constructor() {
        this.AllowedHealth = null
        this.BulletSeccondCount = 0
    }

    callHealKeyDetection(health) {
        if(this.AllowedHealth == null || (mp.players.local.getHealth() + mp.players.local.getArmour()) == 0) return;
        if((health - this.AllowedHealth) === 0){
            mp.events.callRemote("server:anticheat:callGodMode")
        } else {
            mp.events.callRemote("server:anticheat:callHealKey", this.AllowedHealth, health)
        }
    }
}

const anticheat = new AntiCheat();

mp.events.add('playerWeaponShot', (targetPosition, targetEntity) => {
    anticheat.BulletSeccondCount++
});

setInterval(() => {
    if(anticheat.BulletSeccondCount >= 30){
        mp.events.callRemote("server:anticheat:callRapidFire", anticheat.BulletSeccondCount)
    }
    anticheat.BulletSeccondCount = 0
}, 1000);

mp.events.add('incomingDamage', (sourceEntity, sourcePlayer, targetEntity, weapon, boneIndex, damage) => {
	if (targetEntity.type === 'player' && sourcePlayer) {
		if (targetEntity.getVariable("AdminTools:SetInvincible") === true) return true

		let weapondamagedefault = 0;

		let targetentityteam = targetEntity.getVariable("PLAYER_TEAM_ID")
		let sourceplayerteam = sourceEntity.getVariable("PLAYER_TEAM_ID")

		if (weapon in weapondamages) {
			weapondamagedefault = weapondamages[weapon].wdmg;
		}

		if (boneIndex === 20) {
			if (weapon in weapondamages) {
				if (weapondamages[weapon].headdmg > 0) {
					weapondamagedefault = weapondamages[weapon].headdmg;
				}
				else {
					weapondamagedefault = Math.floor(weapondamagedefault * 1.2)
				}
			}
		}

		if(sourceplayerteam == targetentityteam && targetEntity.getVariable("IS_IN_FFA") == false && targetEntity.getVariable("IS_IN_OVO") == false){

		} else{
			targetEntity.applyDamageTo(weapondamagedefault, true);
			if(weapondamagedefault !== 0){
				if((mp.players.local.getHealth() + mp.players.local.getArmour()) >= anticheat.AllowedHealth && anticheat.AllowedHealth != null){
					anticheat.callHealKeyDetection((mp.players.local.getHealth() + mp.players.local.getArmour()))
				}
				anticheat.AllowedHealth = (mp.players.local.getHealth() + mp.players.local.getArmour())
			}
		}

		let targethealth = targetEntity.getHealth() + targetEntity.getArmour()


		if(targethealth <= 0){
			if(isrespawning == true) return
			isrespawning = true
			mp.events.callRemote("PlayerDeathEvent", "" + sourcePlayer.name);
		}
		
		return true;
	}
});

mp.events.add('outgoingDamage', (sourceEntity, targetEntity, sourcePlayer, weapon, boneIndex, damage) => {
	if (targetEntity.type === 'player' && mp.storage.data.floathitmarker == 1) {

		let weapondamagedefault = 0;

		let targetentityteam = targetEntity.getVariable("PLAYER_TEAM_ID")
		let sourceplayerteam = sourceEntity.getVariable("PLAYER_TEAM_ID")

		if (weapon in weapondamages) {
			weapondamagedefault = weapondamages[weapon].wdmg;
		}

		if (boneIndex === 20) {
			if (weapon in weapondamages) {
				if (weapondamages[weapon].headdmg > 0) {
					weapondamagedefault = weapondamages[weapon].headdmg;
				}
				else {
					weapondamagedefault = Math.floor(weapondamagedefault * 1.2)
				}
			}
		}

		if(targetentityteam == sourceplayerteam && targetEntity.getVariable("IS_IN_FFA") == false && targetEntity.getVariable("IS_IN_OVO") == false){
			sourceEntity.applyDamageTo(weapondamagedefault, true)
		}

		HitmarkerObjects.push({
			Position: targetEntity.position,
			Damage: (targetEntity.getHealth() + targetEntity.getArmour()),
			Count: 0
		})

	}
});

const HitmarkerObjects = []

mp.events.add("render", () => {
	HitmarkerObjects.forEach(element => {
		element.Count += 2;
		element.Position.z += 0.02;
		mp.game.graphics.drawText(element.Damage, [element.Position.x, element.Position.y, element.Position.z + 1.2], { font: 2, center: true, color: [255, 255, 255, 155 - element.Count], scale: [0.4, 0.4], outline: true })

		if (element.Count > 155) {
			var find = HitmarkerObjects.findIndex(elemen => elemen == element);

			HitmarkerObjects.splice(find, 1);
		}
	});
})

mp.events.add("playerSpawn", (player) => {
	anticheat.AllowedHealth = (mp.players.local.getHealth() + mp.players.local.getArmour())
	setTimeout(() => {
		mp.players.local.setHealth(200)
		mp.players.local.setArmour(100)
		isrespawning = false
		anticheat.AllowedHealth = (mp.players.local.getHealth() + mp.players.local.getArmour())
	}, 200);
})

mp.events.add("render", () => {
	let health = mp.players.local.getHealth()

	if(health <= 0){
		setTimeout(() => {
			if(isrespawning == true) return
			isrespawning = true;
			mp.events.callRemote("PlayerDeathEvent", "elite-" + (Math.random() + 1).toString(36).substring(2))
			anticheat.AllowedHealth = (mp.players.local.getHealth() + mp.players.local.getArmour())
		}, 50);
	}
})

mp.events.add("Client:AntiCheat:SetHealth", (health, armor) => {
	setTimeout(() => {
		mp.players.local.setHealth(100 + health)
		mp.players.local.setArmour(armor)
		anticheat.AllowedHealth = (mp.players.local.getHealth() + mp.players.local.getArmour())
	}, 50);
});

mp.events.add("Client:AntiCheat:SetOnlyArmour", (armor) => {
	setTimeout(() => {
		mp.players.local.setArmour(armor)
		anticheat.AllowedHealth = (mp.players.local.getHealth() + mp.players.local.getArmour())
	}, 50);
});

//HotKeys & AntiCheat

	mp.keys.bind(188, true, takemedkit);

	mp.keys.bind(190, true, takevest);


function takemedkit(){
    if(hotkeyinaction == false){
		if(mp.gui.cursor.visible) return;
		if(mp.players.local.vehicle) return;
		if(mp.players.local.getHealth() <= 0) return;
		if(!checkCanInteract) return;
		lastInteraction = Date.now
        hotkeyinaction = true
        mp.players.local.freezePosition(true);
        mp.events.callRemote("Server:KeyPress:UseMediKit");
        setTimeout(() => {
            mp.players.local.freezePosition(false);
            hotkeyinaction = false
			mp.players.local.setHealth(200)
			anticheat.AllowedHealth = (mp.players.local.getHealth() + mp.players.local.getArmour())
        }, 4000);
    }
}

function takevest(){
    if(hotkeyinaction == false){
		if(mp.gui.cursor.visible) return;
		if(mp.players.local.vehicle) return;
		if(mp.players.local.getHealth() <= 0) return;
		if(!checkCanInteract) return;
		lastInteraction = Date.now
        hotkeyinaction = true
        mp.players.local.freezePosition(true);
        mp.events.callRemote("Server:KeyPress:UseVest");
        setTimeout(() => {
            mp.players.local.freezePosition(false);
            hotkeyinaction = false
			mp.players.local.setArmour(100)
			anticheat.AllowedHealth = (mp.players.local.getHealth() + mp.players.local.getArmour())
        }, 4000);
    }
}

mp.events.add("render", () => {
    if (hotkeyinaction == true) {
        mp.game.invoke("0x5E6CC07646BBEAB8", mp.players.local, hotkeyinaction); //DISABLE_PLAYER_FIRING
    }
});