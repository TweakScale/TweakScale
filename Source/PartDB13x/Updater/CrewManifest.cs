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

namespace TweakScale.Updater
{
	public class CrewManifest : AbstractWithLog, IRescalable
	{
		public CrewManifest(Part part) : base(part)
		{
			this.SetupCrewManifest();
		}

		void IRescalable.OnRescale(ScalingFactor factor)
		{
			Log.dbg("{0} OnRescale {1} {2}", this.GetType().FullName, this.InstanceID, factor);
			// The this.part.CrewCapacity was already scaled by GenericScaller.
			// We need to tackle down some details here, beyound the scope of the GenericUpdater!
			this.UpdateCrewManifest();
		}

		private void SetupCrewManifest()
		{
			VesselCrewManifest vcm = ShipConstruction.ShipManifest;
			if (vcm == null) { return; }
			PartCrewManifest pcm = vcm.GetPartCrewManifest(part.craftID);
			if (pcm == null) { return; }

			if (pcm.partCrew.Length != this.part.CrewCapacity)
				this.SetCrewManifestSize(pcm, this.part.CrewCapacity);
		}

		//only run the following block in the editor; it updates the crew-assignment GUI
		private void UpdateCrewManifest()
		{
			Log.dbg("UpdateCrewManifest {0}", this.part.GetInstanceID());

#if !CREW_SCALE_UP
			// Small safety guard.
			if (this.part.CrewCapacity > this.prefab.CrewCapacity) this.part.CrewCapacity = this.prefab.CrewCapacity;
#endif

			try // Preventing this thing triggering an eternal loop on the event handling!
			{
				VesselCrewManifest vcm = ShipConstruction.ShipManifest;
				if (vcm == null) return;
				PartCrewManifest pcm = vcm.GetPartCrewManifest(this.part.craftID);
				if (pcm == null) return;

				int len = pcm.partCrew.Length;
				//int newLen = Math.Min(part.CrewCapacity, _prefabPart.CrewCapacity);
				int newLen = this.part.CrewCapacity;
				if (len == newLen) return;

				Log.dbg("UpdateCrewManifest current {0}; new {1}; prefab {2}", len, newLen, this.prefab.CrewCapacity);

#if CREW_SCALE_UP
				this.part.CrewCapacity  = newLen;
#else
				this.part.CrewCapacity = Math.Min(newLen, this.prefab.CrewCapacity);
#endif
				if (EditorLogic.fetch.editorScreen == EditorScreen.Crew)
					EditorLogic.fetch.SelectPanelParts();

				this.SetCrewManifestSize(pcm, newLen);

				ShipConstruction.ShipManifest.SetPartManifest(this.part.craftID, pcm);
			}
			catch(Exception e)
			{
				Log.error(e, this);
			}
		}

		private void SetCrewManifestSize(PartCrewManifest pcm, int crewCapacity)
		{
			string[] newpartCrew = new string[crewCapacity];
			{
				for(int i = 0; i < newpartCrew.Length; ++i)
					newpartCrew[i] = string.Empty;

				int SIZE = Math.Min(pcm.partCrew.Length, newpartCrew.Length);
				for(int i = 0; i < SIZE; ++i)
					newpartCrew[i] = pcm.partCrew[i];

				for(int i = SIZE; i < pcm.partCrew.Length; ++i)
					pcm.RemoveCrewFromSeat(i);
			}
			pcm.partCrew = newpartCrew;
		}
	}
}
