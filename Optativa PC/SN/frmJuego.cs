﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace SN
{
   
    public partial class frmJuego : Form
    {
        private bool btnDown;
        Pen lapiz;
        Graphics grafico;
        clsUsuario usuario;
        frmPrueba prueba;
        clsComunicacion comunicacion;
        

        public frmJuego(clsUsuario us,clsComunicacion c)
        {
            InitializeComponent();
            usuario = us;
            comunicacion = c;
            lapiz = new Pen(Color.Black,(int) nudWidth.Value);
            grafico = pnlDibujo.CreateGraphics();
            lblNick.Text = usuario.User;
            lblPuntos.Text =Convert.ToString(usuario.Puntos);
        
        }

        private void frmJuego_Load(object sender, EventArgs e)
        {
            lblPalabra.Text = comunicacion.PalabraDesignada;
            prueba = new frmPrueba(comunicacion,this);
            prueba.Show();
        }

        private void pnlDibujo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnDown = true;
                
            }
        }

        private void pnlDibujo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnDown = false;
            }
        }

        private void pnlDibujo_MouseMove(object sender, MouseEventArgs e)
        {
            if (btnDown)
            {
                // mover el pictureBox con el raton               
                grafico.DrawLine(lapiz, e.X, e.Y, e.X+1 , e.Y+1);
                grafico.DrawLine(lapiz, e.X, e.Y, e.X -1, e.Y -1);
                comunicacion.enviaEjes(lapiz,e.X, e.Y,prueba);
                
            }
        }
           
       

        private void pnlNegro_Click(object sender, EventArgs e)
        {
            Panel color = (Panel)sender;
            lapiz.Color = color.BackColor;
        }

        public bool rtas(string rta)
        {
            if (comunicacion.corroborar(rta))
                return true;
            else
            {
                lbPalabrasIncorrectas.Items.Add(rta);
                return false;
            }
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            lapiz.Width = (int)nudWidth.Value;
        }
    }
}
