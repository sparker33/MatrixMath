using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMath
{
	public class LogisticKernel : Kernel
	{
		// Private objects
		//reserved

		// Public objects
		//reserved

		/// <summary>
		/// Default public constructor.
		/// </summary>
		public LogisticKernel() : base()
		{
		}

		/// <summary>
		/// Public constructor with input dimensionality.
		/// </summary>
		/// <param name="dimensions"> Kernel dimensionality </param>
		public LogisticKernel(int dimensions) : base(dimensions)
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
				float C = (valuesT * invH * valuesMatrix)[0, 0];
				return (float)(1.0d / (Math.Sqrt(detH) * (2.0d + Math.Exp(C) + Math.Exp(-C))));
			}
			else
			{
				return base.ProbabilityAt(values);
			}
		}
	}
}
