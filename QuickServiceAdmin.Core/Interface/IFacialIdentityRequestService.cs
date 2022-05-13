using System.Threading.Tasks;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Interface
{
    public interface IFacialIdentityRequestService
    {
           Task<FacialIdentityResponseData> RequestFacialIdentity(FacialIdentityRequestParams facialIdentityRequestParams);
    }
}