using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rack_OGame
{
    public class Stack
    {
        public int Size { get; set; }
        public Node? Top { get; set; }

        public Stack()
        {
            Size = 0;
            Top = null;
        }

        public void Push(Node node)
        {
            node.Next = Top;
            Top = node;
            Size++;
        }

        public Node? Pop()
        {
            if (Size == 0) return null;

            Node old = Top!;
            if(Top!.Next is not null) Top = Top.Next;
            Size--;
            old.Next = null;
            return old;
        }

        public int Peek()
        {
            return Top!.Value;
        }
    }
}
