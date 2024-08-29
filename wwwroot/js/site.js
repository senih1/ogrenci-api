let students = []

function render() {
    studentList.innerHTML = students.map(x => `
        <tr data-id="${x.id}">
            <td>${x.name}</td>
            <td>${x.surname}</td>
            <td>${x.age}</td>
            <td>${x.email}</td>
            <td>${x.phoneNumber}</td>
        </tr>
    `).join("");
}

function init() {
    fetch('/api')
        .then(res => res.json())
        .then(function (res) {
            students = res;
            render();
        });
}

init();