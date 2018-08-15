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
        /// <param name="count"> Length of new vector. </param>
        public Vector(int count) : base(count)
        {
        }

        /// <summary>
        /// Class constructor from existing List of float values
        /// </summary>
        /// <param name="entries"></param>
        public Vector(List<float> entries) : base(entries.Count)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                this[i] = entries[i];
            }
        }

        /// <summary>
        /// Method to obtain dot product of two Vectors.
        /// </summary>
        /// <param name="vector1"> First vector. </param>
        /// <param name="vector2"> Second vector. </param>
        /// <returns> Dot product scalar value. Throws SizeMismatchException if Vector lengths do not match. </returns>
        public static float DotProduct(Vector vector1, Vector vector2)
        {
            if (vector1.Count != vector2.Count)
            {
                throw new SizeMismatchException();
            }

            float product = 0.0f;
            for (int i = 0; i < vector1.Count; i++)
            {
                product += vector1[i] * vector2[i];
            }

            return product;
        }

        /// <summary>
        /// Overloaded operator allowing scalar multiplication with a vector.
        /// </summary>
        /// <param name="scalar"> Scalar input.</param>
        /// <param name="vector"> Vector input. </param>
        /// <returns> Scaled vector result </returns>
        public static Vector operator *(float scalar, Vector vector)
        {
            Vector scaledVector = new Vector();
            foreach (float entry in vector)
            {
                scaledVector.Add(scalar * entry);
            }

            return scaledVector;
        }

        /// <summary>
        /// Overloaded operator allowing vector addition
        /// </summary>
        /// <param name="vector1"> First vector. </param>
        /// <param name="vector2"> Second vector. </param>
        /// <returns> Returns resultant vector sum (member-wise).
        /// If vector sizes do not match, returns SizeMismatchException.
        /// </returns>
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            if (vector1.Count != vector2.Count)
            {
                throw new SizeMismatchException();
            }

            Vector sum = new Vector();
            for (int i = 0; i < vector1.Count; i++)
            {
                sum.Add(vector1[i] + vector2[i]);
            }

            return sum;
        }

        /// <summary>
        /// Overloaded operator allowing vector subtraction
        /// </summary>
        /// <param name="vector1"> First vector. </param>
        /// <param name="vector2"> Second vector. </param>
        /// <returns> Returns resultant vector difference (member-wise).
        /// If vector sizes do not match, returns SizeMismatchException.
        /// </returns>
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            if (vector1.Count != vector2.Count)
            {
                throw new SizeMismatchException();
            }

            Vector dif = new Vector();
            for (int i = 0; i < vector1.Count; i++)
            {
                dif.Add(vector1[i] - vector2[i]);
            }

            return dif;
        }

        /// <summary>
        /// Overloaded operator allowing vector negation
        /// </summary>
        /// <param name="vector"> Input vector. </param>
        /// <returns> Negated vector. </returns>
        public static Vector operator -(Vector vector)
        {
            return (-1.0f * vector);
        }
    }
}
