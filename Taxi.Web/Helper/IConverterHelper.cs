using Taxi.Common.Models;
using Taxi.Web.Data.Entities;

namespace Taxi.Web.Helper
{
    public interface IConverterHelper
    {
        TaxiResponse ToTaxiResponse(TaxiEntity taxiEntity);
    }
}
