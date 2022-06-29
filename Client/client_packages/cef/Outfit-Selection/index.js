let clothingBrowser = null
let clothingCamera = null;

mp.events.add("client:createTeamClothingBrowser", (...data) => {
     if (clothingBrowser != null) return;
     if (clothingCamera != null) return;

     clothingBrowser = mp.browsers.new("package://cef/Outfit-Selection/index.html")
     clothingCamera = mp.cameras.new("clothing", new mp.Vector3(-1539.6097, -574.4865, 25.707903), new mp.Vector3(0, 0, -146.42467), 50)

     mp.players.local.position = new mp.Vector3(-1536.7255, -578.67053, 25.707804)
     mp.players.local.setRotation(0, 0, 31.977196, 0, true)

     mp.players.local.freezePosition(true)
     mp.players.local.setAlpha(255);

     mp.game.controls.disableControlAction(0, 32, true);
     mp.game.controls.disableControlAction(0, 33, true);
     mp.game.controls.disableControlAction(0, 34, true);
     mp.game.controls.disableControlAction(0, 35, true);

     clothingCamera.pointAtCoord(-1539.6097, -574.4865, 25.707903)
     clothingCamera.setRot(0, 0, -146.42467, 2)
     clothingCamera.setActive(true)
 
     mp.game.cam.renderScriptCams(true, false, 0, true, false)

     setTimeout(() => {
          clothingBrowser.execute(`LoadClothing(${JSON.stringify(data[0])})`)

          mp.gui.chat.show(false)
          mp.gui.cursor.show(true, true)
     }, 250);
})

mp.events.add("client:deleteTeamClothingBrowser", () => {
     if (clothingBrowser === null) return;

     clothingCamera.setActive(false);
     mp.game.cam.renderScriptCams(false, false, 0, false, false);
     mp.game.invoke(0x31B73D1EA9F01DA2);
     clothingCamera.destroy()
     clothingBrowser.destroy()
     mp.gui.cursor.show(false, false)

     mp.gui.chat.show(true)

     setTimeout(() => {
          clothingBrowser = null
          clothingCamera = null

          mp.players.local.freezePosition(false)
     }, 500);
})

mp.events.add("client:teamClothing:SetTeamClothing", clothingData => {
     if (clothingBrowser === null) return;
     if (clothingData == null) return;
     
     SetTeamClothing(clothingData)
})

mp.events.add("client:teamClothing:SelectTeamClothing", clothingData => {
     if (clothingBrowser === null) return;
     if (clothingData == null) return;

     clothingData = JSON.parse(clothingData)

     if (clothingData.TeamClothingId != null) {
          mp.events.callRemote("Server:TeamClothing:SelectTeamClothing", clothingData.TeamClothingId)
     }
})

const freemodeCharacters = [mp.game.joaat("mp_m_freemode_01"), mp.game.joaat("mp_f_freemode_01")];

function SetTeamClothing(clothingData) {
     let player = mp.players.local

     clothingData = JSON.parse(clothingData)
     mp.players.local.model = freemodeCharacters[clothingData.Gender];

     player.setComponentVariation(1, clothingData.MaskDrawable, clothingData.MaskTexture, 0)
     player.setComponentVariation(3, clothingData.TorsoDrawable, clothingData.TorsoTexture, 0)
     player.setComponentVariation(4, clothingData.LegsDrawable, clothingData.LegsTexture, 0)
     player.setComponentVariation(5, clothingData.BagsNParachuteDrawable, clothingData.BagsNParachuteTexture, 0)
     player.setComponentVariation(6, clothingData.ShoeDrawable, clothingData.ShoeTexture, 0)
     player.setComponentVariation(7, clothingData.AccessiorDrawable, clothingData.AccessiorTexture, 0)
     player.setComponentVariation(8, clothingData.UndershirtDrawable, clothingData.UndershirtTexture, 0)
     player.setComponentVariation(9, clothingData.BodyArmorDrawable, clothingData.BodyArmorTexture, 0)
     player.setComponentVariation(11, clothingData.TopDrawable, clothingData.TopTexture, 0)

     player.setPropIndex(0,  clothingData.HatsDrawable, clothingData.HatsTexture, true)
     player.setPropIndex(1, clothingData.GlassesDrawable, clothingData.GlassesTexture, true)
}
