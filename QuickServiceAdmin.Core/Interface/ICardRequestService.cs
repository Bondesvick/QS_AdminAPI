using System.Threading.Tasks;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Interface
{
    public interface ICardRequestService
    {
        Task RequestCard(CardRequestParams cardRequestParams);
        public Task<CardRequestDetailsResponse> GetCardRequestDetails(string accountNumber);
    }
}