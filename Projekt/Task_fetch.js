document.addEventListener('DOMContentLoaded', function() {
    // Funktion zum Abrufen von Aufgaben
    function getTasks() {
        // Fetch-Anfrage...
    }

    // Formular-Eventlistener hinzufügen
    document.getElementById('taskForm').addEventListener('submit', function(event) {
        event.preventDefault(); // Standardformular-Absenden verhindern

        // Benutzereingaben aus dem Formular abrufen
        const formData = new FormData(event.target);
        const taskData = Object.fromEntries(formData.entries());

        // POST-Anfrage zum Hinzufügen einer Aufgabe senden
        fetch('https://localhost:7230/api/Tasks', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(taskData)
        })
        .then(response => {
            if (response.ok) {
                // Aufgaben neu abrufen, um die aktualisierte Liste anzuzeigen
                getTasks();
                // Formular zurücksetzen
                event.target.reset();
            } else {
                throw new Error('Failed to add task');
            }
        })
        .catch(error => console.error('Error adding task:', error));
    });

    // Aufgaben beim Laden der Seite abrufen
    getTasks();
});
