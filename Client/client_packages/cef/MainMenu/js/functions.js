function backtosettingsfromhudeditor() {
    $('#hud-editor').fadeOut(250)
    $('.hud-editor-hud').fadeOut(250)
    setTimeout(() => {
        $('.settings').fadeIn(250)
    }, 250);
}

function selectFfaArena(id) {
    mp.trigger("client:mainMenu:selectFfaArena", id)
}

function updateGamemodePlayerCount(sf, ffa) {
    $('#current-players-streetfight').text(sf)
    $('#current-players-ffa').text(ffa)
}

function setTeams(allTeams) {
    var teams = JSON.parse(allTeams)
    var $oTeamList = ""
    var $pTeamList = ""
    for (var i in teams) {
        if (teams[i].IsPrivate == false) {
            $oTeamList += `
                    <li onclick="setOnlineFrak(${teams[i].Id})" style="background-image: url(../utils/backgrounds/hoods/${teams[i].ShortName}.png);">
                        <center>
                            <img src="../utils/logos/${teams[i].ShortName}.png">
                            <p id="teamname-1">${teams[i].TeamName}</p>
                            <div class="hr"></div>
                            <p class="playercount">Player: <span>${teams[i].Count}</span></p>
                        </center>
                    </li>`
        } else {
            $pTeamList += `
                    <li onclick="setPrivateFrak(${teams[i].Id})" style="background-image: url(../utils/backgrounds/hoods/${teams[i].ShortName}.png);">
                        <center>
                            <img src="../utils/logos/${teams[i].ShortName}.png">
                            <p>${teams[i].TeamName}</p>
                            <div class="hr"></div>
                            <p class="playercount">Player: <span>${teams[i].Count}</span></p>
                        </center>
                    </li>`
        }
    } //${teams[i].OnlineTeamPlayers.length}

    $('.available-teams-public-list').prepend($oTeamList)
    $('.available-teams-private-list').prepend($pTeamList)
}

function setPrivateFrak(id) {
    mp.trigger("Client:MainMenu:RequestCurrentTeam", id)
}

function setOnlineFrak(id) {
    mp.trigger("Client:MainMenu:RequestCurrentTeam", id)
}

function setDailyMissions(dailyMissionTitleOne, dailyMissionContentOne, dailyMissionTitleTwo, dailyMissionContentTwo, dailyMissionTitleThree, dailyMissionContentThree) {
    $('#dailyMissionTitleOne').text(`${dailyMissionTitleOne}`)
    $('#dailyMissionContentOne').text(`${dailyMissionContentOne}`)
    $('#dailyMissionTitleTwo').text(`${dailyMissionTitleTwo}`)
    $('#dailyMissionContentTwo').text(`${dailyMissionContentTwo}`)
    $('#dailyMissionTitleThree').text(`${dailyMissionTitleThree}`)
    $('#dailyMissionContentThree').text(`${dailyMissionContentThree}`)
}

function setStats(kills, deaths, money, level, playedhours) {
    let num = playedhours;
    let hours = Math.floor((num / 60));
    let minutes = Math.round(((num / 60) - hours) * 60);

    let kd = parseFloat((kills / deaths)).toFixed(1)

    if (isNaN(kd)) {
        kd = 0
    }

    if (kd == Infinity) {
        kd = 0
    }

    $('#stats-kills').text(`${kills}`)
    $('#stats-deaths').text(`${deaths}`)
    $('#stats-kd').text(`${kd}`)
    $('#stats-money').text(`${money}`)
    $('#stats-level').text(`${level}`)
    $('#stats-playedhours').text(`${hours}.${minutes}`)
}

function loadFreeForAllArenas(allArenas) {
    var arenas = JSON.parse(allArenas)
    var $arenaList = ""
    for (var i in arenas) { // style="background-image: url('../utils/ffaimg/${arenas[i].Id}.png')"
        $arenaList += `
            <li id="ffa-arena-${arenas[i].Id}" onclick="selectFfaArena(${arenas[i].Id})" style="background-image: url(../utils/backgrounds/ffa/${arenas[i].Id}.png);">
                <center>
                    <p class="arena-name"><span id="ffa-arena-name">${arenas[i].ArenaName}</span>
                        <div class="hrtop"></div>
                        <div class="hrbottom"></div>
                    <p class="playercount"><span id="ffa-player-count">Spieler</span>: <span id="ffa-player-count">${arenas[i].Count}</span>/<span id="ffa-player-count">${arenas[i].MaxPlayers}</span></p>
                </center>
            </li>`
    }

    $('.available-ffa-arenas').prepend($arenaList)
}

