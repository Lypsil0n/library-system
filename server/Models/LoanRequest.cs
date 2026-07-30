public class LoanRequest
{
    public required Guid UserId {get; set;}

    public required Guid BookId {get; set;}

    public required int PinCode {get; set;}
}