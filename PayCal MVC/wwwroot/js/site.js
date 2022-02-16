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