function sendregistertry() {
    var username = $('#username').val();
    var regex = /[^A-Za-z0-9_-]/;

    if (regex.test(username)) {
        $(".login-box").addClass("animation");
        setTimeout(() => {
            $(".login-box").removeClass("animation");
        }, 300);
        return;
    } else if (username.length < 4) {
        $(".login-box").addClass("animation");
        setTimeout(() => {
            $(".login-box").removeClass("animation");
        }, 300);
        return;
    } else if (username.length > 20) {
        $(".login-box").addClass("animation");
        setTimeout(() => {
            $(".login-box").removeClass("animation");
        }, 300);
        return;
    } else if (username.includes("IlIl") || username.includes("lIlI") || username.includes("IIII") || username.includes("llll")) {
        $(".login-box").addClass("animation");
        setTimeout(() => {
            $(".login-box").removeClass(".animation");
        }, 300);
        return;
    }

    mp.trigger("Client:MainMenu:SendLogin", username)
    logintomain()
}

function login() {
    var username = $('#username').val();
    var regex = /[^A-Za-z0-9_-]/;

    if (regex.test(username)) {
        $(".login-box").addClass("animation");
        setTimeout(() => {
            $(".login-box").removeClass("animation");
        }, 300);
        return;
    } else if (username.length < 4) {
        $(".login-box").addClass("animation");
        setTimeout(() => {
            $(".login-box").removeClass("animation");
        }, 300);
        return;
    } else if (username.length > 20) {
        $(".login-box").addClass("animation");
        setTimeout(() => {
            $(".login-box").removeClass("animation");
        }, 300);
        return;
    } else if (username.includes("IlIl") || username.includes("lIlI") || username.includes("IIII") || username.includes("llll")) {
        $(".login-box").addClass("animation");
        setTimeout(() => {
            $(".login-box").removeClass(".animation");
        }, 300);
        return;
    }

    logintomain();

    mp.trigger("Client:MainMenu:SendLogin", username)
}

function setbuttonchecked() {
    $('#hitsoundswitch').prop('checked', true);
}

function backtomain(current) {
    $('.' + current).fadeOut(250)
    setTimeout(() => {
        $('.main-screen').fadeIn(250)
    }, 250);
}

function maintosettings() {
    $('.main-screen').fadeOut(250)
    setTimeout(() => {
        $('.settings').fadeIn(250)
        $('#settings-box-language').fadeIn(250)
    }, 250);
}

function maintoprofile() {
    $('.main-screen').fadeOut(250)
    setTimeout(() => {
        $('.main-profile').fadeIn(250)
    }, 250);
}

function maintoplay() {
    $('.main-screen').fadeOut(250)
    setTimeout(() => {
        $('.mode-selection').fadeIn(250)
    }, 250);
}

function logintomain() {
    $('.login-box').fadeOut(250)
    setTimeout(() => {
        $('.main-screen').fadeIn(250)
    }, 250);
}

function showloginboxforregister() {
    $('#login-box-shower').fadeIn(100)
}

function quicklogintomain() {
    $('.login-box').fadeOut(250)
    $('.main-screen').fadeIn(250)
}

function openopenfactiontab() {
    $('.available-teams-private').fadeOut(250)
    setTimeout(() => {
        $('.available-teams-public').fadeIn(250)
    }, 250);
}

function openprivatefactiontab() {
    $('.available-teams-public').fadeOut(250)
    setTimeout(() => {
        $('.available-teams-private').fadeIn(250)
    }, 250);
}

function copytext(TextToCopy) {
    var TempText = document.createElement("input");
    TempText.value = TextToCopy;
    document.body.appendChild(TempText);
    TempText.select();

    document.execCommand("copy");
    document.body.removeChild(TempText);
}


function selectLanguage(language) {
    //console.log(language);
    mp.trigger("Client:MainMenu:selectlanguage", language);
}

function setkeysinsettings(menucode, inventorycode, medikitcode, vestcode) {
    let keyCode = menucode;
    let chrCode = keyCode - 48 * Math.floor(keyCode / 48);
    let chr = String.fromCharCode((96 <= keyCode) ? chrCode : keyCode);

    let keyCode2 = inventorycode;
    let chrCode2 = keyCode2 - 48 * Math.floor(keyCode2 / 48);
    let chr2 = String.fromCharCode((96 <= keyCode2) ? chrCode2 : keyCode2);

    let keyCode3 = medikitcode;
    let chrCode3 = keyCode3 - 48 * Math.floor(keyCode3 / 48);
    let chr3 = String.fromCharCode((96 <= keyCode3) ? chrCode3 : keyCode3);

    let keyCode4 = vestcode;
    let chrCode4 = keyCode4 - 48 * Math.floor(keyCode4 / 48);
    let chr4 = String.fromCharCode((96 <= keyCode4) ? chrCode4 : keyCode4);

    $('#hotkey-menu').val(chr)
    $('#hotkey-inventory').val(chr2)
    $('#hotkey-medkit').val(chr3)
    $('#hotkey-vest').val(chr4)
}

