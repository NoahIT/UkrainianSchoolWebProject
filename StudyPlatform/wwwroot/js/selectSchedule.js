document.addEventListener("DOMContentLoaded", function () {
    // Получение данных из элемента script с id "schedule-data"
    var scheduleData = JSON.parse(document.getElementById('schedule-data').textContent);

    var schedules = {
        '10': scheduleData.Schedule10,
        '11': scheduleData.Schedule11
    };

    function selectClassSchedule(classNumber, className) {
        document.getElementById('selectedClass1').innerText = className;
        var schedule = schedules[classNumber];
        var cells = document.querySelectorAll('.schedule-cell');

        cells.forEach(function (cell) {
            var day = cell.getAttribute('data-day');
            var hour = cell.getAttribute('data-hour');
            var cellContent = '';

            schedule.forEach(function (item) {
                if (item.Day === day && item.Time === hour) {
                    cellContent += '<div class="' + item.Teacher.Subject.AbbrSubject + '">';
                    cellContent += item.Time + '<br>' + item.Teacher.Subject.Name + '<br>' + item.Teacher.User.FirstName;
                    cellContent += '</div>';
                }
            });

            cell.innerHTML = cellContent;
        });
    }

    // Default load for class 10
    selectClassSchedule('10', '10 КЛАС');

    // Сделайте функцию доступной глобально
    window.selectClassSchedule = selectClassSchedule;
});
