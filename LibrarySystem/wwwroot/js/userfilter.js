/*const checkboxes = $("[id='rolecheck']");
const userbody = $("#userbody");
const usarname = $("#usarname");
const email = $("#email")
const arr = [];
const personlink = "https://localhost:7291/user/person";
const userlink = "https://localhost:7291/user/users";
const rolelink = "https://localhost:7291/user/roles";
const roleuserlink = "https://localhost:7291/user/rolesuser";
$("#target").on("click", function () {
    arr.length = 0;
    console.log(checkboxes.length);
    for (let i = 0; i < checkboxes.length; i++) {
        
        if (checkboxes[i].checked == true) {
            arr.push(checkboxes[i].value)
        }
    }
    $.get(roleuserlink, function (roledata, status) {
        $.get(userlink, function (userdata, status) {
            $.get(personlink, function (persondata, status) {
                $.get(rolelink, function (rolesndata, status) {
                    for (i = 0; i < roledata.$values.length; i++) {
                        for (a = 0; a < arr.length; a++) {
                            for (p = 0; p < persondata.$values.length; p++) {
                                for (u = 0; u < userdata.$values.length; u++) {
                                    if (roledata.$values[i].roleId == arr[a]) {
                                        if (roledata.$values[i].usersId == userdata.$values[u].id) {
                                            if (userdata.$values[u].personId == persondata.$values[p].id) {
                                                for (r = 0; r < rolesndata.$values.length; r++) {
                                                    if (rolesndata.$values[r].id == arr[a]) {
                                                        var role = rolesndata.$values[r].title;
                                                    }
                                                }
                                                if (usarname.val() == "") {
                                                    console.log("asdasdasd")
                                                }
                                                console.log(usarname.val())
                                                var trElement = $('<tr></tr>');
                                                var tdProduct = $('<td></td>').addClass('product').html('<strong>' + userdata.$values[u].login + '</strong><br>' + persondata.$values[p].firstname + '  ' + persondata.$values[p].lastname);
                                                var tdRate = $('<td></td>').addClass('rate text-center').text(persondata.$values[p].email);
                                                var tdPrice = $('<td></td>').addClass('price text-end').text(role);

                                                // Append the <td> elements to the <tr> element
                                                trElement.append(tdProduct, tdRate, tdPrice)
                                                userbody.append(trElement);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                })
            })
        })
    });
});
*/
const checkboxes = $("[id='rolecheck']");
const userbody = $("#userbody");
const usarname = $("#usarname");
const email = $("#email")
const arr = [];
const personlink = "https://localhost:7291/user/person";
const userlink = "https://localhost:7291/user/users";
const rolelink = "https://localhost:7291/user/roles";
const roleuserlink = "https://localhost:7291/user/rolesuser";
$("#target").on("click", async function () {
    $("#userbody").empty();
    arr.length = 0;
    for (let i = 0; i < checkboxes.length; i++) {

        if (checkboxes[i].checked == true) {
            arr.push(checkboxes[i].value)
        }
    }
    try {
        const [roledata, userdata, persondata, rolesndata] = await Promise.all([
            getData(roleuserlink),
            getData(userlink),
            getData(personlink),
            getData(rolelink)
        ]);
        
        for (const role of roledata.$values) {
            console.log("sada")
            for (a = 0; a < arr.length; a++) {
                if (role.roleId == arr[a]) {
                    console.log("sada")
                    const user = userdata.$values.find(u => u.id === role.usersId);
                    if (user) {
                        const person = persondata.$values.find(p => p.id === user.personId);
                        if (person) {
                            const roleItem = rolesndata.$values.find(r => r.id === role.roleId);
                            const roleTitle = roleItem ? roleItem.title : "";
                            if (usarname.val() === "") {
                                console.log("asdasdasd");
                            }
                            console.log(usarname.val());

                            const trElement = $('<tr></tr>');
                            const tdProduct = $('<td></td>').addClass('product').html(`<strong>${user.login}</strong><br>${person.firstname} ${person.lastname}`);
                            const tdRate = $('<td></td>').addClass('rate text-center').text(person.email);
                            const tdPrice = $('<td></td>').addClass('price text-end').text(roleTitle);

                            trElement.append(tdProduct, tdRate, tdPrice);
                            userbody.append(trElement);
                        }
                    }
                }
            }
        }
    } catch (error) {
        console.error(error);
    }
});

// Define a function to perform the GET request
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