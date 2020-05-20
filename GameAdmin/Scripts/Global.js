function UsersByRole(data) {

    $.ajax({
        url: '/NewsUsersAdmin/GetByRoleName/',
        data: { roleId: data },
        type: 'POST',
        dataType: 'html',
        success: function (data) {
            $("#byRole").html(data);
        },
        error: function () {
            alert("Hata alındı.");
        }
    });
}


function UsersWithOutRole(){

    $.ajax({
        url: '/NewsUsersAdmin/UsersWithOutRole/',
        type: 'POST',
        dataType: 'html',
        success: function (data) {
            $("#byRole").html(data);
        },
        error: function () {
            alert("Hata alındı.");
        }
    });
}

