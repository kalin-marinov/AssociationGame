
window['playSound'] = function (url) {
    var audio = document.createElement('audio');
    audio.style.display = "none";
    audio.src = url;
    audio.autoplay = true;
    audio.onended = function () {
        audio.remove() //Remove when played.
    };
    document.body.appendChild(audio);
}

window['fitWord'] = function(selector){
    window['fitty'](selector, { maxSize: 100, observeMutations : false} );
}

window['deferFit'] = function(selector){
    setTimeout(() => {
        window['fitty'](selector, { maxSize: 100} );
        console.log('resizing ' + selector);
    }, 10);

    return true;
}