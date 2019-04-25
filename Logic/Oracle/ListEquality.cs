using System.Collections.Generic;
using Model.Entities.Oracle;

namespace Logic.Oracle
{
    public class StockListEquality : IEqualityComparer<Stock>
    {
        public bool Equals(Stock x, Stock y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(Stock obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return obj.ToString().GetHashCode();
            }
        }
    }
}
