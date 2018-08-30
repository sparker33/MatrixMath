using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMath
{
	public class Kernel
	{
		// Private objects
		private Matrix smoothingCholeskyBase;
		protected Matrix H;
		protected Vector mu;

		// Public objects
		public int Dimensions { get => mu.Count; }

		/// <summary>
		/// Default public constructor.
		/// </summary>
		public Kernel()
		{
			mu = new Vector();
			smoothingCholeskyBase = new Matrix();
			H = smoothingCholeskyBase * Matrix.Transpose(smoothingCholeskyBase);
		}

		/// <summary>
		/// Public constructor with input dimensionality.
		/// </summary>
		/// <param name="dimensions"> Kernel dimensionality. </param>
		public Kernel(int dimensions)
		{
			mu = new Vector(dimensions);
			smoothingCholeskyBase = new Matrix(dimensions, dimensions);
			for (int i = 0; i < dimensions; i++)
			{
				for (int j = 0; j < dimensions; j++)
				{
					smoothingCholeskyBase[i][j] = 1.0f;
				}
			}
			H = smoothingCholeskyBase * Matrix.Transpose(smoothingCholeskyBase);
		}

		/// <summary>
		/// Method to set the value of this Kernel's center.
		/// </summary>
		/// <param name="center"> Vector defining the Kernel's new center.
		/// If the new Vector's size does not match the existing size,
		/// the Smoothing Matrix will be truncated to the corresponding new length.
		/// </param>
		public void SetCenter(Vector center)
		{
			while (smoothingCholeskyBase.Count > center.Count)
			{
				smoothingCholeskyBase.Remove(smoothingCholeskyBase.Last());
				smoothingCholeskyBase.RemoveColumnAt(smoothingCholeskyBase.Count);
			}
			while (smoothingCholeskyBase.Count < center.Count)
			{
				smoothingCholeskyBase.InsertColumn(smoothingCholeskyBase.Count);
				smoothingCholeskyBase.Add(new Vector(smoothingCholeskyBase.Count + 1));
				smoothingCholeskyBase[smoothingCholeskyBase.Count - 1][smoothingCholeskyBase.Count - 1] = 1.0f;
			}
			mu = new Vector(center);
			H.Clear();
			H = smoothingCholeskyBase * Matrix.Transpose(smoothingCholeskyBase);
		}

		/// <summary>
		/// Sets an individual center value for this Kernel.
		/// </summary>
		/// <param name="index"> Dimension of center whose value will be replaced. </param>
		/// <param name="newCenter"> New value. </param>
		public void SetCenterItem(int index, float value)
		{
			mu[index] = value;
		}

		/// <summary>
		/// Method to increment dimensionality of this kernel.
		/// </summary>
		public void AddDimension()
		{
			mu.Add(0.0f);
			smoothingCholeskyBase.InsertColumn(smoothingCholeskyBase.Count);
			smoothingCholeskyBase.Add(new Vector(smoothingCholeskyBase.Count + 1));
			smoothingCholeskyBase[smoothingCholeskyBase.Count - 1][smoothingCholeskyBase.Count - 1] = 1.0f;
			H.Clear();
			H = smoothingCholeskyBase * Matrix.Transpose(smoothingCholeskyBase);
		}

		/// <summary>
		/// Enables adding a configured dimension with inputs for the new dimension's center and smooting.
		/// </summary>
		/// <param name="newCenter"> Center for new dimension. </param>
		/// <param name="row"> Lower Triangular matrix row to be added. If the input vector does not have Count
		/// equal to the new Dimension of this Kernel, a SizeMismatchException will be thrown. </param>
		public void AddDimension(float newCenter, Vector row)
		{
			if (row.Count != (Dimensions + 1))
			{
				throw new SizeMismatchException();
			}
			mu.Add(newCenter);
			smoothingCholeskyBase.InsertColumn(smoothingCholeskyBase.Count);
			smoothingCholeskyBase.Add(new Vector(row));
			H.Clear();
			H = smoothingCholeskyBase * Matrix.Transpose(smoothingCholeskyBase);
		}

		/// <summary>
		/// Method to decrement dimensionality of this kernel.
		/// </summary>
		public void RemoveDimension()
		{
			this.RemoveDimensionAt(mu.Count - 1);
		}

		/// <summary>
		/// Method to remove a dimension from this kernel at the specified index.
		/// </summary>
		/// <param name="index"> Dimension index to be removed. </param>
		public void RemoveDimensionAt(int index)
		{
			mu.RemoveAt(index);
			smoothingCholeskyBase.RemoveAt(index);
			smoothingCholeskyBase.RemoveColumnAt(index);
			H.Clear();
			H = smoothingCholeskyBase * Matrix.Transpose(smoothingCholeskyBase);
		}

		/// <summary>
		/// Method to specify a row vector of the Lower Triangular base matrix used to
		/// generate this Kernel's Smoothing Matrix.
		/// </summary>
		/// <param name="row"> Lower Triangular matrix row. Will replace the row whose
		/// non-zero element count matches the input Vector length. If the input vector is longer
		/// than any existing row's non-zero elements length, a SizeMismatchException will be thrown. </param>
		public void SetKernelBaseRow(Vector row)
		{
			if (row.Count > smoothingCholeskyBase.Count)
			{
				throw new SizeMismatchException();
			}
			else if (row.Count < 1)
			{
				return;
			}

			for (int i = 0; i < row.Count; i++)
			{
				smoothingCholeskyBase[row.Count - 1][i] = row[i];
			}
			H.Clear();
			H = smoothingCholeskyBase * Matrix.Transpose(smoothingCholeskyBase);
		}

		/// <summary>
		/// Returns probability from this kernal at the input N-dimensional location.
		/// </summary>
		/// <param name="values"> Location at which to retrieve probability. </param>
		/// <returns> Probability level. </returns>
		public virtual float ProbabilityAt(Vector values)
		{
			return 0.0f;
		}
	}
}
