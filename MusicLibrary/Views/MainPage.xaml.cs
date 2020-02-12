using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MusicLibrary.ViewModels;
using MusicLibrary.Models;
using Windows.Media.Core;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MusicLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// We use this object to bind the UI to our data. 
        /// </summary>
        public MainPageViewModel ViewModel { get; } = new MainPageViewModel();

        /// <summary>
        /// Retrieve the list of orders when the user navigates to the page. 
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ViewModel.Songs.Count < 1)
            {
                ViewModel.LoadSongs();
            }
        }

        private void SongsView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            musicPlayer.AutoPlay = true;
            var song = (Song)e.ClickedItem;
            var songFile = Windows.ApplicationModel.Package.Current.InstalledLocation
                .GetFileAsync(@"Assets\Songs\" + song.FileName).GetAwaiter().GetResult();
            musicPlayer.Source = MediaSource.CreateFromStorageFile(songFile);

            var bitmapImage = new BitmapImage
            {
                UriSource = new Uri(Windows.ApplicationModel.Package.Current.InstalledLocation.Path + @"\Assets\Songs\" + song.Album.CoverImage)
            };
            coverImage.Source = bitmapImage;
            albumName.Text = song.Album.Name;
            artistName.Text = song.Album.Artist;
            languageName.Text = song.Album.Language;
            yearName.Text = song.Album.Year.ToString();
        }

        private void AlbumComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var album = e.AddedItems[0].ToString();
            ViewModel.FilterSongsByAlbum(album);
            //artistComboBox.SelectedIndex = -1;
        }

        private void ArtistComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var artist = e.AddedItems[0].ToString();
            ViewModel.FilterSongsByArtist(artist);
            //albumComboBox.SelectedIndex = -1;
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            var search = searchText.Text;
            if (search != searchText.PlaceholderText)
            {
                ViewModel.FilterSongsByName(search);
            }
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            searchText.Text = searchText.PlaceholderText;
            ViewModel.LoadSongs();
        }

        private async void VoiceSearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create an instance of SpeechRecognizer.
                var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

                // Listen for audio input issues.
                //speechRecognizer.RecognitionQualityDegrading += speechRecognizer_RecognitionQualityDegrading;

                // Add a web search grammar to the recognizer.
                var webSearchGrammar = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.WebSearch, "webSearch");


                speechRecognizer.UIOptions.AudiblePrompt = "Say what you want to search for...";
                speechRecognizer.UIOptions.ExampleText = @"Ex. 'Play Rahman songs'";
                speechRecognizer.Constraints.Add(webSearchGrammar);

                // Compile the constraint.
                await speechRecognizer.CompileConstraintsAsync();

                // Start recognition.
                Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();
                //await speechRecognizer.RecognizeWithUIAsync();

                // Do something with the recognition result.
                var messageDialog = new Windows.UI.Popups.MessageDialog(speechRecognitionResult.Text, "Text spoken");
                await messageDialog.ShowAsync();
            }
            catch (Exception err)
            {
                // Define a variable that holds the error for the speech recognition privacy policy. 
                // This value maps to the SPERR_SPEECH_PRIVACY_POLICY_NOT_ACCEPTED error, 
                // as described in the Windows.Phone.Speech.Recognition error codes section later on.
                const int privacyPolicyHResult = unchecked((int)0x80045509);

                // Check whether the error is for the speech recognition privacy policy.
                if (err.HResult == privacyPolicyHResult)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("You will need to accept the speech privacy policy in order to use speech recognition in this app.", "Error");
                    await messageDialog.ShowAsync();
                }
            }
        }
    }
}
