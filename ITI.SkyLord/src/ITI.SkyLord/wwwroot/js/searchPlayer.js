$(document).ready(function () {
    $("#searchPlayer").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Player/Players',
                type: 'GET',
                cache: false,
                data: { name: request.term },
                dataType: 'json',
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item,
                            value: item + ""
                        }
                    }))
                }
            });
        },
        minLength: 1,
    });
});