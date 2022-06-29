let inventoryBrowser = null,
    isopen = false,
    lastInteraction = 0,
    inventoryItemString = "";

let firstweapon = null;
let secondweapon = null;

function checkCanInteract() {
    return lastInteraction + 750 < Date.now()
}

mp.events.add("client:inventory:updateInventoryList", (...data) => {
    inventoryItemString = data[0]
})

mp.events.add("client:inventory:updateInventoryWeapons", (...data) => {
    firstweapon = data[0]
    secondweapon = data[1]
})

mp.events.add("client:Inventory:createInventoryBrowser", invString => {
    if (inventoryBrowser !== null) return;

    inventoryBrowser = mp.browsers.new("package://cef/Inventory/index.html");
    mp.gui.cursor.show(true, true)
    isopen = true

    setTimeout(() => {
        inventoryBrowser.execute(`loadItems(${JSON.stringify(invString)})`)
        inventoryBrowser.execute(`setCurrentSelectedWeapons('${firstweapon}', '${secondweapon}')`)
    }, 20)
})

mp.events.add("client:Inventory:deleteInventoryBrowser", () => {
    if (inventoryBrowser === null) return;

    inventoryBrowser.destroy();
    inventoryBrowser = null;
    mp.gui.cursor.show(false, false)
    isopen = false
    mp.gui.chat.show(true)
})


mp.keys.bind(73, true, openinventory);

// mp.events.add("saveinventoryhotkey", (keycode) => {
//     mp.keys.unbind(mp.storage.data.hotkeyinventory, true, openinventory)
//     mp.storage.data.hotkeyinventory = parseInt(keycode)
//     mp.storage.flush()
//     mp.keys.bind(mp.storage.data.hotkeyinventory, true, openinventory)
// })

function openinventory() {
    if (isopen === false) {
        if (mp.gui.cursor.visible) return;
        if (!checkCanInteract()) return;
        lastInteraction = Date.now()
        mp.gui.chat.show(false)
        isopen = true

        if (inventoryItemString.length == 0) {
            mp.events.callRemote("Server:Inventory:RequestItems")
        }

        if (firstweapon == null) {
            mp.events.callRemote("Server:Inventory:RequestWeapons")
        }

        if (secondweapon == null) {
            mp.events.callRemote("Server:Inventory:RequestWeapons")
        }

        mp.events.call("client:Inventory:createInventoryBrowser", inventoryItemString)
    } else {
        mp.gui.chat.show(true)
        isopen = false
        mp.events.call("client:Inventory:deleteInventoryBrowser")

        mp.events.callRemote("Server:LocalStorage:RecivePlayerWeapons", firstweapon, secondweapon)
    }
}

mp.events.add("Client:LocalStorage:RequestWeapons", () => {
    setTimeout(() => {
        mp.events.callRemote("Server:LocalStorage:RecivePlayerWeapons", firstweapon, secondweapon)
    }, 100);
})

mp.events.add("Client:Inventory:UseItem", itemName => {
    if (inventoryBrowser === null) return;

    mp.events.callRemote("Server:Inventory:UseItem", itemName)
})

mp.events.add("Client:Inventory:RemoveItem", itemName => {
    if (inventoryBrowser === null) return;

    mp.events.callRemote("Server:Inventory:RemoveItem", itemName)
})

mp.events.add("Client:Inventory:SetInventoryFirstWeapon", (weaponhash) => {
    firstweapon = parseInt(weaponhash);
})

mp.events.add("Client:Inventory:SetInventorySeccondWeapon", (weaponhash) => {
    secondweapon = parseInt(weaponhash);
})