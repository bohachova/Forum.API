using Forum.API.DataObjects.Pagination;
using Forum.API.DataObjects.UserObjects.UserResponses;
using MediatR;

namespace Forum.API.BL.Queries
{
    public class GetBannedUsersListQuery : IRequest<PaginatedList<UserResponse>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
