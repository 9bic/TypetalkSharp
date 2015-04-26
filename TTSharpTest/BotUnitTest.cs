using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TypetalkSharp.Bot;
namespace TTSharpTest
{
    [TestClass]
    public class BotUnitTest
    {
        [TestCategory("TypetalkSharp.Bot")]
        [TestMethod]
        public void BotAuthenticateTest() {
            var bot = new TypetalkBot(Credential.TypetalkToken);
        }

        [TestCategory("TypetalkSharp.Bot")]
        [TestMethod]
        public void BotGetTopicsTest() {
            var bot = new TypetalkBot(Credential.TypetalkToken);
            var topicRoot = bot.Topics.Single(12204).Result;
            Assert.IsNotNull(topicRoot);
            Assert.IsNotNull(topicRoot.Topic);
        }
    }
}
