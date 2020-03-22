namespace Lab3
{
    public class Node
    {
        public Node(Node left, Node right, Node up, Node down, Header head, int row)
        {
            Left = left ?? this;
            Right = right ?? this;
            Up = up ?? this;
            Down = down ?? this;
            Head = head ?? this as Header;
            Row = row;
        }

        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }
        public Header Head { get; }
        public int Row { get; }

        public void AttachLeftRight()
        {
            this.Left.Right = this;
            this.Right.Left = this;
        }

        public void AttachUpDown()
        {
            this.Up.Down = this;
            this.Down.Up = this;
        }

        public void DetachLeftRight()
        {
            this.Left.Right = this.Right;
            this.Right.Left = this.Left;
        }

        public void DetachUpDown()
        {
            this.Up.Down = this.Down;
            this.Down.Up = this.Up;
        }

    }

    public class Header : Node
    {
        public Header(Node left, Node right) : base(left, right, null, null, null, -1)
        {
        }

        public int Size { get; set; }
    }
}