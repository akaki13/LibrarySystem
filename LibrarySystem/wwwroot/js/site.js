/*var domainName = window.location.origin;
const getauthorlink = "/bookcategory/getauthor";
const getgenrelink = "/bookcategory/getgenres";
const getpublisherlink = "/bookcategory/getpublishers";
const getlanguagelink = "/bookcategory/getlanguage";
const getstoragelink = "/bookcategory/getstorage";
const addauthorlink = "/bookcategory/addauthor";
const deleteauthorlink = "/bookcategory/deleteauthor/";
const updateauthorlink = "/bookcategory/updateauthor";*/
const createModalBody = "data is not saved";
const delteModalBody = "can not delete data";
var usersonpage = 20;

function initializePagination() {
    $('#pagination-container').MyPagination({
        totalPages: totalPage,
        visiblePages: 5,
        onPageClick: function (pageNumber) {
            pages = pageNumber;
            displayData();
        },
        currentPage: pages
    });
}


function sortDataByTable(data, key,data2, id) {
<<<<<<< HEAD
    data.$values.sort((a, b) => {
        if (sortBy === key) {
            if (orderBy === "asc") {
                return borrowdata.$values.sort((a, b) => {
                    const personA = data2[a[id]];
                    const personB = data2[b[id]];

                    const firstnameA = personA[key].toLowerCase();
                    const firstnameB = personB[key].toLowerCase();

                    if (firstnameA < firstnameB) return -1;
                    if (firstnameA > firstnameB) return 1;
                    return 0;
                });
            }
            else if (orderBy === "desc") {
                return borrowdata.$values.sort((a, b) => {
                    const personA = data2[a[id]];
                    const personB = data2[b[id]];

                    const firstnameA = personA[key].toLowerCase();
                    const firstnameB = personB[key].toLowerCase();

                    if (firstnameA > firstnameB) return -1;
                    if (firstnameA < firstnameB) return 1;
                    return 0;
                });
            }
        }
        
    });
=======
    
        if (sortBy === key) {
            if (orderBy === "asc") {
                return data.$values.sort((a, b) => {
                    const bookA = data2.$values.find(x => x.id === a[id]);
                    const bookB = data2.$values.find(x => x.id === b[id]);
                    const bookNameA = bookA ? bookA[key] : '';
                    const bookNameB = bookB ? bookB[key] : '';

                    return bookNameA.localeCompare(bookNameB);
                });
            }
            else if (orderBy === "desc") {
                return data.$values.sort((a, b) => {
                    const bookA = data2.$values.find(x => x.id === a[id]);
                    const bookB = data2.$values.find(x => x.id === b[id]);
                    const bookNameA = bookA ? bookA[key] : '';
                    const bookNameB = bookB ? bookB[key] : '';

                    return bookNameB.localeCompare(bookNameA);
                });
            
            }
        }
        

>>>>>>> rdlc
}


function CheckSearchInt(search, titles) {
    return search.val() ? titles.toString().includes(search.val().toString()) : true;
}

function CheckSearch(search, titles) {
    return search.val() ? titles.toLowerCase().includes(search.val().toLowerCase()) : true;
}

function sortData(data, key) {
    if (sortBy === key) {
        if (orderBy === "asc") {
            return data.$values.sort((a, b) => {
                const valueA = a[key] || ""; 
                const valueB = b[key] || ""; 
                return valueA.localeCompare(valueB);
            });
        } else if (orderBy === "desc") {
            return data.$values.sort((a, b) => {
                const valueA = a[key] || ""; 
                const valueB = b[key] || "";
                return valueB.localeCompare(valueA);
            });
        }
    }
}

function sortTable(data, itemData, relatedData, relatedIdKey, relatedTitleKeys, key) {
    if (sortBy === key) {
        if (orderBy === "asc") {
            return data.$values.sort((a, b) => {
                const genreA = getEntityTitles(a.id, itemData, relatedData, relatedIdKey, relatedTitleKeys);
                const genreB = getEntityTitles(b.id, itemData, relatedData, relatedIdKey, relatedTitleKeys);
                return genreA.localeCompare(genreB);
            });

        }
        else if (orderBy === "desc") {
            return data.$values.sort((a, b) => {
                const genreA = getEntityTitles(a.id, itemData, relatedData, relatedIdKey, relatedTitleKeys);
                const genreB = getEntityTitles(b.id, itemData, relatedData, relatedIdKey, relatedTitleKeys);
                return genreB.localeCompare(genreA);
            });
        }
    }
}

