document.addEventListener('DOMContentLoaded', function() {
    // Funktion zum Abrufen von Ereignissen
    function getEvents() {
        // Fetch-Anfrage zum Abrufen der Ereignisse
        fetch('https://localhost:7230/api/Events')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to fetch events');
                }
                return response.json();
            })
            .then(data => {
                const eventsContainer = document.getElementById('events');
                eventsContainer.innerHTML = '';

                data.forEach(event => {
                    const eventElement = document.createElement('div');
                    eventElement.className = 'event';
                    eventElement.innerHTML = `
                        <h3>${event.title}</h3>
                        <p>${event.description}</p>
                        <p>Date: ${event.date}</p>
                    `;
                    eventsContainer.appendChild(eventElement);
                });
            })
            .catch(error => console.error('Error fetching events:', error));
    }

    // Formular-Eventlistener hinzufügen
    document.getElementById('eventForm').addEventListener('submit', function(event) {
        event.preventDefault(); // Standardformular-Absenden verhindern

        // Benutzereingaben aus dem Formular abrufen
        const formData = new FormData(event.target);
        const eventData = Object.fromEntries(formData.entries());

        // POST-Anfrage zum Hinzufügen eines Ereignisses senden
        fetch('https://localhost:7230/api/Events', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(eventData)
        })
        .then(response => {
            if (response.ok) {
                // Ereignisse neu abrufen, um die aktualisierte Liste anzuzeigen
                getEvents();
                // Formular zurücksetzen
                event.target.reset();
            } else {
                throw new Error('Failed to add event');
            }
        })
        .catch(error => console.error('Error adding event:', error));
    });

    // Ereignisse beim Laden der Seite abrufen
    getEvents();
});
