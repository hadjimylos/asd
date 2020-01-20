// must use jquery as daterangepicker dependency
let $ = require('jquery');
require('moment');
require('daterangepicker');

const single_select_calendars = $('.single-select-calendar');
const range_select_calendars = $('.range-select-calendar');

// current page
const employee_id = document.querySelector('[name="employee_id"]');

// configs
var min_date = new Date();
var max_date = new Date(new Date().getFullYear() + 1, 11, 31)

// default configs for daterangepicker
var options = {
    opens: 'center',
    drops: 'up',
    locale: {
        // show UK date format
        format: 'DD/MM/YYYY'
    },
};

function add_eventlistener_by_domelement(calendar_element) {
    if (calendar_element.value) {
        // set selected start date
        options.startDate = calendar_element.value;
        $(calendar_element).daterangepicker(options);

        // remove config property
        delete options.startDate;
    } else {
        // add without start date selection (sets to default [today])
        $(calendar_element).daterangepicker(options);
    }
}

function bind_calendars() {
    if (range_select_calendars.length > 0) {
        range_select_calendars.each(function (index, element) {
            add_eventlistener_by_domelement(element);
        });
    }

    if (single_select_calendars.length > 0) {
        options.singleDatePicker = true;
        options.endDate = max_date;

        // bind to each element individually and set start date to populated value
        single_select_calendars.each(function (index, element) {
            add_eventlistener_by_domelement(element);
        });
    }
}

// if page consists of any calendar components bind accordingly
if (range_select_calendars.length > 0 || single_select_calendars.length > 0) {
    // simple bind
    if (!employee_id) {
        bind_calendars();
    }

    // get employee bookings and unallow in calendar
    if (employee_id) {
        options.minDate = min_date;
        options.maxDate = max_date;

        $.get({
            url: `/bank-holidays/employees/${employee_id.value}`,
            success: (holidays) => {

                // define disallowed dates in range pickers
                options.isInvalidDate = (date) => {
                    // date iterates through all dates available in calendar picker (if condition met and return true it gets disabled)
                    if (holidays.includes(date.format('YYYY-MM-DD'))) {
                        return true;
                    }
                }

                bind_calendars();
            }
        });
    }
    
}
