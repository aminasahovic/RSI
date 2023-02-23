using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using System;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.Models
{
    public class MaticnaKnjiga
    {
        public int id { get; set; }
        public Student student { get; set; }
        public DateTime datumUpisa { get; set; }
        public int godinaStudija { get; set; }
        public AkademskaGodina  akademskaGodina { get; set; }
        public float cijena { get; set; }
        public DateTime? datumOvjere { get; set; }

        public bool obnova { get; set; }
        public string? napomena { get; set; } = string.Empty;
        public KorisnickiNalog evidentirao { get; set; }
    }
}
