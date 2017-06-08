using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherOfCaesar.Test
{
    [TestClass]
    public class BinaryTreeOfWordsTest
    {
        [TestMethod]
        public void TreeTest()
        {
            BinaryTreeOfWords value = new BinaryTreeOfWords("start");
            value.Add("item");
            value.Add("xxx");
            value.Add("aaa");
            value.Add("item2");

            Assert.AreEqual(true, value.Search("item"));
            Assert.AreEqual(false, value.Search("oaishdqw"));
            Assert.AreEqual(false, value.Search("ITEM"));
            Assert.AreEqual(true, value.Search("aaa"));
            Assert.AreEqual(true, value.Search("xxx"));
            Assert.AreEqual(true, value.Search("item2"));
        }

    }
}
