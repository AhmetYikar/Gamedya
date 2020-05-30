function ApproveBlog(blogId) {
    var failDiv = $("#failMessage");
    var successDiv = $("#successMessage");

    $.ajax({
        url: '/Blog/Approve/',
        data: { id: blogId },
        type: 'POST',
        dataType: 'Json',
        success: function (mesaj) {
            if (mesaj==0) {
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