using System;

namespace MusicLibrary.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Singers { get; set; }

        public TimeSpan Length { get; set; }

        public string FileName { get; set; }

        public Album Album { get; set; }

    }
}
