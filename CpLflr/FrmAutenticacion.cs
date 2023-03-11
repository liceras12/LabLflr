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
    public partial class FrmAutenticacion : Form
    {
        public FrmAutenticacion()
        {
            InitializeComponent();
        }

        private bool validar()
        {
            bool esValido = true;
            erpUsuario.SetError(txbUsuario, "");
            erpClave.SetError(txbClave, "");

            if (string.IsNullOrEmpty(txbUsuario.Text))
            {
                erpUsuario.SetError(txbUsuario, "El campo usuario es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txbClave.Text))
            {
                erpClave.SetError(txbClave, "El campo contraseña es obligatorio");
                esValido = false;
            }
            return esValido;
        }

        private void btAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var usuario = UsuarioCln.validar(txbUsuario.Text, txbClave.Text);//Util.Encrypt(txbClave.Text));
                if (usuario != null)
                {
                    Util.usuario = usuario;
                    txbClave.Text = string.Empty;
                    txbUsuario.Focus();
                    txbUsuario.SelectAll();
                    this.Visible = false;
                    new FrmSerie(this).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña incorrecto", "::: // Error // :::",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
