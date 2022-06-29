let mainMenuBrowser = null,
    crosshairenabled = false;

let teamJsonList = ""
let ffaJsonList = ""
let playerJsonList = ""

mp.events.add("client:updatePlayerMainMenu", (...data) => {
    playerJsonList = data[0]
})

mp.events.add("client:createMainMenuBrowser", (...data) => {
    if (mainMenuBrowser !== null) return

    mainMenuBrowser = mp.browsers.new("package://cef/MainMenu/index.html")
    mp.gui.cursor.show(true, true)
    mp.gui.chat.show(false)

    setTimeout(() => {
        let locallanguage = 1
        let selectedlanguage = mp.storage.data.language

        if (selectedlanguage != undefined) {
            locallanguage = selectedlanguage
        }

        if (playerJsonList.length == 0) {
            playerJsonList = data[0]
        }

        let playerJson = JSON.parse(playerJsonList)

        teamJsonList = data[1]
        ffaJsonList = data[2]

        let teamList = JSON.parse(teamJsonList)
        let ffaList = JSON.parse(ffaJsonList)

        let streetCount = 0
        let ffaCount = 0

        teamList.forEach(element => {
            streetCount += element["Count"]
        })

        ffaList.forEach(element => {
            ffaCount += element["Count"]
        })

        mainMenuBrowser.execute(`setlanguagedata(${locallanguage});`)
        mainMenuBrowser.execute(`setsettingsbuttonstate('${mp.storage.data.hitsound}', '${mp.storage.data.floathitmarker}', '${mp.storage.data.hitmarker}', '${mp.storage.data.defaultcrosshair}')`)
        mainMenuBrowser.execute(`setkeysinsettings('${mp.storage.data.hotkeymenu}', '${mp.storage.data.hotkeyinventory}', '${mp.storage.data.hotkeymedikit}', '${mp.storage.data.hotkeyvest}')`)

        mainMenuBrowser.execute(`setStats(${playerJson["Kills"]}, ${playerJson["Deaths"]}, ${playerJson["Money"]}, ${playerJson["Level"]}, ${playerJson["PlayedHours"]});`)
        mainMenuBrowser.execute(`setTeams(${JSON.stringify(teamJsonList)})`)
        mainMenuBrowser.execute(`loadFreeForAllArenas(${JSON.stringify(ffaJsonList)})`)
        mainMenuBrowser.execute(`updateGamemodePlayerCount(${streetCount}, ${ffaCount});`)
        //mainMenuBrowser.execute(`setDailyMissions("${dailyMissionTitleOne}", "${dailyMissionContentOne}", "${dailyMissionTitleTwo}", "${dailyMissionContentTwo}", "${dailyMissionTitleThree}", "${dailyMissionContentThree}");`)

        setTimeout(() => {
            if (playerJson["HasAccount"] == false) {
                mainMenuBrowser.execute(`showloginboxforregister()`)
            } else if (playerJson["HasAccount"] == true) {
                mainMenuBrowser.execute(`quicklogintomain()`)
            }
        }, 100)

        setTimeout(() => {
            mp.gui.cursor.show(true, true)
        }, 500)
    }, 25)
})

mp.events.add("client:deleteMainMenuBrowser", () => {
    if (mainMenuBrowser === null) return;

    mp.gui.cursor.show(false, false)
    mp.gui.chat.show(true)
    mainMenuBrowser.destroy();
    mainMenuBrowser = null;
})

mp.events.add("client:mainmenu:serverregistercallback", () => {
    if (mainMenuBrowser === null) return;

    mainMenuBrowser.execute(`quicklogintomain()`)
})

mp.events.add("client:mainMenu:selectFfaArena", id => {
    if (mainMenuBrowser === null) return;

    mp.events.callRemote("Server:MainMenu:SelectFfaArena", id)
})

mp.events.add("Client:MainMenu:SendLogin", (username) => {
    if (mainMenuBrowser === null) return;

    mp.events.callRemote("Server:Login:RequestLogin", username);
})

mp.events.add("Client:MainMenu:RequestCurrentTeam", teamId => {
    if (mainMenuBrowser === null) return;

    mp.events.callRemote("Server:MainMenu:RequestCurrentTeam", teamId)
    mp.events.call("client:deleteMainMenuBrowser")
})

mp.events.add("Client:MainMenu:FFA", () => {
    if (mainMenuBrowser === null) return;

    mp.events.callRemote("Server:FFA:Join")
    mp.events.call("client:deleteMainMenuBrowser")
})

mp.events.add("client:MainMenu:testasdfasdfasdfasdfasdf", () => {
    mp.events.call("client:createTeamClothingBrowser")
})

mp.events.add("render", () => {
    if (crosshairenabled == true) {
        mp.game.ui.hideHudComponentThisFrame(14)
    }
})

//lang select
mp.events.add("Client:MainMenu:selectlanguage", (language) => {
    if (language == undefined) return;

    mp.storage.data.language = parseInt(language)
    mp.storage.flush();
    mainMenuBrowser.execute(`setlanguagedata(${language});`)
    mp.events.callRemote("server:selectLanguage", language)
});

//Settings (Localstorage)

mp.events.add("togglefloathitmarker", (state) => {
    if (state === 1) {
        mp.storage.data.floathitmarker = parseInt(1)
        mp.storage.flush()
    } else {
        mp.storage.data.floathitmarker = parseInt(0)
        mp.storage.flush()
    }
})

mp.events.add("togglehitsound", (state) => {
    if (state === 1) {
        mp.storage.data.hitsound = parseInt(1)
        mp.storage.flush()
    } else {
        mp.storage.data.hitsound = parseInt(0)
        mp.storage.flush()
    }
})

mp.events.add("togglehitmarker", (state) => {
    if (state === 1) {
        mp.storage.data.hitmarker = parseInt(1)
        mp.storage.flush()
    } else {
        mp.storage.data.hitmarker = parseInt(0)
        mp.storage.flush()
    }
})

mp.events.add("toggledefaultcrosshair", (state) => {
    if (state === 1) {
        mp.storage.data.defaultcrosshair = parseInt(1)
        mp.storage.flush()
        crosshairenabled = true;
    } else {
        mp.storage.data.defaultcrosshair = parseInt(0)
        mp.storage.flush()
        crosshairenabled = false;
    }
})

mp.keys.bind(0x71, true, openmainmenu)

function openmainmenu() {
    if (!mp.gui.cursor.visible) {
        mp.events.call("client:deleteHudBrowser")
        mp.events.callRemote("Server:Open:MainMenu")
    }
}

mp.events.add("Client:Language:Select", (language) => {
    //ToDo: Send to server lg
    mp.storage.data.language = parseInt(language)
    mp.storage.data.flush()

    let locallanguagetwo = 1
    let selectedlanguagetwo = mp.storage.data.language

    if (selectedlanguagetwo != undefined) {
        locallanguagetwo = selectedlanguagetwo
    }

    mainMenuBrowser.execute(`setlanguagedata(${locallanguagetwo});`)
})