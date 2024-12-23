using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace Browser
{
    public partial class MainWindow : Window
    {
        private List<string> favorites = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            Browser.Source = new Uri("https://www.google.com");
        }
        private void Go(object sender, RoutedEventArgs e)
        {
            string url = UrlTextBox.Text;

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter a URL", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;

            try
            {
                Browser.Source = new Uri(url);
            }
            catch (UriFormatException)
            {
                MessageBox.Show("The URL entered is not valid.", "Invalid URL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Browser.CoreWebView2.GoBack();
        }
        private void AddToFavoritesClick(object sender, RoutedEventArgs e)
        {
            string url = Browser.Source.ToString();
            AddToFavorites(url);
        }

        private void AddToFavorites(string url)
        {
            favorites.Add(url);
            MessageBox.Show("URL added to favorites.", "Favorites", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ShowFavoritesClick(object sender, RoutedEventArgs e)
        {
            ShowFavorites();
        }

        private void ShowFavorites()
        {
            if (favorites.Count == 0)
            {
                MessageBox.Show("No favorites added.", "Favorites", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string favoriteList = string.Join("\n", favorites.Select((url, index) => $"{index + 1}. {url}"));

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Select a favorite to open in a new window:\n\n" + favoriteList,
                "Favorites", "");

            if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= favorites.Count)
            {
                OpenInNewWindow(favorites[selectedIndex - 1]);
            }
            else
            {
                MessageBox.Show("Invalid selection.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void OpenInNewWindow(string url)
        {
            var newWindow = new BrowserWindow(url);
            newWindow.Show();
        }

        private void OpenCurrentUrlInNewWindow(object sender, RoutedEventArgs e)
        {
            if (Browser.Source != null)
            {
                OpenInNewWindow(Browser.Source.ToString());
            }
            else
            {
                MessageBox.Show("No URL is currently loaded.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class BrowserWindow : Window
    {
        public BrowserWindow(string url)
        {
            Title = "New Browser Window";
            Width = 800;
            Height = 450;

            var webView = new Microsoft.Web.WebView2.Wpf.WebView2
            {
                Source = new Uri(url)
            };

            Content = webView;
        }
    }
}