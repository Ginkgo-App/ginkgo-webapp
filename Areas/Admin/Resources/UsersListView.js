$(document).ready(function () {
    $('#usersDataTable').DataTable({
        "ajax": {
            "type": "POST",
            "url": $('input[name="getUsersController"]').val(),
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
            {   
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var editBtn = '<a href="user/edit/' + full.Id + '" class="btn btn-circle btn-sm bg-success text-gray-100"><i class="far fa-edit"></i></a>';
                    return editBtn;
                }
            },
            {
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var deleteBtn = '<button class="btnDeleteUser btn btn-circle btn-sm bg-danger text-gray-100" data-id="' + full.Id +'" data-name="' + full.Name + '"><i class="far fa-trash-alt"></i></button>';
                    return deleteBtn;
                }
            }
        ],

        "serverSide": true,
        "processing": true,
        "order": [0, "asc"],
    });


    $('body').on('click', '.btnDeleteUser', function () {
        var userId = $(this).data('id');
        console.log(userId);
        var userName = $(this).data('name');
        console.log(userName);
        var msg = 'Confirm to delete user: "' + userName + '" ?';

        $('#deleteUserModal .btn-primary').attr('userid', userId);
        $('#deleteUserModal .modal-body').html(msg);

        $('#deleteUserModal').modal('show');
    });

    $('#deleteUserModal .btn-primary').click(function () {
        var userId = $('#deleteUserModal .btn-primary').attr('userid');

        $('#deleteUserModal .modal-body').html('Deleting ...');

        $.ajax({
            type: "POST",
            url: $('input[name="deleteUserController"]').val(),
            data: {
                userId: userId,
            },
            success: function (data) {
                $('#deleteUserModal .btn-primary').attr('userid', '');
                $('#deleteUserModal .modal-body').html('');
                $('#deleteUserModal').modal('hide');

                var oTable = $('#usersDataTable').dataTable();
                oTable.fnDraw(false);
            },
            error: function (ex) {
                $('#deleteUserModal .btn-primary').attr('userid', '');
                $('#deleteUserModal .modal-body').html('');
                console.log(ex);
            }
        });
        
    });
});