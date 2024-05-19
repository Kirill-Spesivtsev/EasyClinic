namespace EasyClinic.AppointmentsService.Domain.Helpers
{
    public class PagedList<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalElements { get; set; }

        public int TotalPages { get; set; }

        public bool HasPrevious { get; set; }

        public bool HasNext { get; set; }

        public List<T>? Data { get; set; }

    }
}
