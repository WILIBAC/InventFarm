$(document).ready(function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/medicamentosHub")
        .build();

    connection.on("ReceiveNotification", (title, message) => {
        const notificationCountElement = $("#notificationCount");
        const notificationListElement = $("#notificationList");

        // Incrementar el contador de notificaciones
        let count = parseInt(notificationCountElement.text());
        count += 1;
        notificationCountElement.text(count);

        // Agregar la nueva notificación al dropdown
        const notificationItem = `<a class="dropdown-item" href="#">${title}: ${message}</a>`;
        notificationListElement.prepend(notificationItem);
    });

    connection.start()
        .then(() => console.log('Conectado a SignalR'))
        .catch(err => console.error('Error al conectar a SignalR:', err.toString()));
});
