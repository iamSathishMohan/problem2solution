using System.Collections;

namespace DataStructures
{
    /// <summary>
    /// A node in the LinkedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedListNode<T>
    {
        /// <summary>
        /// Constructs a new node with the given value.
        /// </summary>
        /// <param name="value"></param>
        public LinkedListNode(T value)
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
        public LinkedListNode<T> Next { get; set; }
    }

    /// <summary>
    /// A linked list collection capable of basic operations such as Add, Remove, Find and Enumerate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedList<T> : ICollection<T>
    {
        /// <summary>
        /// The first node in the list or null if empty
        /// </summary>
        public LinkedListNode<T> Head { get; private set; }

        /// <summary>
        /// The last node in the list or null if empty
        /// </summary>
        public LinkedListNode<T> Tail { get; private set; }

        #region Add
        /// <summary>
        /// Adds the given value to the start of the linked list
        /// </summary>
        /// <param name="value">The value to add to the start of the linked list</param>
        public void AddHead(T value)
        {
            AddHead(new LinkedListNode<T>(value));
        }

        /// <summary>
        /// Adds the given node to the start of the linked list
        /// </summary>
        /// <param name="node">The value to add to the start of the linked list</param>
        public void AddHead(LinkedListNode<T> node)
        {
            // STEP 1: Save off the head node so we don't lose it
            LinkedListNode<T> temp = Head;

            // STEP 2: Point head to the new node
            Head = node;

            // STEP 3: Insert the rest of the list behind the head
            Head.Next = temp;

            // STEP 4: Pointer reference update (if the list is empty)
            if (Count == 0)
            {
                // if the linked list was empty then Head and Tail should both point to the new node.
                Tail = node;
            }

            // STEP 5: Increment the counter
            Count++;
        }

        /// <summary>
        /// Adds the given value to the end of the linked list
        /// </summary>
        /// <param name="value">The value to add to the end of the linked list</param>
        public void AddTail(T value)
        {
            AddTail(new LinkedListNode<T>(value));
        }

        /// <summary>
        /// Adds the given node to the end of the linked list
        /// </summary>
        /// <param name="node">The value to add to the end of the linked list</param>
        public void AddTail(LinkedListNode<T> node)
        {
            // STEP 1: Pointer reference update
            if (Count == 0)
            {
               Head = node;
            }
            else
            {
                Tail.Next = node;
            }

            // STEP 2: Point tail to the new node
            Tail = node;

            // STEP 3: Increment the counter
            Count++;
        }
        #endregion Add

        #region Remove
        /// <summary>
        /// Removes the first node from the linked list.
        /// </summary>
        public void RemoveFirst()
        {
            if (Count > 0)
            {
                Head = Head.Next;
                Count--;

                if (Count == 0)
                {
                    Tail = null;
                }
            }
        }

        /// <summary>
        /// Removes the last node from the linked list.
        /// </summary>
        public void RemoveLast()
        {
            if (Count > 0)
            {
                if (Count == 1)
                {
                    Head = null;
                    Tail = null;
                }
                else
                {
                    LinkedListNode<T> current = Head;
                    while (current.Next != Tail)
                    {
                        current = current.Next;
                    }

                    current.Next = null;
                    Tail = current;
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
        /// Adds the given value to start of the linked list
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
        /// Returns linked list node if the list contains the specified item, null otherwise.
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>linked list node if the item is found, null otherwise.</returns>
        public LinkedListNode<T> Find(T item)
        {
            LinkedListNode<T> current = Head;
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
        /// Returns true if the list contains the specified item, false otherwise.
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
        /// <param name="array">The array to copy the linked list values to</param>
        /// <param name="arrayIndex">The index in the array to start copying at</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        /// <summary>
        /// Removes the first occurance of the item from the list (searching
        /// from Head to Tail).
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>True if the item was found and removed, false otherwise</returns>
        public bool Remove(T item)
        {
            LinkedListNode<T> previous = null;
            LinkedListNode<T> current = Head;

            // 1: Empty list - do nothing
            // 2: Single node: (previous is null)
            // 3: Many nodes
            //    a: node to remove is the first node
            //    b: node to remove is the middle or last

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    // it's a node in the middle or end
                    if (previous != null)
                    {
                        // Case 3b
                        // Before: Head -> 3 -> 5 -> null
                        // After:  Head -> 3 ------> null
                        previous.Next = current.Next;

                        // it was the end - so update Tail
                        if (current.Next == null)
                        {
                            Tail = previous;
                        }

                        Count--;
                    }
                    else
                    {
                        // Case 2 or 3a
                        RemoveFirst();
                    }

                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Enumerates over the linked list values from Head to Tail
        /// </summary>
        /// <returns>A Head to Tail enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = Head;
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
        /// Enumerates over the linked list values from Tail to Head
        /// </summary>
        /// <returns>A Tail to Head enumerator</returns>
        public IEnumerable<T> GetReverseEnumerator()
        {
            var nodes = new Stack<T>();
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                nodes.Push(current.Value);
                current = current.Next;
            }

            while(nodes.Count > 0)
            {
                yield return nodes.Pop();
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