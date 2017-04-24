using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dkab.Client.Tests
{
    [TestClass()]
    public class GameSessionTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            GameSession session = new GameSession();

            Assert.IsTrue(session.Login("judgegc", "LoadLibrary") != 0);
            Assert.IsTrue(session.Login("judgegc", "12345") == 0);
        }
    }
}