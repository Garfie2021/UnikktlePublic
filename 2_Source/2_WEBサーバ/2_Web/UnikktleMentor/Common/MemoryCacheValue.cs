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

namespace UnikktleMentor.Pages.Home
{
    public static class MemoryCacheValue
    {
        // 設問
        public static List<Question> GetQuestionList(IMemoryCache cache, int currentPage)
        {
            if (currentPage < 6)
            {
                var start = currentPage * 20;

                var questionList = InMemoryCache.Question(cache).QuestionList;
                return questionList.GetRange(start, 20);

                //var list = HttpContext.Session.Get<List<InputModel_MindEdit>>(SessionKey.AnswerList);
            }
            else
            {
                return new List<Question>();
            }
        }

    }
}
