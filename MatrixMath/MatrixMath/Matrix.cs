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
        public System.Drawing.Size Size => new System.Drawing.Size(this.RowCount, this.ColumnCount);

        /// <summary>
        /// Default class constructor
        /// </summary>
        public Matrix() : base()
        {
        }

        /// <summary>
        /// Constructor with row and column count inputs.
        /// </summary>
        /// <param name="rowCount"> Number of rows. </param>
        /// <param name="columnCount"> Number of columns. </param>
        public Matrix(int rowCount, int columnCount) : base(rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                this.Add(new Vector(columnCount));
            }
        }

        /// <summary>
        /// Class constructor to generate new Matrix from an input List of row Vectors
        /// </summary>
        /// <param name="rows"></param>
        public Matrix(List<Vector> rows) : base(rows.Count)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                this.Add(new Vector(rows[i].Count));
                for (int j = 0; j < rows[i].Count; j++)
                {
                    this[i][j] = rows[i][j];
                }
            }
        }

		/// <summary>
		/// Replaces the general List.Add method, adding a check that the new row
		/// vector has the correct length. If not, throws a SizeMismatchException.
		/// </summary>
		/// <param name="newRow"> New row to add to this Matrix. </param>
		new public void Add(Vector newRow)
		{
			if (this.RowCount != 0 && newRow.Count != this.ColumnCount)
			{
				throw new SizeMismatchException();
			}
			base.Add(newRow);
		}

        /// <summary>
        /// Method to add a column to all rows of this matrix.
        /// </summary>
        /// <param name="index"> New index of row to be added. </param>
        public void InsertColumn(int index)
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
        public void RemoveColumnAt(int index)
        {
            for (int i = 0; i < RowCount; i++)
            {
                this[i].RemoveAt(index);
            }
        }

        /// <summary>
        /// Overloads multiplication operator for a Matrix with a scalar.
        /// Generates scalar product of the input matrix with the input scalar.
        /// </summary>
        /// <param name="scalar"> Scalar input. </param>
        /// <param name="matrix"> Matrix input. </param>
        /// <returns> Scaled matrix. </returns>
        public static Matrix operator *(float scalar, Matrix matrix)
        {
            Matrix returnMatrix = new Matrix(matrix.RowCount, matrix.ColumnCount);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    returnMatrix[i, j] = scalar * matrix[i, j];
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// Overloads multiplication operator for a Matrix with a Vector.
        /// Method to left-multiply an input vector by the input matrix.
        /// </summary>
        /// <param name="matrix"> Input matrix. </param>
        /// <param name="vector"> Input vector. </param>
        /// <returns> Product vector. </returns>
        public static Vector operator *(Matrix matrix, Vector vector)
        {
            if (vector.Count() != matrix.ColumnCount)
            {
                throw new SizeMismatchException();
            }

            Vector product = new Vector();
            for (int i = 0; i < matrix.RowCount; i++)
            {
                float element = 0.0f;
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    element += matrix[i][j] * vector[j];
                }
                product.Add(element);
            }
            return product;
        }

        /// <summary>
        /// Overloads multiplication operator for a pair of instances of the Matrix class.
        /// Generates matrix product of the input matrices, multiplied in the input order.
        /// </summary>
        /// <param name="leftMatrix"> Left-hand matrix input. </param>
        /// <param name="rightMatrix"> Right-hand matrix input. </param>
        /// <returns> Matrix product. </returns>
        public static Matrix operator *(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (rightMatrix.RowCount != leftMatrix.ColumnCount)
            {
                throw new SizeMismatchException();
            }

            Matrix returnMatrix = new Matrix(leftMatrix.RowCount, rightMatrix.ColumnCount);
            for (int i = 0; i < rightMatrix.ColumnCount; i++)
            {
                for (int j = 0; j < leftMatrix.RowCount; j++)
                {
                    float nextValue = 0.0f;
                    for (int k = 0; k < leftMatrix.ColumnCount; k++)
                    {
                        nextValue += leftMatrix[j, k] * rightMatrix[k, i];
                    }
                    returnMatrix[j, i] = nextValue;
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// Overloads addition operator for a pair of instances of the Matrix class.
        /// Generates member-wise sum of the input matrices.
        /// </summary>
        /// <param name="matrix1"> First matrix input. </param>
        /// <param name="matrix2"> Second matrix input. </param>
        /// <returns> Matrix member-wise sum. </returns>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Size != matrix2.Size)
            {
                throw new SizeMismatchException();
            }

            Matrix returnMatrix = new Matrix(matrix1.RowCount, matrix1.ColumnCount);
            for (int i = 0; i < matrix1.RowCount; i++)
            {
                for (int j = 0; j < matrix1.ColumnCount; j++)
                {
                    returnMatrix[i][j] = matrix1[i][j] + matrix2[i][j];
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// Overloads binary subtraction operator for a pair of instances of the Matrix class.
        /// Generates member-wise difference of the input matrices.
        /// </summary>
        /// <param name="matrix1"> First matrix input. </param>
        /// <param name="matrix2"> Second matrix input. </param>
        /// <returns> Matrix member-wise difference. </returns>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Size != matrix2.Size)
            {
                throw new SizeMismatchException();
            }

            Matrix returnMatrix = new Matrix(matrix1.RowCount, matrix1.ColumnCount);
            for (int i = 0; i < matrix1.RowCount; i++)
            {
                for (int j = 0; j < matrix1.ColumnCount; j++)
                {
                    returnMatrix[i][j] = matrix1[i][j] - matrix2[i][j];
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// Overloads unary subtraction operator for instances of the Matrix class.
        /// Generates negative of the input matrix.
        /// </summary>
        /// <param name="matrix"> Matrix input. </param>
        /// <returns> Negated matrix. </returns>
        public static Matrix operator -(Matrix matrix)
        {
            return (-1.0f * matrix);
        }

        /// <summary>
        /// Static method to get the transpose of an input matrix
        /// </summary>
        /// <param name="matrix"> Input matrix. </param>
        /// <returns> Transposed matrix. </returns>
        public static Matrix Transpose(Matrix matrix)
        {
            Matrix transpose = new Matrix(matrix.ColumnCount, matrix.RowCount);
            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                for (int j = 0; j < matrix.RowCount; j++)
                {
                    transpose[i][j] = matrix[j][i];
                }
            }

            return transpose;
        }

        /// <summary>
        /// Solves the equation A * X = Y for input matrix A and vector y (where "*" is a cross-product, not convolution).
        /// </summary>
        /// <param name="matrix"> Input "A" matrix. </param>
        /// <param name="problem"> Input "Y" vector. </param>
        /// <param name="solution"> Output "X" vector. </param>
        /// <returns> Return boolean specifying whether Solve was successful (true) or not (false). </returns>
        public static bool Solve(Matrix matrix, Vector problem, out Vector solution)
        {
            if (problem.Count() != matrix.RowCount || !matrix.IsSquare)
            {
                throw new SizeMismatchException();
            }

            solution = new Vector(problem.Count);

            float[][] choleskyDecomp = new float[matrix.RowCount][];
            for (int i = 0; i < matrix.RowCount; i++)
            {
                choleskyDecomp[i] = new float[i + 1];
            }
            try
            {
                choleskyDecomp = CholeskyDecomp(matrix, out bool isReal);
                if (!isReal)
                {
                    return false;
                }
            }
            catch (DivideByZeroException)
            {
                return false;
            }

            try
            {
                IEnumerator<float> vectorEnumerator = problem.GetEnumerator();
                for (int i = 0; i < matrix.RowCount; i++)
                {
                    vectorEnumerator.MoveNext();
                    solution[i] = vectorEnumerator.Current;
                    for (int j = 0; j < i; j++)
                    {
                        solution[i] -= choleskyDecomp[i][j] * solution[j];
                    }
                    solution[i] /= choleskyDecomp[i][i];
                }
                for (int i = matrix.RowCount - 1; i > -1; i--)
                {
                    for (int j = i + 1; j < matrix.ColumnCount; j++)
                    {
                        solution[i] -= choleskyDecomp[j][i] * solution[j];
                    }
                    solution[i] /= choleskyDecomp[i][i];
                }
            }
            catch (System.DivideByZeroException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Obtains the trace of the input matrix if possible.
        /// </summary>
        /// <param name="matrix"> Input matrix. </param>
        /// <param name="tr"> Output to hold resultant trace. </param>
        /// <returns> Returns true if Trace calculation was valid. Otherwise false. </returns>
        public static bool Trace(Matrix matrix, out float tr)
        {
            tr = 0.0f;

            if (!matrix.IsSquare)
            {
                return false;
            }

            for (int i = 0; i < matrix.RowCount; i++)
            {
                tr += matrix[i][i];
            }

            return true;
        }

        /// <summary>
        /// Obtains the inverse of the input matrix if possible.
        /// </summary>
        /// <param name="matrix"> Input matrix. </param>
        /// <param name="inverse"> Output to hold resultant inverted matrix. </param>
        /// <returns>Returns true if Inverse calculation was valid. Otherwise false. </returns>
        public static bool Inverse(Matrix matrix, out Matrix inverse)
        {
            inverse = new Matrix(matrix.RowCount, matrix.ColumnCount);

            if (!matrix.IsSquare)
            {
                return false;
            }

            // Get Cholesky decomposition of matrix
            float[][] choleskyDecomp = new float[matrix.RowCount][];
            for (int i = 0; i < matrix.RowCount; i++)
            {
                choleskyDecomp[i] = new float[i + 1];
            }
            try
            {
                choleskyDecomp = CholeskyDecomp(matrix, out bool isReal);
                if (!isReal)
                {
                    return false;
                }
            }
            catch (DivideByZeroException)
            {
                return false;
            }

            // Invert Cholesky decomposition
            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                choleskyDecomp[i][i] = 1.0f / choleskyDecomp[i][i];
                for (int j = i + 1; j < matrix.RowCount; j++)
                {
                    float c = 0.0f;
                    for (int k = 0; k < j; k++)
                    {
                        c -= choleskyDecomp[k][i] * choleskyDecomp[j][k];
                    }
                    choleskyDecomp[j][i] = c / choleskyDecomp[j][j];
                }
            }

            // Convert inverted Cholesky jagged array to Matrix
            Matrix choleskyDecompInverseMatrix = new Matrix(matrix.RowCount, matrix.ColumnCount);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < choleskyDecomp[i].Length; j++)
                {
                    choleskyDecompInverseMatrix[i][j] = choleskyDecomp[i][j];
                }
                for (int j = choleskyDecomp[i].Length; j < matrix.ColumnCount; j++)
                {
                    choleskyDecompInverseMatrix[i][j] = 0.0f;
                }
            }

            // Calculate matrix inverse from inverted Cholesky decomposition
            inverse = Matrix.Transpose(choleskyDecompInverseMatrix) * choleskyDecompInverseMatrix;

            return true;
        }

        /// <summary>
        /// Obtains the determinant of the input matrix if possible.
        /// </summary>
        /// <param name="matrix"> Input matrix. </param>
        /// <param name="det"> Output determinant  value. </param>
        /// <returns> Returns true if Determinant calculation was valid. Otherwise false.</returns>
        public static bool Determinant(Matrix matrix, out float det)
        {
            det = 0.0f;

            if (!matrix.IsSquare)
            {
                return false;
            }

            for (int i = 0; i < matrix.Count; i++)
            {
                float diagProd = 1.0f;
                for (int j = 0; j < matrix.Count; j++)
                {
                    diagProd *= matrix[j][(i + j) % matrix.Count];
                }
                det += diagProd;
            }
            for (int i = matrix.Count - 1; i < 2 * matrix.Count - 2; i++)
            {
                float diagProd = 1.0f;
                for (int j = 0; j < matrix.Count; j++)
                {
                    diagProd *= matrix[j][(i + j) % matrix.Count];
                }
                det -= diagProd;
            }

            return true;
        }

        /// <summary>
        /// Determines the eigenvalues and eigenvectors of the input Matrix.
        /// </summary>
        /// <param name="matrix"> Matrix input. </param>
        /// <param name="eigenValues">
        /// Reference List of floats to be populated with eigenvalues.
        /// Order of returned values matches  order of returned entries in eigenvectors List.
        /// </param>
        /// <param name="eigenVectors">
        /// Reference List of vectors to be populated with eigenvectors.
        /// Order of returned vectors matches order of returned entries in eigenvalues List.
        /// </param>
        /// <returns> Returns true if eigenvalue/vector calculations were valid. Otherwise false. </returns>
        public static bool EigenPairs(Matrix matrix, out List<float> eigenValues, out List<Vector> eigenVectors)
        {
            eigenValues = new List<float>();
            eigenVectors = new List<Vector>();

            if (!matrix.IsSquare)
            {
                return false;
            }

            float[][] choleskyDecomp = new float[matrix.RowCount][];
            for (int i = 0; i < matrix.RowCount; i++)
            {
                choleskyDecomp[i] = new float[i + 1];
            }
            try
            {
                choleskyDecomp = CholeskyDecomp(matrix, out bool isReal);
                if (!isReal)
                {
                    return false;
                }
            }
            catch (DivideByZeroException)
            {
                return false;
            }

            //algorithm here

            //return true;
            return false; // Until this function is complete, return false to indicate method failure
        }
        
        /// <summary>
        /// Private helper method to get the Cholesky decomp of a matrix.
        /// </summary>
        /// <param name="matrix"> Input matrix. </param>
        /// <param name="ResultIsReal">
        /// Boolean flag indicating whether or not the Cholesky
        /// decomposition could successfully be completed over Reals.
        /// </param>
        /// <returns> Reutrns Cholesky decomp jagged array. </returns>
        private static float[][] CholeskyDecomp(Matrix matrix, out bool ResultIsReal)
        {
            ResultIsReal = true;
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
                    if (decomp[i][i] <= 0.0f)
                    {
                        decomp[i][i] = 0.0f;
                        ResultIsReal = false;
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
            catch (DivideByZeroException)
            {
                throw;
            }
            return decomp;
        }
    }
}
