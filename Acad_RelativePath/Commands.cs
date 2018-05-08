using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

[assembly: CommandClass(typeof(Acad_RelativePath.Commands))]



namespace Acad_RelativePath
{
    class Commands
    {
        [CommandMethod("cdrel")]
        public void changeRealtive()
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
        }
    }
}
