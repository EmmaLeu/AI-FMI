$(function () {

    //click on hidden input file when user clicks the 'add photo' button
    var $coverPhotoBtn = $("#coverPhotoBtn"),
        $coverPhotoHidden = $("#coverPhotoHidden");
   

    $coverPhotoBtn.on("click", function (e) {
        e.preventDefault();
        $coverPhotoHidden.click();
    });

    $coverPhotoHidden.on("change", function () {

        $("#coverPhotoForm").submit();
    })
});