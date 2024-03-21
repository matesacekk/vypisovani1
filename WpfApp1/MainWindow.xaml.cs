using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string FilePath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadTextFromFile(string path)
        {
            if (File.Exists(path))
            {
                txtInput.Text = File.ReadAllText(path);
                FilePath = path;
            }
            else
            {
                MessageBox.Show("Zvolený soubor neexistuje.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveTextToFile()
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                string changeLog = $"{DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")}: {txtInput.Text}{Environment.NewLine}";
                File.AppendAllText(FilePath, changeLog);
                AddChangeToLog(changeLog);
                MessageBox.Show("Text byl úspěšně uložen.", "Uloženo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                SaveAs();
            }
        }

        private void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Textové soubory (*.txt)|*.txt|Všechny soubory (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                File.WriteAllText(FilePath, txtInput.Text);
                MessageBox.Show("Text byl úspěšně uložen.", "Uloženo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddChangeToLog(string change)
        {
            TextBlock changeBlock = new TextBlock();
            changeBlock.Text = change;
            changeLogPanel.Children.Add(changeBlock);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Textové soubory (*.txt)|*.txt|Všechny soubory (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                LoadTextFromFile(openFileDialog.FileName);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveTextToFile();
        }
    }
}
