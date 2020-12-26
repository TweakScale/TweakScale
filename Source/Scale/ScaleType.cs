/*
		This file is part of TweakScale /L
		© 2018-2020 LisiasT
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
		along with TweakScale /L If not, see <https://www.gnu.org/licenses/>.
*/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TweakScale
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class TechUpdater : MonoBehaviour
    {
        public void Start()
        {
            Tech.Reload();
        }
    }

    public static class Tech
    {
        private static HashSet<string> _unlockedTechs = new HashSet<string>();

        public static void Reload()
        {
            if (HighLogic.CurrentGame == null)
                return;
            if (HighLogic.CurrentGame.Mode != Game.Modes.CAREER && HighLogic.CurrentGame.Mode != Game.Modes.SCIENCE_SANDBOX)
                return;

			string persistentfile = KSPUtil.ApplicationRootPath + "saves/" + HighLogic.SaveFolder + "/persistent.sfs";
			ConfigNode config = ConfigNode.Load(persistentfile);
			ConfigNode gameconf = config.GetNode("GAME");
			ConfigNode[] scenarios = gameconf.GetNodes("SCENARIO");
			ConfigNode thisScenario = scenarios.FirstOrDefault(a => a.GetValue("name") == "ResearchAndDevelopment");
            if (thisScenario == null)
                return;
			ConfigNode[] techs = thisScenario.GetNodes("Tech");

            _unlockedTechs = techs.Select(a => a.GetValue("id")).ToHashSet();
            _unlockedTechs.Add("");
        }

        public static bool IsUnlocked(string techId)
        {
            if (HighLogic.CurrentGame == null)
                return true;
            if (HighLogic.CurrentGame.Mode != Game.Modes.CAREER && HighLogic.CurrentGame.Mode != Game.Modes.SCIENCE_SANDBOX)
                return true;
            return techId == "" || _unlockedTechs.Contains(techId);
        }
    }

    /// <summary>
    /// Configuration values for TweakScale.
    /// </summary>
    public class ScaleType
    {
        /// <summary>
        /// Fetches the scale ScaleType with the specified name.
        /// </summary>
        /// <param name="name">The name of the ScaleType to fetch.</param>
        /// <returns>The specified ScaleType or the default ScaleType if none exists by that name.</returns>
        /*private static ScaleType GetScaleConfig(string name)
        {
            var config = GameDatabase.Instance.GetConfigs("SCALETYPE").FirstOrDefault(a => a.name == name);
            if (config == null)
            {
                Log.warn("No SCALETYPE with name {0}", name);
            }
            return config; // == null ? DefaultScaleType : new ScaleType(config.config);
        }*/

        public class NodeInfo
        {
            public readonly string Family;
            public readonly float Scale;

            private NodeInfo()
            {
            }

            public NodeInfo(string family, float scale) : this()
            {

                Family = family;
                Scale = scale;
                if (Mathf.Abs(Scale) < 0.01)
                {
                    Log.warn("Invalid scale for family {0}: {1}", family, scale);
                }
            }

            public NodeInfo(string s) : this()
            {
				string[] parts = s.Split(':');
                if (parts.Length == 1)
                {
                    if (!float.TryParse(parts[0], out Scale))
                        Log.warn("Invalid attachment node string \"{0}\"", s);
                    return;
                }
                if (parts.Length == 0)
                {
                    return;
                }
                if (!float.TryParse(parts[1], out Scale))
                {
                    Log.warn("Invalid attachment node string \"{0}\"", s);
                    return;
                }
                Family = parts[0];
                if (Mathf.Abs(Scale) < 0.01)
                {
                    Log.warn("Invalid scale for family {0}: {1}", Family, Scale);
                }
            }

            public override string ToString()
            {
                return string.Format("({0}, {1})", Family, Scale);
            }
        }

        private static List<ScaleType> _scaleTypes;
        public static List<ScaleType> AllScaleTypes => _scaleTypes = _scaleTypes ??
                        (GameDatabase.Instance.GetConfigs("SCALETYPE")
                            .Select(a => new ScaleType(a.config))
                            .ToList<ScaleType>());

        //private static readonly ScaleType DefaultScaleType = new ScaleType();

        private float[] _scaleFactors = {};
        private readonly string[] _scaleNames = {};
        public readonly Dictionary<string, ScaleExponents> Exponents = new Dictionary<string, ScaleExponents>();

        public readonly bool IsFreeScale = true;
        public readonly string[] TechRequired = {};
        //public readonly Dictionary<string, NodeInfo> AttachNodes = new Dictionary<string, NodeInfo>();
        //public readonly float MinValue = 0f;
        //public readonly float MaxValue = 0f;
        public float DefaultScale = -1;
        public float[] IncrementSlide = {};
        public string Suffix = null;
        public readonly string Name = null;
        public readonly string Family;
        /*public float BaseScale {
            get { return AttachNodes["base"].Scale; }
        }*/

        public float[] AllScaleFactors => _scaleFactors;

        public float[] ScaleFactors
        {
            get
            {
                if (TechRequired.Length == 0)
                    return _scaleFactors;
				float[] result = _scaleFactors.ZipFilter(TechRequired, Tech.IsUnlocked).ToArray();
                return result;
            }
        }

        public string[] ScaleNames
        {
            get
            {
                if (TechRequired.Length == 0)
                    return _scaleNames;

				string[] result = _scaleNames.ZipFilter(TechRequired, Tech.IsUnlocked).ToArray();
                return result;
            }
        }

        public int[] ScaleNodes { get; private set; }

        private ScaleType()
        {
            ScaleNodes = new int[] {};
            //AttachNodes = new Dictionary<string, NodeInfo>();
            //AttachNodes["base"] = new NodeInfo("", 1);
        }

        // config is a part config
        public ScaleType(ConfigNode partConfig)
        {
            ConfigNode scaleConfig = null;
            if (!(partConfig is null))
            {
                Name = Tools.ConfigValue(partConfig, "type", Name);
                ScaleExponents.LoadGlobalExponents();

                if (Name != null)
                {
					UrlDir.UrlConfig tmp = GameDatabase.Instance.GetConfigs("SCALETYPE").FirstOrDefault(a => a.name == Name);
                    if (tmp != null) scaleConfig = tmp.config;
                    if (scaleConfig != null)
                    {
                        // search scaletype for values
                        IsFreeScale = Tools.ConfigValue(scaleConfig, "freeScale", IsFreeScale);
                        DefaultScale = Tools.ConfigValue(scaleConfig, "defaultScale", DefaultScale);
                        Suffix = Tools.ConfigValue(scaleConfig, "suffix", Suffix);
                        _scaleFactors = Tools.ConfigValue(scaleConfig, "scaleFactors", _scaleFactors);
                        ScaleNodes = Tools.ConfigValue(scaleConfig, "scaleNodes", ScaleNodes);         // currently not used!
                        _scaleNames = Tools.ConfigValue(scaleConfig, "scaleNames", _scaleNames).Select(a => a.Trim()).ToArray();
                        TechRequired = Tools.ConfigValue(scaleConfig, "techRequired", TechRequired).Select(a => a.Trim()).ToArray();
                        Family = Tools.ConfigValue(scaleConfig, "family", "default");
                        //AttachNodes   = GetNodeFactors(scaleConfig.GetNode("ATTACHNODES"), AttachNodes);  // currently not used!
                        IncrementSlide = Tools.ConfigValue(scaleConfig, "incrementSlide", IncrementSlide); // deprecated!

                        Exponents = ScaleExponents.CreateExponentsForModule(scaleConfig, Exponents);
                        Log.dbg("scaleConfig:{0}", scaleConfig);
                        Log.dbg("scaleConfig:{0}", this);
                        Log.dbg("{0}", Exponents);
                    }
                }
                else
                    Name = "";

                // search part config for overrides
                IsFreeScale   = Tools.ConfigValue(partConfig, "freeScale",    IsFreeScale);
                DefaultScale  = Tools.ConfigValue(partConfig, "defaultScale", DefaultScale);
                Suffix        = Tools.ConfigValue(partConfig, "suffix",       Suffix);
                _scaleFactors = Tools.ConfigValue(partConfig, "scaleFactors", _scaleFactors);
                ScaleNodes    = Tools.ConfigValue(partConfig, "scaleNodes",   ScaleNodes);
                _scaleNames   = Tools.ConfigValue(partConfig, "scaleNames",   _scaleNames).Select(a => a.Trim()).ToArray();
                TechRequired  = Tools.ConfigValue(partConfig, "techRequired", TechRequired).Select(a=>a.Trim()).ToArray();
                Family        = Tools.ConfigValue(partConfig, "family",       "default");
                //AttachNodes   = GetNodeFactors(partConfig.GetNode("ATTACHNODES"), AttachNodes);
                IncrementSlide= Tools.ConfigValue(partConfig, "incrementSlide", IncrementSlide);

                Exponents = ScaleExponents.CreateExponentsForModule(partConfig, Exponents);
                ScaleExponents.treatMassAndCost(Exponents);

#if DEBUG
				{
					string log = "finished ScaleExponents: ";
					foreach (KeyValuePair<string, ScaleExponents> e in Exponents) { log += e.ToString() + ", \n"; }
					Log.dbg(log);
				}
#endif

                Log.dbg("partConfig:{0}", partConfig);
                Log.dbg("partConfig:{0}", this);
                Log.dbg("{0}", Exponents);
            }

            if (IsFreeScale && (_scaleFactors.Length > 1))
            {
                bool error = false;
                for (int i=0; i<_scaleFactors.Length-1; i++)
                    if (_scaleFactors[i + 1] <= _scaleFactors[i])
                        error = true;

                if (error)
                {
                    Log.warn("scaleFactors must be in ascending order on {0}! \n{1}", Name, this);
                    _scaleFactors = new float[0];
                }
            }

            // fill in missing values
            if ((DefaultScale <= 0) || (_scaleFactors.Length == 0))
                RepairScaletype(scaleConfig, partConfig);

            if (!IsFreeScale && (_scaleFactors.Length != _scaleNames.Length))
            {
                if(_scaleNames.Length != 0)
                    Log.warn("Wrong number of scaleFactors compared to scaleNames in scaleType \"{0}\": {1} scaleFactors vs {2} scaleNames\n{3}", Name, _scaleFactors.Length, _scaleNames.Length, this);

                _scaleNames = new string[_scaleFactors.Length];
                for (int i=0; i<_scaleFactors.Length; i++)
                    _scaleNames[i] = _scaleFactors[i].ToString();
            }

            if (!IsFreeScale)
            {
                DefaultScale = Tools.Closest(DefaultScale, AllScaleFactors);
            }
            DefaultScale = Tools.Clamp(DefaultScale, _scaleFactors.Min(), _scaleFactors.Max());

            if (IncrementSlide.Length == 0)
            {
                IncrementSlide = new float[_scaleFactors.Length-1];
                for (int i = 0; i<_scaleFactors.Length-1; i++)
                    IncrementSlide[i] = (_scaleFactors[i+1]-_scaleFactors[i])/50f;
            }

            if (IsFreeScale)
            {
				// workaround for stock bug in tweakable UI_ScaleEdit:
				// add a tiny dummy interval to the range because the highest one is bugged
				float[] tmp = _scaleFactors;
                _scaleFactors = new float[tmp.Length + 1];
                for (int i = 0; i < tmp.Length; i++)
                    _scaleFactors[i] = tmp[i];

                _scaleFactors[tmp.Length] = _scaleFactors[tmp.Length - 1] + 0.1f * IncrementSlide.Max();
            }

			int numTechs = TechRequired.Length;
			if ((numTechs > 0) && (numTechs != _scaleFactors.Length))
			{
				Log.dbg("Wrong number of techRequired compared to scaleFactors in scaleType \"{0}\": {1} scaleFactors vs {2} techRequired", Name, _scaleFactors.Length, TechRequired.Length);
				if (numTechs < _scaleFactors.Length)
				{
					string lastTech = TechRequired[TechRequired.Length - 1];
                    TechRequired = TechRequired.Concat(lastTech.Repeat()).Take(_scaleFactors.Length).ToArray();
                }
            }

            Log.dbg("finished config:{0}", this);
            Log.dbg("{0}", Exponents);
        }

        private void RepairScaletype(ConfigNode scaleConfig, ConfigNode partConfig)
        {
            if ((DefaultScale <= 0) && (_scaleFactors.Length == 0))
            {
                DefaultScale = 100;
                if (Suffix == null)
                    Suffix = "%";
                if (IncrementSlide.Length == 0)
                    IncrementSlide = new float[] {1f, 1f, 1f, 2f, 5f}; 
            }
            if ((DefaultScale > 0) && (_scaleFactors.Length == 0))
            {
                _scaleFactors = new float[] { DefaultScale/10f, DefaultScale/4f, DefaultScale/2f, DefaultScale, DefaultScale*2f, DefaultScale*4f };
            }
            else if ((DefaultScale <= 0) && (_scaleFactors.Length > 0))
            {
                DefaultScale = _scaleFactors[0];
            }
            else
            {
                // Legacy support: min/maxValue
                float minScale = -1;
                float maxScale = -1;
                if (scaleConfig != null)
                {
                    minScale = Tools.ConfigValue(scaleConfig, "minScale", minScale);    // deprecated!
                    maxScale = Tools.ConfigValue(scaleConfig, "maxScale", maxScale);    // deprecated!
                }
                if (partConfig != null)
                {
                    minScale = Tools.ConfigValue(partConfig, "minScale", minScale);
                    maxScale = Tools.ConfigValue(partConfig, "maxScale", maxScale);
                }
                if ((minScale > 0) && (maxScale > 0))
                {
                    if (minScale > 0 && maxScale > 0)
                    {
                        _scaleFactors = DefaultScale > minScale && DefaultScale < maxScale
                            ? (new float[] { minScale, DefaultScale, maxScale })
                            : (new float[] { minScale, maxScale });
                    }
                }
            }
        }

        private Dictionary<string, NodeInfo> GetNodeFactors(ConfigNode node, Dictionary<string, NodeInfo> source)
        {
			Dictionary<string, NodeInfo> result = source.Clone();

            if (node != null)
            {
                foreach (ConfigNode.Value v in node.values.Cast<ConfigNode.Value>())
                {
                    result[v.name] = new NodeInfo(v.value);
                }
            }

            if (!result.ContainsKey("base"))
            {
                result["base"] = new NodeInfo(Family, 1.0f);
            }

            return result;
        }

        public override string ToString()
        {
            string result = "ScaleType {";
            result += "\n name = " + Name??"<NULL!>" ;
            result += "\n family = " + Family??"<NULL!>" ;
            result += "\n suffix = " + Suffix??"<NULL!>" ;
            result += "\n isFreeScale = " + IsFreeScale;
            result += "\n " + Exponents.Keys.Count + " Exponents = ";
            foreach (KeyValuePair<string, ScaleExponents> s in Exponents)
                result += "\n\t" +string.Format("[ {0}:{1} ]", s.Key, s.Value);
            result += "\n " + _scaleFactors.Length  + " scaleFactors = ";
            foreach (float s in _scaleFactors)
                result += s + "  ";
            result += "\n " + _scaleNames.Length  + " scaleNames = ";
            foreach (string s in _scaleNames)
                result += s + "  ";
            result += "\n " + IncrementSlide.Length + " incrementSlide = ";
            foreach (float s in IncrementSlide)
                result += s + "  ";
            result += "\n " + TechRequired.Length + " TechRequired = ";
            foreach (string s in TechRequired)
                result += s + "  ";
            result += "\n defaultScale = " + DefaultScale;
            result += "\n scaleNodes = " + ScaleNodes;
            return result + "\n}";
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((ScaleType)obj);
        }

        public static bool operator ==(ScaleType a, ScaleType b)
        {
            if (a is null) return b is null;
            if (b is null) return false;
            return a.Name == b.Name;
        }

        public static bool operator !=(ScaleType a, ScaleType b)
        {
            return !(a == b);
        }

        protected bool Equals(ScaleType other)
        {
            return string.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
