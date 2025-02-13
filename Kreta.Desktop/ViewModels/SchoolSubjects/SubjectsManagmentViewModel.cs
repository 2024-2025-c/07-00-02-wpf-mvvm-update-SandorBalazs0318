using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;

namespace Kreta.Desktop.ViewModels.SchoolSubjects
{
    public partial class SubjectsManagmentViewModel : BaseViewModel
    {
        private readonly ISubjectHttpService _httpService;
    }
}
