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

    $("#coverPhotoCaptionBtn").on("click", function (e) {
        e.preventDefault();
        $("#editCaptionModal").modal('show');
    });

    $("body").on("click", "#edit-caption", function () {
        $self = $(this);
        $("#editCaptionModal").modal('show');
        var json = {
            caption: $("#editedCaption").val()
        };
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: urlUpdateCaption,
            data: JSON.stringify(json)
        }).done(function (resp) {
            if (resp == 'ok') {
                $(".modal-body>.alert-success").remove();
                $(".modal-body.modal-edit").append('<p class="alert-success left">Update successfully done.</p>');
                $("#caption-heading").text($("#editedCaption").val());
            }
            else {
            }
        });
    });
});