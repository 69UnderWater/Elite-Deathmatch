let garageCamera = null

mp.events.add("client:createGarageCam", () => {
     if (garageCamera !== null) return
 
     garageCamera = mp.cameras.new("garage", new mp.Vector3(-181.21529, -172.35983, 44.62322), new mp.Vector3(0, 0, -20.053106), 50)
 
     garageCamera.pointAtCoord(-181.21529, -172.35983, 44.62322)
     garageCamera.setRot(0, 0, -20.053106, 2)
     garageCamera.setActive(true)
 
     mp.players.local.freezePosition(true)
     mp.players.local.setAlpha(0)

     mp.gui.chat.show(false)
 
     mp.players.local.position = new mp.Vector3(-181.21529, -172.35983, 40)
     mp.players.local.setRotation(0, 0, -20.053106, 0, true)
 
     mp.game.cam.renderScriptCams(true, false, 0, true, false)
})

mp.events.add("client:deleteGarageCam", () => {
    if (garageCamera === null) return

    garageCamera.setActive(false);
    mp.game.cam.renderScriptCams(false, false, 0, true, false);
    mp.game.invoke(0x31B73D1EA9F01DA2);
    garageCamera.destroy()

    mp.gui.chat.show(true)
    
    mp.players.local.setAlpha(255)
    mp.players.local.freezePosition(false)

    garageCamera = null

    if (currentVehicle === null) return
    currentVehicle.destroy()
    currentVehicle = null
})

let currentVehicle = null


