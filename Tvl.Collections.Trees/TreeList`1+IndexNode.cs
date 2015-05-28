﻿namespace Tvl.Collections.Trees
{
    using System;

    partial class TreeList<T>
    {
        private sealed class IndexNode : Node
        {
            private readonly int[] _offsets;
            private readonly Node[] _nodes;
            private int _nodeCount;
            private int _count;

            internal IndexNode(int branchingFactor)
            {
                _offsets = new int[branchingFactor];
                _nodes = new Node[branchingFactor];
            }

            internal IndexNode(int branchingFactor, Node child1, Node child2)
                : this(branchingFactor)
            {
                _nodes[0] = child1;
                _nodes[1] = child2;
                _offsets[1] = child1.Count;
                _nodeCount = 2;
                _count = child1.Count + child2.Count;
            }

            private static int FindLowerBound(int[] data, int length, int value)
            {
                int index = Array.BinarySearch(data, 0, length, value);
                if (index < 0)
                    return -index - 2;

                return index;
            }

            internal override int Count
            {
                get
                {
                    return _count;
                }
            }

            internal override T this[int index]
            {
                get
                {
                    int pageIndex = FindLowerBound(_offsets, _nodeCount, index);
                    return _nodes[pageIndex][index - _offsets[pageIndex]];
                }

                set
                {
                    int pageIndex = FindLowerBound(_offsets, _nodeCount, index);
                    _nodes[pageIndex][index - _offsets[pageIndex]] = value;
                }
            }

            internal override int IndexOf(T item)
            {
                for (int i = 0; i < Count; i++)
                {
                    int index = _nodes[i].IndexOf(item);
                    if (index >= 0)
                        return _offsets[i] + index;
                }

                return -1;
            }

            internal override Node Insert(int branchingFactor, bool isAppend, int index, T item)
            {
                int pageIndex = FindLowerBound(_offsets, _nodeCount, index);
                Node splitChild = _nodes[pageIndex].Insert(branchingFactor, isAppend, index - _offsets[pageIndex], item);
                if (splitChild == null)
                {
                    for (int i = pageIndex + 1; i < _nodeCount; i++)
                        _offsets[i]++;

                    _count++;
                    return null;
                }

                for (int i = pageIndex + 1; i < _nodeCount; i++)
                    _offsets[i] = _offsets[i - 1] + _nodes[i - 1].Count;

                _count = _offsets[_nodeCount - 1] + _nodes[_nodeCount - 1].Count;
                return InsertIndex(branchingFactor, isAppend, pageIndex + 1, splitChild);
            }

            private Node InsertIndex(int branchingFactor, bool isAppend, int index, Node node)
            {
                if (_nodeCount < _nodes.Length)
                {
                    if (index < _nodeCount)
                    {
                        Array.Copy(_nodes, index, _nodes, index + 1, _nodeCount - index);
                        Array.Copy(_offsets, index, _offsets, index + 1, _nodeCount - index);
                    }
                    else
                    {
                        _offsets[_nodeCount] = Count;
                    }

                    _nodes[index] = node;
                    _nodeCount++;

                    int delta = node.Count;
                    for (int i = index + 1; i < _nodeCount; i++)
                        _offsets[i] += delta;

                    _count += delta;
                    return null;
                }

                if (isAppend)
                {
                    // optimize the case of adding at the end of the overall list
                    IndexNode result = new IndexNode(branchingFactor);
                    result._nodes[0] = node;
                    result._nodeCount = 1;
                    result._count = node.Count;
                    return result;
                }
                else
                {
                    // split the node
                    IndexNode splitNode = new IndexNode(branchingFactor);
                    int splitPoint = _nodeCount / 2;
                    Array.Copy(_nodes, splitPoint, splitNode._nodes, 0, _nodeCount - splitPoint);
                    Array.Copy(_offsets, splitPoint, splitNode._offsets, 0, _nodeCount - splitPoint);
                    Array.Clear(_nodes, splitPoint, _nodeCount - splitPoint);
                    Array.Clear(_offsets, splitPoint, _nodeCount - splitPoint);

                    splitNode._nodeCount = _nodeCount - splitPoint;
                    int adjustment = splitNode._offsets[0];
                    for (int i = 0; i < splitNode._nodeCount; i++)
                        splitNode._offsets[i] -= adjustment;

                    splitNode._count = _count - adjustment;

                    _nodeCount = splitPoint;
                    _count = adjustment;

                    // insert the new element into the correct half
                    if (index <= splitPoint)
                        InsertIndex(branchingFactor, false, index, node);
                    else
                        splitNode.InsertIndex(branchingFactor, false, index - splitPoint, node);

                    return splitNode;
                }
            }
        }
    }
}
