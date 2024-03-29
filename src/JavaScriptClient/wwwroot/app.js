﻿function log() {
    document.getElementById("results").innerText = "";

    Array.prototype.forEach.call(arguments,
        function(msg) {
            if (msg instanceof Error) {
                msg = "Error: " + msg.message;
            } else if (typeof msg !== "string") {
                msg = JSON.stringify(msg, null, 2);
            }
            document.getElementById("results").innerHTML += msg + "\r\n";
        });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);

var config = {
    authority: "https://localhost:5001",
    client_id: "js",
    redirect_uri: "http://localhost:7115/callback.html",
    response_type: "code",
    scope: "openid profile offline_access user_profile message photo",
    post_logout_redirect_uri: "http://localhost:7115/index.html",
    clockSkew: 5,
    automaticSilentRenew: true
};
var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    } else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        console.log("user");
        console.log(user);
        var url = "http://localhost:11011/identity";
        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.onerror = function (e) {
            console.log(e);
        };
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}