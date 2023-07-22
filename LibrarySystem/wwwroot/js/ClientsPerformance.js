var domainName = window.location.origin;
const getPerfomancelink = "/Report/GetClientsPerformance";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchPersonName = $("#personName");
const searchBookTaken = $("#booksTaken");
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
            getData(domainName + getPerfomancelink),
        ]);
        sortData(data, 'personName');
        sortIntData(data, 'booksTaken');
        for (const item of data.$values) {
            const personMatch = CheckSearch(searchPersonName, item.personName);
            const bookMatch = CheckSearchInt(searchBookTaken, item.booksTaken);
            if (personMatch && bookMatch) {
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

