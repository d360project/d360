using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D360
{
    public class TabSensitiveTextBox : TextBox
    {
        protected override bool IsInputKey(Keys keyData)
        {
            if ((keyData == Keys.Tab) || keyData == (Keys.Shift | Keys.Tab)) return true;
            return base.IsInputKey(keyData);
        }
    }
}
