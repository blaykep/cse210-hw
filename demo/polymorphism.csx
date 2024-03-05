using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;
using System.Threading;
using Internal;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Razor;
using Microsoft.VisualStudio.Editor.Razor;

abstract class Employee {

    public string Name;
    public abstract void CalculatePay();
}

class SalaryEmployee : Employee {
    public decimal annualSalary;

    public SalaryEmployee(string name, float annualSalary) : base(name) {
        this.annualSalary = (decimal) annualSalary;
    }

    public override decimal CalculatePay()
    {
        return 0;
    }
}

class HourlyEmployee : Employee {
    protected decimal hourlyRate;

    protected int hoursWorked;

    public HourlyEmployee(string name, double hourlyRate, int hoursWorked) : base(name) {
        this.hourlyRate = (decimal) hourlyRate;
        this.hoursWorked = hoursworked;
    }

    public override void CalculatePay()
    {
        return (hourslyRate * hoursworked) * 2;
    }
}

SalaryEmployee salaryEmployee = new SalaryEmployee("John", 96000);
HourlyEmployee hourlyEmployee = new HourlyEmployee("Jane", 13.50, 80);

ConstructorBuilder.WriteLine($"{SalaryEmployee.name} makes ${salaryEmployee.CalculatePay():N2}")
bi-weekly.");