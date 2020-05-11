function AddComment(data) {
    var yourComment = $("#comment").val();
    var yourName = $("#guestName").val();

    $.ajax({
        url: '/NewsComment/AddComment/',
        data: { comment: yourComment, guestName:yourName, newsId: data },
        type: 'POST',
        dataType: 'json',
        success: function (data) { alert("Yorumunuz gönderildi. Onaylandıktan sonra yayınlanacaktır.");},
       
    })
}


function AddLike(data) {
   
    $.ajax({
        url: '/LikeUnlike/Like/',
        data: { commentId: data },
        type: 'POST',
        dataType: 'json',
        success: function () {alert("Like"); }
        

    })
}

function AddUnLike(data) {

    $.ajax({
        url: '/LikeUnlike/UnLike/',
        data: { commentId: data },
        type: 'POST',
        dataType: 'json',
        success: function () { alert("Unlike"); }


    })
}

   

