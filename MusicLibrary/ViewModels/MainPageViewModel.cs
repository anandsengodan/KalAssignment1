using System;
using MusicLibrary.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;

namespace MusicLibrary.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<Song> Songs { get; private set; } = new ObservableCollection<Song>();

        public ObservableCollection<string> Artists { get; private set; } = new ObservableCollection<string>();

        public ObservableCollection<string> Albums { get; private set; } = new ObservableCollection<string>();

        /// <summary>
        /// Retrieves songs from the data source.
        /// </summary>
        public async void LoadSongs()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Songs.Clear();
                Artists.Clear();
                Albums.Clear();
            });

            var songs = App.Repository.GetAll();
            var artists = App.Repository.GetArtists();
            var albums = App.Repository.GetAlbums();

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                foreach (var song in songs)
                {
                    Songs.Add(song);
                }

                foreach (var artist in artists)
                {
                    Artists.Add(artist);
                }

                foreach (var album in albums)
                {
                    Albums.Add(album);
                }
            });
        }
        public async void FilterSongsByName(string songName)
        {
            if (string.IsNullOrEmpty(songName)) return;

            var songs = App.Repository.GetAll();
            var filteredSongs = songs.Where(x => x.Title.Contains(songName, StringComparison.OrdinalIgnoreCase));

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Songs.Clear();
                foreach (var song in filteredSongs)
                {
                    Songs.Add(song);
                }
            });
        }

        public async void FilterSongsByArtist(string artistName)
        {
            if (string.IsNullOrEmpty(artistName)) return;

            var songs = App.Repository.GetAll();
            var filteredSongs = songs.Where(x => x.Album.Artist.Equals(artistName));

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Songs.Clear();
                foreach (var song in filteredSongs)
                {
                    Songs.Add(song);
                }
            });
        }

        public async void FilterSongsByAlbum(string albumName)
        {
            if (string.IsNullOrEmpty(albumName)) return;

            var songs = App.Repository.GetAll();
            var filteredSongs = songs.Where(x => x.Album.Name.Equals(albumName));
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Songs.Clear();
                foreach (var song in filteredSongs)
                {
                    Songs.Add(song);
                }
            });
        }
    }
}
