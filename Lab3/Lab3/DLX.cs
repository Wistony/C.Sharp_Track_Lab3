using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    public class DLX //Some functionality elided
    {
        private readonly Header root = new Header(null, null) {Size = int.MaxValue};
        private readonly List<Header> columns;
        private readonly List<Node> rows;
        private readonly Stack<Node> solutionNodes = new Stack<Node>();
        private int initial = 0;

        public DLX(int rowCapacity, int columnCapacity)
        {
            columns = new List<Header>(columnCapacity);
            rows = new List<Node>(rowCapacity);
        }

        public void AddHeader()
        {
            Header h = new Header(root.Left, root);
            h.AttachLeftRight();
            columns.Add(h);
        }

        public void AddRow(params int[] newRow)
        {
            Node first = null;
            if (newRow != null)
            {
                for (int i = 0; i < newRow.Length; i++)
                {
                    if (newRow[i] < 0) continue;
                    if (first == null) first = AddNode(rows.Count, newRow[i]);
                    else AddNode(first, newRow[i]);
                }
            }

            rows.Add(first);
        }

        private Node AddNode(int row, int column)
        {
            Node n = new Node(null, null, columns[column].Up, columns[column], columns[column], row);
            n.AttachUpDown();
            n.Head.Size++;
            return n;
        }

        private void AddNode(Node firstNode, int column)
        {
            Node n = new Node(firstNode.Left, firstNode, columns[column].Up, columns[column], columns[column],
                firstNode.Row);
            n.AttachLeftRight();
            n.AttachUpDown();
            n.Head.Size++;
        }

        public void Give(int row)
        {
            solutionNodes.Push(rows[row]);
            CoverMatrix(rows[row]);
            initial++;
        }

        public IEnumerable<int[]> Solutions()
        {
            try
            {
                var node = ChooseSmallestColumn().Down;
                do
                {
                    if (node == node.Head)
                    {
                        if (node == root)
                        {
                            yield return solutionNodes.Select(n => n.Row).ToArray();
                        }

                        if (solutionNodes.Count > initial)
                        {
                            node = solutionNodes.Pop();
                            UncoverMatrix(node);
                            node = node.Down;
                        }
                    }
                    else
                    {
                        solutionNodes.Push(node);
                        CoverMatrix(node);
                        node = ChooseSmallestColumn().Down;
                    }
                } while (solutionNodes.Count > initial || node != node.Head);
            }
            finally
            {
                Restore();
            }
        }

        private void Restore()
        {
            while (solutionNodes.Count > 0) UncoverMatrix(solutionNodes.Pop());
            initial = 0;
        }

        private Header ChooseSmallestColumn()
        {
            Header traveller = root, choice = root;
            do
            {
                traveller = (Header) traveller.Right;
                if (traveller.Size < choice.Size) choice = traveller;
            } while (traveller != root && choice.Size > 0);

            return choice;
        }

        private void CoverRow(Node row)
        {
            Node traveller = row.Right;
            while (traveller != row)
            {
                traveller.DetachUpDown();
                traveller.Head.Size--;
                traveller = traveller.Right;
            }
        }

        private void UncoverRow(Node row)
        {
            Node traveller = row.Left;
            while (traveller != row)
            {
                traveller.AttachUpDown();
                traveller.Head.Size++;
                traveller = traveller.Left;
            }
        }

        private void CoverColumn(Header column)
        {
            column.DetachLeftRight();
            Node traveller = column.Down;
            while (traveller != column)
            {
                CoverRow(traveller);
                traveller = traveller.Down;
            }
        }

        private void UncoverColumn(Header column)
        {
            Node traveller = column.Up;
            while (traveller != column)
            {
                UncoverRow(traveller);
                traveller = traveller.Up;
            }

            column.AttachLeftRight();
        }

        private void CoverMatrix(Node node)
        {
            Node traveller = node;
            do
            {
                CoverColumn(traveller.Head);
                traveller = traveller.Right;
            } while (traveller != node);
        }

        private void UncoverMatrix(Node node)
        {
            Node traveller = node;
            do
            {
                traveller = traveller.Left;
                UncoverColumn(traveller.Head);
            } while (traveller != node);
        }
        
    }

    static class Extensions
    {
        public static IEnumerable<string> Cut(this string input, int length)
        {
            for (int cursor = 0; cursor < input.Length; cursor += length)
            {
                if (cursor + length > input.Length) yield return input.Substring(cursor);
                else yield return input.Substring(cursor, length);
            }
        }

        public static string DelimitWith<T>(this IEnumerable<T> source, string separator) =>
            string.Join(separator, source);
    }
}