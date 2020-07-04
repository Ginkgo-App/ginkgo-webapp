$(document).ready(function () {
    $('#tourInfosDataTable').DataTable({
        "ajax": {
            "type": "POST",
            "url": $('input[name="getTourInfosController"]').val(),
            "dataType": "json",
        },
        "columns": [
            { "data": "Name", "name": "Name" },
            { "data": "Rating", "name": "Rating" },
            { "data": "StartPlace.Name", "name": "StartPlace" },
            { "data": "DestinatePlace.Name", "name": "DestinatePlace" },
            {
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var editBtn = '<a href="tourinfo/edit/' + full.Id + '" class="btn btn-circle btn-sm bg-success text-gray-100"><i class="far fa-edit"></i></a>';
                    return editBtn;
                }
            },
            {
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var editBtn = '<a href="tourinfo/detail/' + full.Id + '" class="btn btn-circle btn-sm bg-info text-gray-100"><i class="fas fa-info"></i></a>';
                    return editBtn;
                }
            },
            {
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var deleteBtn = '<button class="btnDeleteTourInfo btn btn-circle btn-sm bg-danger text-gray-100" data-id="' + full.Id + '" data-name="' + full.Name + '"><i class="far fa-trash-alt"></i></button>';
                    return deleteBtn;
                }
            }
        ],

        "serverSide": true,
        "processing": true,
        "order": [0, "asc"],
    });


    $('body').on('click', '.btnDeleteTourInfo', function () {
        var tourInfoId = $(this).data('id');
        var tourInfoName = $(this).data('name');
        var msg = 'Confirm to delete tour info: "' + tourInfoName + '" ?';

        $('#deleteTourInfoModal .btn-primary').attr('tourinfoid', tourInfoId);
        $('#deleteTourInfoModal .modal-body').html(msg);

        $('#deleteTourInfoModal').modal('show');
    });

    $('#deleteTourInfoModal .btn-primary').click(function () {
        var tourInfoId = $('#deleteTourInfoModal .btn-primary').attr('tourinfoid');

        $('#deleteTourInfoModal .modal-body').html('Deleting ...');

        $.ajax({
            type: "POST",
            url: $('input[name="deleteTourInfoController"]').val(),
            data: {
                tourInfoId: tourInfoId,
            },
            success: function (data) {
                $('#deleteTourInfoModal .btn-primary').attr('tourinfoid', '');
                $('#deleteTourInfoModal .modal-body').html('');
                $('#deleteTourInfoModal').modal('hide');

                var oTable = $('#deleteTourInfoModal').dataTable();
                oTable.fnDraw(false);
            },
            error: function (ex) {
                $('#deleteTourInfoModal .btn-primary').attr('tourinfoid', '');
                $('#deleteTourInfoModal .modal-body').html('');
                console.log(ex);
            }
        });

    });
});