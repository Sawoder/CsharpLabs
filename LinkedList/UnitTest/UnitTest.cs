using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLinkedList;
using System.Collections.Generic;

namespace UnitTestMyLinkedList
{
    [TestClass]
    public class UnitTest
    {
        MyLinkedList<int> testList;

        [TestInitialize]
        public void Test_Init()
        {
            testList = new MyLinkedList<int>();
        }

        [TestMethod]
        public void List_Add()
        {
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            Assert.AreEqual(0, testList.First.Item);
            Assert.AreEqual(3, testList.Last.Item);
            Assert.AreEqual(4, testList.Count);
        }

        [TestMethod]
        public void List_Contains()
        {
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            Assert.IsTrue(testList.Contains(2));
            Assert.IsFalse(testList.Contains(4));

        }

        [TestMethod]
        public void List_CopyTo()
        {
            int[] array = new int[10];
            testList.Add(1);
            testList.Add(1);
            testList.Add(1);
            testList.Add(1);
            testList.CopyTo(array, 4);
            for (int i = 0; i < array.Length; i++)
            {
                if (i < 4)
                    Assert.IsTrue(array[i] == 0);
                else if (i >= 4 && i < array.Length - 2)
                    Assert.IsTrue(array[i] == 1);
            }

        }
        [TestMethod]
        public void List_Remove()
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Remove(2);
            Assert.AreEqual(3, testList.Count);
            Assert.IsTrue(testList.First.Next.Item == 3);
        }
        [TestMethod]
        public void List_GetEnumerator()
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            int i = 1;
            foreach (int a in testList)
            {
                Assert.AreEqual(a, i++);
            }
        }
        [TestMethod]
        public void List_Insert()
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Insert(7, 3);
            Assert.AreEqual(5, testList.Count);
            Assert.AreEqual(7, testList.First.Next.Next.Item);
        }

        [TestMethod]
        public void List_Actions()
        {
            List<string> events = new List<string>();
            testList.Added += delegate (int add) { events.Add($"Add new Item = {add}"); };
            testList.Removed += delegate (int rem) { events.Add($"Remove Item = {rem}"); };
            testList.Cleared += delegate { events.Add("Clear"); };
            testList.Add(1);
            testList.Add(2);
            testList.Remove(2);
            testList.Clear();
            Assert.AreEqual("Add new Item = 1", events[0]);
            Assert.AreEqual("Remove Item = 2", events[2]);
            Assert.AreEqual("Clear", events[3]);
        }

        [TestCleanup]
        public void Test_Clean()
        {
            testList = null;
        }

    }
}
