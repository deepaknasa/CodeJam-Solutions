using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions.Util
{
    public static class Helper
    {
        public static List<string> GetLines(this StreamReader stream, int noOfLines)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < noOfLines; i++)
            {
                if (stream.Peek() >= 0)
                {
                    result.Add(stream.ReadLine());
                }
                else
                {
                    return result;
                }
            }
            return result;
        }

        public static int ToInt(this string str)
        {
            return Convert.ToInt32(str);
        }

        public static UInt64 ToUInt64(this string str)
        {
            return Convert.ToUInt64(str);
        }

        public static bool ToBool(this string str)
        {
            if (str.Equals("0"))
            {
                return false;
            }
            else if (str.Equals("1"))
            {
                return true;
            }
            return Convert.ToBoolean(str);
        }
    }


    public enum SortOrder
    {
        Asc,
        Desc
    }

    public class ArrComparer : IComparer<Int64>
    {
        private SortOrder _order;
        public ArrComparer()
        {
            _order = SortOrder.Asc;
        }

        public ArrComparer(SortOrder order)
        {
            _order = order;
        }

        public int Compare(Int64 x, Int64 y)
        {
            if (x == y)
            {
                return 0;
            }

            switch (_order)
            {
                case SortOrder.Asc:
                    if (x > y)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                case SortOrder.Desc:
                    if (x < y)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                default:
                    throw new NotImplementedException();
            }


        }
    }

    public class Node<T>
    {
        private T data;
        private NodeList<T> children;

        public Node()
        {
            data = default(T);
            children = null;
        }

        public Node(T data): this(data, null)
        {
            this.data = data;
        }

        public Node(T data, NodeList<T> children)
        {
            this.data = data;
            this.children = children;
        }

        public T Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public NodeList<T> Children
        {
            get
            {
                return children;
            }
            set
            {
                children = value;
            }
        }
    }

    public class NodeList<T>: Collection<Node<T>>
    {
        public NodeList(): base() { }

        public NodeList(int size)
        {
            for (int i = 0; i < size; i++)
            {
                base.Items.Add(default(Node<T>));
            }
        }

        public Node<T> FindByValue(T value)
        {
            foreach (var node in base.Items)
                if (node.Value.Equals(value)) return node;

            return null;
        }
    }
}
