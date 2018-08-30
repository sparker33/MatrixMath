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
		protected float h = 0.0f;
		protected float mu = 0.0f;

		// Public objects
		public float Smoothing { get => h; set => h = value; }
		public float Mean { get => mu; set => mu = value; }

		/// <summary>
		/// Default public constructor.
		/// </summary>
		public Kernel()
		{
		}

		/// <summary>
		/// Public constructor with input Smooting value.
		/// </summary>
		public Kernel(float smoothing, float mean)
		{
			Smoothing = smoothing;
			Mean = mean;
		}

		/// <summary>
		/// Returns probability from this kernal at the input value.
		/// </summary>
		/// <param name="value"> Location at which to retrieve probability. </param>
		/// <returns> Probability level. </returns>
		public virtual float ProbabilityAt(float value)
		{
			return 0.0f;
		}
	}
}
