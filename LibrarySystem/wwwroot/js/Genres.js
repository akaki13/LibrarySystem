var domainName = window.location.origin;
const addgenrelink = "/book/addgenre";
const deletegenrelink = "/book/deletegenre/";
const updategenrelink = "/book/updategenre";
const getgenrelink = "/book/getgenres";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";

$(".sort").on("click", async function () {
    sortBy = $(this).data('sort');
    $(".bi-sort-alpha-down").removeClass("bi-sort-alpha-down");
    $(".bi-sort-alpha-up").removeClass("bi-sort-alpha-up");
    if (orderBy === "asc") {
        orderBy = "desc";
        $(this).addClass('bi-sort-alpha-down');
    }
    else {
        $(this).addClass('bi-sort-alpha-up');
        orderBy = "asc";
    }
    displayGenre();
});
$(document).on('click', '.create-btn', function () {
    var row = $(this).closest('tr');
    var column1Value = row.find('td:eq(0)').text();
    var data = {
        Name: column1Value,
    };
    var $button = $(this); 
    postData(domainName + addgenrelink, data)
        .then(function (response) {
            row.attr('data-value', response);
            row.find('td:not(:last-child)').attr('contenteditable', false);
            $button.text('Edit').removeClass('create-btn').addClass('edit-btn'); 
            row.find('.destroy-btn').removeClass('destroy-btn').addClass('delete-btn');
        })
        .catch(function (error) {
            if (error.status === 400) {
                alert("Error: " + error.responseText);
            } else {
                alert("An error occurred while processing the request.");
            }
        });
});

$(document).on('click', '.delete-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var deleteModal = new bootstrap.Modal($('#deleteModal'));
    deleteModal.show();
    $(document).off('click', '.confirm-delete');
    $(document).on('click', '.confirm-delete', function () {
        deleteData(domainName + deletegenrelink + id);
        row.remove();
        deleteModal.hide();
    });
});

$(document).on('click', '.save-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var column1Value = row.find('td:eq(0)').text();
    var data = {
        Id: id,
        Name: column1Value,
    };
    var $button = $(this); 
    postData(domainName + updategenrelink, data)
        .then(function (response) {
            row.find('td:not(:last-child)').attr('contenteditable', false);
            $button.text('Edit').removeClass('save-btn').addClass('edit-btn'); 
            row.find('.cancel-btn').text('Delete').removeClass('cancel-btn').addClass('delete-btn');
        
        })
        .catch(function (error) {
            if (error.status === 400) {
                alert("Error: " + error.responseText); 
            } else {
                alert("An error occurred while processing the request."); 
            }
        });
});
displayGenre();
async function displayGenre() {
    body.empty();
    usernumber = 0;
    alluser = 0;
    try {
        const [data] = await Promise.all([
            getData(domainName + getgenrelink),
        ]);
        if (sortBy === "name") {
            if (orderBy === "asc") {
                data.$values.sort((a, b) => a.name.localeCompare(b.name));
            }
            else if (orderBy === "desc") {
                data.$values.sort((a, b) => b.name.localeCompare(a.name));
            }
        }
        for (const item of data.$values) {
            alluser++;
            var sum = usersonpage * pages;
            var min = sum - usersonpage;
            if (usernumber < sum && usernumber >= min) {
                var trElement = $('<tr></tr>').attr('data-value', item.id);
                var tdElement1 = $('<td></td>').addClass('text-center').text(item.name);
                var tdElement2 = $('<td></td>').addClass('text-center').html('<button class="edit-btn">Edit</button> <button class="delete-btn">Delete</button>');
                trElement.append(tdElement1, tdElement2);
                body.prepend(trElement);
                usernumber++
            }
            if (usernumber < min) {
                usernumber++;
            }
        }
        
        totalPage = Math.ceil(alluser / usersonpage);    
        initializePagination();
    } catch (error) {
        console.error(error);
    }
}

function initializePagination() {

    $('#pagination-container').MyPagination({
        totalPages: totalPage,
        visiblePages: 5,
        onPageClick: function (pageNumber) {
            pages = pageNumber;
            displayPupblisher();
        },
        currentPage: pages
    });
    
}