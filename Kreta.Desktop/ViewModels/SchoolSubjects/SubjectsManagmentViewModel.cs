using CommunityToolkit.Mvvm.ComponentModel;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Models.Entites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.SchoolSubjects
{
    public partial class SubjectsManagmentViewModel : BaseViewModel
    {
        private readonly ISubjectHttpService _httpService;
        
        // 2. A lekért adatok ebben az adatstruktúrában jelennek meg madj a viewn
        [ObservableProperty]
        private ObservableCollection<Subject> _subjects = new ObservableCollection<Subject>();
        
        // 2.b AdatStruktúra a kiválasztott tantárgynak
        private Subject _selectedSubject = new Subject();
        
        public SubjectsManagmentViewModel()
        {
        }

        // 1.b Konstruktorban innektáljuk a ISubjectsHttpService
        public SubjectsManagmentViewModel(ISubjectHttpService? httpService)
        {
            _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
        }

        // 1.feladat: tantárgyak adatainak lekérése backandről
        // 1.a: Adatok menüpont kiválasztására jelenjenej meg (InicializeAsync) 
        public override async Task InitializeAsync()
        {
            await UpdateViewAsync();
            await base.InitializeAsync();
        }

        private async Task UpdateViewAsync()
        {
            // 1.d HttpServic-en keresztül backend hívás 
           List<Subject> subjects = await _httpService.GetAllAsync();
            // 2.a A megérkezett adatokat újra létrehozzuk a Subjects ObserableCollection
            Subjects = new ObservableCollection<Subject>(subjects);
        }
    }
}
