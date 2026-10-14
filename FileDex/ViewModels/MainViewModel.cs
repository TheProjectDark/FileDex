using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FileDex.Models;
using FileDex.Services;
using Microsoft.Win32;

namespace FileDex.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IMetadataReader _reader;

    private string? _selectedFile;
    private FileMetadata? _currentMetadata;
    private bool _isBusy;
    private string _searchText = string.Empty;

    public MainViewModel() : this(new FileMetadataReader()) { }

    public MainViewModel(IMetadataReader reader)
    {
        _reader = reader;
        OpenFileCommand = new RelayCommand(_ => OpenFile());
        RefreshCommand = new RelayCommand(async _ => await LoadAsync(_selectedFile!), _ => !string.IsNullOrEmpty(_selectedFile));
        CopyValueCommand = new RelayCommand(param => CopyToClipboard(param as string));
    }

    public ObservableCollection<MetadataEntry> Entries { get; } = new();

    public ICommand OpenFileCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand CopyValueCommand { get; }

    public string? SelectedFile
    {
        get => _selectedFile;
        private set => SetField(ref _selectedFile, value);
    }

    public FileMetadata? CurrentMetadata
    {
        get => _currentMetadata;
        private set => SetField(ref _currentMetadata, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        private set => SetField(ref _isBusy, value);
    }

    public string SearchText
    {
        get => _searchText;
        set { if (SetField(ref _searchText, value)) ApplyFilter(); }
    }

    private async void OpenFile()
    {
        var dlg = new OpenFileDialog
        {
            Title = "Choose file",
            Filter = "All files|*.*"
        };
        if (dlg.ShowDialog() == true)
            await LoadAsync(dlg.FileName);
    }

    private async Task LoadAsync(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return;

        try
        {
            IsBusy = true;
            SelectedFile = path;

            if (_reader.CanRead(path))
            {
                var meta = await _reader.ReadAsync(path);
                CurrentMetadata = meta;
                ApplyFilter();
            }
        }
        catch (System.Exception ex)
        {
            MessageBox.Show($"Error reading metadata: {ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ApplyFilter()
    {
        Entries.Clear();
        if (CurrentMetadata is null) return;

        var query = string.IsNullOrWhiteSpace(SearchText)
            ? CurrentMetadata.Entries
            : CurrentMetadata.Entries.Where(e =>
                e.Key.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                e.Value.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase));

        foreach (var e in query) Entries.Add(e);
    }

    private static void CopyToClipboard(string? text)
    {
        if (!string.IsNullOrEmpty(text))
            Clipboard.SetText(text);
    }
}