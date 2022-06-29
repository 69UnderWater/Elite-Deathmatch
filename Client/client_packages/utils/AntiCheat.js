mp.events.add("Client:AntiCheat:TestGodMode", () => {
    let health = mp.players.local.getHealth() + mp.players.local.getArmour();
    mp.players.local.applyDamageTo(1, true);
    setTimeout(() => {
        mp.players.local.setHealth(health)
        mp.events.callRemote("server:GodmodeCheckDone", health, mp.players.local.getHealth());
    }, 1000);
});