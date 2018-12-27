const uri = "api/vraag";
let vragen = null;
function getCount(data) {
    const el = $("#counter");
    let name = "Vraag";
    if (data) {
        if (data > 1) {
            name = "Vragen";
        }
        el.text(data + " " + name);
    } else {
        el.text("No " + name);
    }
}

$(document).ready(function () {
    getData();
    getClass();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#vragen");

            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append(
                        $("<td style=\"text-align:center\"></td>").append(
                            $("<input/>", {
                                type: "checkbox",
                                disabled: true,
                                checked: item.active
                            })
                        )
                )
                    .append($("<td></td>").text(item.id))
                    .append($("<td></td>").text(item.vraag))
                    .append($("<td></td>").text(item.type))
                    .append($("<td></td>").text(item.hint))
                    .append(
                        $("<td></td>").append(
                            $("<button class='btn btn-primary'>Antwoorden</button>").on("click", function () {
                                showAntwoorden(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button class='btn btn-primary'>Delete</button>").on("click", function () {
                                deleteItem(item.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            vragen = data;
        },
        error: function (er) {
            console.log(er);
        }
    });
}

var loggedUser = { username: "Rick", klas: "H5P", rolId: 1 };

function getClass() {
    $.ajax({
        type: "GET",
        url: "api/account",
        cache: false,
        success: function (data) {
            const tBody = $("#gebruikers");
            $(tBody).empty();
            console.log(data);

            $.each(data, function (key, item) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.id))
                    .append($("<td></td>").text(item.username))
                    .append($("<td></td>").text(item.rol))
                    .append($("<td></td>").html("<button class='btn btn-primary'>Level up</button>"));

                tr.appendTo(tBody);
            });
        }
    });
}

function addItem() {
    const item = {
        Vraag: $("#add-name").val(),
        Type: $("#add-type").val(),
        Hint: $("#add-hint").val(),
        MinLevel: $("#add-minlevel").val(),
        MaxLevel: $("#add-maxlevel").val()
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Kon de vraag helaas niet toevoegen door een server error");
        },
        success: function (result) {
            getData();
            $("#add-name").val("");
            $("#add-type").val(0);
            $("#add-hint").val("");
            $("#add-minlevel").val("");
            $("#add-maxlevel").val("");
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(vragen, function (key, item) {
        if (item.id === id) {
            $("#edit-name").val(item.name);
            $("#edit-id").val(item.id);
            $("#edit-isComplete")[0].checked = item.isComplete;
        }
    });
    $("#spoiler").css({ display: "block" });
}

function showAntwoorden(id) {
    const tBody = $("#antwoorden");

    $(tBody).empty();

    $.each(vragen, function (key, item) {
        if (item.id === id) {
            $.each(item.antwoorden, function (key, antwoord) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(antwoord.id))
                    .append($("<td></td>").text(antwoord.waarde))
                    .append($("<td></td>").text(antwoord.correctness));

                tr.appendTo(tBody);
            });
        }
    });    
}

$(".my-form").on("submit", function () {
    const item = {
        name: $("#edit-name").val(),
        isComplete: $("#edit-isComplete").is(":checked"),
        id: $("#edit-id").val()
    };

    $.ajax({
        url: uri + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $("#spoiler").css({ display: "none" });
}