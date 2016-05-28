$(function(){
    $("body").on("click", "#toggle-publications-btn", function () {
        $("#publication-content-big").toggle('show');
    });
    $("body").on("click", "#toggle-software-btn", function () {
        $("#software-content-big").toggle('show');
    });
    $("body").on("click", "#toggle-datasets-btn", function () {
        $("#datasets-content-big").toggle('show');
    });
});