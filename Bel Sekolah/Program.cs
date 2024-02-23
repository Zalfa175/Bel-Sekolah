using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using Component;
using Database;
using System.Threading;

namespace Bel_Sekolah
{
    class Program
    {
        public static AccessDatabaseHelper DB = new AccessDatabaseHelper("./Jadwal.accdb");

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Warna();

            Kotak Kepala = new Kotak();
            Kepala.X = 0;
            Kepala.Y = 0;
            Kepala.Width = 119;
            Kepala.Height = 5;
            Kepala.SetBackColor(ConsoleColor.DarkBlue);
            Kepala.Tampil();
            

            Kotak Kaki = new Kotak();
            Kaki.X = 0;
            Kaki.Y = 25;
            Kaki.Width = 119;
            Kaki.Height = 3;
            Kaki.SetBackColor(ConsoleColor.DarkMagenta);
            Kaki.Tampil();

            Kotak Kiri = new Kotak();
            Kiri.X = 0;
            Kiri.Y = 6;
            Kiri.Width = 30;
            Kiri.Height = 18;
            Kiri.SetBackColor(ConsoleColor.Yellow);
            Kiri.Tampil();

            Kotak Kanan = new Kotak();
            Kanan.X = 31;
            Kanan.Y = 6;
            Kanan.Width = 88;
            Kanan.Height = 18;
            Kanan.SetBackColor(ConsoleColor.Gray);
            Kanan.Tampil();

            Tulisan Nama_App = new Tulisan();
            Nama_App.SetForeColor(ConsoleColor.White).SetBackColor(ConsoleColor.DarkBlue);
            Nama_App.Text = "BEL SEKOLAH";
            Nama_App.X = 0;
            Nama_App.Y = 1;
            Nama_App.Length = 119;
            Nama_App.TampilTengah();

            Tulisan Sekolah = new Tulisan();
            Sekolah.SetForeColor(ConsoleColor.White).SetBackColor(ConsoleColor.DarkBlue);
            Sekolah.Text = "WEARNES EDUCATION CENTER MADIUN";
            Sekolah.X = 0;
            Sekolah.Y = 2;
            Sekolah.Length = 119;
            Sekolah.TampilTengah();

            Tulisan Alamat = new Tulisan();
            Alamat.SetForeColor(ConsoleColor.White).SetBackColor(ConsoleColor.DarkBlue);
            Alamat.Text = "Jl.THAMRIN NO.35A KOTA MADIUN";
            Alamat.X = 0;
            Alamat.Y = 3;
            Alamat.Length = 119;
            Alamat.TampilTengah();

            Tulisan Nama = new Tulisan();
            Nama.SetBackColor(ConsoleColor.DarkMagenta).SetText("NABILA ZALFA ANANDITA").SetXY(0, 26).SetLength(119).TampilTengah();

            new Tulisan().SetBackColor(ConsoleColor.DarkMagenta).SetText("IK 2").SetXY(0, 27).SetLength(119).TampilTengah();

            Menu menu = new Menu("JALANKAN", "LIHAT JADWAL", "TAMBAH JADWAL", "EDIT JADWAL", "HAPUS JADWAL", "KELUAR");
            menu.SetXY(5, 10);
            menu.ForeColor = ConsoleColor.Black;
            menu.SetBackColor(ConsoleColor.Yellow);
            menu.SelectedBackColor = ConsoleColor.DarkYellow;
            menu.SelectedForeColor = ConsoleColor.White;
            menu.Tampil();

            bool IsProgramJalan = true;

