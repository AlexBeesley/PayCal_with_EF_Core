function getEmployees() {
  fetch('https://localhost:7229/GetEmployees')
    .then(data => console.log(data.text().then(function (text) {
      document.getElementById("employeesOutput").innerHTML = text;
    })));
}

function getPayCalID(){
  x = document.getElementById("employeeID").value;
}

function payCalculator() {
  getPayCalID();
  console.log(x);
  fetch(`https://localhost:7229/PayCalculator?ID=${x}`)
    .then(data => console.log(data.text().then(function (text) {
      document.getElementById("payCalculatorOutput").innerHTML = text;
    })));
}

function AddEmployee_perm() {
  document.getElementById("fname").visible;
  document.getElementById("lname").visible;
  document.getElementById("salary").visible;
  document.getElementById("bonus").visible;
}

function AddEmployee_temp(){
  document.getElementById("fname").visible;
  document.getElementById("lname").visible;
  document.getElementById("dayrate").visible;
  document.getElementById("weeksworked").visible;
}

function dropdown() {
  document.getElementById("employmentTypeDropdown").classList.toggle("show");
}

window.onclick = function(event) {
  if (!event.target.matches('.dropbtn')) {
    var dropdowns = document.getElementsByClassName("dropdown-content");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
      var openDropdown = dropdowns[i];
      if (openDropdown.classList.contains('show')) {
        openDropdown.classList.remove('show');
      }
    }
  }
}