let $ = require('jquery');

var maxHeightProp = 'max-height' + 
	(window.innerHeight - 400) +
    'px';

// set max height of screen
$('.full-height .items')
	.attr(
		'style',
		maxHeightProp
	);

$('.pane').on('click', function(e) {
	var target = $(e.currentTarget).data('panel');
	$('#' + target).toggleClass('hide');
});

// set height of nav component to screen on load
const navGroups = document.querySelector('.nav-component .wrapper .side-nav-groups');
if (navGroups) {
	const fixedHeight = document.body.scrollHeight > window.innerHeight ? document.body.scrollHeight : window.innerHeight;
	navGroups.style.height = `${fixedHeight}px`;

	const anchors = navGroups.querySelectorAll('a');
	if (anchors.length > 0) {
		var evenHeight = (window.innerHeight) / anchors.length;
		anchors.forEach((anchor) => {
			anchor.style.marginBottom = `${evenHeight - 15}px`;
		});
	}

	
}
