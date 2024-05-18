using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPalListen.Common;
using UnikktleCommon;

namespace TestPayPalListen
{
    [TestClass]
    public class Test1PayPalCommon
    {
        [TestMethod]
        public void TestDecoding_�Z������()
        {
            try
            {
                // .Net Core �� "Shift_JIS"�ϊ�����ɂ́A�G���R�[�h�v���o�C�_�[�̓o�^���K�v
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var encoding = Encoding.GetEncoding("shift_jis");
                encoding = Encoding.GetEncoding("Shift_JIS");

                var SandboxIPN��M�f�[�^ = @"mc_gross=1000&protection_eligibility=Eligible&payer_id=GG33K42VH2X2S&payment_date=05%3A50%3A34+Aug+25%2C+2019+PDT&payment_status=Completed&charset=Shift_JIS&first_name=xxx&option_selection1=1000+Credit&mc_fee=76&notify_version=3.9&custom=&payer_status=verified&business=xxx%40xxx&quantity=1&verify_sign=ADYqdxYv8fWSbE7hxfs0xkToFz92ADQQxq-yZLTfO5NosYQGvEGXRXtu&xxx&option_name1=Credit&txn_id=6KS76434V47096732&payment_type=instant&last_name=%8D%B2%93%A1&receiver_email=xxx%40xxx&payment_fee=&shipping_discount=0&receiver_id=xxx&insurance_amount=0&txn_type=web_accept&item_name=Credit&discount=0&mc_currency=JPY&item_number=xxx&residence_country=JP&test_ipn=1&shipping_method=Default&transaction_subject=&payment_gross=&ipn_track_id=1842da133e033";

                var a = PayPalCommon.Decoding(SandboxIPN��M�f�[�^);

            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }

        [TestMethod]
        public void TestDecoding_�Z���L��()
        {
            try
            {
                // .Net Core �� "Shift_JIS"�ϊ�����ɂ́A�G���R�[�h�v���o�C�_�[�̓o�^���K�v
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var encoding = Encoding.GetEncoding("shift_jis");
                encoding = Encoding.GetEncoding("Shift_JIS");

                // �Z���L��B�@���u�ڋq�̔z����Z�����K�v�ł����F �͂��v
                var SandboxIPN��M�f�[�^ = @"mc_gross=1000&protection_eligibility=Eligible&address_status=confirmed&payer_id=GG33K42VH2X2S&address_street=Nishi+4-chome%2C+Kita+55-jo%2C+Kita-ku&payment_date=05%3A49%3A24+Aug+23%2C+2019+PDT&payment_status=Completed&charset=Shift_JIS&address_zip=xxx&first_name=xxx&option_selection1=1000+Credit&mc_fee=76&address_country_code=JP&address_name=%8D%B2%93%A1+xxx&notify_version=3.9&custom=&payer_status=verified&business=xxx%40xxx&address_country=Japan&address_city=xxx&quantity=1&verify_sign=A05WP2oX1s0Fm5iMzcHYC5mvySGjAoBxjmf1.1TIY-rAl3sXhgQkX54Q&xxx&option_name1=Credit&txn_id=9GS413983Y164064V&payment_type=instant&last_name=%8D%B2%93%A1&address_state=xxx&receiver_email=xxx%40xxx&payment_fee=&shipping_discount=0&insurance_amount=0&receiver_id=xxx&txn_type=web_accept&item_name=Credit&discount=0&mc_currency=JPY&item_number=c1&residence_country=JP&test_ipn=1&shipping_method=Default&transaction_subject=&payment_gross=&ipn_track_id=de459dacf618";

                var a = PayPalCommon.Decoding(SandboxIPN��M�f�[�^);

            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }
    }
}
