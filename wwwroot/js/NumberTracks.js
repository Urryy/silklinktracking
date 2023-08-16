class NumberTracks {

    constructor() {
        this.userId = null;
        this.numberTrackId = null;
    }

    async GetAll() {
        $.ajax({
            type: 'GET',
            url: "/NumberTracks/Index",
            success: function () {
            },
            failure: function () {
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }

    async Delete(id) {
        $('#modal').modal('hide');
        $.ajax({
            type: 'POST',
            url: "/NumberTracks/Delete",
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

    async Create(numberTrackViewModel) {
        $('#modal').modal('hide');
        $.ajax({
            type: 'POST',
            url: "/NumberTracks/Create",
            data: { "numberTrackViewModel": numberTrackViewModel },
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

    async Update(numberTrackViewModel) {
        $('#modal').modal('hide');
        $.ajax({
            type: 'POST',
            url: "/NumberTracks/Update",
            data: { "numberTrackViewModel": numberTrackViewModel },
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