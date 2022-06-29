let deathmatchBrowser = null

mp.events.add("client:createDeathmatchBrowser", () => {
     if (deathmatchBrowser !== null) return

     deathmatchBrowser = mp.browsers.new("package://cef/Gangwar/index.html")
})

mp.events.add("client:deathmatch:setup", (maxDeaths, teamOne, teamTwo, maxTime) => {
     if (deathmatchBrowser === null) return

     deathmatchBrowser.execute(`setup(${maxDeaths}, "${teamOne}", "${teamTwo}", ${maxTime});`)
})

mp.events.add("client:deathmatch:withdrawWinner", (deathmatchId, teamName) => {
     if (deathmatchBrowser === null) return

     mp.events.callRemote("Server:Deathmatch:WithdrawWinner", deathmatchId, teamName);
})
