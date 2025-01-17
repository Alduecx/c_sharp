using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Shared.Model.DTO;

namespace Employee.Models;
public class EmployeesReader
{
    
    public IEnumerable<Shared.Model.Records.Employee> ReadJuniors(string filePath)
    {
        return ReadEmployee(filePath, EmployeeTypes.Junior);
    }

    public IEnumerable<Shared.Model.Records.Employee> ReadTeamLeads(string filePath)
    {
        return ReadEmployee(filePath, EmployeeTypes.TeamLead);
    }

    private record EmployeeRaw(int Id, string Name);
    private IEnumerable<Shared.Model.Records.Employee> ReadEmployee(string filePath, EmployeeTypes type)
    {
        using var reader = new StreamReader(filePath);
        using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" });
        var employees = csvReader.GetRecords<EmployeeRaw>()
            .Select(e => new Shared.Model.Records.Employee(e.Id, e.Name, type))
            .ToList();

        return employees;
    }
}