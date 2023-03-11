using CadLflr;
using ClnLflr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpLflr
{
    public partial class FrmSerie : Form
    {
        FrmAutenticacion frmAutenticacion;
        bool esNuevo = true;
        bool controlActualizacion = false;
        public FrmSerie(FrmAutenticacion frmAutenticacion)
        {
            InitializeComponent();
            this.frmAutenticacion = frmAutenticacion;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void FrmSerie_Load(object sender, EventArgs e)
        {
            Size = new Size(500, 423);
            listar();
        }

        private bool validar()
        {
            bool esValido = true;
            erpTitulo.SetError(txbTitulo, "");
            erpSinopsis.SetError(txbSinopsis, "");
            erpDirector.SetError(txbDirector, "");
            erpDuracion.SetError(txbDuracion, "");

            if (string.IsNullOrEmpty(txbTitulo.Text))
            {
                erpTitulo.SetError(txbTitulo, "El campo Titulo es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txbSinopsis.Text))
            {
                erpSinopsis.SetError(txbSinopsis, "El campo Sinopsis es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txbDirector.Text))
            {
                erpDirector.SetError(txbDirector, "El campo Director es obligatorio");
                esValido = false;
            }

            if (string.IsNullOrEmpty(txbDuracion.Text))
            {
                erpDuracion.SetError(txbDuracion, "El campo Duracion es obligatorio");
                esValido = false;
            }
            if (int.Parse(txbDuracion.Text) < 0)
            {
                erpDuracion.SetError(txbDuracion, "El campo Duracion no debe ser negativo");
                esValido = false;
            }
            return esValido;
        }

        private void btAnadir_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var serie = new Serie();
                serie.titulo = txbTitulo.Text.Trim();
                serie.sinopsis = txbSinopsis.Text.Trim();
                serie.director = txbDirector.Text.Trim();
                serie.duracion = int.Parse(txbDuracion.Text);
                serie.usuarioRegistro = Util.usuario.usuario1;
                serie.fechaEstreno = DateTime.Now;

                if (esNuevo)
                {
                    serie.registroActivo = true;
                    SerieCln.insertar(serie);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    serie.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    SerieCln.actualizar(serie);
                }

                listar();
                MessageBox.Show("La serie se añadio correctamente", "::: // Mensaje // :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listar()
        {
            var series = SerieCln.listar();
            dgvLista.DataSource = series;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["titulo"].HeaderText = "Titulo";
            dgvLista.Columns["sinopsis"].HeaderText = "Sinopsis";
            dgvLista.Columns["director"].HeaderText = "Director";
            dgvLista.Columns["duracion"].HeaderText = "Duracion";
            dgvLista.Columns["fechaEstreno"].HeaderText = "Fecha de Estreno";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["registroActivo"].Visible = false;

            btEliminar.Enabled = series.Count > 0;
            if (series.Count > 0) dgvLista.Rows[0].Cells["titulo"].Selected = true;
        }

        private void btSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string codigo = dgvLista.Rows[index].Cells["titulo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea eliminar {codigo}?", "::: // Pregunta // :::",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                SerieCln.eliminar(id);
                listar();
                MessageBox.Show("La serie de elimino correctamente", "::: // Mensaje // :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btActualizar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var serie = SerieCln.get(id);

            txbTitulo.Text = serie.titulo;
            txbSinopsis.Text = serie.sinopsis;
            txbDirector.Text = serie.director;
            txbDuracion.Text = serie.duracion.ToString();
            txbTitulo.Focus();

            controlActualizacion = true;

        }

        private void btLimpiar_Click(object sender, EventArgs e)
        {
            txbTitulo.Text = String.Empty;
            txbSinopsis.Text = String.Empty;
            txbDirector.Text = String.Empty;
            txbDuracion.Text = String.Empty;
        }

        private void btConfirmar_Click(object sender, EventArgs e)
        {
            if (controlActualizacion)
            {
                if (validar())
                {
                    var serie = new Serie();
                    serie.titulo = txbTitulo.Text.Trim();
                    serie.sinopsis = txbSinopsis.Text.Trim();
                    serie.director = txbDirector.Text.Trim();
                    serie.duracion = int.Parse(txbDuracion.Text);
                    serie.usuarioRegistro = Util.usuario.usuario1;
                    serie.fechaEstreno = DateTime.Now;

                    if (esNuevo)
                    {
                        serie.registroActivo = true;
                        SerieCln.insertar(serie);
                        int index = dgvLista.CurrentCell.RowIndex;
                        int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                        string codigo = dgvLista.Rows[index].Cells["titulo"].Value.ToString();
                        SerieCln.eliminar(id);
                        listar();

                    }
                    else
                    {
                        int index = dgvLista.CurrentCell.RowIndex;
                        serie.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                        SerieCln.actualizar(serie);
                    }
                }
            }
        }

        private void btMostrar_Click(object sender, EventArgs e)
        {
            listar();
        }
    }
}
