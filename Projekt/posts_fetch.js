document.addEventListener('DOMContentLoaded', () => {
    fetchTasks();

    // Event-Listener für das Löschen von Aufgaben
    document.getElementById('tasks').addEventListener('click', handleTaskDelete);

    // Event-Listener für das Hinzufügen einer neuen Aufgabe
    document.getElementById('addTaskForm').addEventListener('submit', handleTaskFormSubmit);
});

function fetchTasks() {
    fetch('https://localhost:7230/api/Tasks')
        .then(response => {
            if (!response.ok) {
                throw new Error('Fehler beim Abrufen der Aufgaben');
            }
            return response.json();
        })
        .then(tasks => {
            const tasksContainer = document.getElementById('tasks');
            tasksContainer.innerHTML = '';

            tasks.forEach(task => {
                const taskElement = createTaskElement(task);
                tasksContainer.appendChild(taskElement);
            });
        })
        .catch(error => console.error('Fehler beim Abrufen der Aufgaben:', error));
}

function createTaskElement(task) {
    const taskElement = document.createElement('div');
    taskElement.classList.add('card', 'mb-3');
    taskElement.innerHTML = `
        <div class="card-body">
            <h5 class="card-title">${task.title}</h5>
            <p class="card-text">${task.note}</p>
            <p class="card-text"><small class="text-muted">Fällig am: ${task.dueDate}</small></p>
            <button class="btn btn-danger btn-sm delete-task" data-id="${task.id}">Löschen Aufgabe</button>
        </div>
    `;
    return taskElement;
}

function handleTaskFormSubmit(event) {
    event.preventDefault();

    const formData = new FormData(event.target);
    const taskData = {
        title: formData.get('title'),
        note: formData.get('note'),
        dueDate: formData.get('dueDate')
    };

    addTask(taskData);
    event.target.reset();
}

function addTask(taskData) {
    fetch('https://localhost:7230/api/Tasks', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(taskData)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Aufgabe konnte nicht hinzugefügt werden');
        }
        fetchTasks(); // Aktualisieren der Aufgaben nach dem Hinzufügen
    })
    .catch(error => console.error('Fehler beim Hinzufügen der Aufgabe:', error));
}

function handleTaskDelete(event) {
    if (event.target.classList.contains('delete-task')) {
        const taskId = event.target.getAttribute('data-id');
        deleteTask(taskId);
    }
}

function deleteTask(taskId) {
    fetch(`https://localhost:7230/api/Tasks/${taskId}`, {
        method: 'DELETE'
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Aufgabe konnte nicht gelöscht werden');
        }
        fetchTasks(); // Aktualisieren der Aufgaben nach dem Löschen
    })
    .catch(error => console.error('Fehler beim Löschen der Aufgabe:', error));
}