function sethotkeyininput(event, input, triggername) {
    let k = event.keyCode
    event.preventDefault();

    if (k == 96 || k == 110 || k == 97 || k == 98 || k == 99 || k == 100 || k == 101 || k == 102 || k == 103 || k == 104 || k == 105 || k == 107 || k == 109 || k == 106 || k == 111) {
        $('#' + input).val("numpad " + event.key)
        mp.trigger("" + triggername, event.keyCode)
        return;
    }

    $('#' + input).val(event.key)
    mp.trigger("" + triggername, event.keyCode)
}

function changehudwidth() {
    let sliderval = $('#hudwidthslider').val()

    $('.hud-editor-container').css("left", sliderval / 1 + '%')
    $('.hud-editor-container-stats').css("left", sliderval / 1 + 3 + '%')
}

function changehudheight() {
    let sliderval = $('#hudheightslider').val()

    $('.hud-editor-container').css("top", sliderval / 1 + '%')
    $('.hud-editor-container-stats').css("top", sliderval / 1 + '%')
}

function hidemainmenufromsettings() {
    $('.settings').fadeOut(250)
    setTimeout(() => {
        $('#hud-editor').fadeIn(250)
        $('.hud-editor-hud').fadeIn(250)
    }, 250);
}

function setsettingsbuttonstate(hitsound, floatinghitmarker, hitmarker, defaultcrosshair) {
    if (hitsound == 1) {
        $('#hitsound').prop("checked", true);
    }
    if (floatinghitmarker == 1) {
        $('#floatinghitmarker').prop("checked", true);
    }
    if (hitmarker == 1) {
        $('#hitmarker').prop("checked", true);
    }
    if (defaultcrosshair == 1) {
        $('#defaultcrosshair').prop("checked", true);
    }
}

