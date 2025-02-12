/**
 * Theme: Attex - Responsive Bootstrap 5 Admin Dashboard
 * Author: Coderthemes
 * Module/App: Flat picker
*/

flatpickr('#basic-datepicker');

flatpickr('#datetime-datepicker', {
    enableTime: true,
    dateFormat: "Y-m-d H:i"
});

flatpickr('#humanfd-datepicker', {
    altInput: true,
    altFormat: "F j, Y",
    dateFormat: "Y-m-d",
});

flatpickr('#minmax-datepicker', {
    minDate: "2020-01",
    maxDate: "2020-03"
});

flatpickr('#disable-datepicker', {
    onReady: function () {
        this.jumpToDate("2025-01")
    },
    disable: ["2025-01-10", "2025-01-21", "2025-01-30", new Date(2025, 4, 9)],
    dateFormat: "Y-m-d",
});

flatpickr('#multiple-datepicker', {
    mode: "multiple",
    dateFormat: "Y-m-d"
});

flatpickr('#conjunction-datepicker', {
    mode: "multiple",
    dateFormat: "Y-m-d",
    conjunction: " :: "
});

flatpickr('#range-datepicker', {
    mode: "range"
});

flatpickr('#inline-datepicker', {
    inline: true
});

flatpickr('#basic-timepicker', {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i"
});

flatpickr('#fullhours-timepicker', {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    time_24hr: true
});

flatpickr('#minmax-timepicker', {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    minDate: "16:00",
    maxDate: "22:30",
});

flatpickr('#preloading-timepicker', {
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    defaultDate: "01:45"
});



//  Form Advanced
class FormAdvanced {

    initMask() {
        document.querySelectorAll('[data-toggle="input-mask"]').forEach(e => {
            const maskFormat = e.getAttribute('data-mask-format').toString().replaceAll('0', '9');
            e.setAttribute("data-mask-format", maskFormat);
            const im = new Inputmask(maskFormat);
            im.mask(e);
        });
    }

    // Choices Select plugin
    initFormChoices() {
        var choicesExamples = document.querySelectorAll("[data-choices]");
        choicesExamples.forEach(function (item) {
            var choiceData = {};
            var isChoicesVal = item.attributes;
            if (isChoicesVal["data-choices-groups"]) {
                choiceData.placeholderValue = "This is a placeholder set in the config";
            }
            if (isChoicesVal["data-choices-search-false"]) {
                choiceData.searchEnabled = false;
            }
            if (isChoicesVal["data-choices-search-true"]) {
                choiceData.searchEnabled = true;
            }
            if (isChoicesVal["data-choices-removeItem"]) {
                choiceData.removeItemButton = true;
            }
            if (isChoicesVal["data-choices-sorting-false"]) {
                choiceData.shouldSort = false;
            }
            if (isChoicesVal["data-choices-sorting-true"]) {
                choiceData.shouldSort = true;
            }
            if (isChoicesVal["data-choices-multiple-remove"]) {
                choiceData.removeItemButton = true;
            }
            if (isChoicesVal["data-choices-limit"]) {
                choiceData.maxItemCount = isChoicesVal["data-choices-limit"].value.toString();
            }
            if (isChoicesVal["data-choices-limit"]) {
                choiceData.maxItemCount = isChoicesVal["data-choices-limit"].value.toString();
            }
            if (isChoicesVal["data-choices-editItem-true"]) {
                choiceData.maxItemCount = true;
            }
            if (isChoicesVal["data-choices-editItem-false"]) {
                choiceData.maxItemCount = false;
            }
            if (isChoicesVal["data-choices-text-unique-true"]) {
                choiceData.duplicateItemsAllowed = false;
            }
            if (isChoicesVal["data-choices-text-disabled-true"]) {
                choiceData.addItems = false;
            }
            isChoicesVal["data-choices-text-disabled-true"] ? new Choices(item, choiceData).disable() : new Choices(item, choiceData);
        });
    }

    init() {
        this.initMask();
        this.initFormChoices();
    }

}
