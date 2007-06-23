//using System;
//using System.Collections.Generic;
//using System.Text;
//using NUnit.Framework;
//using RegexWizard.Framework;
//using Regulazy.UISupport.UserActions;
//
//namespace Regulazy.UISupport.Tests.ActionTests
//{
//    [TestFixture]
//    public class ScopeRenameActionTests
//    {
//        Scope root = new Scope("abc");
//        [SetUp]
//        public void SetUp()
//        {
//            root = new Scope("abc");
//        }
//
//        [Test]
//        public void Create_ScopeWithnoName_SimpleNotes()
//        {
//            ScopeRenameAction action = new ScopeRenameAction(root);
//            Assert.AreEqual("Rename..", action.Title);
//        }
//       
//        [Test]
//        public void Create_ScopeWithName_NotesContainName()
//        {
//            root.Name = "ScopeName";
//            ScopeRenameAction action = new ScopeRenameAction(root);
//            Assert.AreEqual("Rename [ScopeName]..", action.Title);
//        }
//        
//        [Test]
//        public void Create_ScopeWithName_NotesContainName2()
//        {
//            root.Name = "ScopeName1";
//            ScopeRenameAction action = new ScopeRenameAction(root);
//            Assert.AreEqual("Rename [ScopeName1]..", action.Title);
//        }
//        [Test]
//        public void Execute_Rename()
//        {
//            root.Name = "Before";
//            ScopeRenameAction action = new ScopeRenameAction(root);
//            ActionInputDelegate del = delegate(UserAction act, ActionUserInput input)
//                                          {
//                                              input.UserInput = "After";
//                                          };
//            action.GetInput += del;
//            action.Execute();
//            Assert.AreEqual("After", root.Name);
//        }
//        
//        [Test]
//        public void Execute_Rename_ReturnsTrue()
//        {
//            root.Name = "Before";
//            ScopeRenameAction action = new ScopeRenameAction(root);
//            ActionInputDelegate del = delegate(UserAction act, ActionUserInput input)
//                                          {
//                                              input.UserInput = "After";
//                                          };
//            action.GetInput += del;
//            Assert.IsTrue(action.Execute());
//        }
//        
//        [Test]
//        public void Execute_RenameCancel_ReturnsFalse()
//        {
//            root.Name = "Before";
//            ScopeRenameAction action = new ScopeRenameAction(root);
//            ActionInputDelegate del = delegate(UserAction act, ActionUserInput input)
//                                          {
//                                              input.Cancel = true;
//                                          };
//            action.GetInput += del;
//            Assert.IsFalse(action.Execute());
//        }
//        
//        [Test]
//        public void Execute_RenameEithbadInput_ReturnsFalse()
//        {
//            root.Name = "Before";
//            ScopeRenameAction action = new ScopeRenameAction(root);
//            ActionInputDelegate del = delegate(UserAction act, ActionUserInput input)
//                                          {
//                                              input.UserInput = 1;
//                                          };
//            action.GetInput += del;
//            Assert.IsFalse(action.Execute());
//        }
//    }
//}
