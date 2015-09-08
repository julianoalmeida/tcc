using System.Web.Script.Serialization;
using Entidades.Enums;
using NUnit.Framework;


namespace IntegrationTests
{
    [TestFixture]
    public class AccessProfileEnumTest : BaseTests
    {
        private readonly AccessProfileEnum accessProfile = AccessProfileEnum.Administrador;

        [Test]
        public void Compare()
        {
            var test = new JavaScriptSerializer().Serialize(accessProfile);    
            Assert.IsNotNull(test);
        }
    }
}
