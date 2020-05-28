function AddComment(data) {
    var yourComment = $("#comment").val();
    var yourName = $("#guestName").val();
    var messageDiv = $("#back");
    var id = data;

    $.ajax({

        url: '/BlogComment/AddComment/',
        data: { comment: yourComment, guestName: yourName, blogPostId: id },
        type: 'POST',
        dataType: 'json',
        success: function (mesaj) {
            $(messageDiv).empty(),
                $(messageDiv).append(mesaj);
        }
    });
}

function ClearMesajBack() {
    var messageDiv = $("#back");
    $(messageDiv).empty();
}


function AddLike(id) {
    $.ajax({
        url: '/BlogComment/Like/',
        data: { commentId: id },
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            if (data > 0) {
                $("#" + id).empty(),
                    $("#" + id).append(data),
                    $("#" + id).css('color', 'green'),
                    $("#" + id).css('font-size', '16px');
            }

        }
    })
}

function AddDislike(id) {

    $.ajax({
        url: '/BlogComment/Dislike/',
        data: { commentId: id },
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            if (data > 0) {
                $("#dis" + id).empty(),
                    $("#dis" + id).append(data),
                    $("#dis" + id).css('color', 'maroon'),
                    $("#dis" + id).css('font-size', '16px');
            }
        }
    })
}



