$(document).ready(function () {
    $('#toursDataTable').DataTable({
        "ajax": {
            "type": "POST",
            "url": $('input[name="getToursController"]').val(),
            "dataType": "json",
            "data": {
                "tourInfoId": $('input[name="tourInfoId"').val(),
            }
        },
        "columns": [
            { "data": "Name", "name": "Name" },
            { "data": "StartDay", "name": "StartDay" },
            { "data": "EndDay", "name": "EndDay" },
            { "data": "TotalDay", "name": "TotalDay" },
            { "data": "TotalNight", "name": "TotalNight" },
            { "data": "Price", "name": "Price" },
            {
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var editBtn = '<a href="/admin/tour/edit/?tourInfoId=' + $('input[name="tourInfoId"').val() + '&tourId=' + full.Id + '" class="btn btn-circle btn-sm bg-success text-gray-100"><i class="far fa-edit"></i></a>';
                    return editBtn;
                }
            }
        ],
        "serverSide": true,
        "processing": true,
        "order": [0, "asc"],
    });


    $('body').on('click', '.btnDeleteTour', function () {
        var tourId = $(this).data('id');
        var tourName = $(this).data('name');
        var msg = 'Confirm to delete tour info: "' + tourName + '" ?';

        $('#deleteTourModal .btn-primary').attr('tourid', tourId);
        $('#deleteTourModal .modal-body').html(msg);

        $('#deleteTourModal').modal('show');
    });

    $('#deleteTourModal .btn-primary').click(function () {
        var tourId = $('#deleteTourModal .btn-primary').attr('tourid');

        $('#deleteTourModal .modal-body').html('Deleting ...');

        $.ajax({
            type: "POST",
            url: $('input[name="deleteTourInfoController"]').val(),
            data: {
                tourId: tourId,
            },
            success: function (data) {
                $('#deleteTourModal .btn-primary').attr('tourid', '');
                $('#deleteTourModal .modal-body').html('');
                $('#deleteTourModal').modal('hide');

                var oTable = $('#deleteTourModal').dataTable();
                oTable.fnDraw(false);
            },
            error: function (ex) {
                $('#deleteTourModal .btn-primary').attr('tourid', '');
                $('#deleteTourModal .modal-body').html('');
                console.log(ex);
            }
        });

    });
});