using System.Collections;

namespace DataStructures
{
    /// <summary>
    /// A node in the LinkedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoublyLinkedListNode<T>
    {
        /// <summary>
        /// Constructs a new node with the given value.
        /// </summary>
        /// <param name="value"></param>
        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// The node value
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// The next node in the linked list (null if last node)
        /// </summary>
        public DoublyLinkedListNode<T> Next { get; set; }

        /// <summary>
        /// The previous node in the linked list (null if head node)
        /// </summary>
        public DoublyLinkedListNode<T> Previous { get; set; }
    }

    /// <summary>
    /// A linked list collection capable of basic operations such as Add, Remove, Find and Enumerate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoublyLinkedList<T> : ICollection<T>
    {
        /// <summary>
        /// The first node in the list or null if empty
        /// </summary>
        public DoublyLinkedListNode<T> Head { get; private set; }

        /// <summary>
        /// The last node in the list or null if empty
        /// </summary>
        public DoublyLinkedListNode<T> Tail { get; private set; }

        #region Add
        /// <summary>
        /// Adds the given value to the start of the doubly linked list
        /// </summary>
        /// <param name="value">The value to add to the start of the doubly linked list</param>
        public void AddHead(T value)
        {
            AddHead(new DoublyLinkedListNode<T>(value));
        }

        /// <summary>
        /// Adds the given node to the start of the doubly linked list
        /// </summary>
        /// <param name="node">The value to add to the start of the doubly linked list</param>
        public void AddHead(DoublyLinkedListNode<T> node)
        {
            // STEP 1: Save off the head node so we don't lose it
            DoublyLinkedListNode<T> temp = Head;

            // STEP 2: Point head to the new node
            Head = node;

            // STEP 3: Insert the rest of the list behind the head
            Head.Next = temp;

            // STEP 4: Pointer reference update 
            if (Count == 0)
            {
                // if the doubly linked list was empty then Head and Tail should both point to the new node.
                Tail = Head;
            }
            else
            {
                // Assume inserting 3
                // Before: Head -------> 5 <-> 7 -> null
                // After:  Head -> 3 <-> 5 <-> 7 -> null

                // if the doubly linked list wasn't empty then old head node's previous should point to the new node.
                temp.Previous = Head;
            }

            // STEP 5: Increment the counter
            Count++;
        }

        /// <summary>
        /// Adds the given value to the end of the doubly linked list
        /// </summary>
        /// <param name="value">The value to add to the end of the doubly linked list</param>
        public void AddTail(T value)
        {
            AddTail(new DoublyLinkedListNode<T>(value));
        }

        /// <summary>
        /// Adds the given node to the end of the doubly linked list
        /// </summary>
        /// <param name="node">The value to add to the end of the doubly linked list</param>
        public void AddTail(DoublyLinkedListNode<T> node)
        {
            // STEP 1: Pointer reference update
            if (Count == 0)
            {
                Head = node;
            }
            else
            {
                // Assume inserting 7
                // Before: Head -> 3 <-> 5 -> null
                // After:  Head -> 3 <-> 5 <-> 7 -> null
                // 7.Previous = 5

                Tail.Next = node;

                // Point new node's previous to old tail
                node.Previous = Tail;
            }

            // STEP 2: Point tail to the new node
            Tail = node;

            // STEP 3: Increment the counter
            Count++;
        }

        #endregion Add

        #region Remove
        /// <summary>
        /// Removes the first node from the doubly linked list.
        /// </summary>
        public void RemoveFirst()
        {
            if (Count > 0)
            {
                // Assume removing 3
                // Before: Head -> 3 <-> 5
                // After:  Head -------> 5

                // Head -> 3 -> null
                // Head ------> null

                Head = Head.Next;
                Head.Previous = null;
                Count--;

                if (Count == 0)
                {
                    Tail = null;
                }
            }
        }

        /// <summary>
        /// Removes the last node from the doubly linked list.
        /// </summary>
        public void RemoveLast()
        {
            if (Count > 0)
            {
                // Assume removing 7
                // Before: Head --> 3 --> 5 --> 7
                //         Tail = 7
                // After:  Head --> 3 --> 5 --> null
                //         Tail = 5
                // Null out 5's Next pointer

                if (Count == 1)
                {
                    Head = null;
                    Tail = null;
                }
                else
                {
                    Tail = Tail.Previous;
                    Tail.Next = null;
                }

                Count--;
            }
        }
        #endregion Remove

        #region ICollection
        /// <summary>
        /// The number of items currently in the list
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// True if the collection is readonly, false otherwise.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the specified value to the front of the list
        /// </summary>
        /// <param name="item">The value to add</param>
        public void Add(T item)
        {
            AddHead(item);
        }

        /// <summary>
        /// Removes all the nodes from the list
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        /// <summary>
        /// Returns doubly linked list node if the list contains the specified item, null otherwise.
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>doubly linked list node if the item is found, null otherwise.</returns>
        public DoublyLinkedListNode<T> Find(T item)
        {
            DoublyLinkedListNode<T> current = Head;
            while (current != null)
            {
                // Assume Searching 5
                // Head -> 3 -> 5 -> 7
                // Value: 5
                if (current.Value.Equals(item))
                {
                    return current;
                }

                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// Returns true if the doubly linked list contains the specified item, false otherwise.
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>True if the item is found, false otherwise.</returns>
        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        /// <summary>
        /// Copies the node values into the specified array.
        /// </summary>
        /// <param name="array">The array to copy the doubly linked list values to</param>
        /// <param name="arrayIndex">The index in the array to start copying at</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            DoublyLinkedListNode<T> current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Removes the first occurance of the item from the doubly linked list (searching from Head to Tail).
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>True if the item was found and removed, false otherwise</returns>
        public bool Remove(T item)
        {
            DoublyLinkedListNode<T> found = Find(item);
            if (found == null)
            {
                return false;
            }

            DoublyLinkedListNode<T> previous = found.Previous;
            DoublyLinkedListNode<T> next = found.Next;

            if (previous == null)
            {
                // we're removing the head node
                RemoveFirst();
            }
            else
            {
                previous.Next = next;
                Count--;
            }

            if (next == null)
            {
                // we're removing the tail
                RemoveLast();
            }
            else
            {
                next.Previous = previous;
                Count--;
            }

            return true;
        }

        /// <summary>
        /// Enumerates over the doubly linked list values from Head to Tail
        /// </summary>
        /// <returns>A Head to Tail enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            DoublyLinkedListNode<T> current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Enumerates over the linked list values from Head to Tail
        /// </summary>
        /// <returns>A Head to Tail enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion ICollection

        #region Extension Methods
        /// <summary>
        /// Enumerates over the doubly linked list values from Tail to Head
        /// </summary>
        /// <returns>A Tail to Head enumerator</returns>
        public IEnumerable<T> GetReverseEnumerator()
        {
            DoublyLinkedListNode<T> current = Tail;
            while (current != null)
            {
                yield return current.Value;
                current = current.Previous;
            }
        }

        /// <summary>
        /// Returns head node value if the head is not null, default value of T otherwise.
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>head node value if the head is not null, default value of T otherwise.</returns>
        public bool GetHeadValue(out T value)
        {
            if (Count > 0)
            {
                value = Head.Value;
                return true;
            }

            value = default(T);
            return false;
        }

        /// <summary>
        /// Returns tail node value if the tail is not null, default value of T otherwise.
        /// </summary>
        /// <param name="value">The output value</param>
        /// <returns>tail node value if the tail is not null, default value of T otherwise.</returns>
        public bool GetTailValue(out T value)
        {
            if (Count > 0)
            {
                value = Tail.Value;
                return true;
            }

            value = default(T);
            return false;
        }
        #endregion Extension Methods
    }
}
