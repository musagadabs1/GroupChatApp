// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let currentGroupId = null;

var pusher = new Pusher('PUSHER_APP_KEY', {
    cluster: 'PUSHER_APP_CLUSTER',
    encrypted: true
});

var channel = pusher.subscribe('group_chat');
channel.bind('new_group', function (data) {
    reloadGroup();
});

$('#CreateNewGroupButton').click(function () {
    let userNames = $("input[name='UserName[]']:checked").map(function () {
        return $(this).val();
    }).get();
    let myData = {
        GroupName: $('GroupName').val(),
        UserNames: userNames
    };
    $.ajax({
        type: 'POST',
        url: "api/group",
        data: JSON.stringify(myData),
        success: (myData) => {
            $('#createNewGroup').modal('hide');
        },
        dataType: 'json',
        contentType:'application/json'
    });
});

$('#groups').on('click',".group",function () {
    let group_id = $(this).attr("data-group_id");
    $('.group').css({ "border-style": "none", cursor: "pointer" });
    $(this).css({ "border-style": "inset", cursor: "default" });

    $('#currentGroup').val(group_id);//update the current group_id to html file...
    currentGroupId = group_id;

    //Get all the messages  for the group and populate it
    $.get("/api/message/" + group_id, function (data) {
        let message = "";

        data.forEach(function (data) {
            let position = (data.addedBy == $('#userName').val()) ? "float-right" : "";

            message += '<div class="row chat_message' + position + '"> <b>' + data.addedBy + ':</b>' + data.message + '</div>';
        });
        $('chat_body').html(message);
    });
    // check the user have subscribed to the channel before.
    if (!pusher.channel('private-' + group_id)) {
        let group_channel = pusher.subscribe('private-' + group_id);

        group_channel.bind('new_message', function (data) {
            if (currentGroupId == data.new_message.GroupId) {

                $(".chat_body").append(`<div class="row chat_message"><b>` + data.new_message.AddedBy + `: </b>` + data.new_message.message + ` </div>`);
            }
        });  
    }
});

$("#SendMessage").click(function () {
    $.ajax({
        type: "POST",
        url: "/api/message",
        data: JSON.stringify({
            AddedBy: $("#UserName").val(),
            GroupId: $("#currentGroup").val(),
            message: $("#Message").val(),
            socketId: pusher.connection.socket_id
        }),
        success: (data) => {
            $(".chat_body").append(`<div class="row chat_message float-right"><b>`
                + data.data.addedBy + `: </b>` + $("#Message").val() + `</div>`
            );

            $("#Message").val('');
        },
        dataType: 'json',
        contentType: 'application/json'
    });
});

function reloadGroup() {
    $.get("/api/group", function (data) {
        let groups = "";

        data.forEach(function (group) {
            groups += `<div class="group" data-group_id="`
                + group.groupId + `">` + group.groupName +
                `</div>`;
        });

        $("#groups").html(groups);
    });
}