            while (IsProgramJalan)
            {
                ConsoleKeyInfo Tombol = Console.ReadKey(true);

                if (Tombol.Key == ConsoleKey.DownArrow)
                {
                    menu.Next();
                    menu.Tampil();
                }
                else if (Tombol.Key == ConsoleKey.UpArrow)
                {
                    menu.Prev();
                    menu.Tampil();
                }
                else if (Tombol.Key == ConsoleKey.Enter)
                {
                    int MenuTerpilih = menu.SelectedIndex;

                    if(MenuTerpilih == 0)
                    {
                        Jalankan();
                    }
                    else if (MenuTerpilih == 1)
                    {
                        LihatJadwal();
                    }
                    else if (MenuTerpilih == 2)
                    {
                        TambahJadwal();
                    }
                    else if (MenuTerpilih == 3)
                    {
                        EditJadwal();
                    }
                    else if (MenuTerpilih == 4)
                    {
                        HapusJadwal();
                    }
                    else if (MenuTerpilih == 5)
                    {
                        Keluar();
                        IsProgramJalan = false;
                    }
                }
            }
        }

        static void Jalankan()
        {
            new Clear(32, 7, 87, 16).Tampil();

            Console.BackgroundColor = ConsoleColor.Gray;
            warna2();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 8).SetText(".:Jalankan Program:.").SetLength(88).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.Gray);
            Judul.TampilTengah();

            Tulisan HariSekarang = new Tulisan().SetXY(33, 10).SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);
            Tulisan JamSekarang = new Tulisan().SetXY(33, 11).SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);

            string QSelect = "SELECT * FROM tb_Jadwal WHERE hari=@hari AND jam=@jam;";

            DB.Connect();

            bool Play = true;

            while (Play)
            {
                DateTime Sekarang = DateTime.Now;

                HariSekarang.SetText("HARI SEKARANG : " + Sekarang.ToString("dddd"));
                HariSekarang.Tampil();

                JamSekarang.SetText("JAM SEKARANG  : " + Sekarang.ToString("HH:mm:ss"));
                JamSekarang.Tampil();

                DataTable DT = DB.RunQuery(QSelect,
                    new OleDbParameter("@hari", Sekarang.ToString("dddd")),
                    new OleDbParameter("@jam", Sekarang.ToString("HH:mm")));

                if (DT.Rows.Count > 0)
                {
                    Audio suara = new Audio();
                    suara.File = "./Suara/" + DT.Rows[0]["sound"];
                    suara.Play();

                    new Tulisan().SetXY(31, 14).SetText("BEL TELAH BERBUNYI !!!").SetBackColor(ConsoleColor.Green).SetLength(88).TampilTengah();
                    new Tulisan().SetXY(31, 15).SetText(DT.Rows[0]["ket"].ToString()).SetBackColor(ConsoleColor.Green).SetLength(88).TampilTengah();

                    Play = false;
                }

                Thread.Sleep(1000);
            }
        }

        static void LihatJadwal()
        {
            new Clear(32, 7, 87, 16).Tampil();

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            warna2();


            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 8).SetText(".:Lihat Data Jadwal:.").SetForeColor(ConsoleColor.Yellow).SetBackColor(ConsoleColor.DarkYellow).SetLength(88);
            Judul.TampilTengah();

            DB.Connect();
            DataTable DT = DB.RunQuery("SELECT * FROM tb_jadwal;");
            DB.Disconnect();

            new Tulisan("┌────┬──────────────┬─────────┬──────────────────────────────────────────────────┐").SetForeColor(ConsoleColor.DarkYellow).SetBackColor(ConsoleColor.Yellow).SetXY(34, 10).Tampil();
            new Tulisan("│ NO │     HARI     │   JAM   │                    KETERANGAN                    │").SetForeColor(ConsoleColor.DarkYellow).SetBackColor(ConsoleColor.Yellow).SetXY(34, 11).Tampil();
            new Tulisan("├────┼──────────────┼─────────┼──────────────────────────────────────────────────┤").SetForeColor(ConsoleColor.DarkYellow).SetBackColor(ConsoleColor.Yellow).SetXY(34, 12).Tampil();

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string ID = DT.Rows[i]["id"].ToString();
                string Hari = DT.Rows[i]["hari"].ToString();
                string Jam = DT.Rows[i]["jam"].ToString();
                string Ket = DT.Rows[i]["ket"].ToString();

                string isi = String.Format("│{0,-4}│{1,-14}│{2,-9}│{3,-50}│", ID, Hari, Jam, Ket);
                new Tulisan(isi).SetXY(34, 13 + i).SetBackColor(ConsoleColor.Yellow).SetForeColor(ConsoleColor.DarkYellow).Tampil();
            }

            new Tulisan("└────┴──────────────┴─────────┴──────────────────────────────────────────────────┘").SetForeColor(ConsoleColor.DarkYellow).SetBackColor(ConsoleColor.Yellow).SetXY(34, 13 + DT.Rows.Count).Tampil();
        }

        static void TambahJadwal()
        {
            new Clear(32, 7, 87, 16).Tampil();

            Console.BackgroundColor = ConsoleColor.Gray;
            warna2();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 8).SetText(".:Tambahkan Data Jadwal:.").SetLength(88).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.Gray);
            Judul.TampilTengah();

            Inputan HariInput = new Inputan();
            HariInput.Text = "Masukkan Hari       :";
            HariInput.SetXY(33, 10);
            HariInput.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);

            Inputan JamInput = new Inputan();
            JamInput.Text = "Masukkan Jam        :";
            JamInput.SetXY(33, 11);
            JamInput.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);

            Inputan KetInput = new Inputan();
            KetInput.Text = "Masukkan Keterangan :";
            KetInput.SetXY(33, 12);
            KetInput.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);

            //Inputan SoundInput = new Inputan();
            //SoundInput.Text = "Masukkan Sound :";
            //SoundInput.SetXY(33, 12);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(
                "pembuka.wav",
                "Pelajaran ke 1.wav",
                "Pelajaran ke 2.wav",
                "Pelajaran ke 3.wav",
                "Akhir Pelajaran A.wav",
                "Istirahat I.wav",
                "5 Menit Akhir Istirahat I.wav",
                "Akhir Pekan 1.wav");

            SoundInput.Text = "Masukkan Audio      : ";
            SoundInput.SetXY(33, 13);
            SoundInput.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);

            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string Ket = KetInput.Read();
            string Sound = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("INSERT INTO tb_Jadwal(hari, jam, ket, sound) VALUES (@hari, @jam, @ket, @sound);",
                new OleDbParameter("@hari", Hari),
                new OleDbParameter("@jam",Jam),
                new OleDbParameter("@ket", Ket),
                new OleDbParameter("@sound", Sound));
            DB.Disconnect();

            new Tulisan().SetXY(31, 15).SetText("Data Berhasil Disimpan!!!").SetForeColor(ConsoleColor.White).SetBackColor(ConsoleColor.DarkGreen).SetLength(88).TampilTengah();
        }

        static void EditJadwal()
        {
            new Clear(32, 7, 87, 16).Tampil();

            Console.BackgroundColor = ConsoleColor.Gray;
            warna2();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 8).SetText(".:Edit Data Jadwal:.").SetLength(88).SetBackColor(ConsoleColor.Gray).SetForeColor(ConsoleColor.Black);
            Judul.TampilTengah();

            Inputan IDInputDirubah = new Inputan();
            IDInputDirubah.Text = "Masukkan ID Jadwal Yang Ingin Dirubah : ";
            IDInputDirubah.SetXY(33, 10);
            IDInputDirubah.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);

            Inputan HariInput = new Inputan();
            HariInput.Text = "Masukkan Hari       : ";
            HariInput.SetXY(33, 12);
            HariInput.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);

            Inputan JamInput = new Inputan();
            JamInput.Text = "Masukkan Jam        : ";
            JamInput.SetXY(33, 13);
            JamInput.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);

            Inputan KetInput = new Inputan();
            KetInput.Text = "Masukkan Keterangan : ";
            KetInput.SetXY(33, 14);
            KetInput.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);


            //Inputan SoundInput = new Inputan();
            //SoundInput.Text = "Masukkan Sound : ";
            //SoundInput.SetXY(33, 12);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(
                "pembuka.wav",
                "Pelajaran ke 1.wav",
                "Pelajaran ke 2.wav",
                "Pelajaran ke 3.wav",
                "Akhir Pelajaran A.wav",
                "Istirahat I.wav",
                "5 Menit Akhir Istirahat I.wav",
                "Akhir Pekan 1.wav");

            SoundInput.Text = "Masukkan Audio      : ";
            SoundInput.SetXY(33, 15);
            SoundInput.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);

            string IDRubah = IDInputDirubah.Read();
            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string Ket = KetInput.Read();
            string Sound = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("UPDATE tb_Jadwal SET hari=@hari, jam=@jam, ket=@ket, sound=@sound WHERE id=@id;",
                new OleDbParameter("@hari", Hari),
                new OleDbParameter("@jam", Jam),
                new OleDbParameter("@ket", Ket),
                new OleDbParameter("@sound", Sound),
                new OleDbParameter("@id", IDRubah));
            DB.Disconnect();

            new Tulisan().SetXY(31, 17).SetText("Data Berhasil DIUPDATE!!!").SetBackColor(ConsoleColor.DarkGreen).SetLength(88).TampilTengah();

        }

        static void HapusJadwal()
        {
            new Clear(32, 7, 87, 16).Tampil();

            Console.BackgroundColor = ConsoleColor.Gray;
            warna2();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 8).SetText(".:Hapus Data Jadwal:.").SetLength(88).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.Gray);
            Judul.TampilTengah();

            Inputan IDInput = new Inputan();
            IDInput.Text = "Masukkan ID Yang Akan Dihapus : ";
            IDInput.SetXY(33, 10);
            IDInput.SetForeColor(ConsoleColor.DarkBlue).SetBackColor(ConsoleColor.Gray);
            string ID = IDInput.Read();

            //Cara 1
            //DB.Connect();
            //DB.RunNonQuery("DELETE FROM tb_jadwal WHERE id=" + ID + ";");

            DB.Connect();
            DB.RunNonQuery("DELETE FROM tb_jadwal WHERE id=@id;",
                new OleDbParameter("@id", ID));
            DB.Disconnect();

            new Tulisan().SetXY(31, 13).SetText("Data Berhasil Dihapus!!!").SetBackColor(ConsoleColor.DarkRed).SetLength(88).TampilTengah();
        }

        static void Keluar()
        {

        }

        static void Warna()
        {
            Console.SetCursorPosition(1, 1);
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            for (int i = 1; i <= 119; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 2);
            for (int i = 1; i <= 119; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 3);
            for (int i = 1; i <= 119; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 4);
            for (int i = 1; i <= 119; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 7);
            Console.BackgroundColor = ConsoleColor.Gray;

            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 8);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 9);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 9);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 10);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 11);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 12);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 13);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 14);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }


            Console.SetCursorPosition(31, 15);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 16);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 17);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 18);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 19);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 20);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 21);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 22);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 23);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 24);
            for (int i = 1; i <= 88; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 26);
            Console.BackgroundColor = ConsoleColor.DarkMagenta;

            for (int i = 1; i <= 119; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 27);
            for (int i = 1; i <= 119; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 7);
            Console.BackgroundColor = ConsoleColor.Yellow;

            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 8);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 9);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 10);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 11);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 12);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 13);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 14);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 15);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 16);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 17);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 18);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 19);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 20);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 21);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 22);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(1, 23);
            for (int i = 1; i <= 31; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

        }

        static void warna2()
        {
            Console.SetCursorPosition(31, 6);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 7);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 8);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 9);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 10);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 11);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 12);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 13);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 14);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }


            Console.SetCursorPosition(31, 15);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 16);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 17);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 18);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 19);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 20);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 21);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 22);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 23);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(31, 24);
            for (int i = 1; i <= 89; i++)
            {
                Console.Write(" ");
                Thread.Sleep(0);
            }
        }

    }
}
