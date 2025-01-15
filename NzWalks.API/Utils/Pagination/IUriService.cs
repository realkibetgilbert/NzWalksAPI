
namespace NzWalks.API.Utils.Pagination
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
