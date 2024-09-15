class Reader {
    public static IEnumerable<Employee> ReadJuniors(TextReader reader) {
        return ReadEmployee(reader);
    }

    public static IEnumerable<Employee> ReadTeamLeads(TextReader reader) {
        return ReadEmployee(reader);
    }

    private static IEnumerable<Employee> ReadEmployee(TextReader reader) {
        var employees = new List<Employee>();

        // First string: Id;Name
        string? line = reader.ReadLine();
        if (line == null) return employees;

        // Next strings: <Id>;<Name>
        while((line = reader.ReadLine()) != null) {
            var parts = line.Split(';');
            if (parts.Length == 2) {
                int id;
                if (int.TryParse(parts[0], out id)) {
                    var employee = new Employee(Id: id, Name: parts[1]);
                    employees.Add(employee);
                }
            }
        }

        return employees;
    }
}