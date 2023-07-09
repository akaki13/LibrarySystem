var domainName = window.location.origin;
const getpersonlink = "/user/getperson";
const updateperson = "/user/updateperson/";
const deleteperson = "/user/DeletePerson/";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchName = $("#firstname");
const searchLasrname = $("#lastname");
const searchDoB = $("#dateOfBirth");
const searchPhone = $("#phone");
const searchEmail = $("#email");
const searchAddress = $("#address");
const Submit = $("#submit");

$(document).ready(async function () {
   

    $(document).on('click', '.edit-btn', function () {
        var row = $(this).closest('tr');
        id = row.data("value");
        window.location = updateperson + id;
    });

    const [persondata ] = await Promise.all([
        getData(domainName + getpersonlink),
        ]);

    const phoneList = persondata.$values.map(person => ({
        value: person.phone,
    }));
    const addressList = persondata.$values.map(person => ({
        value: person.address,
    }));
    const emailList = persondata.$values.map(person => ({
        value: person.email,
    }));
    const dateOfBirthList = persondata.$values.map(person => ({
        value: formatDate(person.dateOfBirth),
    }));

    searchPhone.autocomplete({
        source: phoneList,
        minLength: 0,
        multiselect: true,
    });
    searchAddress.autocomplete({
        source: addressList,
        minLength: 0,
        multiselect: true,
    });
    searchEmail.autocomplete({
        source: emailList,
        minLength: 0,
        multiselect: true,
    });
    searchDoB.autocomplete({
        source: dateOfBirthList,
        minLength: 0,
        multiselect: true,
    });
/*    searchGenre.on('click', function () {
        searchGenre.autocomplete('search', '');
    });*/
    
    Submit.on('click', function () {
        pages = 1;
        displayData();
    });
    /*function initializePagination() {
        $('#pagination-container').MyPagination({
            totalPages: totalPage,
            visiblePages: 5,
            onPageClick: function (pageNumber) {
                pages = pageNumber;
                displayBoook();
            },
            currentPage: pages
        });
    }*/
    
});
displayData();
$(document).on('click', '.delete-btn', function () {
    var row = $(this).closest('tr');
    id = row.data("value");
    var deleteModal = new bootstrap.Modal($('#deleteModal'));
    deleteModal.show();
    $(document).off('click', '.confirm-delete');
    $(document).on('click', '.confirm-delete', async function () {
        deleteData(domainName + deleteperson + id)
            .then(function (response) {
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
async function displayData() {
    body.empty();
    usernumber = 0;
    alluser = 0;
    const [personsdata] = await Promise.all([
        getData(domainName + getpersonlink),
    ]);


    sortData(personsdata, 'firstname');
    sortData(personsdata, 'lastname');
    sortData(personsdata, 'phone');
    sortData(personsdata, 'address');
    sortData(personsdata, 'email');
    sortData(personsdata, 'dateOfBirth');
    /*bookdata.$values.sort((a, b) => {
        const genreA = getBookGenreTitles(a);
        const genreB = getBookGenreTitles(b);
        console.log(genreB)
        return genreB.localeCompare(genreA);
    });*/
    try {
        for (const item of personsdata.$values) {
            var formattedDate = moment(item.dateOfBirth).format("YYYY-MM-DD");
            const addressMatch = CheckSearch(searchAddress, item.address);
            const emailMatch = CheckSearch(searchEmail, item.email);
            const phoneMatch = CheckSearch(searchPhone, item.phone);
            const dateOfBirthMatch = CheckSearch(searchDoB, formattedDate);
            const lastnameMatch = CheckSearch(searchLasrname, item.lastname);
            const nameMatch = CheckSearch(searchName, item.firstname);
            if (addressMatch && emailMatch && phoneMatch && dateOfBirthMatch && lastnameMatch && nameMatch) {
                alluser++
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var trElement = $('<tr></tr>').attr('data-value', item.id);
                    var tdElement1 = creatTdElement(item.firstname);
                    var tdElement2 = creatTdElement(item.lastname);
                    var tdElement3 = creatTdElement(formattedDate);
                    var tdElement4 = creatTdElement(item.phone);
                    var tdElement5 = creatTdElement(item.email);
                    var tdElement6 = creatTdElement(item.address);
                    var tdElement7 = $('<td></td>').addClass('text-center').html('<button class="edit-btn">Edit</button> <button class="delete-btn">Delete</button>');
                    trElement.append(tdElement1, tdElement2, tdElement3, tdElement4, tdElement5, tdElement6, tdElement7);
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
