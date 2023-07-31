var domainName = window.location.origin;
const getPerfomancelink = "/Report/GetClientsPerformance";
const getpdflink = "/Report/GenerateClientsPerformancePdf";
const getcsvlink = "/Report/GenerateClientsPerformancecsv";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchPersonName = $("#personName");
const searchMin = $("#minBorrow");
const searchMax = $("#maxBorrow");
const fileNamecsv = "ClientsPerformance.csv";
const fileNamepdf = "ClientsPerformance.pdf";

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
            MinBooksTaken: searchMin.val() || null,
            MaxBooksTaken: searchMax.val() || null
        }
        const [data] = await Promise.all([
            getDataWithParameters(domainName + getPerfomancelink, param),
        ]);
        sortData(data, 'personName');
        sortIntData(data, 'booksTaken');
        for (const item of data.$values) {
            alluser++;
            var sum = usersonpage * pages;
            var min = sum - usersonpage;
            if (usernumber < sum && usernumber >= min) {
                var trElement = $('<tr></tr>');
                var tdElement1 = creatTdElement(item.personName);
                var tdElement2 = creatTdElement(item.booksTaken);
                trElement.append(tdElement1, tdElement2);
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
        MinBooksTaken: searchMin.val() || null,
        MaxBooksTaken: searchMax.val() || null
    }
    url = domainName + getpdflink;
    postDataGeneretePdf(url, param, fileNamepdf)
});

$("#csv-btn").on("click", function () {
    var param =
    {
        PersonNameSearch: searchPersonName.val() || null,
        MinBooksTaken: searchMin.val() || null,
        MaxBooksTaken: searchMax.val() || null
    }
    url = domainName + getcsvlink;
    postDataGeneretecsv(url, param, fileNamecsv);
});
