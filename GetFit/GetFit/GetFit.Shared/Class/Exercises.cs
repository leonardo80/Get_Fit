using System;
using System.Collections.Generic;
using System.Text;

namespace GetFit.Shared.Class
{
    class Exercises
    {
        public string nama { get; set; }
        public string deskripsi { get; set; }
        public string kategori { get; set; }

        public Exercises(string nama, string deskripsi, string kategori)
        {
            this.nama = nama;
            this.deskripsi = deskripsi;
            this.kategori = kategori;
        }
    }
}
