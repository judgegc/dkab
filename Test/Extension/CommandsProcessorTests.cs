using Microsoft.VisualStudio.TestTools.UnitTesting;
using dkab.Extension.Commands;
using System;

namespace dkab.Extension.Tests
{
    [TestClass()]
    public class CommandsProcessorTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            CommandsProcessor cp = new CommandsProcessor();

            Assert.IsInstanceOfType(cp.Parse("events"), typeof(PullEvents));
            Assert.IsInstanceOfType(cp.Parse("mypos 0 0"), typeof(MyDestination));

            MyDestination myposValid = cp.Parse("mypos 10 20") as MyDestination;
            Assert.IsTrue(myposValid.X == 10 && myposValid.Y == 20);

            Assert.ThrowsException<Exception>(() => cp.Parse("mypos 10 20 30"));
            Assert.ThrowsException<Exception>(() => cp.Parse("mypos a b"));
        }
    }
}