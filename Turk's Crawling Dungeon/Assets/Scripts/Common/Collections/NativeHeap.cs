using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace TCD
{
    public struct NativeHeap<T> : IDisposable where T : struct, INativeHeapItem<T>
    {
        private const int INVALID_SWAP_INDEX = 0;

        [DeallocateOnJobCompletion] private NativeArray<T> items;
        private int count;

        public T this[int index] => items[index];

        public int Count => count;

        public NativeHeap(int maxSize, Allocator allocator)
        {
            items = new NativeArray<T>(maxSize, allocator);
            count = 0;
        }

        public void Add(T item)
        {
            item.HeapIndex = count;
            items[count] = item;
            SortUp(item);
            count++;
        }

        private void SortUp(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;
            while (true)
            {
                T parent = items[parentIndex];
                if (item.CompareTo(parent) > 0)
                    Swap(item, parent);
                else
                    break;
                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }

        private void Swap(T a, T b)
        {
            items[a.HeapIndex] = b;
            items[b.HeapIndex] = a;
            int i = a.HeapIndex;
            a.HeapIndex = b.HeapIndex;
            b.HeapIndex = i;
        }

        public T RemoveFirst()
        {
            if (count == 0)
                ExceptionHandler.Handle(new HeapException(
                    "Cannot remove first item from empty heap!"));
            T firstItem = items[0];
            count--;
            items[0] = items[count];
            T newFirstItem = items[0];
            newFirstItem.HeapIndex = 0;
            SortDown(newFirstItem);
            return firstItem;
        }

        private void SortDown(T item)
        {
            while (true)
            {
                int swapIndex = GetChildSwapIndex(item);
                if (swapIndex != INVALID_SWAP_INDEX)
                {
                    T swapItem = items[swapIndex];
                    if (item.CompareTo(swapItem) < 0)
                        Swap(item, swapItem);
                    else
                        return;
                }
                else
                    return;
            }
        }

        private int GetChildSwapIndex(T item)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = INVALID_SWAP_INDEX;

            if (childIndexLeft < count)
            {
                swapIndex = childIndexLeft;
                T childLeft = items[childIndexLeft];

                if (childIndexRight < count)
                {
                    T childRight = items[childIndexRight];
                    if (childLeft.CompareTo(childRight) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }
            }

            return swapIndex;
        }

        public bool Contains(T item) => Equals(items[item.HeapIndex], item);

        public void UpdateItem(T item) => SortUp(item);

        public void Reset()
        {
            for (int i = Count - 1; i >= 0; i--)
                items[i] = default;
            count = 0;
        }

        public void Dispose()
        {
            items.Dispose();
        }
    }
}
