using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMath
{
    public class Matrix : List<Vector>
    {
        public int RowCount => this.Count;
        public int ColumnCount => ((RowCount == 0) ? 0 : this[0].Count);
        public float this[int r, int c]
        {
            get
            {
                return this[r][c];
            }
            set
            {
                this[r][c] = value;
            }
        }
        public bool IsSquare => (RowCount == ColumnCount);

        /// <summary>
        /// Constructor with row and column count inputs.
        /// </summary>
        /// <param name="rowCount"> Number of rows. </param>
        /// <param name="columnCount"> Number of columns. </param>
        public Matrix(int rowCount, int columnCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                this.Add(new Vector(columnCount));
            }
        }

        /// <summary>
        /// Method to add a column to all rows of this matrix
        /// </summary>
        /// <param name="index"> New index of row to be added. </param>
        public void AddColumn(int index)
        {
            foreach (Vector row in this)
            {
                row.Insert(index, 0.0f);
            }
        }

        /// <summary>
        /// Method to remove a column from all rows of this matrix
        /// </summary>
        /// <param name="index"> Index of existing row to be removed. </param>
        public void RemoveColumn(int index)
        {
            for (int i = 0; i < RowCount; i++)
            {
                this[i].RemoveAt(index);
            }
        }

        /// <summary>
        /// Method to left-multiply an input vector by this matrix.
        /// </summary>
        /// <param name="vector"> Input vector. </param>
        /// <returns> Product vector. </returns>
        public Vector VectorMultiply(Vector vector)
        {
            if (vector.Count() != this.ColumnCount)
            {
                throw new SizeMismatchException();
            }

            Vector product = new Vector();
            for (int i = 0; i < RowCount; i++)
            {
                float element = 0.0f;
                for (int j = 0; j < ColumnCount; j++)
                {
                    element += this[i][j] * vector[j];
                }
                product.Add(element);
            }
            return product;
        }

        /// <summary>
        /// Generates matrix product of the input matrix left-multiplied by this matrix.
        /// </summary>
        /// <param name="rightMatrix"> Right-hand matrix input. </param>
        /// <returns> Matrix product. </returns>
        public Matrix MatrixMultiply(Matrix rightMatrix)
        {
            if (rightMatrix.RowCount != this.ColumnCount)
            {
                throw new SizeMismatchException();
            }

            Matrix returnMatrix = new Matrix(this.RowCount, rightMatrix.ColumnCount);
            for (int i = 0; i < rightMatrix.ColumnCount; i++)
            {
                for (int j = 0; j < this.RowCount; j++)
                {
                    float nextValue = 0.0f;
                    for (int k = 0; k < this.ColumnCount; k++)
                    {
                        nextValue += this[j, k] * rightMatrix[k, i];
                    }
                    returnMatrix[j, i] = nextValue;
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// Solves the equation A x v = y for input vector v and this matrix A
        /// </summary>
        /// <param name="vector"> Input "y" vector. </param>
        /// <returns> Return "v" solution. </returns>
        public Vector SolveFor(Vector vector)
        {
            if (vector.Count() != this.RowCount || !this.IsSquare)
            {
                throw new SizeMismatchException();
            }

            Vector solution = new Vector();
            float[][] choleskyDecomp = new float[this.RowCount][];
            for (int i = 0; i < this.RowCount; i++)
            {
                choleskyDecomp[i] = new float[i + 1];
            }
            choleskyDecomp = CholeskyDecomp(this);
            try
            {
                IEnumerator<float> vectorEnumerator = vector.GetEnumerator();
                for (int i = 0; i < this.RowCount; i++)
                {
                    vectorEnumerator.MoveNext();
                    solution[i] = vectorEnumerator.Current;
                    for (int j = 0; j < i; j++)
                    {
                        solution[i] -= choleskyDecomp[i][j] * solution[j];
                    }
                    solution[i] /= choleskyDecomp[i][i];
                }
                for (int i = this.RowCount - 1; i > -1; i--)
                {
                    for (int j = i + 1; j < this.ColumnCount; j++)
                    {
                        solution[i] -= choleskyDecomp[j][i] * solution[j];
                    }
                    solution[i] /= choleskyDecomp[i][i];
                }
            }
            catch (System.DivideByZeroException)
            {
                throw new DivideByZeroException("Division by zero in SolveFor()");
            }
            return solution;
        }

        /// <summary>
        /// Private helper method to get the Cholesky decomp of a matrix for use in matrix equation-solving.
        /// </summary>
        /// <returns> Reutrns Cholesky decomp jagged array. </returns>
        private static float[][] CholeskyDecomp(Matrix matrix)
        {
            float[][] decomp = new float[matrix.RowCount][];
            for (int i = 0; i < matrix.RowCount; i++)
            {
                decomp[i] = new float[i + 1];
            }
            try
            {
                for (int i = 0; i < matrix.RowCount; i++)
                {
                    decomp[i][i] = matrix[i][i];
                    for (int k = 0; k < i; k++)
                    {
                        decomp[i][i] -= decomp[i][k] * decomp[i][k];
                    }
                    decomp[i][i] = (float)Math.Sqrt((double)decomp[i][i]);
                    for (int j = i + 1; j < matrix.ColumnCount; j++)
                    {
                        decomp[j][i] = matrix[i][j];
                        for (int k = 0; k < i; k++)
                        {
                            decomp[j][i] -= decomp[i][k] * decomp[j][k];
                        }
                        decomp[j][i] /= decomp[i][i];
                    }
                }
            }
            catch (System.DivideByZeroException)
            {
                throw new DivideByZeroException("Division by zero in CholeskyDecomp()");
            }
            return decomp;
        }
    }
}
