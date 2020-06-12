using System.Collections.Generic;
using System;

namespace Problem16
{
    public sealed class Orders
    {
        public int N { get; private set; }
        public LinkedList<int> Ids { get; private set; }

        public Orders(int n)
        {
            Ids = new LinkedList<int>();
            N = n;
        }

        // Made in O(1) even if the max amount of logs is reached.
        public void Record(int orderId)
        {
            // Retrieving the value of this property is an O(1) operation.
            if (Ids.Count == N)
                // This method is an O(1) operation.
                Ids.RemoveFirst();

            // This method is an O(1) operation.
            Ids.AddLast(orderId);
        }

        // Made in O(N/2) in the worst case.
        public int GetLast(int i)
        {
            // Retrieving the value of this property is an O(1) operation.
            if (Ids.Count < i)
                throw new ArgumentOutOfRangeException("i", i, $"The value of this parameter should not exceed size of the current orders ({Ids.Count}).");

            int half = N / 2;

            // Check by which side start to reach the ith element, done in O(N/2) in worst case.
            if(i < half)
            {
                LinkedListNode<int> goNext = Ids.First;
                int currentIndex = 1;
                while(currentIndex != i)
                {
                    currentIndex++;
                    goNext = goNext.Next;
                }

                return goNext.Value;
            }
            else
            {
                LinkedListNode<int> goPrevious = Ids.Last;
                int currentIndex = Ids.Count;
                while(currentIndex != i)
                {
                    currentIndex--;
                    goPrevious = goPrevious.Previous;
                }

                return goPrevious.Value;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
