var domainName = window.location.origin;
const getTransactionlink = "/Report/GetOverdueTransaction";
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
const searchReturnTime = $("#returnTime");
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
        const [data] = await Promise.all([
            getData(domainName + getTransactionlink),
        ]);
        sortData(data, 'personName');
        sortData(data, 'bookName');
        sortData(data, 'returnTime');
        for (const item of data.$values) {
            const personMatch = CheckSearch(searchPersonName, item.personName);
            const bookMatch = CheckSearch(searchBookName, item.bookName);
            const returnMatch = CheckSearch(searchReturnTime, item.returnTime);
            if (personMatch && bookMatch && returnMatch) {
                alluser++;
                var sum = usersonpage * pages;
                var min = sum - usersonpage;
                if (usernumber < sum && usernumber >= min) {
                    var formattedDate = moment(item.returnTime).format("YYYY-MM-DD");
                    var trElement = $('<tr></tr>');
                    var tdElement1 = creatTdElement(item.personName);
                    var tdElement2 = creatTdElement(item.bookName);
                    var tdElement3 = creatTdElement(formattedDate);
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
            displayData();
        }
        totalPage = Math.ceil(alluser / usersonpage);    
        initializePagination();
    } catch (error) {
        console.error(error);
    }
}

