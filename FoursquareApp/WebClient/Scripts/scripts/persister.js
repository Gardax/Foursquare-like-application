﻿/// <reference path="http-requester.js" />
/// <reference path="class.js" />
/// <reference path="http://crypto-js.googlecode.com/svn/tags/3.1.2/build/rollups/sha1.js" />
var persisters = (function () {
    var username = localStorage.getItem("username");
    var sessionKey = localStorage.getItem("sessionKey");

    function saveUserData(userData) {
        localStorage.setItem("username", userData.username);
        localStorage.setItem("sessionKey", userData.sessionKey);
        username = userData.username;
        sessionKey = userData.sessionKey;
    }

    function clearUserData() {
        localStorage.removeItem("username");
        localStorage.removeItem("sessionKey");
        username = "";
        sessionKey = "";
    }

    var MainPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl;
            this.user = new UserPersister(this.rootUrl)
            this.message = new MessagesPersister(this.rootUrl);
        },
        isUserLoggedIn: function () {

            var isLoggedIn = username != null && sessionKey != null;
            //check is logged

            return isLoggedIn;
        },
        username: function () {
            return username;
        }
    });
    var UserPersister = Class.create({
        init: function (rootUrl) {
            //...api/user/
            this.rootUrl = rootUrl + "User/";
        },
        login: function (user, success, error) {
            var url = this.rootUrl + "login";
            var userData = {
                username: user.username,
                authCode: CryptoJS.SHA1(user.username + user.password).toString()
            };
            $("logout").css("display", "block");
            httpRequester.postJSON(url, userData,
				function (data) {
				    saveUserData(data);
				    success(data);
				}, error);
        },
        register: function (user, success, error) {
            var url = this.rootUrl + "register";
            var userData = {
                username: user.username,
                authCode: CryptoJS.SHA1(user.username + user.password).toString()
            };
            httpRequester.postJSON(url, userData,
				function (data) {
				    saveUserData(data);
				    success(data);
				}, error);
        },
        logout: function (success, error) {
            var url = this.rootUrl + "logout/" + sessionKey;
            httpRequester.getJSON(url, function (data) {
                clearUserData();
                success(data);
            }, error)
        },
    });

    var MessagesPersister = Class.create({
        init: function (url) {
            this.rootUrl = url + "messages/";
        },
        unread: function (success, error) {
            var url = this.rootUrl + "unread/" + sessionKey;
            httpRequester.getJSON(url, success, error);
        },
        all: function (success, error) {
            var url = this.rootUrl + "all/" + sessionKey;
            httpRequester.getJSON(url, success, error);
        }
    });
    return {
        get: function (url) {
            return new MainPersister(url);
        }
    };
}());