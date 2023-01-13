var today = new Date().toISOString().slice(0, 10);
document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        initialDate: today,
        /*titleFormat: 'MMMM, dddd D, YYYY',*/
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        events: [
            {
                title: 'All Day Event',
                start: '2023-01-10',
                end: '2023-02-10'
            },
            {
                title: 'Long Event',
                start: '2022-12-07',
                end: '2022-12-10'
            },
            {
                groupId: '999',
                title: 'Repeating Event',
                start: '2022-12-09T16:00:00'
            },
            {
                groupId: '999',
                title: 'Repeating Event',
                start: '2022-12-16T16:00:00'
            },
            {
                title: 'Conference',
                start: '2022-12-11',
                end: '2022-12-13'
            },
            {
                title: 'Meeting',
                start: '2022-12-12T10:30:00',
                end: '2022-12-12T12:30:00'
            },
            {
                title: 'Lunch',
                start: '2022-12-12T12:00:00'
            },
            {
                title: 'Meeting',
                start: '2022-12-12T14:30:00'
            },
            {
                title: 'Birthday Party',
                start: '2022-12-13T07:00:00'
            },
            {
                title: 'Click for Google',
                url: 'http://google.com/',
                start: '2022-12-28'
            },
            {
                title: 'Zaidddd',
                start: '2022-12-28',
                end: '2022-12-30'
            }
        ]
    });

    calendar.render();
});

