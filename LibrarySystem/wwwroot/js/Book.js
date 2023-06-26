var domainName = window.location.origin;
const getbookstoragelink = "/book/getbookstorage";
const getbookpublisherlink = "/book/getbookpublisher";
const getbooklanguagelink = "/book/getbooklanguage";
const getbookgenrelink = "/book/getbookgenre";
const getbookauthorlink = "/book/getbookauthor";
const getbooklink = "/book/getbook";
const getauthorlink = "/bookcategory/getauthor";
const getgenrelink = "/bookcategory/getgenres";
const getpublisherlink = "/bookcategory/getpublishers";
const getlanguagelink = "/bookcategory/getlanguage";
const getstoragelink = "/bookcategory/getstorage";
const deletebook = "/book/deletebook/";
const updateBook = "/book/updatebook/";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchDescription = $("#description");
const searchName = $("#name");
const searchGenre = $("#genre");
const searchPublisher = $("#publisher");
const searchShelf = $("#shelf");
const searchLanguage = $("#language");
const searchAuthor = $("#author");
const Submit = $("#submit");



$(document).ready(async function () {
    $(document).on('click', '.delete-btn', function () {
        var row = $(this).closest('tr');
        id = row.data("value");
        var deleteModal = new bootstrap.Modal($('#deleteModal'));
        deleteModal.show();
        $(document).off('click', '.confirm-delete');
        $(document).on('click', '.confirm-delete', async function () {
            deleteData(domainName + deletebook + id)
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

    $(document).on('click', '.edit-btn', function () {
        var row = $(this).closest('tr');
        id = row.data("value");
        window.location = updateBook + id;
    });

    const [ authordata, genredata, publisherdata, languagedata, storagedata] = await Promise.all([
            getData(domainName + getauthorlink),
            getData(domainName + getgenrelink),
            getData(domainName + getpublisherlink),
            getData(domainName + getlanguagelink),
            getData(domainName + getstoragelink),
        ]);

    const genreList = genredata.$values.map(genre => ({
        value: genre.name,
    }));
    const publisherList = publisherdata.$values.map(publisher => ({
        value: publisher.name,
    }));
    const shelfList = storagedata.$values.map(storage => ({
        value: storage.name,
    }));
    const languageList = languagedata.$values.map(language => ({
        value: language.title,
    }));
    const authorList = authordata.$values.map(author => ({
        value: author.name + " " + author.surname,
    }));

    searchGenre.autocomplete({
        source: genreList,
        minLength: 0,
        multiselect: true,
    });
    searchPublisher.autocomplete({
        source: publisherList,
        minLength: 0,
        multiselect: true,
    });
    searchShelf.autocomplete({
        source: shelfList,
        minLength: 0,
        multiselect: true,
    });
    searchLanguage.autocomplete({
        source: languageList,
        minLength: 0,
        multiselect: true,
    });
    searchAuthor.autocomplete({
        source: authorList,
        minLength: 0,
        multiselect: true,
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
/*    searchGenre.on('click', function () {
        searchGenre.autocomplete('search', '');
    });*/
    
    Submit.on('click', function () {
        pages = 1;
        displayData();
    });

    displayData();
    async function displayData() {
        body.empty();
        usernumber = 0;
        alluser = 0;
        const [bookdata, bookstoragedata, bookpublisherdata, booklanguagedata, bookgenredata,
            bookauthordata] = await Promise.all([
                getData(domainName + getbooklink),
                getData(domainName + getbookstoragelink),
                getData(domainName + getbookpublisherlink),
                getData(domainName + getbooklanguagelink),
                getData(domainName + getbookgenrelink),
                getData(domainName + getbookauthorlink),
            ]);

        
        sortData(bookdata, 'name');
        sortData(bookdata, 'description');
        sortTable(bookdata, bookauthordata, authordata, 'autorId', ['name', 'surname'], 'author');
        sortTable(bookdata, bookgenredata, genredata, 'genreId', ['name'], 'genre');
        sortTable(bookdata, bookpublisherdata, publisherdata, 'publisherId', ['name'], 'publisher');
        sortTable(bookdata, bookstoragedata, storagedata, 'storageId', ['name'], 'shelf');
        sortTable(bookdata, booklanguagedata, languagedata, 'languagesId', ['title'], 'language');
        /*bookdata.$values.sort((a, b) => {
            const genreA = getBookGenreTitles(a);
            const genreB = getBookGenreTitles(b);
            console.log(genreB)
            return genreB.localeCompare(genreA);
        });*/
        try {
            for (const item of bookdata.$values) {
                
                const authorsTitles = getEntityTitles(item.id, bookauthordata, authordata, 'autorId', ['name', 'surname']);
                const genresTitles = getEntityTitles(item.id, bookgenredata, genredata, 'genreId', ['name']);
                const publishersTitles = getEntityTitles(item.id, bookpublisherdata, publisherdata, 'publisherId', ['name']);
                const storagesTitles = getEntityTitles(item.id, bookstoragedata, storagedata, 'storageId', ['name']);
                const languagesTitles = getEntityTitles(item.id, booklanguagedata, languagedata, 'languagesId', ['title']);
               

                const languagesMatch = CheckSearch(searchLanguage, languagesTitles);
                const storageMatch = CheckSearch(searchShelf, storagesTitles);
                const publisherMatch = CheckSearch(searchPublisher, publishersTitles);
                const genreMatch = CheckSearch(searchGenre, genresTitles);
                const authorMatch = CheckSearch(searchAuthor, authorsTitles);
                const descriptionMatch = CheckSearch(searchDescription, item.description);
                const nameMatch = CheckSearch(searchName, item.name);


                if (languagesMatch && storageMatch && publisherMatch && genreMatch && authorMatch && descriptionMatch && nameMatch) {
                    alluser++
                    var sum = usersonpage * pages;
                    var min = sum - usersonpage;
                    if (usernumber < sum && usernumber >= min) {
                        var trElement = $('<tr></tr>').attr('data-value', item.id);
                        var tdElement1 = creatTdElement(item.name);
                        var tdElement2 = creatTdElement(item.description);
                        var tdElement3 = creatTdElement(authorsTitles);
                        var tdElement4 = creatTdElement(genresTitles);
                        var tdElement5 = creatTdElement(publishersTitles);
                        var tdElement6 = creatTdElement(storagesTitles);
                        var tdElement7 = creatTdElement(languagesTitles);
                        var tdElement8 = $('<td></td>').addClass('text-center').html('<button class="edit-btn">Edit</button> <button class="delete-btn">Delete</button>');
                        trElement.append(tdElement1, tdElement2, tdElement3, tdElement4, tdElement5, tdElement6, tdElement7, tdElement8);
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


