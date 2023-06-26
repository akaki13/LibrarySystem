var domainName = window.location.origin;
const getauthorlink = "/bookcategory/getauthor";
const getgenrelink = "/bookcategory/getgenres";
const getpublisherlink = "/bookcategory/getpublishers";
const getlanguagelink = "/bookcategory/getlanguage";
const getstoragelink = "/bookcategory/getstorage";
/*$(document).ready(async function () {
    const [genredata] = await Promise.all([
        getData(domainName + getgenrelink),
    ]);
    console.log(genredata)
    const genreList = genredata.$values.map(genre => ({
        value: genre.name,
        id: genre.id,
    }));

    genreInput.autocomplete({
        source: genreList,
        minLength: 0,
        multiselect: true,
        select: function (event, ui) {
            const selectedGenreId = ui.item.id;
            genreId.val(selectedGenreId);
            console.log(ui.item)
        }
    });

    genreInput.on('click', function () {
        genreInput.autocomplete('search', '');
    });
});*/

$(document).ready(async function () {
    const [genredata, publisherdata, authordata, languagedata, storagedata ] = await Promise.all([
        getData(domainName + getgenrelink),
        getData(domainName + getpublisherlink),
        getData(domainName + getauthorlink),
        getData(domainName + getlanguagelink),
        getData(domainName + getstoragelink),
    ]); 
    const genreList = genredata.$values.map(genre => ({
        text: genre.name,
        id: genre.id,
    }));
    const publisherList = publisherdata.$values.map(publisher => ({
        text: publisher.name,
        id: publisher.id,
    }));
    const authorList = authordata.$values.map(author => ({
        text: author.name +" "+ author.surname,
        id: author.id,
    }));
    const languageList = languagedata.$values.map(language => ({
        text: language.title,
        id: language.id,
    }));
    const storageList = storagedata.$values.map(storage => ({
        text: storage.name,
        id: storage.id,
    }));
    $('#GenreSelect').select2({
        placeholder: 'Select Genre',
        data: genreList,
    });
    $('#PublisherSelect').select2({
        placeholder: 'Select Publisher',
        data: publisherList,
    });
    $('#AuthorSelect').select2({
        placeholder: 'Select Author',
        data: authorList,
    });
    $('#LanguageSelect').select2({
        placeholder: 'Select Language',
        data: languageList,
    });
    $('#StorageSelect').select2({
        placeholder: 'Select Storage',
        data: storageList,
    });

    if (publisherIds !== '') {
        var parsedPublisheIds = JSON.parse(publisherIds);
        $('#PublisherSelect').val(parsedPublisheIds).trigger('change');
    }
    if (authorIds !== '') {
        var parsedAuthorIds = JSON.parse(authorIds);
        $('#AuthorSelect').val(parsedAuthorIds).trigger('change');
    }
    if (languageIds !== '') {
        var parsedLanguageIds = JSON.parse(languageIds);
        $('#LanguageSelect').val(parsedLanguageIds).trigger('change');
    }
    if (storageIds !== '') {
        var parsedStorageIds = JSON.parse(storageIds);
        $('#StorageSelect').val(parsedStorageIds).trigger('change');
    }
    if (genreIds !== '') {
        var parsedGenreIds = JSON.parse(genreIds);
        $('#GenreSelect').val(parsedGenreIds).trigger('change');
    }

});
