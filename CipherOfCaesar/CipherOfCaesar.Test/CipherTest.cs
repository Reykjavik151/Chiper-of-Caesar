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
            value.Enter = "this";
            value.CalculateFrequency();
            value.Enter = "is";
            value.CalculateFrequency();
            value.Enter = "test";
            value.CalculateFrequency();
            Assert.AreEqual(5, value.Frequency.Count);
            Assert.AreEqual(3, value.Frequency['t']);
            Assert.AreEqual(1, value.Frequency['h']);
            Assert.AreEqual(2, value.Frequency['i']);
            Assert.AreEqual(3, value.Frequency['s']);
            Assert.AreEqual(1, value.Frequency['e']);
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
