$(document).ready(function () {
    $('#postsDataTable').DataTable({
        "ajax": {
            "type": "POST",
            "url": $('input[name="getPostsController"]').val(),
            "dataType": "json",
        },
        "columns": [
            { "data": "Id", "name": "Id" },
            { "data": "Content", "name": "Content" },
            { "data": "Author.Name", "name": "Author" },
            { "data": "Rating", "name": "Rating" },
            { "data": "TotalLike", "name": "TotalLike" },
            { "data": "TotalComment", "name": "TotalComment" },
            {
                "searchable": false,
                "sortable": false,
                "render": function (data, type, full, meta) {
                    var deleteBtn = '<button class="btnDeletePost btn btn-circle btn-sm bg-danger text-gray-100" data-id="' + full.Id + '" data-name="' + full.Name + '"><i class="far fa-trash-alt"></i></button>';
                    return deleteBtn;
                }
            }
        ],
        "serverSide": true,
        "processing": true,
        "order": [0, "asc"],
    });


    $('body').on('click', '.btnDeletePost', function () {
        var postId = $(this).data('id');
        var msg = 'Confirm to delete post: "' + postId + '" ?';

        $('#deletePostModal .btn-primary').attr('postId', postId);
        $('#deletePostModal .modal-body').html(msg);

        $('#deletePostModal').modal('show');
    });

    $('#deletePostModal .btn-primary').click(function () {
        var postId = $('#deletePostModal .btn-primary').attr('postId');

        $('#deletePostModal .modal-body').html('Deleting ...');

        $.ajax({
            type: "POST",
            url: $('input[name="deletePostController"]').val(),
            data: {
                postId: postId,
            },
            success: function (data) {
                $('#deletePostModal .btn-primary').attr('postId', '');
                $('#deletePostModal .modal-body').html('');
                $('#deletePostModal').modal('hide');

                var oTable = $('#postsDataTable').dataTable();
                oTable.fnDraw(false);
            },
            error: function (ex) {
                $('#deletePostModal .btn-primary').attr('postId', '');
                $('#deletePostModal .modal-body').html('');
                console.log(ex);
            }
        });

    });
});