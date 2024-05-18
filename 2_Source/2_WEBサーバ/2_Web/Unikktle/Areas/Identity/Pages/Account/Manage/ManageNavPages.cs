using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Unikktle.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string UserSetting => "UserSetting";

        public static string Feedback => "Feedback";

        public static string Business => "Business";

        public static string Mind => "Mind";

        public static string Credit => "Credit";

        public static string ChangePassword => "ChangePassword";

        //public static string ExternalLogins => "ExternalLogins";

        public static string PersonalData => "PersonalData";

        //public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string UserSettingNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserSetting);

        public static string FeedbackNavClass(ViewContext viewContext) => PageNavClass(viewContext, Feedback);

        public static string BusinessNavClass(ViewContext viewContext) => PageNavClass(viewContext, Business);

        public static string MindNavClass(ViewContext viewContext) => PageNavClass(viewContext, Mind);

        public static string CreditNavClass(ViewContext viewContext) => PageNavClass(viewContext, Credit);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        //public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        //public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}