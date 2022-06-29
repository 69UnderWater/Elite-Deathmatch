mp.gui.chat.show(true)

mp.discord.update("Elite Deathmatch", "Original Copy")

mp.game.player.setWeaponDamageModifier(0.1);
mp.game.ped.setAiWeaponDamageModifier(0.1);

mp.players.local.setSuffersCriticalHits(false)

mp.nametags.enabled = false

mp.events.add('playerReady', () => {
    mp.game.invoke('0xF314CF4F0211894E', 143, 252, 94, 3, 180);
    mp.game.invoke('0xF314CF4F0211894E', 116, 252, 94, 3, 180);
    mp.game.gxt.set('PM_PAUSE_HDR', "Elite Deathmatch");


    for (let i = 0; i < 20; i++) {
        mp.objects.new('prop_shuttering02', new mp.Vector3(513.9765014648438, 229.38340759277344, 104.3177719116211),
        {
            rotation: new mp.Vector3(90, 70.30481719970703, 0),
            alpha: 255,
            dimension: i * 81,
        });
    
        mp.objects.new('prop_shuttering02', new mp.Vector3(523.3015747070312, 204.4503173828125, 104.3177719116211),
        {
            rotation: new mp.Vector3(90, -20, 0),
            alpha: 255,
            dimension: i * 81,
        });
    
        mp.objects.new('prop_shuttering02', new mp.Vector3(487.1582336425781, 215.30447387695312, 104.3177719116211),
        {
            rotation: new mp.Vector3(90, 159.99993896484375, 0),
            alpha: 255,
            dimension: i * 81,
        });   
    }

    if (mp.storage.data.hitsound == undefined || mp.storage.data.floathitmarker == undefined || mp.storage.data.hitmarker == undefined || mp.storage.data.defaultcrosshair == undefined || mp.storage.data.hotkeyinventory == undefined || mp.storage.data.hotkeymedikit == undefined || mp.storage.data.hotkeyvest == undefined || mp.storage.data.firstweapon == undefined || mp.storage.data.seccondweapon == undefined) {

        //HUD
        mp.storage.data.floathitmarker = parseInt(1);
        mp.storage.data.hitsound = parseInt(0);
        mp.storage.data.hitmarker = parseInt(0);
        mp.storage.data.defaultcrosshair = parseInt(0);

        mp.storage.data.hudleft = parseInt(1000)
        mp.storage.data.hudtop = parseInt(1000)

        //hotkeys
        mp.storage.data.hotkeyinventory = parseInt(73);
        mp.storage.data.hotkeymedikit = parseInt(188);
        mp.storage.data.hotkeyvest = parseInt(190);
        mp.storage.data.interactionhotkey = parseInt(69)

        //weapons
        mp.storage.data.firstweapon = parseInt(-771403250);
        mp.storage.data.seccondweapon = parseInt(-1357824103);

        mp.storage.data.language = parseInt(3);

        mp.storage.flush()

        // setTimeout(() => {
        //     mp.events.call("sethealkeys")
        //     mp.events.call("setinteractionhotkeys")
        //     mp.events.call("setinventoryhotkeys")
        // }, 5000);
    }
})

mp.events.add("client:enableNametags", (state) => {
    mp.nametags.enabled = state
})

mp.events.add("client:freezePlayer", (state) => {
    mp.players.local.freezePosition(state);
})

mp.events.add('render', () => {
    mp.game.player.setHealthRechargeMultiplier(0);
    mp.game.controls.disableControlAction(2, 44, true);
    mp.game.controls.disableControlAction(1, 140, true);
    mp.game.controls.disableControlAction(0, 140, true);
    mp.game.controls.disableControlAction(0, 142, true);

    mp.game.ui.hideHudComponentThisFrame(2)
    mp.game.ui.hideHudComponentThisFrame(9)

    mp.game.streaming.requestIpl("vw_casino_main")
    mp.game.streaming.requestIpl("vw_casino_garage")
    mp.game.streaming.requestIpl("vw_casino_carpark")
    mp.game.streaming.requestIpl("vw_casino_penthouse")
});

setInterval(() => {
    mp.game.invoke('0x9E4CFFF989258472')
    mp.game.invoke('0xF4F2C0D4EE209E20')
}, 20000);

let clothingCamera = null

mp.events.add("client:createTeamClothingCamera", () => {
    if (clothingCamera !== null) return

    clothingCamera = mp.cameras.new("clothing", new mp.Vector3(-1539.6097, -574.4865, 25.707903), new mp.Vector3(0, 0, -146.42467), 50)

    clothingCamera.pointAtCoord(-1539.6097, -574.4865, 25.707903)
    clothingCamera.setRot(0, 0, -146.42467, 2)
    clothingCamera.setActive(true)

    mp.players.local.freezePosition(true)
    mp.players.local.setAlpha(255)

    mp.players.local.position = new mp.Vector3(-1536.7255, -578.67053, 25.707804)
    mp.players.local.setRotation(0, 0, 31.977196, 0, true)

    mp.game.cam.renderScriptCams(true, false, 0, true, false)

    // setTimeout(() => {
    //     mp.gui.chat.show(false)
    // }, 250)
})

mp.events.add("client:deleteTeamClothingCamera", () => {
    if (clothingCamera === null) return

    clothingCamera.setActive(false);
    mp.game.cam.renderScriptCams(false, false, 0, true, false);
    mp.game.invoke(0x31B73D1EA9F01DA2);
    clothingCamera.destroy()

    // mp.gui.chat.show(true)

    clothingCamera = null
})

mp.events.add("client:teamClothing:SetTeamClothing", clothingData => {
    if (clothingData == null) return

    SetTeamClothing(clothingData)
})

const freemodeCharacters = [mp.game.joaat("mp_m_freemode_01"), mp.game.joaat("mp_f_freemode_01")]

function SetTeamClothing(clothingData) {
    let player = mp.players.local

    clothingData = JSON.parse(clothingData)
    mp.players.local.model = freemodeCharacters[clothingData.Gender]

    player.setComponentVariation(1, clothingData.MaskDrawable, clothingData.MaskTexture, 0)
    player.setComponentVariation(3, clothingData.TorsoDrawable, clothingData.TorsoTexture, 0)
    player.setComponentVariation(4, clothingData.LegsDrawable, clothingData.LegsTexture, 0)
    player.setComponentVariation(5, clothingData.BagsNParachuteDrawable, clothingData.BagsNParachuteTexture, 0)
    player.setComponentVariation(6, clothingData.ShoeDrawable, clothingData.ShoeTexture, 0)
    player.setComponentVariation(7, clothingData.AccessiorDrawable, clothingData.AccessiorTexture, 0)
    player.setComponentVariation(8, clothingData.UndershirtDrawable, clothingData.UndershirtTexture, 0)
    player.setComponentVariation(9, clothingData.BodyArmorDrawable, clothingData.BodyArmorTexture, 0)
    player.setComponentVariation(11, clothingData.TopDrawable, clothingData.TopTexture, 0)

    player.setPropIndex(0, clothingData.HatsDrawable, clothingData.HatsTexture, true)
    player.setPropIndex(1, clothingData.GlassesDrawable, clothingData.GlassesTexture, true)
}

mp.events.addDataHandler("AdminTools:IsInvisible", (entity, value, oldvalue) => {
    if(value) {
        entity.setAlpha(0)
    } else {
        entity.resetAlpha()
    }
})

mp.events.add('entityStreamIn', (entity) => {
    if(entity.getVariable('AdminTools:IsInvisible') === true) {
        entity.setAlpha(0)
    } else {
        entity.resetAlpha()
    }
})

