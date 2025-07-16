using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calendar.Models;
using Calendar.Services;

namespace Calendar.ViewModels
{
    public class TasksViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private string _newTaskTitle;
        private string _newTaskDescription;
        private DateTime _dueDate;
        private string _selectedPriority;
        private ObservableCollection<TaskItem> _tasks;
        private ObservableCollection<TaskItem> _filteredTasks;
        private bool _isLoading;
        private TaskItem _editingTask;
        private string _currentFilter = "All";

        public TasksViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _dueDate = DateTime.Today.AddDays(1);
            _selectedPriority = "Baixa";
            _tasks = new ObservableCollection<TaskItem>();
            _filteredTasks = new ObservableCollection<TaskItem>();

            PriorityOptions = new ObservableCollection<string> { "Baixa", "Média", "Alta" };

            AddTaskCommand = new Command(async () => await AddTask(), () => CanAddTask());
            DeleteTaskCommand = new Command<TaskItem>(async (task) => await DeleteTask(task));
            EditTaskCommand = new Command<TaskItem>(async (task) => await EditTask(task));
            ToggleTaskCompletionCommand = new Command<TaskItem>(async (task) => await ToggleTaskCompletion(task));
            FilterTasksCommand = new Command<string>(async (filter) => await FilterTasks(filter));

            _ = LoadTasks();
        }

        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set
            {
                _newTaskTitle = value;
                OnPropertyChanged();
                ((Command)AddTaskCommand).ChangeCanExecute();
            }
        }

        public string NewTaskDescription
        {
            get => _newTaskDescription;
            set
            {
                _newTaskDescription = value;
                OnPropertyChanged();
            }
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged();
            }
        }

        public string SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> PriorityOptions { get; }

        public ObservableCollection<TaskItem> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TaskItem> FilteredTasks
        {
            get => _filteredTasks;
            set
            {
                _filteredTasks = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTasks));
                OnPropertyChanged(nameof(HasNoTasks));
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

        public bool HasTasks => FilteredTasks?.Count > 0;
        public bool HasNoTasks => !HasTasks;

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand ToggleTaskCompletionCommand { get; }
        public ICommand FilterTasksCommand { get; }

        private bool CanAddTask()
        {
            return !string.IsNullOrWhiteSpace(NewTaskTitle);
        }

        private async Task AddTask()
        {
            if (!CanAddTask()) return;

            try
            {
                IsLoading = true;

                var task = _editingTask ?? new TaskItem();
                task.Title = NewTaskTitle.Trim();
                task.Description = NewTaskDescription?.Trim() ?? string.Empty;
                task.DueDate = DueDate;
                task.Priority = SelectedPriority;

                if (_editingTask == null)
                {
                    task.CreatedAt = DateTime.Now;
                }

                task.UpdatedAt = DateTime.Now;

                await _databaseService.SaveTaskAsync(task);

                ClearForm();
                await LoadTasks();
                await FilterTasks(_currentFilter);

                await Shell.Current.DisplayAlert("Sucesso",
                    _editingTask == null ? "Tarefa adicionada com sucesso!" : "Tarefa atualizada com sucesso!",
                    "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao salvar tarefa: " + ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task DeleteTask(TaskItem task)
        {
            if (task == null) return;

            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar",
                    $"Deseja excluir a tarefa '{task.Title}'?",
                    "Sim", "Não");

                if (confirmed)
                {
                    await _databaseService.DeleteTaskAsync(task);
                    await LoadTasks();
                    await FilterTasks(_currentFilter);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao excluir tarefa: " + ex.Message, "OK");
            }
        }

        private async Task EditTask(TaskItem task)
        {
            if (task == null) return;

            _editingTask = task;
            NewTaskTitle = task.Title;
            NewTaskDescription = task.Description;
            DueDate = task.DueDate;
            SelectedPriority = task.Priority;
        }

        private async Task ToggleTaskCompletion(TaskItem task)
        {
            if (task == null) return;

            try
            {
                task.IsCompleted = !task.IsCompleted;
                task.CompletedAt = task.IsCompleted ? DateTime.Now : null;
                task.UpdatedAt = DateTime.Now;

                await _databaseService.SaveTaskAsync(task);
                await LoadTasks();
                await FilterTasks(_currentFilter);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao atualizar tarefa: " + ex.Message, "OK");
            }
        }

        private async Task FilterTasks(string filter)
        {
            _currentFilter = filter;

            var filteredList = filter switch
            {
                "Pending" => Tasks.Where(t => !t.IsCompleted).ToList(),
                "Completed" => Tasks.Where(t => t.IsCompleted).ToList(),
                _ => Tasks.ToList()
            };

            FilteredTasks = new ObservableCollection<TaskItem>(filteredList);
        }

        private void ClearForm()
        {
            NewTaskTitle = string.Empty;
            NewTaskDescription = string.Empty;
            DueDate = DateTime.Today.AddDays(1);
            SelectedPriority = "Baixa";
            _editingTask = null;
        }

        private async Task LoadTasks()
        {
            try
            {
                IsLoading = true;
                var tasks = await _databaseService.GetTasksAsync();
                Tasks = new ObservableCollection<TaskItem>(tasks.OrderBy(t => t.IsCompleted).ThenBy(t => t.DueDate));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao carregar tarefas: " + ex.Message, "OK");
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
