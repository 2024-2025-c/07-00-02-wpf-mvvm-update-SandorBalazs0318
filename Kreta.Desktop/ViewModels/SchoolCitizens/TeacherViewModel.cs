using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Models.Entites.SchoolCitizens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Kreta.Desktop.ViewModels.SchoolCitizens
{
    public partial class TeacherViewModel : BaseViewModel
    {

        private readonly ITeacherHttpService _httpService;

        [ObservableProperty]
        private Teacher _selectedTeacher = new Teacher();

        [ObservableProperty]
        private ObservableCollection<Teacher> _teachers = new ObservableCollection<Teacher>();

        public TeacherViewModel()
        {
            _selectedTeacher = new Teacher();
            SelectedTeacher.BirthDay = DateTime.Now.AddYears(-14);
            _httpService = new TeacherHttpService();
        }

        public TeacherViewModel(ITeacherHttpService? httpService)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            _selectedTeacher = new Teacher();
            SelectedTeacher.BirthDay = DateTime.Now.AddYears(-14);
        }

        public async override Task InitializeAsync()
        {
            await UpdateViewAsync();
            await base.InitializeAsync();
        }

        private async Task UpdateViewAsync()
        {
            List<Teacher> teachers = await _httpService.GetAllAsync();
            Teachers = new ObservableCollection<Teacher>(teachers);
        }

        [RelayCommand]
        private void DoNewTeacher()
        {
            ClearForm();
        }

        [RelayCommand]
        private async Task DoDeleteTeacher(Teacher teacher)
        {
            if(teacher is not null)
            {
                await _httpService.DeleteAsync(teacher.Id);
                ClearForm();
                await UpdateViewAsync();
            }
        }

        private void ClearForm()
        {
            SelectedTeacher = new Teacher();
            OnPropertyChanged(nameof(SelectedTeacher));
        }
    }
}
