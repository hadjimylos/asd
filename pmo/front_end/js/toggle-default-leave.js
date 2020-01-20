let $ = require('jquery');

$('.toggle-default-isdays').on('change', (e) => {
    const default_val = e.currentTarget.checked ? 25 : 187.5;
    document.querySelector('.data.annual-leave').innerText = default_val
});