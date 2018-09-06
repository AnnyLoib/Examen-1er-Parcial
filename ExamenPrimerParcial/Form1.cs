using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ExamenPrimerParcial
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> _Imagen;

        public Form1()
        {
            InitializeComponent();
        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog img = new OpenFileDialog();
                if (img.ShowDialog() == DialogResult.OK)
                {
                    _Imagen = new Image<Bgr, byte>(img.FileName);
                    imageBox1.Image = _Imagen;
                    imageBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro que quieres salir?", "System Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> _ICanny = new Image<Gray, byte>(_Imagen.Width, _Imagen.Height, new Gray(0));
            _ICanny = _Imagen.Canny(100, 50);
            imageBox1.Image = _ICanny;
        }

        private void histogramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DenseHistogram histRed = new DenseHistogram(256, new RangeF(0, 255));
            histRed.Calculate(new Image<Gray, byte>[] { _Imagen[2] }, false, null);
            Mat r = new Mat();
            histRed.CopyTo(r);
            histogramBox1.AddHistogram("Histograma Color Rojo", Color.Red, r, 256, new float[] { 0, 255 });
            histogramBox1.Refresh();
        }
    }
}
