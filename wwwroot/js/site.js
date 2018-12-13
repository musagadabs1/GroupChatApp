// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
});