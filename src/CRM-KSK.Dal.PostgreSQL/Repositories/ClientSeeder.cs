using Bogus;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class ClientSeeder
{
    public static List<Client> GenerateClients(int count)
    {
        var faker = new Faker("ru");
        var clients = new List<Client>();

        for(int i = 0; i < count; i++)
        {
            clients.Add(new Client
            {
                Id = Guid.NewGuid(),
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Phone = faker.Phone.PhoneNumber("##########"),
                DateOfBirth = DateOnly.FromDateTime(faker.Date.Past(50, DateTime.UtcNow.AddYears(-18))),
                ParentName = faker.Name.FirstName(),
                ParentPhone = faker.Phone.PhoneNumber("##########")
            });

        }
        return clients;
    }

    
}
