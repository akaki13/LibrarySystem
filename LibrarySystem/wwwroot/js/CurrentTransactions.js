var domainName = window.location.origin;
const getTransactionlink = "/Report/GetCurrentTransactions";
const getpdflink = "/Report/GenerateCurrentTransactionsPdf";
const getcsvlink = "/Report/GenerateCurrentTransactionscsv";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchPersonName = $("#personName");
const searchBookName = $("#bookName");
const searchTakeAfter = $("#takeAfter");
const searchTakeBefore = $("#takeBefore");
const searchReturnAfter = $("#returnAfter");
const searchReturnBefore = $("#returnBefore");
const fileNamecsv = "CurrentTransactions.csv";
const fileNamepdf = "CurrentTransactions.pdf";
$("#submit").on("click", async function () {
    pages = 1;
    displayData();
});



displayData();
async function displayData() {
    body.empty();
    usernumber = 0;
    alluser = 0;
    try {

        var param =
        {
            PersonNameSearch: searchPersonName.val() || null,
            BookNameSearch: searchBookName.val() || null,
            TakeTimeAfter: searchTakeAfter.val() || null,
            TakeTimeBefore: searchTakeBefore.val() || null,
            ReturnTimeAfter: searchReturnAfter.val() || null,
            ReturnTimeBefore: searchReturnBefore.val() || null,
        }
        const [data] = await Promise.all([
            getDataWithParameters(domainName + getTransactionlink, param),
        ]);
        sortData(data, 'personName');
        sortData(data, 'bookName');
        sortData(data, 'returnTime');
        sortData(data, 'takeTime');
        for (const item of data.$values) {

            alluser++;
            var sum = usersonpage * pages;
            var min = sum - usersonpage;
            if (usernumber < sum && usernumber >= min) {
                var formattedDate1 = moment(item.returnTime).format("YYYY-MM-DD");
                var formattedDate2 = moment(item.takeTime).format("YYYY-MM-DD");
                var trElement = $('<tr></tr>');
                var tdElement1 = creatTdElement(item.personName);
                var tdElement2 = creatTdElement(item.bookName);
                var tdElement3 = creatTdElement(formattedDate2);
                var tdElement4 = creatTdElement(formattedDate1);
                trElement.append(tdElement1, tdElement2, tdElement3, tdElement4);
                body.append(trElement);
                usernumber++
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
        csvbtn(data, fileName);
    } catch (error) {
        console.error(error);
    }
}

$("#pdf-btn").on("click", function () {

    var param =
    {
        PersonNameSearch: searchPersonName.val() || null,
        BookNameSearch: searchBookName.val() || null,
        TakeTimeAfter: searchTakeAfter.val() || null,
        TakeTimeBefore: searchTakeBefore.val() || null,
        ReturnTimeAfter: searchReturnAfter.val() || null,
        ReturnTimeBefore: searchReturnBefore.val() || null,
    }
    url = domainName + getpdflink;
    postDataGeneretePdf(url, param, fileNamepdf)
});

$("#csv-btn").on("click", function () {
    var param =
    {
        PersonNameSearch: searchPersonName.val() || null,
        BookNameSearch: searchBookName.val() || null,
        TakeTimeAfter: searchTakeAfter.val() || null,
        TakeTimeBefore: searchTakeBefore.val() || null,
        ReturnTimeAfter: searchReturnAfter.val() || null,
        ReturnTimeBefore: searchReturnBefore.val() || null,
    }
    url = domainName + getcsvlink;
    postDataGeneretecsv(url, param, fileNamecsv);
});