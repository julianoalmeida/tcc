namespace Comum.Exceptions
{
    public class UnavailableWebServiceException : BaseException
    {
        public UnavailableWebServiceException() : base(Messages.UNAVAILABLE_WS) { }
    }
}
