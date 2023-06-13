var domainName = window.location.origin;
const addlink = "/book/addstorage";
const deletelink = "/book/deletestorage/";
const updatelink = "/book/updatestorage";
const getlink = "/book/getstorage";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const search = $("#search");

$("#submit").on("click", async function () {
    pages = 1;
    displayStorage();
});
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
    displayStorage();
});

$(document).on('click', '.create-btn', function () {
    var row = $(this).closest('tr');
    var column1Value = row.find('td:eq(0)').text();
    var column2Value = row.find('td:eq(1)').text();
    var column3Value = row.find('td:eq(2)').text();
    var data = {
        Saction: column1Value,
        Row: column2Value,
        Shell: column3Value,
    };
    var $button = $(this); 
    postData(domainName + addlink, data)
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
        deleteData(domainName + deletelink + id);
        row.remove();
        deleteModal.hide();
    });
});

$(document).on('click', '.save-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var column1Value = row.find('td:eq(0)').text();
    var column2Value = row.find('td:eq(1)').text();
    var column3Value = row.find('td:eq(2)').text();
    var data = {
        Id: id,
        Saction: column1Value,
        Row: column2Value,
        Shell: column3Value,
    };
    var $button = $(this); 
    postData(domainName + updatelink, data)
        .then(function (response) {
            row.find('td:not(:last-child)').attr('contenteditable', false);
            console.log("asdasda");
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
displayStorage();
async function displayStorage() {
    body.empty();
    usernumber = 0;
    alluser = 0;
    try {
        const [data] = await Promise.all([
            getData(domainName + getlink),
        ]);
        if (sortBy === "saction") {
            if (orderBy === "asc") {
                data.$values.sort((a, b) => a.saction.localeCompare(b.saction));
            }
            else if (orderBy === "desc") {
                data.$values.sort((a, b) => b.saction.localeCompare(a.saction));
            }
        }
        if (sortBy === "row") {
            if (orderBy === "asc") {
                data.$values.sort((a, b) => a.row.localeCompare(b.row));
            }
            else if (orderBy === "desc") {
                data.$values.sort((a, b) => b.row.localeCompare(a.row));
            }
        }
        if (sortBy === "shell") {
            if (orderBy === "asc") {
                data.$values.sort((a, b) => a.shell.localeCompare(b.shell));
            }
            else if (orderBy === "desc") {
                data.$values.sort((a, b) => b.shell.localeCompare(a.shell));
            }
        }
        for (const item of data.$values) {
            const searchTerm = search.val().toLowerCase();
            const sactionMatch = item.saction.toLowerCase().includes(searchTerm);
            const rowMatch = item.row.toLowerCase().includes(searchTerm);
            const shellMatch = item.shell.toLowerCase().includes(searchTerm);

            if (sactionMatch || rowMatch || shellMatch || search.val() == "") {
                alluser++;
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var trElement = $('<tr></tr>').attr('data-value', item.id);
                    var tdElement1 = $('<td></td>').addClass('text-center').text(item.saction);
                    var tdElement2 = $('<td></td>').addClass('text-center').text(item.row);
                    var tdElement3 = $('<td></td>').addClass('text-center').text(item.shell);
                    var tdElement4 = $('<td></td>').addClass('text-center').html('<button class="edit-btn">Edit</button> <button class="delete-btn">Delete</button>');
                    trElement.append(tdElement1, tdElement2, tdElement3, tdElement4);
                    body.prepend(trElement);
                    usernumber++
                }
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
            displayStorage();
        },
        currentPage: pages
    });
    
}