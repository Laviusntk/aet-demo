/*
* Title     : A script for dashboard ui interactions
* Authors   : Lavius N. Motileng
* Date      : 11/11/2017
*/

/*
* on start of the script, the entrance dashboard navigation button
* is highlighted. A trigger for other buttons is then set to change 
* the background of the button diplay relevent content in the main
* view.
*/

var prevNavButton = null;  // keeps track of the active nav button.

function getSessionKey() {
    var url = window.location.href;
    return url.split('?')[1].split('=')[1].replace("#", "");
}


/*********************API Client*******************/

var BASE_URL = "http://localhost:60409/api/";
var sessionID = getSessionKey();
var user = null;
var applicationsData = null;
var announcementsData = null;

function request(_path, _parameters, _s_callback, _f_callback) {
    $.ajax({
        url: BASE_URL + _path + "?sessionid=" + sessionID + _parameters,
        headers: {
            'Content-Type': 'application/json; charset=utf-8'
        }
    })
        .done(function (data) {
            _s_callback(data);
        })
        .fail(function (data) {
            _f_callback(data);
        });
}


var getProfileSuccessCallBack = function (data) {
    user = data;
    console.log("success");
    console.log(JSON.stringify(data));
    $("#display-name").html(user.firstName.split(" ")[0] + " " + user.lastName);
    $("#headline").html(user.headline);
    $("#profile-pic").attr("src", user.profilePicture);
}
var getProfileFailCallBack = function (data) {
    alert("failed to load profile.");
}
function getUserProfile() {
    request(
        "basicprofile",
        "",
        getProfileSuccessCallBack,
        getProfileFailCallBack
    );
}

var getAppsSuccCallBack = function (data) {
    applicationsData = data;
    $("#num-of-applications").html(data.length);
}
var getAppsFailCallBack = function (data) {
    alert(JSON.stringify(data));
}
function getApplications() {
    request(
        "myapplicationns",
        "",
        getAppsSuccCallBack,
        getAppsFailCallBack
    );
}


var getAnnsSuccCallBack = function (data) {
    announcementsData = data;
    $("#num-of-announcements").html(data.length);
    //alert(JSON.stringify(data));
}
var getAnnsFailCallBack = function (data) {
    alert(JSON.stringify(data));
}
function getAnnouncements() {
    request(
        "myannouncements",
        "",
        getAnnsSuccCallBack,
        getAnnsFailCallBack
    );
}

getAnnouncements();
getApplications();
getUserProfile();




$(document).ready(function () {
    /*
    * Initiates a tricker for navbar buttons.
    */
    $("#init-nav-button").css("background", "rgba(0,0,0,0.25)"); // iniates the dashboard nav button.
    $(".nav-button").click(function () {
        if (prevNavButton == null)
            $("#init-nav-button").css("background", "transparent");
        else if (prevNavButton != null)
            prevNavButton.style.background = "transparent";
        this.style.background = "rgba(0,0,0,0.25)";
        prevNavButton = this;
    });

    var app = angular.module("myApp", ["ngRoute"]);
    app.config(function ($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "/layout/dashboard.htm",
                controller: "Dashboard"
            })
            .when("/announcements", {
                templateUrl: "/layout/announcements.htm",
                controller: "Announcements"
            })
            .when("/applications", {
                templateUrl: "/layout/applications.htm",
                controller: "Applications"
            })
            .when("/messages", {
                templateUrl: "/layout/messages.htm",
                controller: "Messages"
            })
            .when("/uploads", {
                templateUrl: "/layout/uploads.htm",
                controller: "Uploads"
            }) 
            .when("/vacancies", {
                templateUrl: "/layout/vacancies.htm",
                controller: "Vacancies"
            })
            .when("/studentForm", {
                templateUrl: "/layout/studentForm.htm"
            })
            .when("/mentorForm", {
                templateUrl: "/layout/mentorForm.htm"
            })
            .when("/studentshipForm", {
                templateUrl: "/layout/studentshipForm.htm"
            })
            .when("/calendar", {
                templateUrl: "/layout/calendar.htm",
                controller: "Calendar"
            });
    });

    app.controller('Dashboard', function ($scope) {

    });
    app.controller('Announcements', function ($scope) {
        $scope.myannouncements = announcementsData;
    });
    app.controller('Applications', function ($scope) {
        $scope.myapplications = applicationsData;
    });
    app.controller('Messages', function ($scope) {

    });
    app.controller('Uploads', function ($scope) {

    });
    app.controller('Vacancies', function ($scope) {

    });
    app.controller('Calendar', function ($scope) {

    });
});


