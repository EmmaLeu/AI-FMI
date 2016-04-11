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
        });
    });

    $(".publication-header").on("click", "#remove-filters", function () {
        location.reload();
    });
//    //implement scroll pagination
//    $(window).scroll(function () {
//        if ($(window).scrollTop() + $(window).height() == $(document).height()) {
//            if (!isLoading) {
//                getOlderPublications();
//            }
//        }
//    });

//    function getOlderPublications() {
//        $('#divPublicationsLoader').html("Loading posts...")
//        isLoading = true;
//        $.ajax(
//            {
//                type: "GET",
//                url: urlGetOlderPublications,
//                data:
//                    {
//                        lastID: $("#publication-list .publication-container:last").data("id")
//                    },
//                contentType: "application/json",
//                dataType: "json"

//            }).done(function (publications) {

//                if (publications != "") {
//                    for (var p = 0; p < publications.length; p++) {
//                        var html = template(getPublicationContext(p, posts));
//                        $('#post-list').append(html);
//                        $(".comment-panel").hide();
//                        setTime();
//                    }
//                }
//                $('#divPostsLoader').empty();
//                isLoading = false;
//            });
//    }

});