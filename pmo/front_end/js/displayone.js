const components = document.querySelectorAll('.displayone-component');
const component_present = components.length > 0;

function set_active_option(label) {
    const component = label.closest('.displayone-component');
    const option_id = label.dataset.optionLabelId;
    component.querySelector(`[data-option-id="${option_id}"].option`).classList.add('active');
}

function change_selection(label) {
    const component = label.closest('.displayone-component');

    // remove current active from option
    component.querySelector('.option.active').classList.remove('active');
      
    // remove current active label
    component.querySelector('.option-label.active').classList.remove('active');

    // set active label
    label.classList.add('active');

    // set active option
    set_active_option(label)
}

if (component_present) {

    // set defaults
    components.forEach(
        (component) => {
            // remove active from options
            component.querySelectorAll('.option.active').forEach(
                (active) => active.classList.remove('active')
            );

            // ensure at least one label is active
            let active_labels = component.querySelectorAll('.option-label.active');

            // dirty state scenario
            if (active_labels.length > 1) {
                active_labels.forEach((label) => label.classList.remove('active'));
            }

            if (active_labels.length === 1) {
                // set corresponding active
                set_active_option(active_labels[0]);
            } else {
                // set first of label and option to active
                let first_label = component.querySelector('.option-label');
                first_label.classList.add('active');
                set_active_option(first_label);
            }
        }
    );

    // set event listeners
    components.forEach((component) => {
        component.querySelectorAll('.option-label').forEach (
            (label) => {
                label.addEventListener("click", (e) => {
                    change_selection(e.currentTarget);
                });
            }
        )

        
    })
}
