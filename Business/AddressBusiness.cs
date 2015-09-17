using System.Linq;
using Negocio.AddressService;
using System.Collections.Generic;
using Comum.Exceptions;

namespace Negocio
{
    public interface IAddressBusiness
    {
        List<string> GetStates();
        List<string> GetCitiesByState(string uf);
    }

    public class AddressBusiness : IAddressBusiness
    {
        public List<string> GetStates()
        {
            const int INDEX = 0;
            var ufs = new List<string>();

            try
            {
                using (var service = new cidadesSoapClient())
                {
                    var states = service.RETORNA_ESTADOS();
                    ufs.AddRange(from object state in states.Tables[0].Rows select GetDataRowValueFor(state, INDEX));
                }
            }
            catch
            {
                throw new UnavailableWebServiceException();
            }

            return ufs;
        }

        public List<string> GetCitiesByState(string uf)
        {
            const int INDEX = 1;
            var cities = new List<string>();
            try
            {
                using (var service = new cidadesSoapClient())
                {
                    var values = service.RETORNA_CIDADES_ESTADO(uf);
                    cities.AddRange(from object city in values.Tables[0].Rows select GetDataRowValueFor(city, INDEX));
                }
            }
            catch
            {
                throw new UnavailableWebServiceException();
            }

            return cities;
        }

        private static string GetDataRowValueFor(object obj, int arrayPosition)
        {
            return ((System.Data.DataRow)obj).ItemArray[arrayPosition].ToString();
        }
    }
}
