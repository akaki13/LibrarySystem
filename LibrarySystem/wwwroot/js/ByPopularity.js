var domainName = window.location.origin;
const getTransactionlink = "/Report/GetByPopularity";
const body = $("#body");
var usersonpage = 5;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var totalPage = 0;
var sortBy = "";
var orderBy = "asc";
const searchBookName = $("#bookName");
const searchMin = $("#minBorrow");
const searchMax = $("#maxBorrow");
const fileName = "ByPopularity";
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
            BookName: searchBookName.val() || null,
            MinBooksTaken:  searchMin.val() || null,
            MaxBooksTaken: searchMax.val() || null
        }


        const [data] = await Promise.all([
            getDataWithParameters(domainName + getTransactionlink, param),
        ]);

        sortData(data, 'bookName');
        sortIntData(data, 'timesTaken');
        for (const item of data.$values) {
            alluser++;
            var sum = usersonpage * pages;
            var min = sum - usersonpage;
            if (usernumber < sum && usernumber >= min) {
                var trElement = $('<tr></tr>');
                var tdElement1 = creatTdElement(item.bookName);
                var tdElement2 = creatTdElement(item.timesTaken);
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

