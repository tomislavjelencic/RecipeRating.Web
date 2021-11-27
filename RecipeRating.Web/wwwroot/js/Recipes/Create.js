$(document).ready(function () {
    populateDropdown();
});

$('#category').change('input', function (e) {
    populateDropdown();
});

function populateDropdown() {
    var categoryId = $("#category").val();
    if (categoryId != null) {
        $.ajax({
            type: "GET",
            url: "/api/Dishes?categoryId=" + categoryId,
            data: "",
            success: function (data) {
                var s = "";
                for (var i = 0; i < data.length; i++) {
                    s += '<option value="' + data[i].id + '">' + data[i].name + '</option>';
                }
                $("#dish-select").html(s);
                $('#dish-select').selectpicker('refresh');
            }
        });
    }
};

$("#search").on("click", function (e) {
    let input = $('#search-input');
    let val = input.val();
    $("#search-table-tb").empty();
    $.ajax({
        url: "/recipes/CreateAjax?keyword=" + val,
        method: "GET",
        data: "",
        success: function (result) {
            var items = result.items;
            for (var i = 0; i < items.length; i++) {
                $("#search-table-tb").append($('<tr>')
                    .append('<td><a href=https://www.youtube.com/watch?v=' + items[i].id.videoId + '>' + items[i].snippet.title + '</a></td>')
                    .append('<td><img src="' + items[i].snippet.thumbnails.medium.url + '" alt="Image" width="100"></td>')
                    .append('<td>' + items[i].snippet.channelTitle + '</td>')
                    .append('<td><input id="' + items[i].id.videoId + '" data="' + items[i].id.videoId + '" type="submit" value="Input Video Data" class="btn btn-light add-video"/></td>'));
            }
        }
    });
});

$(document).on('click', '.add-video', function () {
    let val = $(this).attr("data")
    importData(val);
});

$("#import-video-id").on("click", function (e) {
    let val = $('#video-id').val();
    importData(val);
});

function importData(val) {
    $.ajax({
        url: "/recipes/GetVideoAjax?id=" + val,
        method: "GET",
        data: "",
        success: function (result) {
            $('#name').val(result.name);
            $('#thumbnail').val(result.thumbnailUrl);
            $('#video-id').val(result.url);
            $('#account-name').val(result.accountName);
            $('#account-id').val(result.accountId);
            $('#provider-account-id').val(result.providerAccountId);
        }
    });
};
