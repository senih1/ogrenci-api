let students = []

function handleSubmit(e) {
    e.preventDefault();
    const formData = new FormData(studentInputForm);
    const formObj = Object.fromEntries(formData);
    fetch("/api", {
        method: "POST",
        body: formData
    }).then(res => res.json())
        .then(function (res) {
            students.unshift(res);
            studentInputForm.reset();
            render();
        });
}
function handleClick() {
    console.log(this.dataset.id)
}
function bindEvents() {
    document.querySelectorAll("#studentList li").forEach(x => x.addEventListener("click", handleClick));
}

studentInputForm.addEventListener("submit", handleSubmit);