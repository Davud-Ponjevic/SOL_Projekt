// Base URL für die Backend-API
const baseURL = "http://localhost:5000/api"; // Ändere dies entsprechend der tatsächlichen URL deines Backends

// Funktion zum Abrufen aller Aufgaben
function getTasks() {
    fetch(`${baseURL}/tasks`)
        .then(response => response.json())
        .then(data => {
            // Hier kannst du die erhaltenen Daten verarbeiten und in der Benutzeroberfläche anzeigen
            console.log("Alle Aufgaben:", data);
        })
        .catch(error => {
            console.error("Fehler beim Abrufen der Aufgaben:", error);
        });
}

// Funktion zum Hinzufügen einer neuen Aufgabe
function addTask(taskData) {
    fetch(`${baseURL}/tasks`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(taskData)
    })
    .then(response => response.json())
    .then(data => {
        // Hier kannst du die Bestätigung des Hinzufügens anzeigen oder andere Aktionen ausführen
        console.log("Neue Aufgabe hinzugefügt:", data);
    })
    .catch(error => {
        console.error("Fehler beim Hinzufügen einer neuen Aufgabe:", error);
    });
}

// Funktion zum Überprüfen des Datums und Aktualisieren des Mindestattributs
function checkDateInput() {
    const today = new Date().toISOString().split('T')[0]; // Aktuelles Datum im Format "YYYY-MM-DD"
    const dateInputs = document.querySelectorAll('input[type="date"]');
    dateInputs.forEach(input => {
        input.min = today; // Setze das Mindestattribut für das Datum auf das aktuelle Datum
    });
}

// Aufruf der Funktion zum Überprüfen des Datums
checkDateInput();
