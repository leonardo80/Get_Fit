using System;
using System.Collections.Generic;
using System.Text;

namespace GetFit.Shared.Class
{
    class Member
    {
        public string uid { get; set; }
        public string email { get; set; }
        public string nama { get; set; }
        public int berat { get; set; }
        public int tinggi { get; set; }
        public string tanggal_lahir { get; set; }
        public string gender { get; set; }
        public string type { get; set; }
        public string premium { get; set; }

        public Member(string uid, string email, string nama, int berat, int tinggi, string tanggal_lahir, string gender, string type, string premium)
        {
            this.uid = uid;
            this.email = email;
            this.nama = nama;
            this.berat = berat;
            this.tinggi = tinggi;
            this.tanggal_lahir = tanggal_lahir;
            this.gender = gender;
            this.type = type;
            this.premium = premium;
        }

        public string check()
        {
            return this.uid;
        }

    }
}
