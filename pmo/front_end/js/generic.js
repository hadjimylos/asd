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
