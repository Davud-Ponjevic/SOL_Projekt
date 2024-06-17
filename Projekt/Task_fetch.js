document.addEventListener('DOMContentLoaded', function() {
    function getTasks() {
        fetch('https://localhost:7230/api/Tasks')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to fetch tasks');
                }
                return response.json();
            })
            .then(data => {
                const tasksContainer = document.getElementById('tasks');
                tasksContainer.innerHTML = '';

                data.forEach(task => {
                    const taskElement = document.createElement('div');
                    taskElement.className = 'card mb-3';
                    taskElement.innerHTML = `
                        <div class="card-body">
                            <h5 class="card-title">${task.title}</h5>
                            <p class="card-text">${task.note}</p>
                            <p class="card-text"><small class="text-muted">Due: ${task.dueDate}</small></p>
                        </div>
                    `;
                    tasksContainer.appendChild(taskElement);
                });
            })
            .catch(error => console.error('Error fetching tasks:', error));
    }

    document.getElementById('taskForm').addEventListener('submit', function(event) {
        event.preventDefault();

        const formData = new FormData(event.target);
        const taskData = Object.fromEntries(formData.entries());

        fetch('https://localhost:7230/api/Tasks', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(taskData)
        })
        .then(response => {
            if (response.ok) {
                getTasks();
                event.target.reset();
            } else {
                throw new Error('Failed to add task');
            }
        })
        .catch(error => console.error('Error adding task:', error));
    });

    getTasks();
});
