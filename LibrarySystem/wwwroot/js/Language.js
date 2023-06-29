var domainName = window.location.origin;
const addPositionlink = "/bookcategory/addlanguage";
const deletepositionlink = "/bookcategory/deletelanguage/";
const updatepositionlink = "/bookcategory/updatelanguage";
const getpositionlink = "/bookcategory/getlanguage";
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
    displayData();
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
    pages = 1;
    displayData();
});
$(document).on('click', '.create-btn', function () {
    var row = $(this).closest('tr');
    var column1Value = row.find('td:eq(0)').text();
    var data = {
        Title: column1Value,
    };
    var $button = $(this);
    postData(domainName + addPositionlink, data)
        .then(function (response) {
            displayData();
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
        deleteData(domainName + deletepositionlink + id).then(function (response) {
            displayData();
            deleteModal.hide();
        })
            .catch(function (error) {
                if (error.status === 400) {
                    alert("Error: " + error.responseText);
                } else {
                    alert("An error occurred while processing the request.");
                }
            });
    });
});

$(document).on('click', '.save-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var column1Value = row.find('td:eq(0)').text();
    var data = {
        Id: id,
        Title: column1Value,
    };
    var $button = $(this); 
    postData(domainName + updatepositionlink, data)
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

displayData();
async function displayData() {
    body.empty();
    usernumber = 0;
    alluser = 0;
    try {
        const [data] = await Promise.all([
            getData(domainName + getpositionlink),
        ]);
        sortData(data, 'title');

        for (const item of data.$values) {
            if (item.title.toLowerCase().includes(search.val().toLowerCase()) || search.val() == "") {
                alluser++;
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var trElement = $('<tr></tr>').attr('data-value', item.id);
                    var tdElement1 = $('<td></td>').addClass('text-center').text(item.title);
                    var tdElement2 = $('<td></td>').addClass('text-center').html('<button class="edit-btn">Edit</button> <button class="delete-btn">Delete</button>');
                    trElement.append(tdElement1, tdElement2);
                    body.append(trElement);
                    usernumber++
                }
            }
            if (usernumber < min) {
                usernumber++;
            }
        }
        if (alluser === min && alluser === usernumber) {
            pages--;
            displayData();
        }
        totalPage = Math.ceil(alluser / usersonpage);
        initializePagination();
    } catch (error) {
        console.error(error);
        
    }
}

/*function initializePagination() {

    $('#pagination-container').MyPagination({
        totalPages: totalPage,
        visiblePages: 5,
        onPageClick: function (pageNumber) {
            pages = pageNumber;
            displayLanguage();
        },
        currentPage: pages
    });
}*/