let languageBrowser = null;
let languageCamera = null;

mp.events.add("client:createLanguageBrowser", () => {
    if (languageBrowser !== null) return;
    if (languageCamera !== null) return;

    languageBrowser = mp.browsers.new("package://cef/LangaugeSelection/index.html")

    languageCamera = mp.cameras.new("langugeSelect", new mp.Vector3(-1593.105, 2099.647, 67.51107), new mp.Vector3(0, 0, 97.1), 50)
    languageCamera.pointAtCoord(-1593.105, 2099.647, 67.51107);
    languageCamera.setRot(0, 0, 97.1, 2);
    languageCamera.setActive(true);
    mp.game.cam.renderScriptCams(true, false, 0, true, false);

    mp.gui.cursor.show(true, true);
    mp.game.ui.displayRadar(false);
});

mp.events.add("client:selectLanguage", (language) => {
    if (languageBrowser === null) return;
    if (languageCamera === null) return;

    languageBrowser.destroy();
    languageBrowser = null;

    languageCamera.setActive(false);
    languageCamera.destroy();
    languageCamera = null;
    mp.game.cam.renderScriptCams(false, false, 0, true, false);

    mp.gui.cursor.show(false, false);
    mp.game.ui.displayRadar(true);

    mp.events.callRemote("server:selectLanguage", language)
});
