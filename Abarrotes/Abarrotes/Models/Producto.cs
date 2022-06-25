using System;
using System.Collections.Generic;

namespace Abarrotes.Models
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal PrecioUnitario { get; set; }
        public decimal Costo { get; set; }
    }
}
