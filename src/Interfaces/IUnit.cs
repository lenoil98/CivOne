// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using CivOne.Enums;
using CivOne.GFX;

namespace CivOne.Interfaces
{
	public interface IUnit : ICivilopedia
	{
		IAdvance RequiredTech { get; }
		IWonder RequiredWonder { get; }
		IAdvance ObsoleteTech { get; }
		UnitClass Class { get; }
		byte Price { get; }
		byte Attack { get; }
		byte Defense { get; }
		byte Move { get; }
		int X { get; set; }
		int Y { get; set; }
		byte Owner { get; set; }
		byte Status { get; set; }
		byte MovesLeft { get; }
		void NewTurn();
		void SkipTurn();
		Picture GetUnit(byte colour);
	}
}