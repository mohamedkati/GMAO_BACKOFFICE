using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Helpers.Responses
{
    public class PagedResponse<T> : ResponseResult<IReadOnlyList<T>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        protected PagedResponse(IReadOnlyList<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            IsSucceeded = true;
        }

        public static PagedResponse<T> Success(IReadOnlyList<T> data, int pageNumber, int pageSize, int totalRecords)
        {
            return new PagedResponse<T>(data, pageNumber, pageSize, totalRecords);
        }
    }
}
