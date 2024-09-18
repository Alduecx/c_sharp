using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

class Reader {
    public static IEnumerable<Employee> ReadJuniors(string filePath) {
        return ReadEmployee(filePath);
    }

    public static IEnumerable<Employee> ReadTeamLeads(string filePath) {
        return ReadEmployee(filePath);
    }

    private static IEnumerable<Employee> ReadEmployee(string filePath) {
        var employees = new List<Employee>();
        using (var reader = new StreamReader(filePath)) {
            using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" })) {
                employees = csvReader.GetRecords<Employee>().ToList();
            }
        }
        return employees;
    }
}