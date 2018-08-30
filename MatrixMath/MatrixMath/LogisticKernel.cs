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
		/// Public constructor with input Smooting value.
		/// </summary>
		public LogisticKernel(float smoothing, float mean) : base(smoothing, mean)
		{
		}

		/// <summary>
		/// Returns probability from this kernal at the input value.
		/// </summary>
		/// <param name="value"> Location at which to retrieve probability. </param>
		/// <returns> Probability level. </returns>
		public override float ProbabilityAt(float value)
		{
			return (float)(1.0d / (h * (2.0d + Math.Exp((value - mu) / h) + Math.Exp(-(value - mu) / h))));
		}
	}
}
