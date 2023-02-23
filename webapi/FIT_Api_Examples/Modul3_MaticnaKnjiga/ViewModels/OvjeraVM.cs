using System;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels
{
    public class OvjeraVM
    {
        public int id { get; set; }
        public DateTime datumOvjere { get; set; }
        public string napomena { get; set; } = string.Empty;
    }
}
