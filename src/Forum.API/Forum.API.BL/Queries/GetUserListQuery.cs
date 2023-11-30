using MediatR;
using Forum.API.DataObjects.UserObjects;
using Forum.API.DataObjects.Pagination;

namespace Forum.API.BL.Queries
{
    public class GetUserListQuery: IRequest<PaginatedList<User>>
    { 
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
