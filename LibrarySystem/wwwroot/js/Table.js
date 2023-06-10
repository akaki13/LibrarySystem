$(document).on('click', '.cancel-btn', function () {
    var row = $(this).closest('tr');

    var originalValues = row.data('original-values');
    row.find('td:not(:last-child)').each(function (index) {
        $(this).text(originalValues[index]);
    });

    row.find('td:not(:last-child)').attr('contenteditable', false);

    row.find('.save-btn').text('Edit').removeClass('save-btn').addClass('edit-btn');
    $(this).text('Delete').removeClass('cancel-btn').addClass('delete-btn');
});

$(document).on('click', '.edit-btn', function () {
    var row = $(this).closest('tr');
    row.data('original-values', row.find('td:not(:last-child)').map(function () {
        return $(this).text();
    }).get());

    row.find('td:not(:last-child)').attr('contenteditable', true);

    $(this).text('Save').removeClass('edit-btn').addClass('save-btn');

    row.find('.delete-btn').text('Cancel').removeClass('delete-btn').addClass('cancel-btn');
});

$(document).on('click', '.destroy-btn', function () {
    var row = $(this).closest('tr');
    row.remove();
});

$('#add-row-btn').click(function () {
    var newRow = $('<tr>');
    var newRowContent = '';
    var rowCount = $('table thead th').length;
    console.log(rowCount)
    for (var i = 1; i < rowCount; i++) {
        newRowContent += '<td contenteditable="true" class="text-center" ></td>';
    }
        newRowContent += '<td class="text-center"><button class="create-btn">Save</button> <button class="destroy-btn">Delete</button></td>';
        newRow.html(newRowContent);
        $('table tbody').prepend(newRow);
});