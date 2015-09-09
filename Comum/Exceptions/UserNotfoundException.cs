namespace Comum.Exceptions
{
    public class UserNotfoundException : BaseException
    {
        public UserNotfoundException() : base(Messages.USER_NOT_FOUND) { }
    }
}
