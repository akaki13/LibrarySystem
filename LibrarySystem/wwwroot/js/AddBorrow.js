var domainName = window.location.origin;
const getbooklink = "/book/getbook";
const getpersonlink = "/user/getperson";
const getauthorlink = "/bookcategory/getauthor";
const getbookauthorlink = "/book/getbookauthor";
const bookSearch = $("#bookSearch");
const bookInput = $("#bookInput");
const boookId = $("#boookId");
const bookResults = $("#bookResults");
const clientSearch = $("#clientSearch");
const clientInput = $("#clientInput"); 
const clientResults = $("#clientResults");
const clientId = $("#clientId");
$(document).ready(async function () {
    const [bookdata, personrdata, bookauthordata , authordata] = await Promise.all([
        getData(domainName + getbooklink),
        getData(domainName + getpersonlink),
        getData(domainName + getbookauthorlink),
        getData(domainName + getauthorlink),
    ]); 
 

    $("#bookModalOpan").on("click", async function () {
        var bookModal = new bootstrap.Modal($('#bookModal'));
        bookModal.show();
        bookSearch.keyup(function (event) {
            bookResults.empty();
            for (const item of bookdata.$values) {
                if (CheckSearch(bookSearch, item.name)) {
                    const authorsTitles = getEntityTitles(item.id, bookauthordata, authordata, 'autorId', ['name', 'surname']);
                    creatPersonResult(item.id,item.name, authorsTitles, 'Author: ', bookResults, 'book');
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

            for (const item of personrdata.$values) {
                var name = item.firstname + " " + item.lastname;
                if (CheckSearch(clientSearch, name)) {
                    
                        creatPersonResult(item.id, name, item.email, 'email: ', clientResults, 'client');
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


    function creatPersonResult(id,data1, data2, texts , result,type) {
        var searchGroup = $('<div>', { class: 'search-group  clickable ' + type, 'data-value': id });
        var text = $('<p>', { class: 'text-center maintext', text: data1 });
        var lineContainer = $('<div>', { class: 'line-container' });
        var lineText1 = $('<p>', { class: 'line-text', text: texts });
        var lineText2 = $('<p>', { class: 'line-text', text: data2 });
        lineContainer.append(lineText1, lineText2);
        searchGroup.append(text, lineContainer);
        result.append(searchGroup)
    }
});


