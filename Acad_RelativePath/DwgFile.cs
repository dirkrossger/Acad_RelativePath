using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;


namespace Acad_RelativePath
{
    class DwgFile
    {

        private Database _db;

        public Database DwgDb
        {
            get
            {
                return _db;
            }

            set
            {
                _db = value;
            }
        }

        public void Read(string filename)
        {
            Database workingDB = HostApplicationServices.WorkingDatabase;
            Database _db = new Database(false, true);

            try
            {
                _db.ReadDwgFile(filename, System.IO.FileShare.ReadWrite, false, "");
                _db.CloseInput(true);
                HostApplicationServices.WorkingDatabase = _db;
                this.DwgDb = _db;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("\nUnable to open .dwg file : " + ex.StackTrace);
            }
        }

        public string GetXrefPath(Database db)
        {
            string result = "";
            bool saveRequired = false;

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                XrefGraph xg = db.GetHostDwgXrefGraph(true);

                int xrefcount = xg.NumNodes;
                for (int j = 0; j < xrefcount; j++)
                {
                    XrefGraphNode xrNode = xg.GetXrefNode(j);
                    string nodeName = xrNode.Name;

                    if (xrNode.XrefStatus == XrefStatus.Resolved)
                    {
                        try
                        {
                            BlockTableRecord bl = tr.GetObject(xrNode.BlockTableRecordId, OpenMode.ForRead) as BlockTableRecord;
                            if (bl.IsFromExternalReference == true)

                                ed.WriteMessage("\nXref path name: " + bl.PathName);

                            saveRequired = true;
                        }
                        catch(System.Exception ex) { }
                   
                        return nodeName;

                        break;
                    }
                }
                tr.Commit();
            }

            //if (saveRequired)
            //    db.SaveAs(db.Filename, DwgVersion.Current);

            return result;
        }

        // Recursively prints out information about the XRef's hierarchy

    }
}

