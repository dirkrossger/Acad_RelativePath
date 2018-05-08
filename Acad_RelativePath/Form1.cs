using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acad_RelativePath
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> _filenames;
        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog _dialog;
            _dialog = new System.Windows.Forms.OpenFileDialog();
            _dialog.Multiselect = true;
            _dialog.InitialDirectory = "C:/";
            _dialog.Title = "Select Autocad dwg files";
            _dialog.Filter = "dwg file | *.dwg";
            _dialog.FilterIndex = 2;
            _dialog.RestoreDirectory = true;

            if (_filenames == null)
                _filenames = new List<string>();

            if (_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < _dialog.FileNames.Count(); i++)
                {
                    _filenames.Add(_dialog.FileNames[i]);
                }
            }

            if (_filenames.Count() > 0)
            {
                foreach (string x in _filenames)
                {
                    listBox1.Items.Add(x);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            if(_filenames.Count() > 0)
            {
                foreach(string file in _filenames)
                {
                    DwgFile xrFile = new DwgFile();
                    xrFile.Read(file);
                    string path = xrFile.GetXrefPath(xrFile.DwgDb);
                }
            }
        }
    }
}