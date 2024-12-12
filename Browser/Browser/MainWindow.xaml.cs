using System;
using System.Text;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace Browser{
    public partial class MainWindow : Window { List<string> favorites = new List<string>();
        public MainWindow(){
            InitializeComponent();
            Browser.Source = new Uri("https://www.google.com"); }
        // ----------------------------------------------------------GO
        private void Go(object sender, RoutedEventArgs e) {
            string url = UrlTextBox.Text;
            
            if (string.IsNullOrWhiteSpace(url)) {
                MessageBox.Show("Please enter a URL", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;}
            if (!url.StartsWith("http://") && !url.StartsWith("https://")) url = "https://" + url;
            try 
                { Browser.Source = new Uri(url); } 
            catch (UriFormatException) 
                { MessageBox.Show("The URL entered is not valid.", "Invalid URL", MessageBoxButton.OK, MessageBoxImage.Error); }}
        // ----------------------------------------------------------Back
        private void Back(object sender, RoutedEventArgs e) {
            Browser.CoreWebView2.GoBack(); }
        private void AddToFavoritesClick(object sender, RoutedEventArgs e) { 
            string url = Browser.Source.ToString();
            AddToFavorites(url); }

        private void ShowFavoritesClick(object sender, RoutedEventArgs e) {
            ShowFavorites(); }
        private void AddToFavorites(string url) {
            favorites.Add(url);
            MessageBox.Show("URL added to favorites.", "Favorites", MessageBoxButton.OK, MessageBoxImage.Information); }
        //--------------------------------Favorite
        private void ShowFavorites() { string favoriteList = string.Join("\n", favorites);
            MessageBox.Show(favoriteList, "Favorites", MessageBoxButton.OK, MessageBoxImage.Information); }}}// END