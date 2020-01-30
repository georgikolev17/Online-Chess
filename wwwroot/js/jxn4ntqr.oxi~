var connection = new signalR.HubConnectionBuilder().whitUrl("/game").build();

connectionl.on("Move", function () {
    var msg = document.createElement("h1");
    msg.textContent = "Your opponent moved!";

    document.getElementById("message").appendChild(msg);
})