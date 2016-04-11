$(function() {
    $("#awards-table").on("click", "#add-award", function () {
    $self = $(this);
    $userId = $self.data("id");
    var json = {
        UserID: $userId,
        Title: $("#Title").val(),
        Description: $("#Description").val(),
    };
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: urlAddAward,
        data: JSON.stringify(json)
    }).done(function (resp) {
        if(resp == 'ok')
        {
           location.reload();
        }
        else
        {
            $(".modal-body").append('<p class="alert-danger left">Make sure you include a title.</p>');
        }
    });
    });

    $("#awards-table").on("click", ".edit-award", function () {
        $self = $(this);
        $awardID = $self.data("id");
        $("#myEditModal-" + $awardID).modal('show');
    });

    $("#awards-table").on("click", ".edit-award-btn", function () {
        
        $self = $(this);
        $awardID = $self.data("id");
        $("#myEditModal-" + $awardID).modal('show');
        var json = {
            AwardID: $awardID,
            Title: $("#EditedTitle").val(),
            Description: $("#EditedDescription").val()
        };
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: urlUpdateAward,
            data: JSON.stringify(json)
        }).done(function (resp) {
            if (resp == 'ok') {
                $(".modal-body.modal-edit").append('<p class="alert-success left">Update successfully done.</p>');
            }
            else {
                $(".modal-body.modal-edit").append('<p class="alert-danger left">Make sure you include a title.</p>');
            }
        });
    });

    $("#awards-table").on("click", ".remove-award", function () {
        $self = $(this);
        $self.closest(".list-group-item").find(".delete-award-modal").modal('show');
        console.log($self.closest(".list-group-item").find(".delete-award-modal")[0]);
    });

    $("#awards-table").on("click", ".delete-award", function () {
        $self = $(this);
        $awardId = $self.closest(".list-group-item").find(".remove-award").data("id");
        console.log($awardId);
        $.ajax({
            type: "POST",
            url: urlDeleteAward,
            data:
            {
                awardID : $awardId
            }
        }).done(function (resp) {
            if(resp == 'ok')
            {       
                location.reload();
            }
            else
            {
                $("#award-table").prepend('<p class="alert-danger">Unsuccessful remove</p>');
            }
        })
    });
});