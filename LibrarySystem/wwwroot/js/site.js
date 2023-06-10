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
    $.ajax({
        url: url,
        type: 'DELETE',
        success: function (response) {
            console.log("ok")
        },
        error: function (xhr, status, error) {
            reject({
                status: xhr.status,
                responseText: xhr.responseText
            });
        }
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
