using MusicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicLibrary.Data
{
    public interface IMusicRepository
    {
        IEnumerable<Song> GetAll();
        IEnumerable<String> GetArtists();
        IEnumerable<String> GetAlbums();
    }
}
