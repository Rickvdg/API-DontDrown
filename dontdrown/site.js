const uri = "api/vraag";
let vragen = null;
let user = null;

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
    try {
        user = JSON.parse($.cookie('user'));
    } catch {
        $('body').html('<h1>Geen gebruiker is ingelogd. Ga <a href="index.html">hier</a> terug naar het inlogscherm.</h1>');
    }

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
                                checked: item.Active
                            })
                        )
                )
                    .append($("<td></td>").text(item.Id))
                    .append($("<td></td>").text(item.Vraag))
                    .append($("<td></td>").text(item.Type))
                    .append($("<td></td>").text(item.Hint))
                    .append(
                        $("<td></td>").append(
                            $("<button class='btn btn-primary'>Antwoorden</button>").on("click", function () {
                                showAntwoorden(item.Id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button class='btn btn-primary'>Delete</button>").on("click", function () {
                                deleteItem(item.Id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            vragen = data;
        },
        error: function (er) {
            console.log(er);
            alert("Er konden geen vragen opgehaald worden door een error");
        }
    });
}

function getClass() {
    $.ajax({
        type: "GET",
        url: "api/account/" + user.Classname,
        cache: false,
        success: function (data) {
            const tBody = $("#gebruikers");
            $(tBody).empty();
            $('#Classname').text(user.Classname);

            $.each(data, function (key, item) {
                var jsonData = {};
                if (item.SaveJson) {
                    jsonData = JSON.parse(item.SaveJson);
                    console.log(jsonData)
                }
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(item.Id))
                    .append($("<td></td>").text(item.Username))
                    .append($("<td></td>").text(item.Rol))

                if (jsonData.Request) {
                    tr.append($("<td></td>").html("<button class='btn btn-primary' onclick='LevelUpUser(" + item.Id + ")'>Level up</button>"));
                } else {
                    tr.append($("<td></td>"));
                }

                tr.appendTo(tBody);
            });
        }
    });
}

function LevelUpUser(id) {
    $.ajax({
        url: "api/save/upgrade/" + id,
        type: 'PUT',
        success: function () {
            getClass();
        },
        error: function () {
            alert("De speler kon door een error niet geupgrade worden.")
        }
    })
}

function addUser() {
    $.ajax({
        url: "api/account",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            "username": $("#username").val(),
            "password": $("#password").val(),
            "rolId": 2,
            "Classname": $("#classname").val()
        }),
        success: function () {
            $("#username").val("");
            $("#password").val("");
            $("#classname").val("");
            getClass();
        },
        error: function () {
            alert('User kon niet toegevoegd worden door een error');
        }
    })
}

function addItem() {
    const item = {
        Vraag: $("#add-name").val(),
        Type: $("#add-type").val(),
        Hint: $("#add-hint").val(),
        Antwoorden: [
            {
                Waarde: $("#antwoord1").val(),
                Correctness: 1
            },
            {
                Waarde: $("#antwoord2").val(),
                Correctness: 2
            },
            {
                Waarde: $("#antwoord3").val(),
                Correctness: 3
            },
            {
                Waarde: $("#antwoord4").val(),
                Correctness: 4
            },
            {
                Waarde: $("#antwoord5").val(),
                Correctness: 5
            },
            {
                Waarde: $("#antwoord6").val(),
                Correctness: 6
            }
        ],
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
        success: function () {
            getData();
            $("#add-name").val("");
            $("#add-hint").val("");
            $("#antwoord1").val("");
            $("#antwoord2").val("");
            $("#antwoord3").val("");
            $("#antwoord4").val("");
            $("#antwoord5").val("");
            $("#antwoord6").val("");
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
        if (item.Id === id) {
            $.each(item.Antwoorden, function (key, antwoord) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(antwoord.Id))
                    .append($("<td></td>").text(antwoord.Waarde))
                    .append($("<td></td>").text(antwoord.Correctness));

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