using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScintillaNET;

namespace JaneIDE.View
{
    /// <summary>
    /// Interaction logic for CodeBox.xaml
    /// </summary>
    public partial class CodeBox : UserControl
    {
        
        public CodeBox()
        {
            InitializeComponent();
            ScintillaNET.Scintilla codeBox = (ScintillaNET.Scintilla)wfh.Child;
            // Initialize CodeBox by ScintillaNET

            // Integration fix from official instruction
           // ScintillaNET.Scintilla CodeBox = (ScintillaNET.Scintilla)wfh.Child; 

            // Codebox configuration from JaNE.xml
            codeBox.ConfigurationManager.Language = "jane";
            codeBox.ConfigurationManager.CustomLocation = @"JaNE.xml";
            codeBox.ConfigurationManager.Configure();

            // Codebox's margins configuration for string numbers and code folding
            codeBox.Margins[0].Width = 25; 
            codeBox.Margins[2].Width = 20;

            // CodeBox's autocomplete
            codeBox.CharAdded += CodeBox_CharAdded;
            codeBox.AutoComplete.MaxWidth = 20;
            codeBox.AutoComplete.MaxHeight = 10;
            codeBox.AutoComplete.List.Sort();

            // Highlighting current line (light grey)
            codeBox.Caret.HighlightCurrentLine = true;
            codeBox.Caret.CurrentLineBackgroundColor = System.Drawing.Color.FromArgb(245, 245, 245);

            //Code folding from default lexer
            codeBox.Folding.IsEnabled = true;
            codeBox.Folding.UseCompactFolding = true;

            //Background color
            codeBox.Selection.BackColor = System.Drawing.Color.FromArgb(190, 190, 190);

            //Brace Matching (red and bold)
            codeBox.IsBraceMatching = true;

            //Handler for snippets
            codeBox.KeyDown += CodeBox_KeyDown;

            codeBox.Text = "This is \r\n a test.\r\nThis is only a test";
            this.BreakPointMarker(2);
            this.ErrorMarker(1);
            this.CurrentMarker(3);
        }


        void CodeBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ScintillaNET.Scintilla codeBox = (ScintillaNET.Scintilla)wfh.Child;
            if (e.Alt && e.KeyCode == System.Windows.Forms.Keys.Space && e.Control)
            {
               codeBox.Snippets.ShowSnippetList();
            }
        }

        void CodeBox_CharAdded(object sender, ScintillaNET.CharAddedEventArgs e)
        {
            ScintillaNET.Scintilla codeBox = (ScintillaNET.Scintilla)wfh.Child;
            if (Char.IsLetter(e.Ch))
            codeBox.AutoComplete.Show();
        }

        void ErrorMarker(int stringNum)
            {
                ScintillaNET.Scintilla codeBox = (ScintillaNET.Scintilla)wfh.Child;
                Marker marker = codeBox.Markers[0];
                marker.Symbol = MarkerSymbol.Circle;
                marker.BackColor = System.Drawing.Color.Red;          
                codeBox.Lines[stringNum-1].AddMarker(marker);  
            }
        void BreakPointMarker(int stringNum)
            {
                ScintillaNET.Scintilla codeBox = (ScintillaNET.Scintilla)wfh.Child;
                Marker marker = codeBox.Markers[1];
                marker.Symbol = MarkerSymbol.FullRectangle;
                marker.BackColor = System.Drawing.Color.Blue;
                codeBox.Lines[stringNum - 1].AddMarker(marker);
            }
        void CurrentMarker(int stringNum)
        {
            ScintillaNET.Scintilla codeBox = (ScintillaNET.Scintilla)wfh.Child;
            Marker marker = codeBox.Markers[3];
            marker.Symbol = MarkerSymbol.Arrow;
            marker.BackColor = System.Drawing.Color.Green;
            codeBox.Lines[stringNum - 1].AddMarker(marker);
        }
    }
}
