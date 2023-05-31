const checkboxes = $("[id='rolecheck']");
const userbody = $("#userbody");
const usarname = $("#usarname");
const email = $("#email");
const arr = [];
const personlink = "https://localhost:7291/user/person";
const userlink = "https://localhost:7291/user/users";
const rolelink = "https://localhost:7291/user/roles";
const roleuserlink = "https://localhost:7291/user/rolesuser";
var usersonpage = 3;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var sortBy = "name";
var orderBy = "ascending";
var totalPage = 0; 
var pagination = null;

$("#target").on("click", async function () {
    pages = 1;
    pagination = null;
    displayUsers();
});

$(".sort-option").on("click", async function () {
    pages = 1;
    sortBy = $(this).data("value");
    pagination = null;
    displayUsers();
});

$(".order-option").on("click", async function () {
    pages = 1;
    orderBy = $(this).data("value");
    pagination = null;
    displayUsers();
});
displayUsers();

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

async function displayUsers() {
    usernumber = 0;
    alluser = 0;
    $("#userbody").empty();
    if (pagination === null)
    {
        $("#pagination-demo").twbsPagination('destroy');
    }
    arr.length = 0;
    for (let i = 0; i < checkboxes.length; i++) {

        if (checkboxes[i].checked == true) {
            arr.push(checkboxes[i].value)
        }
    }
    try {
        const [roleuserdata, userdata, persondata, roledata] = await Promise.all([
            getData(roleuserlink),
            getData(userlink),
            getData(personlink),
            getData(rolelink)
        ]);
        if (sortBy === "name") {
            if (orderBy === "ascending") {
                userdata.$values.sort((a, b) => a.login.localeCompare(b.login));
            }
            else if (orderBy === "descending") {
                userdata.$values.sort((a, b) => b.login.localeCompare(a.login));
            }
        }
        else if (sortBy === "email") {
            if (orderBy === "ascending") {
                userdata.$values.sort((a, b) => {
                    var personA = persondata.$values.find(p => p.id === a.personId);
                    var personB = persondata.$values.find(p => p.id === b.personId);
                    if (personA && personB) {
                        return personA.email.localeCompare(personB.email);
                    }
                    return 0;
                });
            }
            else if (orderBy === "descending") {
                userdata.$values.sort((a, b) => {
                    var personA = persondata.$values.find(p => p.id === a.personId);
                    var personB = persondata.$values.find(p => p.id === b.personId);
                    if (personA && personB) {
                        return personB.email.localeCompare(personA.email);
                    }
                    return 0;
                });
            }
        }
        for (const user of userdata.$values) {

            const roleItam = roleuserdata.$values.find(a => a.usersId === user.id)
            const roleId = roleItam ? roleItam.roleId : "";
            if ($.inArray(roleId.toString(), arr) > -1 || arr.length === 0) {
                const person = persondata.$values.find(p => p.id === user.personId);
                {
                    const roleItems = roledata.$values.find(r => r.id === roleId);
                    const roleTitle = roleItems ? roleItems.title : "";
                    if (user.login.toLowerCase().includes(usarname.val().toLowerCase()) || usarname.val() == "") {
                        if (person.email.includes(email.val()) || email.val() == "") {
                            alluser++;
                            var sum = usersonpage * pages;
                            var min = sum - usersonpage;

                            if (usernumber < sum && usernumber >= min) {
                                var formattedDate = moment(person.dateOfBirth).format("YYYY-MM-DD");
                                const trElement = $('<tr></tr>');
                                const tdProduct = $('<td></td>').addClass('product').html(`<strong>${user.login}</strong><br>Date of birth: ${formattedDate}`);
                                const tdRate = $('<td></td>').addClass('rate text-center').text(person.email);
                                const tdPrice = $('<td></td>').addClass('price text-end').text(roleTitle);
                                trElement.append(tdProduct, tdRate, tdPrice);
                                userbody.append(trElement);
                                usernumber++;
                            }
                            if (usernumber < min) {
                                usernumber++;
                            }
                        }
                    }
                }
            }
        }
        totalPage = Math.ceil(alluser / usersonpage);
        pagination = "sdas";
        initializePagination();
    } catch (error) {
        console.error(error);
    }
};

 function initializePagination() {

    $("#pagination-demo").twbsPagination({
        totalPages: totalPage,
        visiblePages: 5,
        next: 'Next',
        prev: 'Prev',
        startPage: 1,
        onPageClick: function (event, page) {
            //fetch content and render here
            pages = page;
            displayUsers();
        }
    });
}
/*const checkboxes = $("[id='rolecheck']");
const userbody = $("#userbody");
const usarname = $("#usarname");
const email = $("#email");
const previous = $("#previous");
const next = $("#next");
const arr = [];
const personlink = "https://localhost:7291/user/person";
const userlink = "https://localhost:7291/user/users";
const rolelink = "https://localhost:7291/user/roles";
const roleuserlink = "https://localhost:7291/user/rolesuser";
var usersonpage = 3;
var pages = 1;
var usernumber = 0;
var alluser = 0;
var sortBy = "name";
var orderBy = "ascending";
var totalPage = 0;

$("#target").on("click", async function () {
    pages = 1;
    displayUsers();
});

$(".sort-option").on("click", async function () {
    pages = 1;
    sortBy = $(this).data("value");
    displayUsers();
});

$(".order-option").on("click", async function () {
    pages = 1;
    orderBy = $(this).data("value");
    displayUsers();
});

displayUsers();

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

async function displayUsers() {
    usernumber = 0;
    alluser = 0;
    $("#userbody").empty();
    previous.removeClass("disabled");
    previous.attr("aria-disabled", "false");

    arr.length = 0;
    for (let i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked == true) {
            arr.push(checkboxes[i].value)
        }
    }
    try {
        const [roleuserdata, userdata, persondata, roledata] = await Promise.all([
            getData(roleuserlink),
            getData(userlink),
            getData(personlink),
            getData(rolelink)
        ]);
        if (sortBy === "name") {
            if (orderBy === "ascending") {
                userdata.$values.sort((a, b) => a.login.localeCompare(b.login));
            } else if (orderBy === "descending") {
                userdata.$values.sort((a, b) => b.login.localeCompare(a.login));
            }
        } else if (sortBy === "email") {
            if (orderBy === "ascending") {
                userdata.$values.sort((a, b) => {
                    var personA = persondata.$values.find(p => p.id === a.personId);
                    var personB = persondata.$values.find(p => p.id === b.personId);
                    if (personA && personB) {
                        return personA.email.localeCompare(personB.email);
                    }
                    return 0;
                });
            } else if (orderBy === "descending") {
                userdata.$values.sort((a, b) => {
                    var personA = persondata.$values.find(p => p.id === a.personId);
                    var personB = persondata.$values.find(p => p.id === b.personId);
                    if (personA && personB) {
                        return personB.email.localeCompare(personA.email);
                    }
                    return 0;
                });
            }
        }
        for (const user of userdata.$values) {
            const roleItem = roleuserdata.$values.find(a => a.usersId === user.id)
            const roleId = roleItem ? roleItem.roleId : "";
            if ($.inArray(roleId.toString(), arr) > -1 || arr.length === 0) {
                const person = persondata.$values.find(p => p.id === user.personId);
                {
                    const roleItems = roledata.$values.find(r => r.id === roleId);
                    const roleTitle = roleItems ? roleItems.title : "";
                    if (user.login.toLowerCase().includes(usarname.val().toLowerCase()) || usarname.val() == "") {
                        if (person.email.includes(email.val()) || email.val() == "") {
                            alluser++;
                            var sum = usersonpage * pages;
                            var min = sum - usersonpage;

                            if (usernumber < sum && usernumber >= min) {
                                var formattedDate = moment(person.dateOfBirth).format("YYYY-MM-DD");
                                const trElement = $('<tr></tr>');
                                const tdProduct = $('<td></td>').addClass('product').html(`<strong>${user.login}</strong><br>Date of birth: ${formattedDate}`);
                                const tdRate = $('<td></td>').addClass('rate text-center').text(person.email);
                                const tdPrice = $('<td></td>').addClass('price text-end').text(roleTitle);
                                trElement.append(tdProduct, tdRate, tdPrice);
                                userbody.append(trElement);
                                usernumber++;
                            }
                            if (usernumber < min) {
                                usernumber++;
                            }
                        }
                    }
                }
            }
        }
        totalPage = Math.ceil(alluser / usersonpage);

        // Initialize or update the pagination controls
       
        $('#pagination').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            startPage: pages,
            onPageClick: function (event, page) {
                pages = page;
                displayUsers();
            }
        });

    } catch (error) {
        console.error(error);
    }
};*/