$(function () {

    $("#user-table").on("click", ".remove-member", function () {
        $self = $(this);
        $self.closest(".options-member-td").find(".delete-member-modal").modal('show');
    });

    $("#user-table").on("click", ".delete-member", function () {
        $self = $(this);
        $userId = $self.closest(".options-member-td").find(".remove-member").data("id");
        console.log($userId, urlDeleteMember);
        $.ajax({
            type: "POST",
            url: urlDeleteMember,
            data:
            {
                id: $userId
            }
        }).done(function (resp) {
            if (resp == 'ok') {
                location.reload();
            }
            else {
                $("#user-table").prepend('<p class="alert-danger">Unsuccessful remove</p>');
            }
        })
    });
    $("#user-table").on("click", ".update-member", function () {
        $self = $(this);
        $self.closest(".options-member-td").find(".update-member-modal").modal('show');
    });

    $("#user-table").on("click", ".update-member-confirm", function () {
        $self = $(this);
        $userId = $self.closest(".options-member-td").find(".update-member").data("id");
        $.ajax({
            type: "POST",
            url: urlUpdateToFormerOrMember,
            data:
            {
                id: $userId,
                isDeleted: true
            }
        }).done(function (resp) {
            if (resp == 'ok') {
                location.reload();
            }
            else {
                $("#user-table").prepend('<p class="alert-danger">Unsuccessful update</p>');
            }
        })
    });
    $("#collaborator-table").on("click", ".remove-collaborator", function () {
        $self = $(this);
        $self.closest(".options-collaborator-td").find(".delete-collaborator-modal").modal('show');
    });

    $("#collaborator-table").on("click", ".delete-collaborator-confirm", function () {
        alert("ceva");
        $self = $(this);
        $userId = $self.closest(".options-collaborator-td").find(".remove-collaborator").data("id");
        $.ajax({
            type: "POST",
            url: urlDeleteCollaborator,
            data:
            {
                id: $userId
            }
        }).done(function (resp) {
            if (resp == 'ok') {
                location.reload();
            }
            else {
                $("#collaborator-table").prepend('<p class="alert-danger">Unsuccessful update</p>');
            }
        })
    });

    $("#former-table").on("click", ".remove-former", function () {
        $self = $(this);
        $self.closest(".options-former-td").find(".delete-former-modal").modal('show');
    });

    $("#former-table").on("click", ".delete-former-confirm", function () {
        alert("ceva");
        $self = $(this);
        $userId = $self.closest(".options-former-td").find(".remove-former").data("id");
        $.ajax({
            type: "POST",
            url: urlDeleteMember,
            data:
            {
                id: $userId
            }
        }).done(function (resp) {
            if (resp == 'ok') {
                location.reload();
            }
            else {
                $("#former-table").prepend('<p class="alert-danger">Unsuccessful update</p>');
            }
        })
    });
    $("#former-table").on("click", ".update-former", function () {
        $self = $(this);
        $self.closest(".options-former-td").find(".update-former-modal").modal('show');
    });

    $("#former-table").on("click", ".update-former-confirm", function () {
        $self = $(this);
        $userId = $self.closest(".options-former-td").find(".update-former").data("id");
        $.ajax({
            type: "POST",
            url: urlUpdateToFormerOrMember,
            data:
            {
                id: $userId,
                isDeleted: false
            }
        }).done(function (resp) {
            if (resp == 'ok') {
                location.reload();
            }
            else {
                $("#former-table").prepend('<p class="alert-danger">Unsuccessful update</p>');
            }
        })
    });
});