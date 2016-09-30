// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using System;
using System.Drawing;
using System.Linq;
using CivOne.Enums;
using CivOne.Events;
using CivOne.GFX;
using CivOne.Interfaces;
using CivOne.Templates;

namespace CivOne.Screens.Dialogs
{
	internal class DisbandUnit : BaseDialog
	{
		private readonly Bitmap[] _textLines;

		protected override void FirstUpdate()
		{
			int menuWidth = _textLines.Max(b => b.Width) + 5;
			Menu menu = new Menu(Canvas.Image.Palette.Entries, Selection(45, 28, menuWidth, 10))
			{
				X = 103,
				Y = 100,
				Width = menuWidth,
				ActiveColour = 11,
				TextColour = 5,
				FontId = 0
			};
			int i = 0;
			foreach (string choice in new [] { "Unit Disbanded." })
			{
				menu.Items.Add(new Menu.Item(choice, i++));
			}
			menu.Items[0].Selected += Cancel;

			menu.MissClick += Cancel;
			menu.Cancel += Cancel;
			AddMenu(menu);
		}

		private static Bitmap[] TextBitmaps(City city, IUnit unit)
		{
			string[] message = new string[] { $"{city.Name} can't support", $"{unit.Name}." };
			Bitmap[] output = new Bitmap[message.Length];
			for (int i = 0; i < message.Length; i++)
				output[i] = Resources.Instance.GetText(message[i], 0, 15);
			return output;
		}

		public DisbandUnit(City city, IUnit unit) : base(58, 72, TextBitmaps(city, unit).Max(b => b.Width) + 52, 62)
		{
			bool modernGovernment = Human.Advances.Any(a => a.Id == (int)Advance.Invention);
			Bitmap governmentPortrait = Icons.GovernmentPortrait(Human.Government, Advisor.Defense, modernGovernment);
			Color[] palette = Resources.Instance.LoadPIC("SP257").Image.Palette.Entries;
			for (int i = 144; i < 256; i++)
			{
				palette[i] = governmentPortrait.Palette.Entries[i];
			}

			_canvas.SetPalette(palette);

			DialogBox.AddLayer(governmentPortrait, 2, 2);
			DialogBox.DrawText("Defense Minister:", 0, 15, 47, 4);
			DialogBox.FillRectangle(11, 47, 11, 94, 1);

			_textLines = TextBitmaps(city, unit);
			for (int i = 0; i < _textLines.Length; i++)
			{
				DialogBox.AddLayer(_textLines[i], 47, (_textLines[i].Height * i) + 13);
			}
		}
	}
}