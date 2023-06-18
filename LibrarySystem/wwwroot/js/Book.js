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
const Submit = $("#submit");

$(document).ready(async function () {
    const [bookdata, bookstoragedata, bookpublisherdata, booklanguagedata, bookgenredata,
        bookauthordata, authordata, genredata, publisherdata, languagedata, storagedata] = await Promise.all([
            getData(domainName + getbooklink),
            getData(domainName + getbookstoragelink),
            getData(domainName + getbookpublisherlink),
            getData(domainName + getbooklanguagelink),
            getData(domainName + getbookgenrelink),
            getData(domainName + getbookauthorlink),
            getData(domainName + getauthorlink),
            getData(domainName + getgenrelink),
            getData(domainName + getpublisherlink),
            getData(domainName + getlanguagelink),
            getData(domainName + getstoragelink),
        ]);

    const genreList = genredata.$values.map(genre => ({
        value: genre.name,
    }));

    searchGenre.autocomplete({
        source: genreList,
        minLength: 0,
        multiselect: true,
    });

    searchGenre.on('click', function () {
        searchGenre.autocomplete('search', '');
    });

    Submit.on('click', function () {
        console.log(searchGenre.val())
    });




    displayBoook();
    async function displayBoook() {
        body.empty();
        usernumber = 0;
        alluser = 0;
        try {
            for (const item of bookdata.$values) {
                const bookauthorItam = bookauthordata.$values.filter(a => a.bookId === item.id);
                const matcherAuthor = authordata.$values.filter(data => bookauthorItam.some(genre => genre.autorId === data.id));
                const authorTitles = matcherAuthor.map(i => i.name + " " + i.surname)
                var authorsTitles = authorTitles.join(', ');

                const bookgenreItam = bookgenredata.$values.filter(a => a.bookId === item.id);
                const matchedGenre= genredata.$values.filter(data => bookgenreItam.some(genre => genre.genreId === data.id));
                const genreTitles = matchedGenre.map(i => i.name);
                var genresTitles = genreTitles.join(', ');

                const bookpublisherItam = bookpublisherdata.$values.filter(a => a.bookId === item.id);
                const matchedPublisher = publisherdata.$values.filter(data => bookpublisherItam.some(genre => genre.publisherId === data.id));
                const publisherTitle = matchedPublisher.map(i => i.name);
                var publishersTitles = publisherTitle.join(', ');

                const bookstorageItam = bookstoragedata.$values.filter(a => a.bookId === item.id);
                const matchedStorage = storagedata.$values.filter(data => bookstorageItam.some(genre => genre.storageId === data.id));
                const storageTitle = matchedStorage.map(i => i.name);
                var storagesTitles = storageTitle.join(', ');

                const booklanguageItam = booklanguagedata.$values.filter(a => a.bookId === item.id);
                const matchedLanguage = languagedata.$values.filter(data => booklanguageItam.some(genre => genre.languagesId === data.id));
                const languageTitle = matchedLanguage.map(i => i.title);
                var languagesTitles = languageTitle.join(', ')

                alluser++
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var trElement = $('<tr></tr>').attr('data-value', item.id);
                    var tdElement1 = $('<td></td>').addClass('text-center').text(item.name);
                    var tdElement2 = $('<td></td>').addClass('text-center').text(item.description); 
                    var tdElement3 = $('<td></td>').addClass('text-center').text(authorsTitles);
                    var tdElement4 = $('<td></td>').addClass('text-center').text(genresTitles); 
                    var tdElement5 = $('<td></td>').addClass('text-center').text(publishersTitles);
                    var tdElement6 = $('<td></td>').addClass('text-center').text(storagesTitles);
                    var tdElement7 = $('<td></td>').addClass('text-center').text(languagesTitles);
                    var tdElement8 = $('<td></td>').addClass('text-center').html('<button class="edit-btn">Edit</button> <button class="delete-btn">Delete</button>');
                    trElement.append(tdElement1, tdElement2, tdElement3, tdElement4, tdElement5, tdElement6, tdElement7, tdElement8);
                    body.append(trElement);
                    usernumber++
                }

                if (usernumber < min) {
                    usernumber++;
                }
            }
            if (alluser === min && alluser === usernumber) {
                pages--;
                displayBoook();
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
                displayBoook();
            },
            currentPage: pages
        });
    }
});

