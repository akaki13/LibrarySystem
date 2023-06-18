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
    displayPosition();
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
    displayPosition();
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
            displayPosition();
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
            displayPosition();
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
            if (error.status === 400) {
                alert("Error: " + error.responseText);
            } else {
                alert("An error occurred while processing the request.");
            }
        });
});
displayPosition();
async function displayPosition() {
    body.empty();
    usernumber = 0;
    alluser = 0;
    try {
        const [positondata] = await Promise.all([
            getData(domainName + getpositionlink),
        ]);
        if (sortBy === "title") {
            if (orderBy === "asc") {
                positondata.$values.sort((a, b) => a.title.localeCompare(b.title));
            }
            else if (orderBy === "desc") {
                positondata.$values.sort((a, b) => b.title.localeCompare(a.title));
            }
        }
        if (sortBy === "description") {
            if (orderBy === "asc") {
                positondata.$values.sort((a, b) => a.description.localeCompare(b.description));
            }
            else if (orderBy === "desc") {
                positondata.$values.sort((a, b) => b.description.localeCompare(a.description));
            }
        }
        if (sortBy === "salary") {
            if (orderBy === "asc") {
                positondata.$values.sort((a, b) => a.salary - b.salary);
            }
            else if (orderBy === "desc") {
                positondata.$values.sort((a, b) => b.salary - a.salary);
            }
        }
        for (const item of positondata.$values) {
            const titleMatch = searchTitle.val() ? item.title.toLowerCase().includes(searchTitle.val().toLowerCase()) : true;
            const descriptionMatch = searchDescription.val() ? item.description.toLowerCase().includes(searchDescription.val().toLowerCase()) : true;
            const salaryMatch = searchSalary.val() ? item.salary.toString().toLowerCase().includes(searchSalary.val().toString().toLowerCase()) : true;

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
            displayPosition();
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
            displayPosition();
        },
        currentPage: pages
    });
}