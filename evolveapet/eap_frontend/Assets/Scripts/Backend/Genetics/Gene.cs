using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolveAPet
{
   [Serializable]
	public class Gene
    {
		private EnumTrait _trait;
		private bool _dominance;// 1 = dominant
		private char _symbol;
		private int[] _additional; // any additional information connected to this gene, such as precise colour
		
		// GETTERS AND SETTERS
		public EnumTrait Trait{
			get{
				return _trait;
			}
		}

		public bool Dominance{
			get{
				return _dominance;
			}
		}

		public char Symbol{
			get{
				return _symbol;
			}
		}

		public int Additional{
			get {
				return _additional;
			}
		}

		public Gene(String s){
			// TODO - does Player supply new gene in form of String? In otherwords, do I need to do checks here?
		}

		/// <summary>
		/// Returns true if any additional information are associated with this gene.
		/// </summary>
		/// <returns><c>true</c>, if additional was hased, <c>false</c> otherwise.</returns>
		public bool hasAdditional(){
			if (_additional == null) {
				return false;			
			}
			return true;
		}


		public void Mutate(Gene gene){
			this._trait = gene._trait;
			this._dominance = gene._dominance;
			this._symbol = gene._symbol;
			this._additional = gene._additional;
		}
    }
}
