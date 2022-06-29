let hudBrowser = null,
hitsound = null;

mp.events.add("client:createHudBrowser", () => {
    if (hudBrowser !== null) return;

    hudBrowser = mp.browsers.new("package://cef/HUD/index.html");
})

mp.events.add("client:deleteHudBrowser", () => {
    if (hudBrowser !== null) return;

    hudBrowser.destroy()
    hudBrowser = null
})

mp.events.add("client:updateHud", (kills, deaths, level) => {
    if (hudBrowser === null) return;

    hudBrowser.execute(`updateHud(${kills}, ${deaths}, ${level});`)
})
