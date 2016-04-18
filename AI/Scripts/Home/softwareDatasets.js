$("#datasets-content-big").on("click", ".remove-dataset", function () {
    $self = $(this);
    $self.closest('.dataset-box').find(".delete-dataset-modal").modal('show');
});

$("#datasets-content-big").on("click", ".delete-dataset", function () {
    $self = $(this);
    $sdId = $self.data("id");
    $.ajax({
        type: "POST",
        url: urlDeleteSoftware,
        data:
        {
            sdId: $sdId
        }
    }).done(function (resp) {
        if (resp == 'ok') {
            location.reload();
        }
        else {
            $("#datasets-content").prepend('<p class="alert-danger">Unsuccessful remove</p>');
        }
    })
});

$("#software-content-big").on("click", ".remove-software", function () {
    $self = $(this);
    $self.closest('.software-box').find(".delete-software-modal").modal('show');
});

$("#software-content-big").on("click", ".delete-software", function () {
    $self = $(this);
    $sdId = $self.data("id");
    $.ajax({
        type: "POST",
        url: urlDeleteSoftware,
        data:
        {
            sdId: $sdId
        }
    }).done(function (resp) {
        if (resp == 'ok') {
            location.reload();
        }
        else {
            $("#software-content").prepend('<p class="alert-danger">Unsuccessful remove</p>');
        }
    })
});