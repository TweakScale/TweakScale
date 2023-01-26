/*
	This file is part of TweakScale /L
		© 2018-2023 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

	TweakScale /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	TweakScale /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with TweakScale /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with TweakScale /L. If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TweakScale.Updater
{
	public class EmitterUpdater : AbstractWithLog, ISecondaryRescalable, IDisposable
	{
		private struct EmitterData
		{
			public readonly float MinSize, MaxSize, Shape1D;
			public readonly Vector2 Shape2D;
			public readonly Vector3 Shape3D, LocalVelocity, Force;

			public EmitterData(KSPParticleEmitter pe)
			{
				MinSize = pe.minSize;
				MaxSize = pe.maxSize;
				LocalVelocity = pe.localVelocity;
				Shape1D = pe.shape1D;
				Shape2D = pe.shape2D;
				Shape3D = pe.shape3D;
				Force = pe.force;
			}
		}

		private readonly TweakScale ts;
		private bool rescale = true;
		private readonly Dictionary<KSPParticleEmitter, EmitterData> scales = new Dictionary<KSPParticleEmitter, EmitterData>();

		public EmitterUpdater(Part part) : base(part)
		{
			this.ts = part.Modules.OfType<TweakScale>().First();
		}

		public void OnRescale(ScalingFactor factor)
		{
			Log.dbg("{0} OnRescale {1} {2}", this.GetType().FullName, this.InstanceID, factor);
			this.rescale = true;
		}

		private static FieldInfo _mmpFxField;
		private static FieldInfo _mpFxField;

		private void UpdateParticleEmitter(KSPParticleEmitter pe)
		{
			if(pe == null)
			{
				return;
			}
			ScalingFactor factor = this.ts.ScalingFactor;

			if(!scales.ContainsKey(pe))
			{
				scales[pe] = new EmitterData(pe);
			}
			EmitterData ed = scales[pe];

			pe.minSize = ed.MinSize * factor.absolute.linear;
			pe.maxSize = ed.MaxSize * factor.absolute.linear;
			pe.shape1D = ed.Shape1D * factor.absolute.linear;
			pe.shape2D = ed.Shape2D * factor.absolute.linear;
			pe.shape3D = ed.Shape3D * factor.absolute.linear;

			pe.force = ed.Force * factor.absolute.linear;

			pe.localVelocity = ed.LocalVelocity * factor.absolute.linear;
		}

		private static void GetFieldInfos()
		{
			if(_mmpFxField == null)
				_mmpFxField = typeof(ModelMultiParticleFX).GetNonPublicFieldByType<List<KSPParticleEmitter>>();
			if(_mpFxField == null)
				_mpFxField = typeof(ModelParticleFX).GetNonPublicFieldByType<KSPParticleEmitter>();
		}

		public void OnUpdate()
		{
			if(!this.rescale)
				return;
			GetFieldInfos();

			EffectBehaviour[] fxn = this.part.GetComponents<EffectBehaviour>();
			this.rescale = fxn.Length != 0;
			foreach(EffectBehaviour fx in fxn)
			{
				if(fx is ModelMultiParticleFX)
				{
					if(!(_mmpFxField.GetValue(fx) is List<KSPParticleEmitter> p))
						continue;
					foreach(KSPParticleEmitter pe in p)
					{
						UpdateParticleEmitter(pe);
					}
					this.rescale = false;
				}
				else if(fx is ModelParticleFX)
				{
					KSPParticleEmitter pe = _mpFxField.GetValue(fx) as KSPParticleEmitter;
					UpdateParticleEmitter(pe);
					this.rescale = false;
				}
			}
		}

		void IDisposable.Dispose()
		{
			this.scales.Clear();
		}
	}
}
