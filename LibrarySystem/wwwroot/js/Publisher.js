var domainName = window.location.origin;
const addpublisherlink = "/bookcategory/addPublisher";
const deletepublisherlink = "/bookcategory/deletepublisher/";
const updatepublisherlink = "/bookcategory/updatepublisher";
const getpublisherlink = "/bookcategory/getpublishers";
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




$(document).on('click', '.create-btn', function () {
    var row = $(this).closest('tr');
    var column1Value = row.find('td:eq(0)').text();
    var column2Value = row.find('td:eq(1)').text();
    var data = {
        Name: column1Value,
        Address: column2Value,
    };


    postData(domainName + addpublisherlink, data)
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
        deleteData(domainName + deletepublisherlink + id)
            .then(function (response) {
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
    var data = {
        Id: id,
        Name: column1Value,
        Address: column2Value,
    };
    var $button = $(this); 

    postData(domainName + updatepublisherlink, data)
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
            getData(domainName + getpublisherlink),
        ]);
        sortData(positondata, 'name');
        sortData(positondata, 'address');

        for (const item of positondata.$values) {
            if (item.name.toLowerCase().includes(search.val().toLowerCase()) || search.val() == "") {
                alluser++;
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var trElement = $('<tr></tr>').attr('data-value', item.id);
                    var tdElement1 = $('<td></td>').addClass('text-center').text(item.name);
                    var tdElement2 = $('<td></td>').addClass('text-center').text(item.address);
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
        totalPage = Math.ceil(alluser / usersonpage);
        if (alluser === min && alluser === usernumber)
        {
            pages--;
            displayData();
        }
        initializePagination();
    } catch (error) {
        console.error(error);

    }
}

