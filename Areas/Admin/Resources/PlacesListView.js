$(document).ready(function () {
    $('#placesDataTable').DataTable({
        "ajax": {
            "type": "POST",
            "url": $('input[name="getPlacesController"]').val(),
            "dataType": "json",
        },
        "columns": [
            { "data": "Name", "name": "Name" },
            { "data": "Description", "name": "Description" },
            {
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var editBtn = '<a href="place/edit/' + full.Id + '" class="btn btn-circle btn-sm bg-success text-gray-100"><i class="far fa-edit"></i></a>';
                    return editBtn;
                }
            },
            {
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var deleteBtn = '<button class="btnDeletePlace btn btn-circle btn-sm bg-danger text-gray-100" data-id="' + full.Id + '" data-name="' + full.Name + '"><i class="far fa-trash-alt"></i></button>';
                    return deleteBtn;
                }
            }
        ],
        "serverSide": true,
        "processing": true,
        "order": [0, "asc"],
    });


    $('body').on('click', '.btnDeletePlace', function () {
        var placeId = $(this).data('id');
        var placeName = $(this).data('name');
        var msg = 'Confirm to delete place: "' + placeName + '" ?';

        $('#deletePlaceModal .btn-primary').attr('placeId', placeId);
        $('#deletePlaceModal .modal-body').html(msg);

        $('#deletePlaceModal').modal('show');
    });

    $('#deletePlaceModal .btn-primary').click(function () {
        var placeId = $('#deletePlaceModal .btn-primary').attr('placeId');

        $('#deletePlaceModal .modal-body').html('Deleting ...');

        $.ajax({
            type: "POST",
            url: $('input[name="deletePlaceController"]').val(),
            data: {
                placeId: placeId,
            },
            success: function (data) {
                $('#deletePlaceModal .btn-primary').attr('placeId', '');
                $('#deletePlaceModal .modal-body').html('');
                $('#deletePlaceModal').modal('hide');

                var oTable = $('#placesDataTable').dataTable();
                oTable.fnDraw(false);
            },
            error: function (ex) {
                $('#deletePlaceModal .btn-primary').attr('placeId', '');
                $('#deletePlaceModal .modal-body').html('');
                console.log(ex);
            }
        });

    });
});