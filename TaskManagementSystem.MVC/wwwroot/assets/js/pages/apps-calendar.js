document.addEventListener('DOMContentLoaded', async function () {
    const calendarEl = document.getElementById('calendar');
    const appointments = await fetchAppointments();

    const events = appointments.map(app => ({
        title: app.dateName,
        start: app.startDate,
        end: app.endDate,
        description: app.description || "No description",
        location: app.location || "No location",
        className: 'bg-primary'
    }));

    const calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        editable: true,
        selectable: true,
        locale: 'az',
        events: events,
        eventClick: function (info) {
            alert("Görüş: " + info.event.title + "\nMəkan: " + info.event.extendedProps.location);
        }
    });

    calendar.render();
});

async function fetchAppointments() {
    try {
        const response = await fetch('/api/appointment');
        if (!response.ok) {
            throw new Error('Appointments alınmadı');
        }
        return await response.json();
    } catch (error) {
        console.error('Appointment məlumatı alınmadı:', error);
        return [];
    }
}