var domainName = window.location.origin;
const getbooklink = "/book/getbook";
const getborrowlink = "/borrow/getborrow";
const getpersonlink = "/user/getperson";
const deleteBorrowlink = "/borrow/DeleteBorrow/";
const bookReturnedlink = "/borrow/BookReturned/";
const body = $("#body");
<<<<<<< HEAD
var usersonpage = 5;
=======
>>>>>>> rdlc
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchBook = $("#book");
const searchPerson = $("#person");
const searchTake = $("#takeTime");
const searchReturn = $("#returnedTime");
const searchActualReturn = $("#actualReturnedTime");
const searchComment = $("#comment");
const Submit = $("#submit");

    
Submit.on('click', function () {
    pages = 1;
    displayData();
});

displayData();

async function displayData() {
    body.empty();
    usernumber = 0;
    alluser = 0;
    const [bookdata, borrowdata, persondata] = await Promise.all([
        getData(domainName + getbooklink),
        getData(domainName + getborrowlink),
        getData(domainName + getpersonlink),
    ]);


    sortDataByTable(borrowdata, 'name', bookdata, 'bookId');
<<<<<<< HEAD
    sortDataByTable(borrowdata, 'firstname', persondata, 'bookId' );
=======
    sortDataByTable(borrowdata, 'firstname', persondata, 'personId' );
>>>>>>> rdlc
    sortData(borrowdata, 'returnedTime');
    sortData(borrowdata, 'actualReturnedTime');
    sortData(borrowdata, 'comment');
    sortData(borrowdata, 'takeTime');
<<<<<<< HEAD
=======

>>>>>>> rdlc
    try {
        for (const item of borrowdata.$values) {
            var personTitles = getNames(item.personId, persondata, ['firstname', 'lastname']);
            var bookTitles = getNames(item.bookId, bookdata, ['name']);
            var formattedDate1 = moment(item.takeTime).format("YYYY-MM-DD");
            var formattedDate2 = moment(item.returnedTime).format("YYYY-MM-DD");
            var formattedDate3 = item.actualReturnedTime ? moment(item.actualReturnedTime).format("YYYY-MM-DD") : "N/A";

            const personMatch = CheckSearch(searchPerson, personTitles);
            const bookMatch = CheckSearch(searchBook, bookTitles);
            const takeMatch = CheckSearch(searchTake, formattedDate1);
            const returnMatch = CheckSearch(searchReturn, formattedDate2);
            const actualReturnMatch = CheckSearch(searchActualReturn, formattedDate3);
            const commentMatch = CheckSearch(searchComment, item.comment);
<<<<<<< HEAD

=======
          
>>>>>>> rdlc

            if (personMatch && bookMatch && takeMatch && returnMatch && actualReturnMatch && commentMatch) {
                alluser++
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var trElement = $('<tr></tr>').attr('data-value', item.id);
                    var tdElement1 = creatTdElement(bookTitles);
                    var tdElement2 = creatTdElement(personTitles);
                    var tdElement3 = creatTdElement(formattedDate1);
                    var tdElement4 = creatTdElement(formattedDate3);
                    var tdElement5 = creatTdElement(formattedDate2);
                    var tdElement6 = creatTdElement(item.comment);
                    if (item.actualReturnedTime != null) {
                        var tdElement8 = $('<td></td>').addClass('text-center').html('<button class="dismissed">Book Returned</button> <button class="delete-btn">Delete</button>');
                    }
                    else {
                        var tdElement8 = $('<td></td>').addClass('text-center').html('<button class="return-btn">Book Returned</button> <button class="delete-btn">Delete</button>');
                    }
                    trElement.append(tdElement1, tdElement2, tdElement3, tdElement4, tdElement5, tdElement6, tdElement8);
                    body.append(trElement);
                    usernumber++
                }

                if (usernumber < min) {
                    usernumber++;
                }
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

$(document).on('click', '.delete-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var deleteModal = new bootstrap.Modal($('#deleteModal'));
    deleteModal.show();
    $(document).off('click', '.confirm-delete');
    $(document).on('click', '.confirm-delete', async function () {
<<<<<<< HEAD
        deleteData(domainName + deleteBorrowlink + id)
=======
        postData(domainName + deleteBorrowlink + id)
>>>>>>> rdlc
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

$(document).on('click', '.return-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var deleteModal = new bootstrap.Modal($('#returnModal'));
    deleteModal.show();
    $(document).off('click', '.confirm-return');
    $(document).on('click', '.confirm-return', async function () {
        postData(domainName + bookReturnedlink + id)
            .then(function (response) {
                displayData();
                deleteModal.hide();
            })
            .catch(function (error) {
                var exampleModal = new bootstrap.Modal($('#exampleModal'));
                $("#modalbody").html(createModalBody);
                exampleModal.show();
            });
    });
});