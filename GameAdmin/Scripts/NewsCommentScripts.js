function ApproveComment(commentId) {
    var failDiv = $("#failMessage" + commentId);
    var successDiv = $("#successMessage"+commentId);

    $.ajax({
        url: '/NewsComments/Approve/',
        data: { id: commentId },
        type: 'POST',
        dataType: 'Json',
        success: function (mesaj) {
            if (mesaj == 0) {
                $(failDiv).empty(),
                    $(failDiv).append("Onay işlemi gerçekleşmedi");
            }
            if (mesaj == 1) {
                $(successDiv).empty(),
                    $(successDiv).append("Onay işlemi tamamlandı");
            }
        },
        error: function () {
            alert("Hata alındı.");
        }
    });
}


    function Delete (commentId) {
        var confirmDelete = confirm("silmek istediğinize emin misiniz?");
        var failDiv = $("#failMessage" + commentId);

            //silinecek "tr" satırı
            if (confirmDelete) {
                var delTr = $("#" + commentId);
                var id = commentId;
                $.ajax({
                    url: '/NewsComments/DeleteByAjax/' + id,
                    type: 'POST',
                    success: function (mesaj) {
                        if (mesaj == 1) {
                            delTr.fadeOut(800, function () {
                                delTr.remove();
                            })
                        }
                        if (mesaj == 0) {
                            $(failDiv).empty(),
                                $(failDiv).append("Silme işlemi gerçekleşmedi");
                        }
                    },
                    error: function () {alert('Hata')}

                });
            }

}


