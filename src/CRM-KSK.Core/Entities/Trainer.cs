namespace CRM_KSK.Core.Entities;

public class Trainer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Color { get; set; } = "#FFFFFF";

    public ICollection<Training> Trainings { get; set; } = [];
}
