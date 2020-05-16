function AddComment(data) {
    var yourComment = $("#comment").val();
    var yourName = $("#guestName").val();

    $.ajax({
        url: '/NewsComment/AddComment/',
        data: { comment: yourComment, guestName: yourName, newsId: data },
        type: 'POST',
        dataType: 'json',
        success: (function (i) {
            $("#back").append("<h5>Yorumunuz Onaylandıktan Sonra Yayınlacaktır</h5>"),
                setInterval(function () {
                    $("#back").fadeOut(3000);
                }, 5000);
        })

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

   

