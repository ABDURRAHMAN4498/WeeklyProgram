// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// wwwroot/js/script.js
$(document).ready(function () {
    $('#treeview li').click(function (e) {
        $(this).children('ul').toggle();
        e.stopPropagation();
    });
});
