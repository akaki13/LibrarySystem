var domainName = window.location.origin;
const getbooklink = "/book/getbook";
const getpersonlink = "/user/getperson";
const bookSearch = $("#bookSearch");
const bookInput = $("#bookInput");
const boookId = $("#boookId");
const bookResults = $("#bookResults");
const clientSearch = $("#clientSearch");
const clientInput = $("#clientInput"); 
const clientResults = $("#clientResults");
const clientId = $("#clientId");
$(document).ready(async function () {
    const [bookdata, personrdata ] = await Promise.all([
        getData(domainName + getbooklink),
        getData(domainName + getpersonlink),
    ]); 
    const bookList = bookdata.$values.map(book => ({
        value: book.name,
        id: book.id,
        secondValue: book.description,
    }));
    const clientList = personrdata.$values.map(person => ({
        value: person.firstname + " " + person.lastname,
        id: person.id,
        secondValue: person.email,
    }));

    $("#bookModalOpan").on("click", async function () {
        var bookModal = new bootstrap.Modal($('#bookModal'));
        bookModal.show();
        bookSearch.keyup(function (event) {
            bookResults.empty();
            for (const item of bookList) {
                if (CheckSearch(bookSearch, item.value)) {
                    creatPersonResult(item, 'description: ', bookResults,'book');
                    console.log("Aasdas");
                }
            }
            bookResults.show();
            if (bookResults.is(':empty')) {
                bookResults.hide();
            }

        });
        $(document).on('click', '.book', function () {

            boookId.val($(this).data("value"));
            bookInput.val($(this).find('.maintext').text())
            bookModal.hide();
            bookResults.hide();
        });
    });
    $("#clientModalOpan").on("click", async function () {
        var clientModal = new bootstrap.Modal($('#clientModal'));
        clientModal.show();
        clientSearch.keyup(function (event) {
            clientResults.empty(); 

            for (const item of clientList) {
                if (CheckSearch(clientSearch, item.value)) {
                    creatPersonResult(item, 'email: ', clientResults,'client ');
                }
            }
            clientResults.show();

            if (clientResults.is(':empty'))
            {
                clientResults.hide();
            }

        });
        $(document).on('click', '.client', function () {
            
            clientId.val($(this).data("value"));
            clientInput.val($(this).find('.maintext').text())
            clientModal.hide();
            clientResults.hide();
        });
    });


    function creatPersonResult(data, texts , result,type) {
        var searchGroup = $('<div>', { class: 'search-group  clickable ' + type, 'data-value': data.id });
        var text = $('<p>', { class: 'text-center maintext', text: data.value });
        var lineContainer = $('<div>', { class: 'line-container' });
        var lineText1 = $('<p>', { class: 'line-text', text: texts });
        var lineText2 = $('<p>', { class: 'line-text', text: data.secondValue });
        lineContainer.append(lineText1, lineText2);
        searchGroup.append(text, lineContainer);
        result.append(searchGroup)
    }
});


