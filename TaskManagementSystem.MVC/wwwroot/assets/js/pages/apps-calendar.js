class MyCalendar {
    constructor() {
        this.body = document.body;
        this.calendar = document.getElementById('calendar');
        this.taskSummary = document.getElementById('task-summary'); // Ümumi task sayı üçün
        this.calendarObj = null;
    }

    updateTaskSummary() {
        if (!this.calendarObj) return;

        let events = this.calendarObj.getEvents();
        let summary = {};

        events.forEach(event => {
            let eventDate = this.formatDate(event.start);
            summary[eventDate] = (summary[eventDate] || 0) + 1;
        });

        let summaryHtml = `<b>📌 Events:</b> `;
        Object.keys(summary).forEach(date => {
            summaryHtml += `<span style="margin: 0 8px; padding: 4px 10px; background: white; color: black; border-radius: 6px; box-shadow: 0 2px 5px rgba(0,0,0,0.2);">${date}: ${summary[date]}</span>`;
        });

        if (this.taskSummary) {
            this.taskSummary.innerHTML = summaryHtml;
        }

        this.updateDayCounts();
    }

    updateDayCounts() {
        if (!this.calendarObj) return;

        let events = this.calendarObj.getEvents();
        let days = document.querySelectorAll('.fc-daygrid-day');

        days.forEach(day => {
            let date = day.getAttribute('data-date'); // Günün tarixi YYYY-MM-DD formatında

            let oldBadge = day.querySelector('.task-count');
            if (oldBadge) {
                oldBadge.remove();
            }

            let count = events.filter(event => this.formatDate(event.start) === date).length;

            if (count > 0) {
                let badge = document.createElement("span");
                badge.classList.add("task-count");

                // Task sayına görə fərqli rənglər
                if (count > 5) {
                    badge.classList.add("high");
                }
                if (count > 10) {
                    badge.classList.add("critical");
                }

                badge.innerText = count;
                day.style.position = "relative";
                day.appendChild(badge);
            }
        });
    }

    // Tarixi düzgün formatlamaq üçün funksiya
    formatDate(date) {
        let d = new Date(date);
        let year = d.getFullYear();
        let month = (d.getMonth() + 1).toString().padStart(2, '0');
        let day = d.getDate().toString().padStart(2, '0');
        return `${year}-${month}-${day}`;
    }

    init() {
        const self = this;
        // API-dən data çəkməyə çalışırıq
        fetch('/api/appointment')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Şəbəkə xətası: Cavab uğurlu olmadı');
                }
                return response.json();
            })
            .then(dto => {
                // API-dən alınan məlumatın appointments hissəsini FullCalendar event-lərinə çeviririk
                const events = dto.appointments.map(appt => ({
                    title: appt.dateName || 'Tədbir',        // "dateName" sahəsini title kimi istifadə edirik
                    start: appt.startDate,                   // "startDate" sahəsi
                    end: appt.endDate,                       // "endDate" sahəsi (əgər varsa)
                    className: 'bg-primary'                  // və ya statusa görə fərqli sinif təyin etmək olar
                }));

                self.createCalendar(events);
            })
            .catch(error => {
                const events = dto.appointments.map(appt => ({
                    title: appt.dateName || 'Tədbir',        // "dateName" sahəsini title kimi istifadə edirik
                    start: appt.startDate,                   // "startDate" sahəsi
                    end: appt.endDate,                       // "endDate" sahəsi (əgər varsa)
                    className: 'bg-primary'                  // və ya statusa görə fərqli sinif təyin etmək olar
                }));

                self.createCalendar(events);
            });
    }

    createCalendar(events) {
        const self = this;
        self.calendarObj = new FullCalendar.Calendar(document.getElementById('calendar'), {
            initialView: 'dayGridMonth',
            editable: true,
            droppable: true,
            selectable: true,
            initialEvents: events,
            datesSet: function () {
                self.updateTaskSummary();
            },
            eventReceive: function (info) {
                self.updateTaskSummary();
            },
            eventDrop: function (info) {
                self.updateTaskSummary();
            },
            eventRemove: function (info) {
                self.updateTaskSummary();
            },
            dayCellDidMount: function () {
                self.updateTaskSummary();
            }
        });

        self.calendarObj.render();
        self.updateTaskSummary();
    }
}

document.addEventListener('DOMContentLoaded', function () {
    // Task summary div-i yaratmaq və calendar elementindən əvvəl yerləşdirmək
    let calendarEl = document.getElementById('calendar');
    if (calendarEl && calendarEl.parentElement) {
        let taskSummaryDiv = document.createElement("div");
        taskSummaryDiv.id = "task-summary";
        taskSummaryDiv.style.padding = "12px";
        taskSummaryDiv.style.marginBottom = "10px";
        taskSummaryDiv.style.fontSize = "18px";
        calendarEl.parentElement.insertBefore(taskSummaryDiv, calendarEl);
    }
    new MyCalendar().init();
});