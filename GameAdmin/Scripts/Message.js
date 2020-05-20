$(document).on("click", ".ileri", function () {
    var id = $("#dropDown").val();
    var contentDiv = $("#messageContent");
    var receiverDiv = $("#receiver");
    $.ajax({
        url: '/Message/MessageContent/' + id,
        type: 'POST',
        success: function (result) {
            $(contentDiv).html(result),
                $(receiverDiv).hide();
        },
        error: function () {
            alert("Hata alındı.");
        }
    });
})

