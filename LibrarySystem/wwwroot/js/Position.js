const addPositionlink = "/user/addposition";
var domainName = window.location.origin;
const deletepositionlink = "/user/deleteposition/";
const updatepositionlink = "/user/updateposition";
$('#add-row-btn').click(function () {
    // Create a new row
    var newRow = $('<tr>');
    var newRowContent = '';
    var rowCount = $('table tbody tr').length;

    // Add the cells to the new row
    newRowContent += '<td contenteditable="true" class="text-center" ></td>';
    newRowContent += '<td contenteditable="true" class="text-center"></td>';
    newRowContent += '<td contenteditable="true" class="text-center"></td>';
    newRowContent += '<td class="text-center"><button class="create-btn">Save</button> <button class="destroy-btn">Delete</button></td>';
    
    // Set the HTML content of the new row
    newRow.html(newRowContent);

    // Append the new row to the table body
    $('table tbody').prepend(newRow);
});
$(document).on('click', '.create-btn', function () {
    var row = $(this).closest('tr');

    // Retrieve the values from the editable columns
    var column1Value = row.find('td:eq(0)').text();
    var column2Value = row.find('td:eq(1)').text();
    var column3Value = row.find('td:eq(2)').text();
    var data = {
        Title: column1Value,
        Description: column2Value,
        Salary: column3Value,
    };

    var $button = $(this); // Store the reference to $(this)

    postData(domainName + addPositionlink, data)
        .then(function (response) {
            row.attr('data-value', response);
            row.find('td:not(:last-child)').attr('contenteditable', false);
            console.log("asdasda");

            $button.text('Edit').removeClass('create-btn').addClass('edit-btn'); // Use the stored reference

            row.find('.destroy-btn').removeClass('destroy-btn').addClass('delete-btn');
        })
        .catch(function (error) {
            if (error.status === 400) {
                alert("Error: " + error.responseText); // Display the error message to the user
            } else {
                alert("An error occurred while processing the request."); // Display a generic error message
            }
        });
});

function postData(url, data) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (response, status) {
                resolve(response);
            },
            error: function (xhr, status, error) {
                reject({
                    status: xhr.status,
                    responseText: xhr.responseText
                });
            }
        });
    });
}
function deleteData(url) {
    $.ajax({
        url: url,
        type: 'DELETE',
        success: function (response) {
            // Handle the success response
            console.log(response);
            // Refresh or update the UI as needed
        },
        error: function (xhr, status, error) {
            // Handle the error response
            console.log(error);
            // Display an error message or handle the error case
        }
    });
}

//delete new creted row
$(document).on('click', '.destroy-btn', function () {
    var row = $(this).closest('tr');
    row.remove();  
});

// Add delete button click event handler
$(document).on('click', '.delete-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var confirmDelete = confirm("Are you sure you want to delete this row?");
    if (confirmDelete) {
        deleteData(domainName + deletepositionlink +id);
        row.remove();
    }
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

// Add save button click event handler
$(document).on('click', '.save-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var column1Value = row.find('td:eq(0)').text();
    var column2Value = row.find('td:eq(1)').text();
    var column3Value = row.find('td:eq(2)').text();
    var data = {
        Id: id,
        Title: column1Value,
        Description: column2Value,
        Salary: Number(column3Value),
    };
    var $button = $(this); // Store the reference to $(this)

    postData(domainName + updatepositionlink, data)
        .then(function (response) {
            row.find('td:not(:last-child)').attr('contenteditable', false);
            console.log("asdasda");

            $button.text('Edit').removeClass('save-btn').addClass('edit-btn'); // Use the stored reference

            row.find('.cancel-btn').text('Delete').removeClass('cancel-btn').addClass('delete-btn');
        
        })
        .catch(function (error) {
            if (error.status === 400) {
                alert("Error: " + error.responseText); // Display the error message to the user
            } else {
                alert("An error occurred while processing the request."); // Display a generic error message
            }
        });
});

// Add cancel button click event handler
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
