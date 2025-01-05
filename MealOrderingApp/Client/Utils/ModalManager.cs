using Blazored.Modal;
using Blazored.Modal.Services;
using MealOrderingApp.Client.CustomComponents.ModalComponent;
using System.Threading.Tasks;

namespace MealOrderingApp.Client.Utils
{

    public class ModalManager
    {
        private readonly IModalService _modalService;

        public ModalManager(IModalService modalService)
        {
            _modalService = modalService;
        }
        public async Task ShowMessageAsync(string title, string message, int duration = 0)
        {
            ModalParameters mParams = new ModalParameters();
            mParams.Add("Message", message);
            var modalRef = _modalService.Show<ShowMessagePopupComponent>(title, mParams);
            if (duration > 0)
            {
                await Task.Delay(duration);
                modalRef.Close();

            }
        }
        public async Task<bool> ConfirmationAsync(string title, string message)
        {
            ModalParameters modalParameters = new ModalParameters();
            modalParameters.Add("Message", message);
            var modalRef = _modalService.Show<ConfirmationPopupComponent>(title, modalParameters);
            var modalResult = await modalRef.Result;
            return !modalResult.Cancelled;
        }
    }
}
