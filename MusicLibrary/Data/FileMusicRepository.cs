using System;
using System.Collections.Generic;
using MusicLibrary.Models;

namespace MusicLibrary.Data
{
    public class FileMusicRepository : IMusicRepository
    {

        public IEnumerable<Song> GetAll()
        {
            var album1 = new Album
            {
                Artist = "Rahman", Name = "Achcham Yenbadhu Madamaiyada", Year = 2016,
                CoverImage = "Achcham-Enbathu-Madamaiyada.jpg", Language = "Tamil"
            };

            var album2 = new Album
            {
                Artist = "Rahman",
                Name = "Alaipayuthey",
                Year = 2000,
                CoverImage = "AlaipayutheyAlbumArt.jpg",
                Language = "Tamil"
            };

            var album3 = new Album
            {
                Artist = "Anirudh Ravichander",
                Name = "Darbar",
                Year = 2020,
                CoverImage = "DarbarAlbumArt.png",
                Language = "Tamil"
            };

            var song1 = new Song
                {Id = 1, Title = "Idhu Naal", Singers = "Aditya Rao, Jonita Gandhi", Length = new TimeSpan(0, 3, 29), FileName = "IdhuNaal.mp3", Album = album1};

            var song2 = new Song
                { Id = 2, Title = "Rasaali", Singers = "Sathya Prakash, Shasha Tirupati", Length = new TimeSpan(0, 5, 38), FileName = "Rasaali.mp3", Album = album1};

            var song3 = new Song
            {
                Id = 3,
                Title = "Alaipayuthey Kanna",
                Singers = "Kalyani Menon, Harini & Neyveli",
                Length = new TimeSpan(0, 3, 41),
                FileName = "Alaipayuthey-Kanna.mp3",
                Album = album2
            };

            var song4 = new Song
            {
                Id = 4,
                Title = "Pachchai Nirame",
                Singers = "Hariharan & Clinton",
                Length = new TimeSpan(0, 5, 58),
                FileName = "Pachchai-Nirame.mp3",
                Album = album2
            };

            var song5 = new Song
            {
                Id = 5,
                Title = "Endrendrum Punnagai",
                Singers = "Praveen, Shankar, Clinton & Srinivas",
                Length = new TimeSpan(0, 3, 57),
                FileName = "Endrendrum-Punnagai.mp3",
                Album = album2
            };

            var song6 = new Song
            {
                Id = 6,
                Title = "Thalaivar Theme",
                Singers = "Anirudh Ravichander",
                Length = new TimeSpan(0, 0, 43),
                FileName = "ThalaivarThemeSong.mp3",
                Album = album3
            };

            var song7 = new Song
            {
                Id = 7,
                Title = "Chumma Kizhi",
                Singers = "S. P. Balasubrahmanyam & Anirudh Ravichander",
                Length = new TimeSpan(0, 3, 50),
                FileName = "ChummaKizhi.mp3",
                Album = album3
            };

            var song8 = new Song
            {
                Id = 8,
                Title = "Thani Vazhi",
                Singers = "Rap By Yogi B, Anirudh Ravichander & Shakthisree",
                Length = new TimeSpan(0, 3, 27),
                FileName = "ThaniVazhi.mp3",
                Album = album3
            };

            return new List<Song> {song1, song2, song3, song4, song5, song6, song7, song8};
        }

        public IEnumerable<string> GetArtists()
        {
            return new List<string> { "Rahman", "Anirudh Ravichander" };
        }

        public IEnumerable<string> GetAlbums()
        {
            return new List<string> { "Achcham Yenbadhu Madamaiyada", "Alaipayuthey", "Darbar" };
        }


    }
}
