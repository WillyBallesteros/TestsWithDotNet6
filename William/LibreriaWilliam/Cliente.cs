using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWilliam
{

    public interface ICliente
    {
        string ClientNombre { get; set; }
        int Descuento { get; set; }
        int OrderTotal { get; set; }

        bool IsPremium { get; set; }

        string CrearNombreCompleto(string nombre, string apellido);
        TipoCliente GetClienteDetalle();
    }
    public class Cliente : ICliente
    {
        public string ClientNombre { get; set; }
        public int Descuento { get; set; }
        public int OrderTotal { get; set; } 

        public bool IsPremium { get; set; }

        public Cliente()
        {
            IsPremium = false;
            Descuento = 10;
        }

        public string CrearNombreCompleto(string nombre, string apellido)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre esta en blanco");
            }
            Descuento = 30;
            ClientNombre = $"{nombre} {apellido}";
            return ClientNombre;
        }

        public TipoCliente GetClienteDetalle()
        {
            if(OrderTotal < 500)
            {
                return new ClienteBasico();
            }
            return new ClientePremium();
        }
    }

    public class TipoCliente { }
        public class ClienteBasico : TipoCliente { }
        public class ClientePremium : TipoCliente { }
    
}
