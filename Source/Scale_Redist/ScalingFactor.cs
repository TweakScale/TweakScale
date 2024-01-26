/*
	This file is part of TweakScale /L
		© 2018-2024 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

	THIS FILE is licensed to you under:
		* WTFPL - http://www.wtfpl.net
			* Everyone is permitted to copy and distribute verbatim or modified
 				copies of this license document, and changing it is allowed as long
				as the name is changed.

	THIS FILE is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/
namespace TweakScale
{
	public struct ScalingFactor
	{
		public struct FactorSet
		{
			private readonly float _linear;
			private readonly float _quadratic;
			private readonly float _cubic;

			/// <summary>
			/// Linear scaling, i.e. length.
			/// </summary>
			public float linear => this._linear;

			/// <summary>
			/// Quadratic scaling, i.e. surface area.
			/// </summary>
			public float quadratic => this._quadratic;

			/// <summary>
			/// Cubic scaling, i.e. volume.
			/// </summary>
			public float cubic => this._cubic;

			public FactorSet(float factor)
			{
				this._linear = factor;
				this._quadratic = this._linear * this._linear;
				this._cubic = this._quadratic * this._linear;
			}

			public override string ToString()
				=> string.Format("{0}(linear:{1}; quad:{2}; cubic:{3})", this.GetType().Name, this.linear, this.quadratic, this.cubic);
		}

		private readonly FactorSet _absolute;
		private readonly FactorSet _relative;
		private readonly int _index;

		/// <summary>
		/// Scale factors relative to the part's default scale.
		/// </summary>
		public FactorSet absolute => this._absolute;

		/// <summary>
		/// Scale factors relative the part's current scale.
		/// </summary>
		public FactorSet relative => this._relative;

		/// <summary>
		/// The 0-based index of the currently chosen scale. -1 if freely scalable.
		/// </summary>
		public int index => this._index;

		public ScalingFactor(float abs, float rel, int idx)
		{
			this._absolute = new FactorSet(abs);
			this._relative = new FactorSet(rel);
			this._index = idx;
		}

		public override string ToString()
			=> string.Format("{0}(i:{1}; absolute:{2}; relative:{3})", this.GetType().Name, this._index, this._absolute, this._relative);
	}
}
