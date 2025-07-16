using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calendar.Models;
using Calendar.Services;

namespace Calendar.ViewModels
{
    public class DiaryViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private string _newNoteTitle;
        private string _newNoteContent;
        private ObservableCollection<DiaryNote> _recentNotes;
        private bool _isLoading;
        private DiaryNote _editingNote;

        public DiaryViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _recentNotes = new ObservableCollection<DiaryNote>();

            SaveNoteCommand = new Command(async () => await SaveNote(), () => CanSaveNote());
            DeleteNoteCommand = new Command<DiaryNote>(async (note) => await DeleteNote(note));
            EditNoteCommand = new Command<DiaryNote>(async (note) => await EditNote(note));

            _ = LoadRecentNotes();
        }

        public string NewNoteTitle
        {
            get => _newNoteTitle;
            set
            {
                _newNoteTitle = value;
                OnPropertyChanged();
                ((Command)SaveNoteCommand).ChangeCanExecute();
            }
        }

        public string NewNoteContent
        {
            get => _newNoteContent;
            set
            {
                _newNoteContent = value;
                OnPropertyChanged();
                ((Command)SaveNoteCommand).ChangeCanExecute();
            }
        }

        public ObservableCollection<DiaryNote> RecentNotes
        {
            get => _recentNotes;
            set
            {
                _recentNotes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasNotes));
                OnPropertyChanged(nameof(HasNoNotes));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool HasNotes => RecentNotes?.Count > 0;
        public bool HasNoNotes => !HasNotes;

        public ICommand SaveNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand EditNoteCommand { get; }

        private bool CanSaveNote()
        {
            return !string.IsNullOrWhiteSpace(NewNoteTitle) && !string.IsNullOrWhiteSpace(NewNoteContent);
        }

        private async Task SaveNote()
        {
            if (!CanSaveNote()) return;

            try
            {
                IsLoading = true;

                var note = _editingNote ?? new DiaryNote();
                note.Title = NewNoteTitle.Trim();
                note.Content = NewNoteContent.Trim();

                if (_editingNote == null)
                {
                    note.CreatedAt = DateTime.Now;
                }

                note.UpdatedAt = DateTime.Now;

                await _databaseService.SaveDiaryNoteAsync(note);

                ClearForm();
                await LoadRecentNotes();

                await Shell.Current.DisplayAlert("Sucesso",
                    _editingNote == null ? "Nota salva com sucesso!" : "Nota atualizada com sucesso!",
                    "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao salvar nota: " + ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task DeleteNote(DiaryNote note)
        {
            if (note == null) return;

            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar",
                    $"Deseja excluir a nota '{note.Title}'?",
                    "Sim", "Não");

                if (confirmed)
                {
                    await _databaseService.DeleteDiaryNoteAsync(note);
                    await LoadRecentNotes();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao excluir nota: " + ex.Message, "OK");
            }
        }

        private async Task EditNote(DiaryNote note)
        {
            if (note == null) return;

            _editingNote = note;
            NewNoteTitle = note.Title;
            NewNoteContent = note.Content;
        }

        private void ClearForm()
        {
            NewNoteTitle = string.Empty;
            NewNoteContent = string.Empty;
            _editingNote = null;
        }

        private async Task LoadRecentNotes()
        {
            try
            {
                IsLoading = true;
                var notes = await _databaseService.GetDiaryNotesAsync();
                RecentNotes = new ObservableCollection<DiaryNote>(notes.OrderByDescending(n => n.CreatedAt));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao carregar notas: " + ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
