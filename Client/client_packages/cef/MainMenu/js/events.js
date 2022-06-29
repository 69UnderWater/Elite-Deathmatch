/* Click-Event */
$('#settings-to-langselect').click(() => {
    $('#settings-box-crosshair').fadeOut(250)
    $('#settings-box-general').fadeOut(250)
    setTimeout(() => {
        $('#settings-box-language').fadeIn(250)
    }, 250);
})

$('#settings-to-general').click(() => {
    $('#settings-box-crosshair').fadeOut(250)
    $('#settings-box-language').fadeOut(250)
    setTimeout(() => {
        $('#settings-box-general').fadeIn(250)
    }, 250);
})

$('#settings-to-crosshair').click(() => {
    $('#settings-box-general').fadeOut(250)
    $('#settings-box-crosshair').fadeOut(250)
    setTimeout(() => {
        $('#settings-box-language').fadeIn(250)
    }, 250);
})

$('.back-from-settings').click(() => {
    $('.settings').fadeOut(250)
    setTimeout(() => {
        $('.main-screen').fadeIn(250)
    }, 250);
});

$('.back-from-teamselection').click(() => {
    $('.main-teamselection').fadeOut(250)
    setTimeout(() => {
        $('.mode-selection').fadeIn(250)
    }, 250);
});

$('.back-from-ffaSelection').click(() => {
    $('.main-ffa-selection').fadeOut(250)
    setTimeout(() => {
        $('.mode-selection').fadeIn(250)
    }, 250);
});

$('.back-from-profile').click(() => {
    $('.main-profile').fadeOut(250)
    setTimeout(() => {
        $('.main-screen').fadeIn(250)
    }, 250);
});

$('.back-from-play').click(() => {
    $('.mode-selection').fadeOut(250)
    setTimeout(() => {
        $('.main-screen').fadeIn(250)
    }, 250);
});

$('#grid-top-right').click(() => {
    $('.mode-selection').fadeOut(250)
    setTimeout(() => {
        $('.main-ffa-selection').fadeIn(250)
    }, 250);
})

$('#grid-top-left').click(() => {
    $('.mode-selection').fadeOut(250)
    openopenfactiontab()
    setTimeout(() => {
        $('.main-teamselection').fadeIn(250)
    }, 250);
})

$('#teamOne').click(() => {
    $('.main-teamselection').fadeOut(250)

    mp.trigger("Client:MainMenu:RequestCurrentTeam", 1)
    mp.trigger("client:createTeamClothingBrowser")
})

$('#teamTwo').click(() => {
    $('.main-teamselection').fadeOut(250)

    mp.trigger("Client:MainMenu:RequestCurrentTeam", 2)
    mp.trigger("client:createTeamClothingBrowser")
})

$('#teamThree').click(() => {
    $('.main-teamselection').fadeOut(250)

    mp.trigger("Client:MainMenu:RequestCurrentTeam", 3)
    mp.trigger("client:createTeamClothingBrowser")
})

$('#teamFour').click(() => {
    $('.main-teamselection').fadeOut(250)

    mp.trigger("Client:MainMenu:RequestCurrentTeam", 4)
    mp.trigger("client:mainmenu:testasdfasdfasdfasdfasdf")
})

$("#hitsound").on('change', function () {
    if ($(this).is(':checked')) {
        $(this).attr('value', 'true');
        //true
        mp.trigger("togglehitsound", 1)
    } else {
        $(this).attr('value', 'false');
        //flase
        mp.trigger("togglehitsound", 0)
    }
});

$("#floatinghitmarker").on('change', function () {
    if ($(this).is(':checked')) {
        $(this).attr('value', 'true');
        //true
        mp.trigger("togglefloathitmarker", 1)
    } else {
        $(this).attr('value', 'false');
        //false
        mp.trigger("togglefloathitmarker", 0)
    }
});

$('#hitmarker').on('change', function () {
    if ($(this).is(':checked')) {
        $(this).attr('value', 'true');
        //true
        mp.trigger("togglehitmarker", 1)
    } else {
        $(this).attr('value', 'false');
        //false
        mp.trigger("togglehitmarker", 0)
    }
})

$('#defaultcrosshair').on('change', function () {
    if ($(this).is(':checked')) {
        $(this).attr('value', 'true');
        //true
        mp.trigger("toggledefaultcrosshair", 1)
    } else {
        $(this).attr('value', 'false');
        //false
        mp.trigger("toggledefaultcrosshair", 0)
    }
})

$('#de').hover(
    function () {
        $('#title').html("Wähle deine Sprache")
    }
)

$('#en').hover(
    function () {
        $('#title').html('Please select your language')
    }
)

$('#ru').hover(
    function () {
        $('#title').html('Пожалуйста, выберите свой язык')
    }
)

