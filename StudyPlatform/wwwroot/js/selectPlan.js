function selectClass(className) {
    // Обновляем текст кнопки
    document.getElementById('dropdownMenuButton').innerText = className;

    // Обновляем значение всех скрытых полей GradeSubjectClass в формах
    var gradeClassInputs = document.querySelectorAll('.GradeSubjectClassInput');
    gradeClassInputs.forEach(function (input) {
        input.value = className;
    });

    var chk_box1 = document.getElementById('mathematics');
    var chk_box2 = document.getElementById('history');
    var chk_box3 = document.getElementById('ukrainian-language');
    if (className == '10 КЛАС') {
        chk_box1.value = 1;
        chk_box2.value = 2;
        chk_box3.value = 3;
    } else if (className == '11 КЛАС') {
        chk_box1.value = 6;
        chk_box2.value = 5;
        chk_box3.value = 4;
    }

    console.log(chk_box1.value + " " + chk_box2.value + " " + chk_box3.value);
}

document.addEventListener('DOMContentLoaded', function () {
    // Устанавливаем начальное значение скрытого поля на основе кнопки
    var initialClass = document.getElementById('dropdownMenuButton').innerText;
    var gradeClassInputs = document.querySelectorAll('.GradeSubjectClassInput');
    gradeClassInputs.forEach(function (input) {
        input.value = initialClass;
    });

    // Скроем все группы по умолчанию
    updatePlanLessons();
    console.log("DOMContentLoaded ВТОРАЯ ФУНКЦИЯ");
});

function updatePlanLessons() {
    var selectedSubjects = document.querySelectorAll('input[name="subject"]:checked').length;

    // Обновляем отображение групп планов подписок в зависимости от выбранного количества предметов
    var planGroups = document.querySelectorAll('.plans-group');
    planGroups.forEach(function (group) {
        var lessonsCount = parseInt(group.getAttribute('data-lessons-count'), 10);
        if (lessonsCount === selectedSubjects) {
            group.style.display = 'flex';
        } else {
            group.style.display = 'none';
        }
    });

    console.log("updatePlanLessons ФУНКЦИЯ");
}

function updateSelectedSubjects() {
    var selectedCheckboxes = document.querySelectorAll('input[name="subject"]:checked');
    var currentValues = Array.from(selectedCheckboxes).map(cb => cb.value);

    var newValue = currentValues.join(',');

    // Обновляем все скрытые поля SubjectsId
    var subjectsInputs = document.querySelectorAll('input[name="SubjectsId"]');
    subjectsInputs.forEach(function (input) {
        input.value = newValue;
        console.log("Updated SubjectsId value: ", input.value);
    });

    console.log("updateSelectedSubjects ФУНКЦИЯ");
}

function onCheckboxChange() {
    updateSelectedSubjects();
    updatePlanLessons();
}

// Attach event listeners to each checkbox
document.querySelectorAll('#subject-selection input[type="checkbox"]').forEach(function (checkbox) {
    checkbox.addEventListener('change', onCheckboxChange);
});

function scrollToSection() {
    var selectedSubjects = document.querySelectorAll('input[name="subject"]:checked').length;

    if (selectedSubjects === 0) {
        // Прокрутка к чекбоксам
        var target = document.getElementById('ForScroll');
        target.scrollIntoView({ behavior: 'smooth' });
    } else {
        // Обновляем отображение планов перед прокруткой
        updatePlanLessons();

        // Прокрутка к планам
        var target = document.getElementById('plan-selection');
        target.scrollIntoView({ behavior: 'smooth' });
    }
}
