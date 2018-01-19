using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions.Util
{
    public static class Helper
    {

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
}
