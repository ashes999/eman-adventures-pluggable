using Cobalt.Core;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.UnitTests.Core
{
    [TestFixture]
    class ConfigTests
    {
        [Test]
        public void InstanceGetsLatestInstance()
        {
            var c1 = new Config("TestData/Config.json");
            Assert.That(Config.Instance, Is.EqualTo(c1));

            var c2 = new Config("TestData/Config.json");
            Assert.That(Config.Instance, Is.EqualTo(c2));
        }

        public void GetGetsConfigValues(string key, object expected)
        {
            var config = new Config("TestData/Config.json");            
            Assert.That(config.Get<bool>("showFps"), Is.EqualTo(true));
            Assert.That(config.Get<int>("numMonkeys"), Is.EqualTo(37));

            var array = config.Get<JArray>("primeNumbers");
            Assert.That(array[0].Value<int>(), Is.EqualTo(1));
            Assert.That(array[1].Value<int>(), Is.EqualTo(2));
            Assert.That(array[2].Value<int>(), Is.EqualTo(3));
            Assert.That(array[3].Value<int>(), Is.EqualTo(5));
            Assert.That(array[4].Value<int>(), Is.EqualTo(7));
            Assert.That(array[5].Value<int>(), Is.EqualTo(11));

            var monkey = config.Get<JObject>("monkey");
            Assert.That(monkey.GetValue("name").Value<string>, Is.EqualTo("Bobo"));
            Assert.That(monkey.GetValue("age").Value<int>, Is.EqualTo(3));
        }
    }
}
