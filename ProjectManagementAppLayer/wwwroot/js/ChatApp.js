
// to open connection between client and server
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub/chatHub")
    .build();



connection.on("JoinRoom", function (user) {
    msg = "has joind the room";
    var msgs = user + ":" + msg;
    console.log(msgs)
    // how to make it disaaper after 3 sec
    setTimeout(function () {
        $("#alertDiv").fadeOut().empty();
    }, 5000);
    var spans = document.getElementById('alertDiv')
    spans.textContent = msgs
    console.log(spans)
    $("#alertDiv").html(msgs)
});


connection.on("SendUserMessage", function (sender, message, sendAt) {
    var msg = sender + ":" + message + "\t" + sendAt;
    var li = document.createElement("li");
    console.log(msg)
    li.textContent = msg;
    $("#list").append(li);

});



function fullfiled() {
    var Usermsgs = $('#sender').val();
    connection.invoke("JoinRoom", Usermsgs);
    $("#btnSend").on('click', function () {
        var msgs = $('#message').val();
        const date = new Date();
        console.log(date.toLocaleString());
        var sendAt = date.toLocaleString();
        console.log(sendAt)
        connection.invoke("SendMessageSync", Usermsgs, msgs, sendAt);

    })
};
function rejected() {
    alert('Something went wrong !!')
};
connection.start().then(fullfiled, rejected);