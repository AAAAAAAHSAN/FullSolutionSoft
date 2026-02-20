namespace FullSolutionSoft.Application.DTOs
{
    public record CustomerDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName
);

    public record CreateCustomerDto(
        string Email,
        string FirstName,
        string LastName
    );
}
