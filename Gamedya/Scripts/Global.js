function AddComment(data) {
    var yourComment = $("#comment").val();
    var yourName = $("#guestName").val();
    var messageDiv = $("#back");

    $.ajax({

        url: '/NewsComment/AddComment/',
        data: { comment: yourComment, guestName: yourName, newsId: data },
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
        url: '/LikeDislike/Like/',
        data: { commentId: id },
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            if (data>0) {
                $("#" + id).empty(),
                    $("#" + id).append(data),
                    $("#" + id).css('color', 'green');                 
            }     
          
        } 
    })
}

function AddDislike(id) {

    $.ajax({
        url: '/LikeDislike/Dislike/',
        data: { commentId: id },
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            if (data>0) {
                $("#dis" + id).empty(),
                    $("#dis" + id).append(data),
                    $("#dis" + id).css('color', 'maroon');
                   
            }                     
        }
    })
}

   

