"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/GIGMPlusHub?TripRequestId=1&userId=6400f2f9-313a-4741-9349-f7b095b9f7fa").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveCloseVehicles", function (selectedVehicles) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${selectedVehicles}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("RiderSendLocation", 1.0, 1.0, 5).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});