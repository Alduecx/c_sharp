public record Wishlist(int EmployeeId, int[] DesiredEmployees) {
    public void Deconstruct(out int employeeId, out int[] desiredEmployees) {
        employeeId = this.EmployeeId;
        desiredEmployees = this.DesiredEmployees;
    }
}
