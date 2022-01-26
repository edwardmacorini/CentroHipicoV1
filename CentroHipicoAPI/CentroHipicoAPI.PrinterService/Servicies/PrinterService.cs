using CentroHipicoAPI.Nucleo.DTOs;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;

namespace CentroHipicoAPI.PrinterService.Servicies
{
    public class PrinterService : IPrinterService
    {
        private DTOTicket _ticket;

        public PrinterService()
        {
            _ticket = new DTOTicket();
        }

        public bool PrintTicket(DTOTicket ticket)
        {
            try
            {
                _ticket = ticket;

                #region Print
                PrintDocument pd = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();
                pd.PrinterSettings = ps;
                pd.PrintPage += Print;
                pd.Print();
                #endregion

                GC.Collect();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Print(object sender, PrintPageEventArgs e)
        {
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font bodyFont1 = new Font("Arial", 8);
            Font bodyFont2 = new Font("Arial", 10);
            Font bodyFont3 = new Font("Arial", 10, FontStyle.Bold);
            Font footerFont = new Font("Arial", 12);

            int y = 0;
            int ancho = 300;
            string str = ""; for (int i = 0; i < 77; i++) str += "-";

            string specifier = "C";
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            if (e != null && e.Graphics != null)
            {
                e.Graphics.DrawString(_ticket.RazonSocial.ToUpper(), headerFont, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString(_ticket.FechaDocumento.ToShortDateString(), bodyFont2, Brushes.Black, new RectangleF(210, y += 25, ancho - 210, 20));
                e.Graphics.DrawString(str, bodyFont1, Brushes.Black, new RectangleF(0, y += 20, ancho, 20));
                e.Graphics.DrawString("Cliente: " + _ticket.NombreCliente.ToUpper(), bodyFont1, Brushes.Black, new RectangleF(0, y += 15, ancho - 150, 20));
                e.Graphics.DrawString("Carrera Nro.", bodyFont1, Brushes.Black, new RectangleF(210, y, ancho - 210, 30));
                e.Graphics.DrawString(_ticket.NumeroCarrera.ToString(), bodyFont2, Brushes.Black, new RectangleF(230, y += 15, ancho - 230, 30));
                e.Graphics.DrawString("Nro\tEjemplar\t\tMonto $", bodyFont3, Brushes.Black, new RectangleF(0, y += 25, ancho, 30));
                _ticket.Detalles.ForEach(x =>
                {
                    e.Graphics.DrawString(x.NumeroEjemplar.ToString(), bodyFont1, Brushes.Black, new RectangleF(5, y += 25, ancho - 5, 30));
                    e.Graphics.DrawString(x.NombreEjemplar.ToUpper().ToString(), bodyFont1, Brushes.Black, new RectangleF(55, y, ancho - 55, 30));
                    e.Graphics.DrawString(x.Monto.ToString(specifier, culture), bodyFont1, Brushes.Black, new RectangleF(230, y, ancho - 230, 30));
                });
                str = ""; for (int i = 0; i < 10; i++) str += "-";
                e.Graphics.DrawString(str + "¡SUERTE!" + str, footerFont, Brushes.Black, new RectangleF(45, y += 50, ancho - 45, 30));
            }
        }
    }
}
