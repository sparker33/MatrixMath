using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMath
{
    public class Vector : List<float>
    {
        /// <summary>
        /// Class constructor for new empty vector
        /// </summary>
        public Vector() : base()
        {
        }

        /// <summary>
        /// Class constructor for new vector with input length.
        /// </summary>
        /// <param name="length"> Length of new vector. </param>
        public Vector(int length) : base(length)
        {
        }

        /// <summary>
        /// Method to obtain dot product of two Vectors.
        /// </summary>
        /// <param name="otherVector"> Vector to multiply with </param>
        /// <returns> Dot product scalar value. Throws SizeMismatchException if Vector lengths do not match. </returns>
        public float DotProduct(Vector otherVector)
        {
            if (this.Count != otherVector.Count)
            {
                throw new SizeMismatchException();
            }

            float product = 0.0f;
            IEnumerator<float> vectorEnumerator = otherVector.GetEnumerator();
            foreach (float entry in this)
            {
                vectorEnumerator.MoveNext();
                product += vectorEnumerator.Current * entry;
            }

            return product;
        }
    }
}
