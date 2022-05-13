using System.Threading.Tasks;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Interface
{
    public interface IAddressRequestService
    {
        Task<string> RequestAddress(AddressRequestDto addressRequestDto);
        Task<AddressRequestDetailsResponse> GetAddressRequestDetails();
    }
}
