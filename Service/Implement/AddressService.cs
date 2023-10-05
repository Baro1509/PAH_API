using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DataAccess;
using DataAccess.Models;

namespace Service.Implement {
    public class AddressService : IAddressService {
        private readonly IAddressDAO _addressDAO;

        public AddressService(IAddressDAO addressDAO) {
            _addressDAO = addressDAO;
        }

        public void Create(Address address) {
            _addressDAO.Create(address);
        }

        public void Delete(int addressId) {
            
        }

        public Address Get(int addressId) {
            return _addressDAO.Get(addressId);
        }

        public List<Address> GetByCustomerId(int customerId) {
            return _addressDAO.GetByCustomerId(customerId).ToList();
        }

        public void Update(Address address) {
            var db = _addressDAO.Get(address.Id);

            if (db == null) {
                throw new Exception("404: Address not found");
            }

            db.RecipientName = address.RecipientName;
            db.RecipientPhone = address.RecipientPhone;
            db.Province = address.Province;
            db.District = address.District;
            db.DistrictId = address.DistrictId;
            db.Ward = address.Ward;
            db.WardCode = address.WardCode;
            db.Street = address.Street;
            db.UpdatedAt = DateTime.Now;

            _addressDAO.Update(db);
        }
    }
}
