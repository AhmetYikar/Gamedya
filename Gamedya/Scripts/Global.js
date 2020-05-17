function AddComment(data) {
    var yourComment = $("#comment").val();
    var yourName = $("#guestName").val();

    $.ajax({
        
        url: '/NewsComment/AddComment/',
        data: { comment: yourComment, guestName: yourName, newsId: data },
        type: 'POST',
        dataType: 'json',
        success: function () {
            $("#back").empty(),
            $("#back").append("Yorumunuz onaylandıktan sonra yayınlanacak");                  
        }
    })
    setInterval(function () {
        $("#back").fadeOut(3000);
    }, 5000); 
}



function AddLike(id) {
    $.ajax({
        url: '/LikeUnlike/Like/',
        data: { commentId: id },
        type: 'POST',
        dataType: 'json',
        success: function (data) {
           
            $("#" + id).empty(),
            $("#" + id).append(data);        
        }          

    })
}

function AddUnLike(id) {

    $.ajax({
        url: '/LikeUnlike/UnLike/',
        data: { commentId: id },
        type: 'POST',
        dataType: 'json',
        success: function (data) {
            $("#un"+id).empty(),
            $("#un"+id).append(data);
        }
    })
}

   

