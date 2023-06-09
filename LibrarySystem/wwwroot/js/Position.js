var domainName = window.location.origin;
const addPositionlink = "/user/addposition";
const deletepositionlink = "/user/deleteposition/";
const updatepositionlink = "/user/updateposition";
const getpositionlink = "/user/getposition";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchTitle = $("#title");
const searchDescription = $("#description");
const searchSalary = $("#salary");

$("#submit").on("click", async function () {
    pages = 1;
    displayData();
});

$(document).on('click', '.create-btn', function () {
    var row = $(this).closest('tr');
    var column1Value = row.find('td:eq(0)').text();
    var column2Value = row.find('td:eq(1)').text();
    var column3Value = row.find('td:eq(2)').text();
    var data = {
        Title: column1Value,
        Description: column2Value,
        Salary: column3Value,
    };
    var $button = $(this);
    postData(domainName + addPositionlink, data)
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
    $(document).on('click', '.confirm-delete', function () {
        deleteData(domainName + deletepositionlink + id).then(function (response) {
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
        Title: column1Value,
        Description: column2Value,
        Salary: Number(column3Value),
    };
    var $button = $(this); 
    postData(domainName + updatepositionlink, data)
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
        const [positondata] = await Promise.all([
            getData(domainName + getpositionlink),
        ]);
        sortData(positondata, 'title');
        sortData(positondata, 'description');
        sortIntData(positondata, 'salary')

        for (const item of positondata.$values) {
            const titleMatch = CheckSearch(searchTitle, item.title);
            const descriptionMatch = CheckSearch(searchDescription, item.description); 
            const salaryMatch = CheckSearch(searchSalary, item.salary);
            if (titleMatch && descriptionMatch && salaryMatch)  {
                alluser++;
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var trElement = $('<tr></tr>').attr('data-value', item.id);
                    var tdElement1 = $('<td></td>').addClass('text-center').text(item.title);
                    var tdElement2 = $('<td></td>').addClass('text-center').text(item.description);
                    var tdElement3 = $('<td></td>').addClass('text-center').text(item.salary);
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
            displayPosition();
        },
        currentPage: pages
    });
}*/