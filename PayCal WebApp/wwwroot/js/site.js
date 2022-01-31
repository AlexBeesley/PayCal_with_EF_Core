// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function getEmployees() {
    fetch('https://localhost:7229/Permanent/Employees')
      .then(result => result.json())
      .then(data => {
        document.getElementById("permEmployeesOutput").innerHTML = JSON.stringify(data);
      })
  
    fetch('https://localhost:7229/Temporary/Employees')
      .then(result => result.json())
      .then(data => {
        document.getElementById("tempEmployeesOutput").innerHTML = JSON.stringify(data);
      })
  
    document.getElementById("heading1").style.display = "block";
    document.getElementById("heading2").style.display = "block";
    document.getElementById("GetEmployees_btn").innerText = "Refresh";
  }
  
  function getPayCalID4cal() {
    x = document.getElementById("employeeID").value;
  }
  
  function getPayCalID4del() {
    y = document.getElementById("employeeID4delete").value;
  }
  
  function payCalculator() {
    document.getElementById("employeeID").style.display = "block";
    document.getElementById("payCalculator_btn").style.display = "none";
    document.getElementById("Calculator_btn").style.display = "block";
  }
  
  function Calculator() {
    getPayCalID4cal();
    fetch(`https://localhost:7229/PayCalculator?ID=${x}`)
    .then(data => console.log(data.text().then(function (text) {
      document.getElementById("payCalculatorOutput").innerHTML = text;
    })));
  }
  
  function addEmployee_getinfo() {
    document.getElementById("fields").style.display = "block";
    document.getElementById("addEmployee_btn").style.display = "none";
  }
  
  function addEmployee_perm() {
    document.getElementById("perm_fields").style.display = "block";
    document.getElementById("temp_fields").style.display = "none";
  }
  
  function addEmployee_temp() {
    document.getElementById("temp_fields").style.display = "block";
    document.getElementById("perm_fields").style.display = "none";
  }
  
  function submitEmployee() {
    isperm = null;
    var fname = null;
    var lname = null;
    var salary_or_dayrate = null;
    var bonus_or_weeksworked = null;
    var ele = document.getElementsByName('is_perm');     
    for(i = 0; i < ele.length; i++) {
      if(ele[i].checked) {
        isperm = ele[i].value;
        fname = document.getElementById("fname").value;
        lname = document.getElementById("lname").value;
        if (isperm){
          salary_or_dayrate = document.getElementById("salary").value;
          bonus_or_weeksworked = document.getElementById("bonus").value;
        }
        if (!isperm) {
          salary_or_dayrate = document.getElementById("dayrate").value;
          bonus_or_weeksworked = document.getElementById("weeksworked").value;
        }
        console.log(isperm, fname, lname, salary_or_dayrate, bonus_or_weeksworked);
        fetch(`https://localhost:7229/AddEmployee?isperm=${isperm}&fname=${fname}&lname=${lname}&salary_or_dayrate=${salary_or_dayrate}&bonus_or_weeksworked=${bonus_or_weeksworked}`, {
        method: 'PUT'});
        break;
      }
    }
  }
  
  function deleteEmployeeBtn() {
    document.getElementById("employeeID4delete").style.display = "block";
    document.getElementById("deleteEmployee_btn").style.display = "none";
    document.getElementById("Delete_btn").style.display = "block";
  }
  
  function deleteEmployee(){
  getPayCalID4del();
  fetch(`https://localhost:7229/DeleteEmployee?ID=${y}`, {
    method: 'PUT'
  });
  console.log("Deleted employee: " + y);
  }
