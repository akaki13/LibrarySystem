/*var domainName = window.location.origin;
const getauthorlink = "/bookcategory/getauthor";
const getgenrelink = "/bookcategory/getgenres";
const getpublisherlink = "/bookcategory/getpublishers";
const getlanguagelink = "/bookcategory/getlanguage";
const getstoragelink = "/bookcategory/getstorage";
const addauthorlink = "/bookcategory/addauthor";
const deleteauthorlink = "/bookcategory/deleteauthor/";
const updateauthorlink = "/bookcategory/updateauthor";*/
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

function CheckSearch(search, titles) {
    return search.val() ? titles.toLowerCase().includes(search.val().toLowerCase()) : true;
}

function sortData(data, key) {
    if (sortBy === key) {
        if (orderBy === "asc") {
            return data.$values.sort((a, b) => a[key].localeCompare(b[key]));
        }
        else if (orderBy === "desc") {
            return data.$values.sort((a, b) => b[key].localeCompare(a[key]));
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

function creaetTdElement(data) {
    return $('<td></td>').addClass('text-center').text(data);
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
