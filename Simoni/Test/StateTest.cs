using NUnit.Framework;
usign dev.spaccabolle.Test;

namespace StateTest
{
    [TestClass]
    public class TestState
    {
        State stato = null;

        [TestMethod]
        public void TestSetStateAndGetState() 
        {
            State.SetState(stato);
            Assert.AreEqual(stato, State.GetState());
        }
    }
}