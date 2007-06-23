//using System;
//using System.Text;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections;
//using System.Text.RegularExpressions;

//namespace RegexWizard.Framework.Tests
//{
//    [TestClass]   
//    public class GuesserTests
//    {
//        Guesser guess = null;

//        [TestInitialize]
//        public void setup()
//        {
//            guess = new Guesser();
//        }


//        [TestMethod]
//        public void Create()
//        {
//            Guesser g = new Guesser();
//            Assert.IsNotNull(g);
//        }

//        [TestMethod]
//        public void SuggestRegex_NoInput_GetsAppropriateList()
//        {
//            List<string> suggestions = guess.SuggestRegex();
//            Assert.AreEqual<string>("^$", suggestions[0]);
//        }
        
//        [TestMethod]
//        public void SuggestRegex_SingleLetter_GetsAppropriateList()
//        {
//            guess.LookFor = "a";
//            //guess.ReadRules("rules");
//            List<string> suggestions = guess.SuggestRegex();

//            Assert.AreEqual<int>(2, suggestions.Count);
//            assertContains("^.$", suggestions);
//            assertContains("^a$", suggestions);
            
//            //assertContains("^[a-z]$", suggestions);
//            //assertContains("^[a-zA-Z]$", suggestions);
//            //assertContains(@"^\w$", suggestions);
//        }

//        [TestMethod]
//        public void SuggestRegex_Other()
//        {
//            guess.Learn("b", "rtrtr");
//            guess.LookFor = "b";
//            List<string> suggestions = guess.SuggestRegex();

//            Assert.AreEqual<int>(1, suggestions.Count);
//        }


//        void assertContains(string expected, IList list)
//        {
//            Assert.IsTrue(contains<string>(expected,list));
//        }
//        bool contains<T>(T expected, IList list)
//        {
//            foreach (T val in list)
//            {
//                if (val.Equals(expected))
//                {
//                    return true;
//                }
//            }
//            return false;
//        }
//    }
//}
