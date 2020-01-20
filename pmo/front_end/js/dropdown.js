// use jquery
let $ = require('jquery');
require('select2');

// define code
$(document).ready(function () {
    $('.select2').select2();

    var url = window.location.href;
    var id = url.substring(url.lastIndexOf('/') + 1);
    if (Number.isInteger(parseInt(id)) && url.includes('employees')) {
        $('.select2.employee').val(`${id}`);
    }
});
