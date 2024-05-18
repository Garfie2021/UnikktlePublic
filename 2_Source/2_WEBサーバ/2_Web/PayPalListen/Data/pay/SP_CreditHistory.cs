using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PayPalListen.Data
{
    public static class SP_CreditHistory
    {
        public static void Insert(ApplicationDbContext dbContext,
            long userNo, long payPalIpnNo, DateTime registeredDate, string txn_id, int addCredit)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE UnikktleWeb.pay.spCreditHistory_Insert @UserNo, @PayPalIpnNo, @RegisteredDate, @txn_id, @AddCredit",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@PayPalIpnNo", payPalIpnNo),
                new SqlParameter("@RegisteredDate", registeredDate),
                new SqlParameter("@txn_id", txn_id),
                new SqlParameter("@AddCredit", addCredit)
                );
        }
    }
}
