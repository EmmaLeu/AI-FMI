$(function () {

    $("#news-table").on("click", "#add-news", function () {
        $self = $(this);
        $userId = $self.data("id");
        var json = {
            UserID: $userId,
            Title: $("#Title").val(),
            Description: $("#Description").val(),
            Link: $("#Link").val(),
            LinkText: $("#LinkText").val()
        };
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: urlAddNews,
            data: JSON.stringify(json)
        }).done(function (resp) {
            if (resp == 'ok') {
                location.reload();
            }
            else {
                $(".modal-body.modal-edit").append('<p class="alert-danger left">Make sure you include a description.</p>');
            }
        });
    });

    $("#news-table").on("click", ".edit-news", function () {
        $self = $(this);
        $newsID = $self.data("id");
        $("#myEditModal-" + $newsID).modal('show');
    });
    $("#news-table").on("click", ".edit-news-btn", function () {
        $self = $(this);
        $newsID = $self.data("id");
        $("#myEditModal-"+$newsID).modal('show');
        var json = {
            NewsID : $newsID,
            Title: $("#EditedTitle").val(),
            Description: $("#EditedDescription").val(),
            Link: $("#EditedLink").val(),
            LinkText: $("#EditedLinkText").val()
        };
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: urlUpdateNews,
            data: JSON.stringify(json)
        }).done(function (resp) {
            if (resp == 'ok') {
                $(".modal-body").append('<p class="alert-success left">Update successfully done.</p>');
                location.reload();
            }
            else {
                $(".modal-body").append('<p class="alert-danger left">Make sure you include a description.</p>');
            }
        });
    });

    $("#news-table").on("click", ".remove-news", function () {
        $self = $(this);
        $self.closest(".list-group-item").find(".delete-news-modal").modal('show');
    });

    $("#news-table").on("click", ".delete-news", function () {
        $self = $(this);
        $newsId = $self.closest(".list-group-item").find(".remove-news").data("id");
        console.log($newsId);
        $.ajax({
            type: "POST",
            url: urlDeleteNews,
            data:
            {
                newsId: $newsId
            }
        }).done(function (resp) {
            if (resp == 'ok') {
                location.reload();
            }
            else {
                $("#news-table").prepend('<p class="alert-danger">Unsuccessful remove</p>');
            }
        })
    });
});