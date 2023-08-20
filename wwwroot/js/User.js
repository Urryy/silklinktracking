class User {
    constructor() {
        this.userId = null;
    }

    async Delete(id) {
        $('#modal').modal('hide');
        $.ajax({
            type: 'POST',
            url: "/Users/Delete",
            data: { "id": id },
            success: function (response) {
                $('#modal').modal('hide');
                location.reload();
            },
            failure: function () {
                $('#modal').modal('hide')
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
}