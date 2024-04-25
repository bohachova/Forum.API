using MediatR;

namespace Forum.API.BL.Queries
{
    public class RefreshBanDataQuery: IRequest<Unit>
    {
        public bool AllUsers { get; set; }
        public int UserId { get; set; } = 0;
    }
}
