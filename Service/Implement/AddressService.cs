using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Service.Implement {
    public class AddressService : IAddressService {
        private readonly HttpClient _httpClient;

        public AddressService(IHttpClientFactory factory) {
            _httpClient = factory.CreateClient("GHN");
        }
    }
}
