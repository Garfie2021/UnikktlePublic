using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayPalListen.Models;


namespace PayPalListen.Data
{
    public static class SP_PayPalIPN
    {
        public static long GetCount(ApplicationDbContext dbContext, PayPalIPN payPalIPN)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Cnt",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE pay.spPayPalIPN_GetCount @txn_id, @payer_email, @Cnt OUTPUT",
                new SqlParameter("@txn_id", payPalIPN.txn_id),
                new SqlParameter("@payer_email", payPalIPN.payer_email),
                outPara);

            return (int)outPara.Value;
        }


        public static long Insert(ApplicationDbContext dbContext,
            long PayPalIPNlogNo, DateTime RegisteredDate, PayPalIPN payPalIPN)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@No",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE pay.spPayPalIPN_Insert @PayPalIPNlogNo, @RegisteredDate, @mc_gross, @protection_eligibility, @address_status, @payer_id, @address_street, @payment_date, @payment_status, @charset, @address_zip, @first_name, @option_selection1, @mc_fee, @address_country_code, @address_name, @notify_version, @custom, @payer_status, @business, @address_country, @address_city, @quantity, @verify_sign, @payer_email, @option_name1, @txn_id, @payment_type, @last_name, @address_state, @receiver_email, @payment_fee, @shipping_discount, @insurance_amount, @receiver_id, @txn_type, @item_name, @discount, @mc_currency, @item_number, @residence_country, @test_ipn, @shipping_method, @transaction_subject, @payment_gross, @ipn_track_id, @No OUT",
                new SqlParameter("@PayPalIPNlogNo", PayPalIPNlogNo),
                new SqlParameter("@RegisteredDate", RegisteredDate),
                new SqlParameter("@mc_gross", payPalIPN.mc_gross),
                new SqlParameter("@protection_eligibility", payPalIPN.protection_eligibility),
                new SqlParameter("@address_status", payPalIPN.address_status),
                new SqlParameter("@payer_id", payPalIPN.payer_id),
                new SqlParameter("@address_street", payPalIPN.address_street),
                new SqlParameter("@payment_date", payPalIPN.payment_date),
                new SqlParameter("@payment_status", payPalIPN.payment_status),
                new SqlParameter("@charset", payPalIPN.charset),
                new SqlParameter("@address_zip", payPalIPN.address_zip),
                new SqlParameter("@first_name", payPalIPN.first_name),
                new SqlParameter("@option_selection1", payPalIPN.option_selection1),
                new SqlParameter("@mc_fee", payPalIPN.mc_fee),
                new SqlParameter("@address_country_code", payPalIPN.address_country_code),
                new SqlParameter("@address_name", payPalIPN.address_name),
                new SqlParameter("@notify_version", payPalIPN.notify_version),
                new SqlParameter("@custom", payPalIPN.custom),
                new SqlParameter("@payer_status", payPalIPN.payer_status),
                new SqlParameter("@business", payPalIPN.business),
                new SqlParameter("@address_country", payPalIPN.address_country),
                new SqlParameter("@address_city", payPalIPN.address_city),
                new SqlParameter("@quantity", payPalIPN.quantity),
                new SqlParameter("@verify_sign", payPalIPN.verify_sign),
                new SqlParameter("@payer_email", payPalIPN.payer_email),
                new SqlParameter("@option_name1", payPalIPN.option_name1),
                new SqlParameter("@txn_id", payPalIPN.txn_id),
                new SqlParameter("@payment_type", payPalIPN.payment_type),
                new SqlParameter("@last_name", payPalIPN.last_name),
                new SqlParameter("@address_state", payPalIPN.address_state),
                new SqlParameter("@receiver_email", payPalIPN.receiver_email),
                new SqlParameter("@payment_fee", payPalIPN.payment_fee),
                new SqlParameter("@shipping_discount", payPalIPN.shipping_discount),
                new SqlParameter("@insurance_amount", payPalIPN.insurance_amount),
                new SqlParameter("@receiver_id", payPalIPN.receiver_id),
                new SqlParameter("@txn_type", payPalIPN.txn_type),
                new SqlParameter("@item_name", payPalIPN.item_name),
                new SqlParameter("@discount", payPalIPN.discount),
                new SqlParameter("@mc_currency", payPalIPN.mc_currency),
                new SqlParameter("@item_number", payPalIPN.item_number),
                new SqlParameter("@residence_country", payPalIPN.residence_country),
                new SqlParameter("@test_ipn", payPalIPN.test_ipn),
                new SqlParameter("@shipping_method", payPalIPN.shipping_method),
                new SqlParameter("@transaction_subject", payPalIPN.transaction_subject),
                new SqlParameter("@payment_gross", payPalIPN.payment_gross),
                new SqlParameter("@ipn_track_id", payPalIPN.ipn_track_id),
                outPara
                );

            return (long)outPara.Value;
        }
    }
}
