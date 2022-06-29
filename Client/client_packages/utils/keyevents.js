let lastInteraction = 0,
ischatactive = true;

function checkCanInteract(){
    return lastInteraction + 750 < Date.now()
}

mp.keys.bind(69, true, callInteractionHotkey)    

// mp.events.add("saveinteractionhotkey", (keycode) => {
//     mp.keys.unbind(mp.storage.data.interactionhotkey, true, callInteractionHotkey)
//     mp.storage.data.hotkeymenu = parseInt(keycode)
//     mp.storage.flush()
//     mp.keys.bind(mp.storage.data.interactionhotkey, true, callInteractionHotkey)
// })

function callInteractionHotkey(){
    if(mp.gui.cursor.visible) return
    if(!checkCanInteract()) return
    lastInteraction = Date.now()

    mp.events.callRemote("Server:E:Event")
}

mp.keys.bind(113, true, callMainMenuKey)

function callMainMenuKey() {
    if(mp.gui.cursor.visible) return
    if(!checkCanInteract()) return
    lastInteraction = Date.now()

    mp.events.callRemote("Server:Open:MainMenu")
    mp.events.call("triggerWebController", "components.Hud.setState(false)")
}

mp.keys.bind(33, true, hidehudcomponent_chat)

function hidehudcomponent_chat(){
    if(mp.gui.cursor.visible) return;
    if(ischatactive == true){
        mp.gui.chat.show(false)
        ischatactive = false
    } else {
        mp.gui.chat.show(true)
        ischatactive = true
    }
}

mp.keys.bind(73, true, callInventoryKey)

function callInventoryKey() {
    if(mp.gui.cursor.visible) return
    if(!checkCanInteract()) return
    lastInteraction = Date.now()

    mp.events.callRemote("Server:Inventory:RequestInventory")
}
