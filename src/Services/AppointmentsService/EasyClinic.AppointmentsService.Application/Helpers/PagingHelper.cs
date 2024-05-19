using EasyClinic.AppointmentsService.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EasyClinic.AppointmentsService.Application.Helpers
{
    public static class PagingHelper
    {
        public static async Task<PagedList<T>> GetPage<T>(
            this IQueryable<T> list, int pageNumber, int pageSize, int totalCount)
        {
            return new PagedList<T>()
            {
                PageNumber = pageNumber >= 1 ? pageNumber : 1,
                PageSize = pageSize >= 1 ? pageSize : 20,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                HasPrevious = pageNumber > 1,
                HasNext = pageNumber < pageSize,
                TotalElements = totalCount,
                Data = await list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }
    }
}
