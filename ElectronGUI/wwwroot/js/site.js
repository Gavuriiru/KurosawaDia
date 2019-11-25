﻿let formulario;
let video;

function happyOnClick() {
    formulario.hidden = true;
    video.muted = false;
    video.currentTime = 0; 
}

function configuracoesOnClick() {
    let configuracoes = document.getElementById("configuracao");
    formulario.hidden = true;
    configuracoes.hidden = false;
}

$('document').ready(function () {
    if (dbexiste == "False") {
        let iniciardb = document.getElementById("iniciarbt");
        iniciardb.hidden = true;
    }

    formulario = document.getElementById("formulario");
    video = document.getElementById("myVideo");
    document.getElementById('myVideo').addEventListener('ended', function () {
        video.muted = true;
        formulario.hidden = false;
        video.play();
    });
});