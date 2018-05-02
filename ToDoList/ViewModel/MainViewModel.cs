using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList.ViewModel
{
    #region usings
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using ToDoList.DataAccess;
    #endregion

    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private ToDoListModel _model = new ToDoListModel();
        private List<ToDoListModel> _toDoList = new List<ToDoListModel>();
        private ToDoListModel _selectedTask;
        private DateTime _deadlineList;
        private ToDoListDataService toDoListDataService = new ToDoListDataService();
        private int _comboBox;
        private int _tasksNumber;
        #endregion

        #region Class Constructor
        public MainViewModel() => ComboBoxValues = new ObservableCollection<int>() { 7, 14, 21 };
        #endregion

        #region Properties
        public string TaskDescription
        {
            get { return _model.TaskDescription; }
            set
            {
                _model.TaskDescription = value;
                OnPropertyChanged();
            }
        }

        public DateTime Deadline
        {
            get { return _model.Deadline; }
            set
            {
                _model.Deadline = value;
                OnPropertyChanged();
            }
        }
        
        public DateTime DeadlineList
        {
            get { return _deadlineList; }
            set
            {
                _deadlineList = value;
                OnPropertyChanged();
            }
        }

        public List<ToDoListModel> ToDoList
        {
            get { return _toDoList; }
            set
            {
                _toDoList = value;
                OnPropertyChanged();
            }
        }

        public ToDoListModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }
        
        public int ComboBox
        {
            get
            {
                return _comboBox;
            }
            set
            {
                _comboBox = value;
                TasksNumber = FutureTasks(value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<int> ComboBoxValues { get; }

        public int TasksNumber
        {
            get
            {
                return _tasksNumber;
            }
            set
            {
                _tasksNumber = value;
                OnPropertyChanged();
            }
        }

        public int OutstandingTasksNumber
        {
            get
            {
                return OutstandingTasks();
            }
        }
        #endregion

        #region Commands
        private ICommand _add;
        public ICommand Add
        {
            get
            {
                if (_add == null)
                    _add = new RelayCommand(
                        p =>
                        {
                            toDoListDataService.Add(_model);
                            MessageBox.Show("New task has been added.", "ToDoList - Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            TaskDescription = "";
                            TasksNumber = FutureTasks(ComboBox);
                        },
                        p =>
                        {
                            return !String.IsNullOrEmpty(TaskDescription) && Deadline.Date.CompareTo(DateTime.Now.Date) >= 0;
                        }
                        );
                return _add;
            }
        }

        private ICommand _read;
        public ICommand Read
        {
            get
            {
                if (_read == null)
                    _read = new RelayCommand(
                        p =>
                        {
                            ToDoList = toDoListDataService.Read().Where(x => x.Deadline.Equals(DeadlineList)).ToList();
                            if (!ToDoList.Any())
                                MessageBox.Show("There are no tasks on this day.", "ToDoList - Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        },
                        p =>
                        {
                            return DeadlineList.Year > 1;
                        }
                        );
                return _read;
            }
        }

        private ICommand _update;
        public ICommand Update
        {
            get
            {
                if (_update == null)
                    _update = new RelayCommand(
                        p =>
                        {
                            toDoListDataService.Update(SelectedTask);
                            MessageBox.Show("The task has been updated.", "ToDoList - Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            ToDoList = toDoListDataService.Read().Where(x => x.Deadline.Equals(DeadlineList)).ToList();
                            TasksNumber = FutureTasks(ComboBox);
                        },
                        p =>
                        {
                            return SelectedTask != null && SelectedTask.Deadline.Date.CompareTo(DateTime.Now.Date) >= 0;
                        }
                        );
                return _update;
            }
        }

        private ICommand _delete;
        public ICommand Delete
        {
            get
            {
                if (_delete == null)
                    _delete = new RelayCommand(
                        p =>
                        {
                            if (MessageBox.Show("Are you sure?", "ToDoList - Info", MessageBoxButton.YesNo,
                                       MessageBoxImage.Question) == MessageBoxResult.No)
                                return;
                            toDoListDataService.Delete(SelectedTask);
                            MessageBox.Show("The task has been deleted.", "ToDoList - Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            ToDoList = toDoListDataService.Read().Where(x => x.Deadline.Equals(DeadlineList)).ToList();
                            TasksNumber = FutureTasks(ComboBox);
                        },
                        p =>
                        {
                            return SelectedTask != null;
                        }
                        );
                return _delete;
            }
        }
        #endregion

        #region Helper Methods
        public int FutureTasks(int termin)
        {
            return toDoListDataService.Read().Where(x => ((x.Deadline.Date - DateTime.Now.Date).TotalDays <= termin && (x.Deadline.Date - DateTime.Now.Date).TotalDays >= 0)).ToList().Count();
        }

        public int OutstandingTasks()
        {
            return toDoListDataService.Read().Where(x => ((x.Deadline.Date - DateTime.Now.Date).TotalDays < 0)).ToList().Count();
        }
        #endregion
    }
}