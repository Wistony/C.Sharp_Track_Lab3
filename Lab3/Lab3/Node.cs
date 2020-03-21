namespace Lab3
{
    using System.Net.Sockets;
    using System.Xml.Xsl;

    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }
        public int RowCount { get; set; }
        public Header Head { get; set; }
        
        public Node(Node left, Node right, Node up, Node down, Header head, int row)
        {
            Left = left ?? this;
            Right = right ?? this;
            Up = up ?? this;
            Down = down ?? this;
            Head = head ?? this as Header;
            RowCount = row;
        }

        public void AttachRow()
        {
            this.Left.Right = this;
            this.Right.Left = this;
        }

        public void AttachColumn()
        {
            this.Up.Down = this;
            this.Down.Up = this;
        }

        public void DetachRow()
        {
            this.Right.Left = this.Left;
            this.Left.Right = this.Right;
        }

        public void DetachColumn()
        {
            this.Up.Down = this.Down;
            this.Down.Up = this.Up;
        }
    }

    public class Header : Node
    {
        public Header(Node left, Node right) 
            : base(left, right, null, null, null, -1)
        {
            
        }
        
        public int Size { get; set; }
    }
    
}
