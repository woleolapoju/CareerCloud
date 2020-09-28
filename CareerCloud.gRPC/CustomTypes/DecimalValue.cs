using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.CustomTypes

{
    public partial class DecimalValue
    {
        private const decimal NanoFactor = 1_000_000_000;

        public long Units { get; private set; }
        public int Nanos { get; private set; }

        public DecimalValue(long units, int nanos)
        {
            Units = units;
            Nanos = nanos;
        }
        public static implicit operator decimal(DecimalValue decimalValue) => decimalValue.ToDecimal();

        public static implicit operator DecimalValue(decimal value) => FromDecimal(value);

        public decimal ToDecimal()
        {
            return Units + Nanos / NanoFactor;
        }

        public static DecimalValue FromDecimal(decimal value)
        {
            long units = decimal.ToInt64(value);
            int nanos = decimal.ToInt32((value - units) * NanoFactor);
            return new DecimalValue(units, nanos);
        }
    }
}
