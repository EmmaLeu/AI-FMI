$(function () {

    function setTime() {
        $(".from-now").each(function () {
            self = $(this);
            self.html(moment(self.data("time")).fromNow());
        });
    }

    //update elapsed time since posting every minute
    function updateTime() {
        setTime();
        setTimeout(function () { updateTime() }, 36000);
    }

    updateTime();

    $(".publication-header").on("click", "#btn-search-publications", function () {
        $self = $(this);
        $searchText = $("#in-search-publications").val();
        if ($searchText && $searchText.trim()) {
            $isSearch = true;
            $.ajax({
                type: "GET",
                url: urlSearchPublications,
                data:
                {
                    searchText: $searchText,
                    isSearch: $isSearch
                }
            }).done(function (resp) {

                $(".body-content").replaceWith(resp);
                $("#pagination-nav").remove();
            });
        }
    });

    $("#publication-content-big").on("change", "#dropdown-category", function () {
        $sort = $("#dropdown-category").val();
        $.ajax({
            type: "GET",
            url: urlSearchPublications,
            data:
            {
                searchText: "",
                isSearch: false,
                sort: $sort,
                page: 1
            }
        }).done(function (resp) {

            $(".body-content").replaceWith(resp);
        });

    $("#publication-content-big").on("click", ".remove-publication", function () {
        $self = $(this);
        $self.closest('.publication-box').find(".delete-publication-modal").modal('show');
    });

    $("#publication-content-big").on("click", ".delete-publication", function () {
        $self = $(this);
        $publicationId = $self.data("id");
        $.ajax({
            type: "POST",
            url: urlDeletePublication,
            data:
            {
                publicationId: $publicationId
            }
        }).done(function (resp) {
            if (resp == 'ok') {
                location.reload();
            }
            else {
                $("#publication-content").prepend('<p class="alert-danger">Unsuccessful remove</p>');
            }
        })
    });
    });

    $("body").on("click", "#remove-filters", function () {
        location.reload();
    });

    $("#publication-content-big").on("click", ".toggle-abstract-btn", function (e) {
       // e.stopPropagation();
        $self = $(this);
        $self.next(".abstract-div").toggle('show');
    });
});