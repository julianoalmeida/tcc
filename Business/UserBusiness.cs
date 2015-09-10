using Comum.Exceptions;
using Data;
using Entidades;
using Negocio.BaseTypes;

namespace Negocio
{
    public interface IUserBusiness : IBaseBusiness<User>
    {
        User GetByCredentials(string login, string password);
    }

    public class UserBusiness : BaseBusiness<User>, IUserBusiness
    {
        private readonly IUserData _userData;
        public UserBusiness(IUserData data)
            : base(data)
        {
            _userData = data;
        }

        public User GetByCredentials(string login, string password)
        {
            ValidateRequiredFields(login, password);

            var user = _userData.GetByCredentials(login, password);

            ValidateCredentials(user);

            return user;
        }

        private static void ValidateCredentials(User user)
        {
            if (user?.Id == 0)
                throw new UserNotfoundException();
        }

        private static void ValidateRequiredFields(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new RequiredFieldException();
        }

        public override void Validate(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}