﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mensajes;
using misClases;
using System.Threading;
namespace misClases
{
    public class clsComunicacion:ICom
    {
        int segundos=60;

        private Serializador serializador;

        public  void enviaEjes(Pen lapiz, int x1, int y1)
        {
           

        }
        public void contador()
        {
            segundos--;
        }
        public int Segundos
        {
            get { return segundos; }
        }

        public  bool enviaRta(string rta)
        {
            return false;
        }

        string palabraDesignada;

        public string PalabraDesignada
        {
            get { return palabraDesignada; }
        }

        string[] palabras = new string[] { "perro", "gato", "auto", "casa", "celular", "ratón", "gafas", "silla", "mochila", "jarrón", "cuadro", "sillón", "computadora" };
        MiConBase ConBase;
        public clsComunicacion()
        {
            Random r = new Random();
            int i = r.Next(0, palabras.Count());
            palabraDesignada = palabras[i];
            serializador = new Serializador();
            ConBase = new MiConBase(serializador);
            Thread tEscucha = new Thread(ConBase.read);
            tEscucha.Start();
            serializador.Recibir += Serializador_Recibir;
        }

        private void Serializador_Recibir(MensajeBase msg)
        {
            switch (msg.TipoMensaje) {
                case "MensajeLogin": if (Logear != null)
                    {
                        try
                        {
                            MensajeLogin msgL = (MensajeLogin)msg;
                            Logear(msgL);
                        }
                        catch (InvalidCastException e) { }
                    }
                    break;

            }
        }

        public void enviarDibujado(Pen lapiz, Point p1, Point p2)
        {

        }

        public bool corroborar(string palabraEnviada)
        {
            return palabraDesignada.ToUpper() == palabraEnviada.ToUpper();        
        }

        public void conectar(string nombre)
        {
            MensajeLogin intentarLogin = new MensajeLogin(nombre, "", 0);
            serializador.enviarMensaje(intentarLogin);
        }
   
        public delegate void EventoLogeo(MensajeLogin m);
        public event EventoLogeo Logear;
    }
}
