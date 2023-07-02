var domainName = window.location.origin;
const addlink = "/bookcategory/addstorage";
const deletelink = "/bookcategory/deletestorage/";
const updatelink = "/bookcategory/updatestorage";
const getlink = "/bookcategory/getstorage";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchName = $("#name");
const searchLocation = $("#location");
const searchCapacity = $("#capacity");

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
    var column2Value = row.find('td:eq(1)').text();
    var column3Value = row.find('td:eq(2)').text();
    var data = {
        Name: column1Value,
        Location: column2Value,
        Capacity: column3Value,
    };
    var $button = $(this); 
    postData(domainName + addlink, data)
        .then(function (response) {
            displayData();
        })
        .catch(function (error) {
            var exampleModal = new bootstrap.Modal($('#exampleModal'));
            $("#modalbody").html(createModalBody);
            exampleModal.show();
        });
});

$(document).on('click', '.delete-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var deleteModal = new bootstrap.Modal($('#deleteModal'));
    deleteModal.show();
    $(document).off('click', '.confirm-delete');
    $(document).on('click', '.confirm-delete', async function () {
        deleteData(domainName + deletelink + id).then(function (response) {
            displayData();
            deleteModal.hide();
        })
            .catch(function (error) {
                var exampleModal = new bootstrap.Modal($('#exampleModal'));
                $("#modalbody").html(delteModalBody);
                exampleModal.show();
            });
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
        Name: column1Value,
        Location: column2Value,
        Capacity: column3Value,
    };
    var $button = $(this); 
    postData(domainName + updatelink, data)
        .then(function (response) {
            row.find('td:not(:last-child)').attr('contenteditable', false);
            $button.text('Edit').removeClass('save-btn').addClass('edit-btn'); 
            row.find('.cancel-btn').text('Delete').removeClass('cancel-btn').addClass('delete-btn');
        
        })
        .catch(function (error) {
            var exampleModal = new bootstrap.Modal($('#exampleModal'));
            $("#modalbody").html(createModalBody);
            exampleModal.show();
        });
});
displayData();
async function displayData() {
    body.empty();
    usernumber = 0;
    alluser = 0;
    try {
        const [data] = await Promise.all([
            getData(domainName + getlink),
        ]);
        sortData(data, 'name');
        sortData(data, 'location');
        sortIntData(data, 'capacity');

        for (const item of data.$values) {
            const nameMatch = CheckSearch(searchName, item.name);
            const locatiomMatch = CheckSearch(searchLocation, item.location);
            const capacityMatch = CheckSearch(searchCapacity, item.capacity);

            if (nameMatch && locatiomMatch && capacityMatch) {
                alluser++;
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var trElement = $('<tr></tr>').attr('data-value', item.id);
                    var tdElement1 = $('<td></td>').addClass('text-center').text(item.name);
                    var tdElement2 = $('<td></td>').addClass('text-center').text(item.location);
                    var tdElement3 = $('<td></td>').addClass('text-center').text(item.capacity);
                    var tdElement4 = $('<td></td>').addClass('text-center').html('<button class="edit-btn">Edit</button> <button class="delete-btn">Delete</button>');
                    trElement.append(tdElement1, tdElement2, tdElement3, tdElement4);
                    body.append(trElement);
                    usernumber++;
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
            displayStorage();
        },
        currentPage: pages
    });
    
}*/