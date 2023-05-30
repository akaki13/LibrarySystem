const checkboxes = $("[id='rolecheck']");
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
var page = 1;
var usernumber = 0;
var alluser = 0;
var sortBy = "name";
var orderBy = "ascending";

$("#target").on("click", async function () {
    page = 1;
    displayUsers();
});

$(".sort-option").on("click", async function () {
    page = 1;
    sortBy = $(this).data("value")
    displayUsers();
});

$(".order-option").on("click", async function () {
    page = 1;
    orderBy = $(this).data("value");
    console.log(orderBy)
    displayUsers();
});

$(document).on("click", "#next", async function () {
    if (usernumber != alluser) {
        page++;
        displayUsers();
    }
});

$(document).on("click", "#previous", async function () {
    if (page > 1) {
        page--;
        displayUsers();
    }
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
        if (sortBy === "name")
        {
            if (orderBy === "ascending") {
                userdata.$values.sort((a, b) => a.login.localeCompare(b.login));
            }
            else if (orderBy === "descending")
            {
                userdata.$values.sort((a, b) => b.login.localeCompare(a.login));
            }
        }
        else if (sortBy === "email")
        {
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
        console.log(orderBy)
        /*if (sor === 0)
        {
            userdata.$values.sort((a, b) => a.login.localeCompare(b.login));
        }*/

        /*userdata.$values.sort((a, b) => {
            const personA = persondata.$values.find(p => p.id === a.personId);
            const personB = persondata.$values.find(p => p.id === b.personId);
            if (personA && personB) {
                return personA.email.localeCompare(personB.email);
            }
            return 0;
        });*/
    //    userdata.$values.sort((a, b) => a.login.localeCompare(b.login));
    //userdata.$values.sort((a, b) => b.login.localeCompare(a.login));


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
                            var sum = usersonpage * page;
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
        if (usernumber != alluser) {
            next.removeClass("disabled");
            next.attr("aria-disabled", "false");
        }
        else {
            next.addClass("disabled");
            next.attr("aria-disabled", "true");
        }
        if (page === 1) {
            previous.addClass("disabled");
            previous.attr("aria-disabled", "true");
        }
    } catch (error) {
        console.error(error);
    }
}