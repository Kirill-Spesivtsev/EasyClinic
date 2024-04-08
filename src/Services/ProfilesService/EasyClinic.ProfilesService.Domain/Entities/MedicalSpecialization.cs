namespace EasyClinic.ProfilesService.Domain.Entities
{
    public class MedicalSpecialization
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
    }
}