//translation
function setlanguagedata(lang) {
    let language = lang

    if (language == 1) {
        //login
        $('#register-title').html('Registration');
        $('#register-button').html('Register');
        $('#username').attr('placeholder', 'Username');

        //main buttons
        $('#play-button').html('Play');
        $('#profile-button').html('Profile');
        $('#settings-button').html('Language Selection');

        //Play menu
        $('#daily-missions-title').html('Daily Missions');

        $('#modeselection-title').html('Play');
        $('#grid-selection-players-text').html('players');
        $('#grid-selection-players-texttwo').html('players');

        //Profile page
        $('#profile-title').html('Profile');
        $('#profile-money').html('Money');
        $('#profile-playhours').html('hours played');

        //Settings page
        $('#settings-title').html('Language Selection');

        //team-selection
        $('#team-player-countone').html('players');
        $('#team-player-counttwo').html('players');
        $('#team-player-countthree').html('players');
        $('#team-player-countfour').html('players');

        $('#team-public-factions').html('public factions')
        $('#team-private-factions').html('private factions')

        $('#team-selection-title').html('team selection')

        //back-buttons
        $('#back-buttonone').html('Back');
        $('#back-buttontwo').html('Back');
        $('#back-buttonthree').html('Back');
        $('#back-buttonfour').html('Back');

        $('#main-menu-hotkey-text').html('Main-menu');
        $('#inventory-hotkey-text').html('Inventory');
        $('#interaction-hotkey-text').html('Interaction');
        $('#medikit-hotkey-text').html('First-aid kit');
        $('#vest-hoktey-text').html('Vest');
        $('#default-crosshair-text').html('Standard Crosshair');
        $('#hitsound-text').html('Hitsound');
        $('#hitmarker-text').html('Hitmarker');
        $('#hitmarker-text-text').html('Hitmarker (text)');
        $('#hud-position-text').html('HUD Position');
        $('#hud-position-change-hotkey').html('change');

        $('#ffa-title-arena-text').html('FFA Arena selection')

        $('#settings-hotkeys-text').html('HOTKEYS')
        $('#settings-hud-text').html('HUD')
        $('#settings-lang-text').html('Language selection')

    } else if (language == 2) {

        //login
        $('#register-title').html('Регистрация');
        $('#register-button').html('Зарегистрироваться');
        $('#username').attr('placeholder', 'Имя пользователя');

        //main buttons
        $('#play-button').html('Играть');
        $('#profile-button').html('Профиль');
        $('#settings-button').html('Выбор языка');

        //Play menu
        $('#daily-missions-title').html('Ежедневные миссии');

        $('#modeselection-title').html('Играть');
        $('#grid-selection-players-text').html('игроки');
        $('#grid-selection-players-texttwo').html('игроки');

        $('#streefight-title').html('СТРИТФАЙТ')
        $('#ffa-title').html('БЕСПЛАТНО ДЛЯ ВСЕХ')

        $('#soon-text-one').html('СКОРО')
        $('#soon-text-two').html('СКОРО')

        //Profile page
        $('#profile-title').html('Профиль');
        $('#profile-money').html('Деньги');
        $('#profile-playhours').html('сыгранные часы');

        //Settings page
        $('#settings-title').html('Выбор языка');

        //team-selection
        $('#team-player-countone').html('игроки');
        $('#team-player-counttwo').html('игроки');
        $('#team-player-countthree').html('игроки');
        $('#team-player-countfour').html('игроки');

        $('#team-public-factions').html('общественные фракции')
        $('#team-private-factions').html('частные дроби')

        $('#team-selection-title').html('подбор команды')

        //back-buttons
        $('#back-buttonone').html('Назад');
        $('#back-buttontwo').html('Назад');
        $('#back-buttonthree').html('Назад');
        $('#back-buttonfour').html('Назад');

        $('#main-menu-hotkey-text').html('Главное меню');
        $('#inventory-hotkey-text').html('Рюкзак');
        $('#interaction-hotkey-text').html('Взаимодействие');
        $('#medikit-hotkey-text').html('Аптечка первой помощи');
        $('#vest-hoktey-text').html('Жилет');
        $('#default-crosshair-text').html('Стандартное перекрестие');
        $('#hitsound-text').html('Хитсаунд');
        $('#hitmarker-text').html('Хитмаркер');
        $('#hitmarker-text-text').html('Хитмаркер (Текст)');
        $('#hud-position-text').html('Положение HUD');
        $('#hud-position-change-hotkey').html('изменить');

        $('#ffa-title-arena-text').html('Выбор арены FFA')

        $('#settings-hotkeys-text').html('сочетания клавиш')
        $('#settings-hud-text').html('игровой интерфейс')
        $('#settings-lang-text').html('Выбор языка')
    } else {
        //login
        $('#register-title').html('Registrierung');
        $('#register-button').html('Registrieren');
        $('#username').attr('placeholder', 'Benutzername');

        //main buttons
        $('#play-button').html('Spielen');
        $('#profile-button').html('Profil');
        $('#settings-button').html('Sprachauswahl');

        //Play menu
        $('#daily-missions-title').html('Tages Missionen');

        $('#modeselection-title').html('Spielen');
        $('#grid-selection-players-text').html('Spieler');
        $('#grid-selection-players-texttwo').html('Spieler');

        //Profile page
        $('#profile-title').html('Profil');
        $('#profile-money').html('Geld');
        $('#profile-playhours').html('Spielstunden');

        //Settings page
        $('#settings-title').html('Sprachauswahl');

        //team-selection
        $('#team-player-countone').html('Spieler');
        $('#team-player-counttwo').html('Spieler');
        $('#team-player-countthree').html('Spieler');
        $('#team-player-countfour').html('Spieler');

        $('#team-public-factions').html('Öffentliche Fraktionen')
        $('#team-private-factions').html('Private Fraktionen')

        $('#team-selection-title').html('Teamauswahl')

        //back-buttons
        $('#back-buttonone').html('Zurück');
        $('#back-buttontwo').html('Zurück');
        $('#back-buttonthree').html('Zurück');
        $('#back-buttonfour').html('Zurück');

        $('#main-menu-hotkey-text').html('Main-menu');
        $('#inventory-hotkey-text').html('Inventar');
        $('#interaction-hotkey-text').html('Interaktion');
        $('#medikit-hotkey-text').html('Verbandskasten');
        $('#vest-hoktey-text').html('Weste');
        $('#default-crosshair-text').html('Standart Crosshair');
        $('#hitsound-text').html('Hitsound');
        $('#hitmarker-text').html('Hitmarker');
        $('#hitmarker-text-text').html('Hitmarker (text)');
        $('#hud-position-text').html('HUD Position');
        $('#hud-position-change-hotkey').html('Ändern');

        $('#ffa-title-arena-text').html('FFA Arena Auswahl')

        $('#settings-hotkeys-text').html('HOTKEYS')
        $('#settings-hud-text').html('HUD')
        $('#settings-lang-text').html('Sprachauswahl')
    }
}
