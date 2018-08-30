using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMath
{
	public class GaussKernel : Kernel
	{
		// Private objects
		//reserved

		// Public objects
		//reserved

		/// <summary>
		/// Default public constructor.
		/// </summary>
		public GaussKernel() : base()
		{
		}

		/// <summary>
		/// Public constructor with input dimensionality.
		/// </summary>
		/// <param name="dimensions"> Kernel dimensionality </param>
		public GaussKernel(int dimensions) : base(dimensions)
		{
		}

		/// <summary>
		/// Returns probability from this kernal at the input N-dimensional location.
		/// </summary>
		/// <param name="values"> Location at which to retrieve probability. </param>
		/// <returns> Probability level. </returns>
		public override float ProbabilityAt(Vector values)
		{
			Matrix valuesT = new Matrix(new List<Vector> { (values - mu) });
			Matrix valuesMatrix = Matrix.Transpose(valuesT);
			Matrix invH;
			float detH;
			if (Matrix.Inverse(H, out invH) && Matrix.Determinant(H, out detH))
			{
				return (float)(1.0d / Math.Sqrt(Math.Pow(2.0d * Math.PI, Dimensions) * detH) * Math.Exp(-0.5d * (valuesT * invH * valuesMatrix)[0, 0]));
			}
			else
			{
				return base.ProbabilityAt(values);
			}
		}
	}
}
