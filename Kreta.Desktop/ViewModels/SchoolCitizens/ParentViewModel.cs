using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Models.Entites.SchoolCitizens;
using Kreta.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace Kreta.Desktop.ViewModels.SchoolCitizens
{
    public partial class ParentViewModel : BaseViewModel
    {
        private readonly IParentHttpService _httpService;

        [ObservableProperty]
        private Parent _selectedParent = new Parent();

        [ObservableProperty]
        private ObservableCollection<Parent> _parents = new ObservableCollection<Parent>();

        public async override Task InitializeAsync()
        {
            await UpdateViewAsync();
            await base.InitializeAsync();
        }

        private async Task UpdateViewAsync()
        {
            List<Parent> parent = await _httpService.GetAllAsync();
            Parents = new ObservableCollection<Parent>(parent);
        }

        public ParentViewModel(IParentHttpService httpService)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
            _selectedParent = new Parent();
            SelectedParent.BirthDay = DateTime.Now.AddYears(-14);
        }

        [RelayCommand]
        public async Task DoSaveParent(Parent parent)
        {
            if(parent is not null)
            {
                Response response;
                if (parent.HasId)
                {
                    response = await _httpService.UpdateAsync(parent);
                }
                else
                {
                    response = await _httpService.InsertAsync(parent);
                }
                ClearParent();
                await UpdateViewAsync();
            }
        }

        [RelayCommand]
        public void DoNewParent()
        {
            ClearParent();
        }

        [RelayCommand]
        public async Task DoDeleteParent(Parent parent)
        {
            if(parent is not null)
            {
                await _httpService.DeleteAsync(parent.Id);
                ClearParent();
                await UpdateViewAsync();
            }
        }
        private void ClearParent()
        {
            SelectedParent = new Parent();
            OnPropertyChanged(nameof(SelectedParent));
        }

        public ParentViewModel()
        {

        }
    }
}
