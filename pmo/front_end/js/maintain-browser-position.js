let $ = require('jquery');

function getUrlNoParameters() {
	// pattern for everything after '?'
	const questionMarkPattern = /\/?\?.*/g;

	return document.URL.replace(questionMarkPattern, '');
}

$(document).scroll(function() {
	// get position of this page (regardless of get params)
	localStorage.page = getUrlNoParameters();
	localStorage.scrollTop = $(document).scrollTop();
});

$(document).ready(function() {
	if (localStorage.page === getUrlNoParameters()) {
		$(document).scrollTop(localStorage.scrollTop);
	}
});
