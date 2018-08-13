using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMath
{
    class SizeMismatchException : Exception
    {
        public override string Message => "Vector/Matrix sizes incompatible. Operation not possible.";
    }
}
