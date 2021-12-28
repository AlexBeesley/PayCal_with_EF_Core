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
      fetch(`https://localhost:7229/AddEmployee?isperm=${isperm}&fname=${fname}&lname=${lname}salary_or_dayrate=${salary_or_dayrate}&bonus_or_weeksworked=${bonus_or_weeksworked}`, {
      method: 'PUT'})
        .then(data => console.log(data()));
      break;
    }
  }
}