function sortIntData(data, key) {
    if (sortBy === key) {
        if (orderBy === "asc") {
            return data.$values.sort((a, b) => a[key] - b[key]);
        }
        else if (orderBy === "desc") {
            return data.$values.sort((a, b) => b[key] - a[key]);
        }
    }
}

function creatTdElement(data) {
    return $('<td></td>').addClass('text-center').text(data);
}

function creatTdElementPhoto(data) {
    var tdElement = $('<td></td>').addClass('image-cell'); 
    var image = $('<img/>').attr('src', data);
    tdElement.append(image);
    return tdElement;
}
function getNames(Id, Data, relatedTitleKeys) {
    const matchedRelated = Data.$values.filter((a) => a.id === Id);
    const relatedTitles = matchedRelated.map(i => relatedTitleKeys.map(key => i[key]).join(' '));
    const titles = relatedTitles.join(', ');
    return titles;
}

function getEntityTitles(itemId, itemData, relatedData, relatedIdKey, relatedTitleKeys) {
    const itemRelatedItems = itemData.$values.filter(a => a.bookId === itemId);
    const matchedRelated = relatedData.$values.filter(data =>
        itemRelatedItems.some(item => item[relatedIdKey] === data.id)
    );
    const relatedTitles = matchedRelated.map(i => relatedTitleKeys.map(key => i[key]).join(' '));
    const titles = relatedTitles.join(', ');
    return titles;
}

function postData(url, data) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (response, status) {
                resolve(response);
            },
            error: function (xhr, status, error) {
                reject({
                    status: xhr.status,
                    responseText: xhr.responseText
                });
            }
        });
    });
}
function postDataGeneretePdf(url, param, name) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.responseType = "arraybuffer";

    xhr.onload = function () {
        if (xhr.status === 200) {
            var blob = new Blob([xhr.response], { type: "application/pdf" });
            var url = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = url;
            a.download = name;
            a.click();

            URL.revokeObjectURL(url);
        } else {
            
        }
    };

    xhr.onerror = function () {

    };
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(param);
}

function postDataGeneretecsv(url, param, name) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    xhr.responseType = "arraybuffer";

    xhr.onload = function () {
        if (xhr.status === 200) {
            var blob = new Blob([xhr.response], { type: "text/csv" });
            var url = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = url;
            a.download = name;
            a.click();

            URL.revokeObjectURL(url);
        } else {
        }
    };

    xhr.onerror = function () {
    };
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(param);
}
function deleteData(url) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: url,
            type: 'DELETE',
            success: function (response) {
                resolve(response);
            },
            error: function (xhr, status, error) {
                reject({
                    status: xhr.status,
                    responseText: xhr.responseText
                });
            }
        });
    });
}

function getData(url) {
    return new Promise((resolve, reject) => {
        $.get(url, (data, status) => {
            if (status === "success") {
                resolve(data);
            } else {
                reject(new Error(`Failed to retrieve data from ${url}`));
            }
        });
    });
}

function getDataWithParameters(url , params) {
    return new Promise((resolve, reject) => {
        $.get(url, params, (data, status) => {
            if (status === "success") {
                resolve(data);
            } else {
                reject(new Error(`Failed to retrieve data from ${url}`));
            }
        });
    });
}

function formatDate(date) {
    const dob = new Date(date);
    const year = dob.getFullYear().toString().padStart(2, '0');
    const month = (dob.getMonth() + 1).toString().padStart(2, '0');
    const day = dob.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
}

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

function saveDataToCSV(data, name) {
    let csvContent = "";
    csvContent += Object.keys(data[0]).join(",") + "\r\n";

    data.forEach(row => {
        const values = Object.values(row).map(value => {
            if (value instanceof Date) {
                return value.toISOString();
            }
            return value;
        });
        csvContent += values.join(",") + "\r\n";
    });

    const encodedUri = encodeURI(csvContent);

    const dataUri = "data:text/csv;charset=utf-8," + encodedUri;

    const link = document.createElement("a");
    link.href = dataUri;
    link.download = name;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
