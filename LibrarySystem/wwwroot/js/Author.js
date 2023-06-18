var domainName = window.location.origin;
const addauthorlink = "/bookcategory/addauthor";
const deleteauthorlink = "/bookcategory/deleteauthor/";
const updateauthorlink = "/bookcategory/updateauthor";
const getauthorlink = "/bookcategory/getauthor";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchSurname = $("#surname");
const searchName = $("#name");

$("#submit").on("click", async function () {
    pages = 1;
    displayAuthor();
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
    displayAuthor();
});
$(document).on('click', '.create-btn', function () {
    var row = $(this).closest('tr');
    var column1Value = row.find('td:eq(0)').text();
    var column2Value = row.find('td:eq(1)').text();
    var data = {
        Name: column1Value,
        Surname: column2Value,
    };
    var $button = $(this); 
    postData(domainName + addauthorlink, data)
        .then(function (response) {
            displayAuthor();
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
    $(document).on('click', '.confirm-delete',async function () {
        deleteData(domainName + deleteauthorlink + id)
            .then(function (response) {
                displayAuthor();
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
    var data = {
        Id: id,
        Name: column1Value,
        Surname: column2Value,
    };
    var $button = $(this); 
    postData(domainName + updateauthorlink, data)
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
displayAuthor();
async function displayAuthor() {
    body.empty();
    usernumber = 0;
    alluser = 0;
    try {
        const [data] = await Promise.all([
            getData(domainName + getauthorlink),
        ]);
        if (sortBy === "name") {
            if (orderBy === "asc") {
                data.$values.sort((a, b) => a.name.localeCompare(b.name));
            }
            else if (orderBy === "desc") {
                data.$values.sort((a, b) => b.name.localeCompare(a.name));
            }
        }
        if (sortBy === "surname") {
            if (orderBy === "asc") {
                data.$values.sort((a, b) => a.surname.localeCompare(b.surname));
            }
            else if (orderBy === "desc") {
                data.$values.sort((a, b) => b.surname.localeCompare(a.surname));
            }
        }
        for (const item of data.$values) {  
            const nameMatch = searchName.val() ? item.name.toLowerCase().includes(searchName.val().toLowerCase()) : true;
            const surnameMatch = searchSurname.val() ? item.surname.toLowerCase().includes(searchSurname.val().toLowerCase()) : true;   
            if (nameMatch && surnameMatch) {
                alluser++;
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var trElement = $('<tr></tr>').attr('data-value', item.id);
                    var tdElement1 = $('<td></td>').addClass('text-center').text(item.name);
                    var tdElement2 = $('<td></td>').addClass('text-center').text(item.surname);
                    var tdElement3 = $('<td></td>').addClass('text-center').html('<button class="edit-btn">Edit</button> <button class="delete-btn">Delete</button>');
                    trElement.append(tdElement1, tdElement2, tdElement3);
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
            displayAuthor();
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
            displayAuthor();
        },
        currentPage: pages
    });
    
}