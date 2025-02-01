namespace TestApp.Web.Domain.Abstraction.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
    