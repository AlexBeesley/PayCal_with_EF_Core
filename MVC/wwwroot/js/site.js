// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function HiddenToggle(x) {
    if (x.style.display === "block") {
        x.style.display = "none";
    }
    else {
        x.style.display = "block";
    }
}

function ShowMenu_GetEmployeeBtn() {
    HiddenToggle(document.getElementById("getbyID_form"));
}

function ShowMenu_PayCalBtn() {
    HiddenToggle(document.getElementById("PayCal_form"));
}

function ShowMenu_AddEmployeeBtn() {
    HiddenToggle(document.getElementById("Create_form"));
}

function ShowMenu_EditEmployeeBtn() {
    HiddenToggle(document.getElementById("Edit_form"));
}

function ShowMenu_DeleteEmployeeBtn() {
    HiddenToggle(document.getElementById("Delete_form"));
}

function toggleTable() {
    var a = document.getElementById("outputTablePerm");
    var b = document.getElementById("outputConsolePerm");
    var c = document.getElementById("outputTableTemp");
    var d = document.getElementById("outputConsoleTemp");

    if (a.style.display === "none") {
        a.style.display = "block";
        b.style.display = "none";
        c.style.display = "block";
        d.style.display = "none";
        document.getElementById("toggleTable_btn").innerText = "Toggle Console View";
    }
    else {
        a.style.display = "none";
        b.style.display = "block";
        c.style.display = "none";
        d.style.display = "block";
        document.getElementById("toggleTable_btn").innerText = "Toggle Table View";
    }
}