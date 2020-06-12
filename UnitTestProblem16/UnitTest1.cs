using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Problem16;

namespace UnitTestProblem16
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Record_BaseCase()
        {
            Random random = new Random();
            int N = random.Next(1, 200);
            Orders orders = new Orders(N);

            for (int i = 0; i < N; i++)
                orders.Record(i);

            Assert.AreEqual(orders.Ids.First.Value, 0);
        }

        [TestMethod]
        public void Record_MoreThanN()
        {
            Random random = new Random();
            int N = random.Next(1, 200);
            int offset = random.Next(1, 50);
            Orders orders = new Orders(N);

            for (int i = 0; i < (N+offset); i++)
                orders.Record(i);

            Assert.AreEqual(orders.Ids.First.Value, offset);
            Assert.AreEqual(orders.Ids.First.Next.Value, offset + 1);

            Assert.AreEqual(orders.Ids.Last.Value, N + offset - 1);
        }

        [TestMethod]
        public void GetLast_FirstHalf()
        {
            Random random = new Random();
            int N = random.Next(1, 200);
            Orders orders = new Orders(N);

            for (int i = 0; i < N; i++)
                orders.Record(i);

            int n_2rand = random.Next(1, N / 2);
            Assert.AreEqual(orders.GetLast(n_2rand), n_2rand - 1);
        }

        [TestMethod]
        public void GetLast_SecondHalf()
        {
            Random random = new Random();
            int N = random.Next(1, 200);
            Orders orders = new Orders(N);

            for (int i = 0; i < N; i++)
                orders.Record(i);

            int n_2rand = random.Next(N / 2, N);
            Assert.AreEqual(orders.GetLast(n_2rand), n_2rand - 1);
        }

        [TestMethod]
        public void GetLast_Exception()
        {
            Random random = new Random();
            int N = random.Next(1, 200);
            Orders orders = new Orders(N);

            for (int i = 0; i < N; i++)
                orders.Record(i);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                orders.GetLast(N + 1));
        }
    }
}
