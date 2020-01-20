const togglers = document.querySelectorAll('.checkbox-toggle');

togglers.forEach((checkbox) => {
    // on initialize show hide base on is_checked or not
    is_selected = checkbox.checked;
    const css_selector = checkbox.getAttribute('targetcssselector');
    const target = document.querySelector(css_selector);
    let target_is_visible = target.offsetParent !== null;

    // initialized state
    if (is_selected && !target_is_visible) {
        target.style.display = 'block'
    } else if (!is_selected && target_is_visible) {
        target.style.display = 'none'
    }

    checkbox.addEventListener('click', () => {
        target_is_visible = target.offsetParent !== null;

        // toggle state
        if (target_is_visible) {
            target.style.display = 'none'
        } else {
            target.style.display = 'block'
        }
    });
});
