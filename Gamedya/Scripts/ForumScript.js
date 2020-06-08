function AddComment(data) {
    var yourComment = $("#reply").val();  
    var messageDiv = $("#back");
    var id = data;

    $.ajax({

        url: '/ForumReply/AddComment/',
        data: { comment: yourComment, forumPostId: id },
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
        url: '/ForumReply/Like/',
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
        url: '/ForumReply/Dislike/',
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



