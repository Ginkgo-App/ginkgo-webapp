$(document).ready(function () {
    $('#usersDataTable').DataTable({
        "ajax": {
            "type": "POST",
            "url": $("#usersDataTable").attr("data-controller"),
            "dataType": "json",
        },
        "columns": [
            { "data": "Name", "name": "Name" },
            { "data": "Email", "name": "Email" },
            { "data": "PhoneNumber", "name": "PhoneNumber" },
            { "data": "Birthday", "name": "Birthday" },
            { "data": "Gender", "name": "Gender" },
            { "data": "Address", "name": "Address" },
            { "data": "Role", "name": "Role" },
        ],
        "serverSide": true,
        "processing": true,
        "order": [0, "asc"],
    });

    // call ajax to get user data with pagination
    function getUsersDataTable(page, pageSize) {
        var result;
        $.ajax({
            type: "POST",
            url: "UserAjax/GetUsers",
            data: {
                page: page,
                pageSize: pageSize
            },
            success: function (data) {
                var response = jQuery.parseJSON(data);
                result = response.Data;
            },
            error: function (ex) {
                console.log(ex);
            }
        });
        return result;
    }
});