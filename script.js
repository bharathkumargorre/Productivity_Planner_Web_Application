// Load everything when page loads
window.onload = function () {
    loadTasks();
    loadPlanner();
};

// Add task
function addTask() {
    let input = document.getElementById("taskInput");
    if (!input) return;

    let task = input.value.trim();

    if (task === "") {
        alert("Enter a task!");
        return;
    }

    let tasks = JSON.parse(localStorage.getItem("tasks")) || [];
    tasks.push({ text: task, done: false });

    localStorage.setItem("tasks", JSON.stringify(tasks));

    input.value = "";
    loadTasks();
}

// Load tasks
function loadTasks() {
    let taskList = document.getElementById("taskList");
    if (!taskList) return;

    taskList.innerHTML = "";

    let tasks = JSON.parse(localStorage.getItem("tasks")) || [];

    tasks.forEach((task, index) => {
        let li = document.createElement("li");

        // Checkbox
        let checkbox = document.createElement("input");
        checkbox.type = "checkbox";
        checkbox.checked = task.done;

        checkbox.onchange = function () {
            tasks[index].done = checkbox.checked;
            localStorage.setItem("tasks", JSON.stringify(tasks));
            loadTasks();
        };

        // Text
        let span = document.createElement("span");
        span.textContent = task.text;

        if (task.done) {
            span.style.textDecoration = "line-through";
            span.style.opacity = "0.6";
        }

        // Delete button
        let delBtn = document.createElement("button");
        delBtn.textContent = "❌";

        delBtn.onclick = function () {
            li.style.transition = "0.3s";
            li.style.opacity = "0";
            li.style.transform = "translateX(20px)";

            setTimeout(() => {
                tasks.splice(index, 1);
                localStorage.setItem("tasks", JSON.stringify(tasks));
                loadTasks();
            }, 300);
        };

        li.appendChild(checkbox);
        li.appendChild(span);
        li.appendChild(delBtn);

        taskList.appendChild(li);
    });
}

// Enter key support
document.addEventListener("DOMContentLoaded", function () {
    let input = document.getElementById("taskInput");
    if (input) {
        input.addEventListener("keypress", function (e) {
            if (e.key === "Enter") {
                addTask();
            }
        });
    }
});

// Mood selection
function setMood(e) {
    if (e.target.tagName === "SPAN") {
        document.getElementById("mood").value = e.target.textContent;
    }
}

// Save planner data
function saveData() {
    localStorage.setItem("goals", document.getElementById("goals")?.value || "");
    localStorage.setItem("notes", document.getElementById("notes")?.value || "");
    localStorage.setItem("mood", document.getElementById("mood")?.value || "");

    alert("Saved Successfully!");
}

// Load planner data
function loadPlanner() {
    if (document.getElementById("goals"))
        document.getElementById("goals").value = localStorage.getItem("goals") || "";

    if (document.getElementById("notes"))
        document.getElementById("notes").value = localStorage.getItem("notes") || "";

    if (document.getElementById("mood"))
        document.getElementById("mood").value = localStorage.getItem("mood") || "";
}