namespace Lab3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design.Serialization;
    using System.Linq;

    public class DLX
    {
        private Header root = new Header(null, null){ Size = int.MaxValue};
        private List<Header> columns { get; set; }
        private List<Node> rows { get; set; }
        private Stack<Node> solutions = new Stack<Node>();
        private int initial = 0;

        public DLX(int rowsCount, int columnsCount)
        {
            rows = new List<Node>(rowsCount);
            columns = new List<Header>(columnsCount);
        }
        
        public void AddHeader()
        {
            var h = new Header(root.Left, root);
            h.AttachRow();
            columns.Add(h);
        }

        public void AddRow(params int[] newRow)
        {
            Node first = null;
            if (newRow != null)
            {
                foreach (var i in newRow)
                {
                    if (newRow[i] < 0)
                    {
                        continue;
                    }

                    if (first == null)
                    {
                        first = AddNode(rows.Count, newRow[i]);
                    }
                    else
                    {
                        AddNode(first, newRow[i]);
                    }
                }
            }
            rows.Add(first);
        }
        
        private Node AddNode(int row, int column) {
            var n = new Node(null, null, columns[column].Up, columns[column], columns[column], row);
            n.AttachColumn();
            n.Head.Size++;
            return n;
        }
        private void AddNode(Node firstNode, int column) {
            var n = new Node(firstNode.Left, firstNode, columns[column].Up, columns[column], columns[column], firstNode.RowCount);
            n.AttachRow();
            n.AttachColumn();
            n.Head.Size++;
        }
        
        public void Give(int row) {
            solutions.Push(rows[row]);
            CoverMatrix(rows[row]);
            initial++;
        }
        
        public IEnumerable<int[]> Solutions() 
        {
            try
            {
                var node = ChooseSmallestColumn().Down;
                do {
                    if (node == node.Head) 
                    {
                        if (node == root) 
                        {
                            yield return solutions.Select(n => n.RowCount).ToArray();
                        }
                        if (solutions.Count > initial) 
                        {
                            node = solutions.Pop();
                            UncoverMatrix(node);
                            node = node.Down;
                        }
                    } 
                    else 
                    {
                        solutions.Push(node);
                        CoverMatrix(node);
                        node = ChooseSmallestColumn().Down;
                    }
                } while(solutions.Count > initial || node != node.Head);
            } 
            finally 
            {
                Restore();
            }
        }
 
        private void Restore()
        {
            while (solutions.Count > 0)
            {
                UncoverMatrix(solutions.Pop());
            }
            initial = 0;
        }
 
        private Header ChooseSmallestColumn()
        {
            Header traveller = root, choice = root;
            do {
                traveller = (Header)traveller.Right;
                if (traveller.Size < choice.Size)
                {
                    choice = traveller;
                }
            } while (traveller != root && choice.Size > 0);
            
            return choice;
        }
        
        private void CoverRow(Node row) 
        {
            var traveller = row.Right;
            while (traveller != row) {
                traveller.DetachColumn();
                traveller.Head.Size--;
                traveller = traveller.Right;
            }
        }
 
        private void UncoverRow(Node row)
        {
            var traveller = row.Left;
            while (traveller != row) {
                traveller.AttachColumn();
                traveller.Head.Size++;
                traveller = traveller.Left;
            }
        }
 
        private void CoverColumn(Header column)
        {
            column.DetachRow();
            var traveller = column.Down;
            while (traveller != column) {
                CoverRow(traveller);
                traveller = traveller.Down;
            }
        }
 
        private void UncoverColumn(Header column) 
        {
            var traveller = column.Up;
            while (traveller != column) {
                UncoverRow(traveller);
                traveller = traveller.Up;
            }
            column.AttachRow();
        }
 
        private void CoverMatrix(Node node) 
        {
            var traveller = node;
            do {
                CoverColumn(traveller.Head);
                traveller = traveller.Right;
            } while (traveller != node);
        }
 
        private void UncoverMatrix(Node node) 
        {
            var traveller = node;
            do {
                traveller = traveller.Left;
                UncoverColumn(traveller.Head);
            } while (traveller != node);
        }

    }
}