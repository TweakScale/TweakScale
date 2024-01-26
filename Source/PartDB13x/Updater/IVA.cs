/*
	This file is part of TweakScale /L
		© 2018-2024 LisiasT
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

using UnityEngine;

namespace TweakScale.Updater
{
	public class IVA : AbstractWithLog, ISecondaryRescalable, IUpdateable
	{
		private Vector3 originalIvaScale = Vector3.zero;
		private float currentFactor = 1;
		private int state = 0; // 0 == iddle; See OnUpdate for the rest.

		/// <summary>
		/// Cached scale vector, we need this because the game regularly reverts the scaling of the IVA overlay
		/// </summary>
		private Vector3 savedIvaScale = Vector3.zero;

		public IVA(Part part) : base(part)
		{
			if (HighLogic.LoadedSceneIsFlight && null != this.part.vessel)
				GameEvents.onVesselSwitching.Add(this.OnVesselSwitching);
		}
		~IVA()	{ GameEvents.onVesselSwitching.Remove(this.OnVesselSwitching); }

		// scale IVA overlay
		void IRescalable.OnRescale(ScalingFactor factor)
		{
			if (!HighLogic.LoadedSceneIsFlight) return;
			Log.dbg("{0} OnRescale {1} {2}", this.GetType().FullName, this.InstanceID, factor);
			this.currentFactor = factor.absolute.linear;
			this.state = 1;
		}

		void IUpdateable.OnUpdate()
		{
			switch (this.state)
			{
				case 0:	// iddle
						return;
				case 1:	// scaling_wait
						// By some reason beyound me, OnUpdate is being called **before** the transform is built. (sigh)
						if (null == this.part.internalModel) return;
						if (null == this.part.internalModel.transform) return;
						if (null == this.part.internalModel.transform.localScale) return;
						this.state = 2;
						break;
				case 2:	// scaling
						if (Vector3.zero == this.originalIvaScale) this.originalIvaScale = part.internalModel.transform.localScale;
						this.savedIvaScale = part.internalModel.transform.localScale * this.currentFactor;
						this.state = (1f == this.currentFactor || this.originalIvaScale == this.savedIvaScale) ? 0 : 3;
						break;
				case 3: // scaled
						//this.ScaleIVA();
						this.state = 4;
						break;
				case 4: // Keep th scaling
						// flight scene frequently nukes our OnStart resize some time later
						this.RestoreIVAScaling();
						break;
				case 5: // Lost focus
						this.state = 0;
						break;
			}
		}

		protected void RestoreIVAScaling()
		{
			if (this.savedIvaScale == this.part.internalModel.transform.localScale) return;
			this.ScaleIVA();
		}

		protected void ScaleIVA()
		{
			part.internalModel.transform.localScale = this.savedIvaScale;
			part.internalModel.transform.hasChanged = true;
		}

		private void OnVesselSwitching(Vessel from, Vessel to)
		{
			if (null != from && from.GetInstanceID() == this.part.vessel.GetInstanceID()) this.state = 5;
			else if (null != to && to.GetInstanceID() == this.part.vessel.GetInstanceID()) this.state = 1;
		}
	}
}
