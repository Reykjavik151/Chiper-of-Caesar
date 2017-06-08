using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherOfCaesar.Test
{
    [TestClass]
    [DeploymentItem("glossary.txt")]
    public class CipherTest
    {
        [TestMethod]
        public void RotateTest()
        {
            Cipher value = new Cipher(26);
            value.Enter = "abcd";
            Assert.AreEqual("cdef", value.Rotate(2));
            Assert.AreEqual("yzab", value.Rotate(-2));
            Assert.AreEqual("abcd", value.Rotate(0));
            Assert.AreEqual("abcd", value.Rotate(26));
            Assert.AreEqual("cdef", value.Rotate(28));
            Assert.AreEqual("yzab", value.Rotate(-28));
        }

        [TestMethod]
        public void SearchWordTest()
        {
            Cipher value = new Cipher(26);
            Assert.AreEqual(true, value.SearchWord("item"));
            Assert.AreEqual(false, value.SearchWord("asdqfbasw"));
            Assert.AreEqual(false, value.SearchWord("ItEm"));
        }

        [TestMethod]
        public void CalculateFrequencyTest()
        {
            Cipher value = new Cipher(26);
            value.Frequency.Add('d', 11);
            value.Frequency.Add('a', 25);
            Assert.AreEqual(2, value.Frequency.Count);
            Assert.AreEqual(11, value.Frequency['d']);
            Assert.AreEqual(25, value.Frequency['a']);
        }

        [TestMethod]
        public void NullFrequencyTest()
        {
            Cipher value = new Cipher(26);
            value.NullFrequency();
            Assert.AreEqual(0, value.Frequency.Count);
            value.Frequency.Add('a', 20);
            value.Frequency.Add('d', 11);
            value.NullFrequency();
            Assert.AreEqual(0, value.Frequency.Count);
        }


        [TestMethod]
        public void GuessTest()
        {
            Cipher value = new Cipher(26);
            value.Enter = "item";
            value.Enter = value.Rotate(25);
            List<int> buff = value.Guess();
            Assert.AreEqual(1, buff.Count);
            Assert.AreEqual("item", value.Rotate(buff[0]));
        }
    }

    
}
