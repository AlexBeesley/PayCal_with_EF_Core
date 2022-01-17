using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

namespace PayCal_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository<PermEmployeeData> perm = new PermEmployeeRepository();
            IRepository<TempEmployeeData> temp = new TempEmployeeRepository();
            PermEmployeeData permED = new PermEmployeeData();
            TempEmployeeData tempED = new TempEmployeeData();
            ICalculator cal = new Calculator(perm, temp);

            int Output;
            string[] Fields = { "Enter First Name:  ", "Enter Surname:  ", "Enter Salary (if applicable):  £", "Enter Bonus (if applicable):  £",
                        "Enter Day Rate (if applicable):  £", "Enter Weeks Worked (if applicable):  " };

            Console.WriteLine("Welcome to the PayCal System, a Salary Calculator.\n");

            while (true)
            {
                Console.WriteLine(@"
Please Select from the following options:
Display Employee Information --------------------------------------------------------------------------------- 1
Add new Employee --------------------------------------------------------------------------------------------- 2
Delete Employee ---------------------------------------------------------------------------------------------- 3
Pay Calculator ----------------------------------------------------------------------------------------------- 4");
                Console.Write(">>>  ");
                string Selection = Console.ReadLine();

                if (Selection == "1")
                {
                    Console.Clear();
                    Console.WriteLine("EMPLOYEE INFORMATION\n");
                    Console.WriteLine($"{(string.Concat(perm.ReadAll()))}{string.Concat(temp.ReadAll())}");
                    Console.WriteLine($"Total Number of current Employees: {temp.Count() + perm.Count()}");
                }

                if (Selection == "2")
                {
                    bool NELoopComplete = false;
                    bool permbool = false;
                    while (NELoopComplete == false)
                    {
                        Console.Clear();
                        Console.WriteLine("NEW EMPLOYEE ENTRY\n");
                        Console.WriteLine($"{string.Concat(perm.ReadAll())}{string.Concat(temp.ReadAll())}");

                        bool typeConvComplete = false;
                        while (typeConvComplete == false)
                        {
                            Console.WriteLine("Is Employment Permanent? [Y/N]  ");
                            string newIsPerm = Console.ReadLine();

                            if (newIsPerm == "Y" || newIsPerm == "y")
                            {
                                permbool = true;
                                Console.Write(Fields[0]);
                                permED.FName = Console.ReadLine();

                                Console.Write(Fields[1]);
                                permED.LName = Console.ReadLine();

                                for (int i = 2; i < 4; i++)
                                {
                                    Console.Write(Fields[i]);
                                    string Input = Console.ReadLine();
                                    bool valid = int.TryParse(Input, out Output);
                                    if (valid)
                                    {
                                        if (i == 2) //Salary
                                        {
                                            permED.Salaryint = Output;
                                        }
                                        if (i == 3) //Bonus
                                        {
                                            permED.Bonusint = Output;
                                        }
                                    }

                                    else
                                    {
                                        Console.WriteLine("invaild input.");
                                    }
                                }
                                typeConvComplete = true;
                            }

                            if (newIsPerm == "N" || newIsPerm == "n")
                            {
                                Console.Write(Fields[0]);
                                tempED.FName = Console.ReadLine();

                                Console.Write(Fields[1]);
                                tempED.LName = Console.ReadLine();

                                for (int i = 4; i < 6; i++)
                                {
                                    Console.Write(Fields[i]);
                                    string Input = Console.ReadLine();
                                    bool valid = int.TryParse(Input, out Output);
                                    if (valid)
                                    {
                                        if (i == 4) //Day Rate
                                        {
                                            tempED.DayRateint = Output;
                                        }
                                        if (i == 5) //Weeks Worked
                                        {
                                            tempED.WeeksWorkedint = Output;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("invaild input.");
                                    }
                                }
                                typeConvComplete = true;
                            }

                            bool commitComplete = false;
                            bool commit = false;
                            while (commitComplete == false)
                            {
                                Console.Write("Confirm data is correct and fit for injection. [Y/n]  ");
                                string confirm = Console.ReadLine();
                                if (confirm == "Y" || confirm == "y" || confirm == "")
                                {
                                    commit = true;
                                    commitComplete = true;
                                    if (permbool)
                                    {
                                        perm.Create(permED.FName, permED.LName, permED.Salaryint, permED.Bonusint);
                                    }
                                    else
                                    {
                                        temp.Create(tempED.FName, tempED.LName, tempED.DayRateint, tempED.WeeksWorkedint);
                                    }
                                }
                                if (confirm == "N" || confirm == "n")
                                {
                                    commitComplete = true;
                                }
                            }
                            if (commit == true)
                            {
                                NELoopComplete = true;
                            }
                        }
                    }
                }

                if (Selection == "3")
                {
                    bool CalLoop = false;
                    while (CalLoop == false)
                    {
                        Console.Clear();
                        Console.WriteLine("DELETE EMPLOYEE\n");
                        Console.WriteLine($"{string.Concat(perm.ReadAll())}{string.Concat(temp.ReadAll())}");
                        Console.Write("\nSelect ID of Employee to be deleted:  ");
                        string Input = Console.ReadLine();
                        bool valid = int.TryParse(Input, out Output);
                        if (valid)
                        {
                            int selectedID = Output;
                            bool x = perm.Delete(selectedID);
                            if (!x)
                            {
                                temp.Delete(selectedID);
                            }
                            Console.WriteLine($"{string.Concat(perm.ReadAll())}{string.Concat(temp.ReadAll())}");
                            Console.WriteLine($"Employee with ID: {selectedID} has been deleted.");
                            CalLoop = true;
                        }
                    }
                }

                if (Selection == "4")
                {
                    bool CalLoop = false;
                    while (CalLoop == false)
                    {
                        Console.Clear();
                        Console.WriteLine("CALCULATE ANNUAL PAY\n");
                        Console.WriteLine($"{string.Concat(perm.ReadAll())}{string.Concat(temp.ReadAll())}");
                        Console.Write("\nSelect ID of Employee:  ");
                        string Input = Console.ReadLine();
                        bool valid = int.TryParse(Input, out Output);
                        if (valid)
                        {
                            int selectedID = Output;
                            try
                            {
                                Console.WriteLine("Employee Name:  " + perm.Read(selectedID).FName + " " + perm.Read(selectedID).LName);
                                Console.WriteLine("Employment Type:  Permanent");
                                Console.WriteLine("Gross Annual Pay:  £" + cal.CalculateEmployeePay(selectedID));
                                Console.WriteLine("Annual Pay after Tax:  £" + cal.CalculateEmployeePay(selectedID));
                            }
                            catch
                            {
                                Console.WriteLine("Employee Name:  " + temp.Read(selectedID).FName + " " + temp.Read(selectedID).LName);
                                Console.WriteLine("Employment Type:  Temporary");
                                Console.WriteLine("Gross Annual Pay:  £" + cal.CalculateEmployeePay(selectedID));
                                Console.WriteLine("Annual Pay after Tax:  £" + cal.CalculateEmployeePay(selectedID));
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invaild ID.");
                        }
                        CalLoop = true;
                    }
                }
            }
        }
    }
}