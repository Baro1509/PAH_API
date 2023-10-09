using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implement {
    public class TransactionDAO : DataAccessBase<Transaction>, ITransactionDAO {
        public TransactionDAO(PlatformAntiquesHandicraftsContext context) : base(context) {
        }

        public IQueryable<Transaction> GetByUserId(int userId) {
            return GetAll().Where(p => p.Id == userId);
        }
    }
}
