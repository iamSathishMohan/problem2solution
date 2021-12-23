using NUnit.Framework;

namespace DataStructures.Tests
{
    [TestFixture]
    public class DoublyLinkedListTests
    {
        [Test]
        public void InitalizeEmptyListTest()
        {
            var list = new DoublyLinkedList<int>();
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void AddHeadTest()
        {
            var list = new DoublyLinkedList<int>();
            for (int i = 1; i <= 5; i++)
            {
                list.AddHead(i);
                Assert.AreEqual(i, list.Count);
            }

            int expected = 5;
            foreach (int x in list)
            {
                Assert.AreEqual(expected--, x);
            }
        }

        [Test]
        public void AddTailTest()
        {
            var list = new DoublyLinkedList<int>();
            for (int i = 1; i <= 5; i++)
            {
                list.AddTail(i);
                Assert.AreEqual(i, list.Count);
            }

            int expected = 1;
            foreach (int x in list)
            {
                Assert.AreEqual(expected++, x);
            }
        }

        [Test]
        public void RemoveTest()
        {
            DoublyLinkedList<int> delete1to10 = CreateDoublyLinkedList(1, 10);
            Assert.AreEqual(10, delete1to10.Count);

            for (int i = 1; i <= 10; i++)
            {
                Assert.IsTrue(delete1to10.Remove(i));
                Assert.IsFalse(delete1to10.Remove(i));
            }

            Assert.AreEqual(0, delete1to10.Count);

            DoublyLinkedList<int> delete10to1 = CreateDoublyLinkedList(1, 10);
            Assert.AreEqual(10, delete10to1.Count);

            for (int i = 10; i >= 1; i--)
            {
                Assert.IsTrue(delete10to1.Remove(i));
                Assert.IsFalse(delete10to1.Remove(i));
            }

            Assert.AreEqual(0, delete10to1.Count);
        }

        [Test]
        public void ContainsTest()
        {
            DoublyLinkedList<int> list = CreateDoublyLinkedList(1, 10);
            for (int i = 1; i <= 10; i++)
            {
                Assert.IsTrue(list.Contains(i));
            }

            Assert.IsFalse(list.Contains(0));
            Assert.IsFalse(list.Contains(11));
        }

        [Test]
        public void ForwardIteratorTest()
        {
            DoublyLinkedList<int> list = CreateDoublyLinkedList(1, 10);
            int expected = 1;
            foreach (int actual in list)
            {
                Assert.AreEqual(expected++, actual);
            }
        }

        [Test]
        public void ReverseIteratorTest()
        {
            DoublyLinkedList<int> list = CreateDoublyLinkedList(1, 10);
            int expected = 10;
            foreach (int actual in list.GetReverseEnumerator())
            {
                Assert.AreEqual(expected--, actual);
            }
        }

        private static DoublyLinkedList<int> CreateDoublyLinkedList(int start, int end)
        {
            var list = new DoublyLinkedList<int>();
            for (int i = start; i <= end; i++)
            {
                list.AddTail(i);
            }

            return list;
        }
    }
}
