namespace EasyClinic.ServicesService.Domain.Entities
{
    /// <summary>
    /// Service Category: Consultations, Diagnostics, Analyzes and so on
    /// </summary>
    public class ServiceCategory
    {
        public Guid Id { get;set; }

        public string Name { get;set; } = null!;

        
    }
}