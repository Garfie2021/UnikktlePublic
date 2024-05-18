using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnikktleMentor.Cache;
using UnikktleMentor.Common;
using UnikktleMentor.Models;
using UnikktleMentorEngine;

namespace UnikktleMentor.Common
{
    public static class SessionValue
    {
        //// 現在ページ
        //public static int GetCurrentPage(ISession session)
        //{
        //    var currentPage = session.GetInt32(SessionKey.CurrentPage);
        //    if (currentPage == null)
        //    {
        //        currentPage = 0;
        //        session.SetInt32(SessionKey.CurrentPage, (int)currentPage);
        //    }

        //    return (int)currentPage;
        //}

        // 回答
        public static List<InputModel_AnswerEdit> GetAnswerList(ISession session)
        {
            var list = session.Get<List<InputModel_AnswerEdit>>(SessionKey.AnswerList);

            if (list == null)
            {
                // 6ページ
                list = new List<InputModel_AnswerEdit>();
                list.Add(new InputModel_AnswerEdit());
                list.Add(new InputModel_AnswerEdit());
                list.Add(new InputModel_AnswerEdit());
                list.Add(new InputModel_AnswerEdit());
                list.Add(new InputModel_AnswerEdit());
                list.Add(new InputModel_AnswerEdit());

//#if DEBUG
//                foreach (var item in list)
//                {
//                    item.A0 = 回答選択肢.はい;
//                    item.A1 = 回答選択肢.はい;
//                    item.A2 = 回答選択肢.はい;
//                    item.A3 = 回答選択肢.はい;
//                    item.A4 = 回答選択肢.はい;
//                    item.A5 = 回答選択肢.はい;
//                    item.A6 = 回答選択肢.はい;
//                    item.A7 = 回答選択肢.はい;
//                    item.A8 = 回答選択肢.はい;
//                    item.A9 = 回答選択肢.はい;
//                    item.A10 = 回答選択肢.はい;
//                    item.A11 = 回答選択肢.はい;
//                    item.A12 = 回答選択肢.はい;
//                    item.A13 = 回答選択肢.はい;
//                    item.A14 = 回答選択肢.はい;
//                    item.A15 = 回答選択肢.はい;
//                    item.A16 = 回答選択肢.はい;
//                    item.A17 = 回答選択肢.はい;
//                    item.A18 = 回答選択肢.はい;
//                    item.A19 = 回答選択肢.はい;
//                }
//#endif
            }

            return list;
        }

        // 回答
        public static InputModel_Answer_Supplement GetAnswerSupplement(ISession session)
        {
            var answerSupplement = session.Get<InputModel_Answer_Supplement>(SessionKey.AnswerSupplement);

            if (answerSupplement == null)
            {
                answerSupplement = new InputModel_Answer_Supplement();
            }

            return answerSupplement;
        }

        public static void SetQuestionAnswerList(ISession session, int currentPage, InputModel_AnswerEdit input)
        {
            var list = GetAnswerList(session);

            // 対象の回答を置き換える。
            list[currentPage] = input;

            session.Set(SessionKey.AnswerList, list);
        }

    }
}
