using Comum.Exceptions;
using Data;
using Entidades;
using Moq;
using Negocio;
using NUnit.Framework;

namespace UnitTests.BusinessRules
{
    [TestFixture]
    public class UserBusinessTest
    {
        private IUserBusiness _userBusiness;
        private readonly Mock<IUserData> _userData = new Mock<IUserData>();

        [Test]
        [ExpectedException(typeof(UserNotfoundException))]
        public void ThrowsUserNotFindExceptionWhenCantFindUserOnDatabase()
        {
            _userData.Setup(a => a.GetByCredentials(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new User());

            SetupData();

            _userBusiness.GetByCredentials("login", "password");
        }

        [Test]
        [ExpectedException(typeof(RequiredFieldException))]
        public void CanValidateRequiredFields()
        {
            _userData.Setup(a => a.GetByCredentials(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new User());

            SetupData();

            _userBusiness.GetByCredentials("", "password");
        }

        public void SetupData()
        {
            _userBusiness = new UserBusinessBusiness(_userData.Object);
        }
    }
}
