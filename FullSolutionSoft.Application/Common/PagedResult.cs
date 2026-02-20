namespace FullSolutionSoft.Application.Common
{
    public record PagedResult<T>(IEnumerable<T> Items, int TotalCount, int PageNumber, int PageSize)
    {
        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
