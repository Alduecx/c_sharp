public record Employee(int Id, string Name) {
    public void Deconstruct(out int id, out string name) {
        id = this.Id;
        name = this.Name;
    }
}
