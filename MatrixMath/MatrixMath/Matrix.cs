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
    }
}
