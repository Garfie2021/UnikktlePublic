using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Common;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using UnikktleMentorEngine;

namespace UnikktleMentor.Cache
{
    // キャッシュ処理レイヤークラス
    public static class InMemoryCache
    {

        public static List<CareerCategory> GetCareerAndCategoryList(ApplicationDbContext dbContext, IMemoryCache cache)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "ja")
            {
                // キャッシュ有無をチェック
                if (cache.TryGetValue(SessionKey.CareerAndCategoryList_JA, out List<CareerCategory> careerList))
                {
                    // キャッシュが存在する場合はキャッシュから返す
                    return careerList;
                }

                careerList = ControlCommon.GetCareerAndCategoryList_JA(dbContext);

                cache.CreateEntry(SessionKey.CareerAndCategoryList_JA)
                    //.SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                    .SetSize(1)                                     // オブジェクト数
                    .SetPriority(CacheItemPriority.NeverRemove)     // キャッシュの優先順位
                    .SetValue(careerList)                         // キャッシュするオブジェクトを設定
                    .Dispose();                                     // キャッシュ反映

                return careerList;
            }
            else
            {
                // キャッシュ有無をチェック
                if (cache.TryGetValue(SessionKey.CareerAndCategoryList_EN, out List<CareerCategory> careerList))
                {
                    // キャッシュが存在する場合はキャッシュから返す
                    return careerList;
                }

                careerList = ControlCommon.GetCareerAndCategoryList_EN(dbContext);

                cache.CreateEntry(SessionKey.CareerAndCategoryList_EN)
                    //.SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                    .SetSize(1)                                     // オブジェクト数
                    .SetPriority(CacheItemPriority.NeverRemove)     // キャッシュの優先順位
                    .SetValue(careerList)                         // キャッシュするオブジェクトを設定
                    .Dispose();                                     // キャッシュ反映

                return careerList;
            }
        }

        public static List<Career> GetCareerAllList(ApplicationDbContext dbContext, IMemoryCache cache)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "ja")
            {
                // キャッシュ有無をチェック
                if (cache.TryGetValue(SessionKey.CareerAllList_JA, out List<Career> careerList))
                {
                    // キャッシュが存在する場合はキャッシュから返す
                    return careerList;
                }

                careerList = ControlCommon.GetCareerAllList_JA(dbContext);

                cache.CreateEntry(SessionKey.CareerAllList_JA)
                    //.SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                    .SetSize(1)                                     // オブジェクト数
                    .SetPriority(CacheItemPriority.NeverRemove)     // キャッシュの優先順位
                    .SetValue(careerList)                         // キャッシュするオブジェクトを設定
                    .Dispose();                                     // キャッシュ反映

                return careerList;
            }
            else
            {
                // キャッシュ有無をチェック
                if (cache.TryGetValue(SessionKey.CareerAllList_EN, out List<Career> careerList))
                {
                    // キャッシュが存在する場合はキャッシュから返す
                    return careerList;
                }

                careerList = ControlCommon.GetCareerAllList_EN(dbContext);

                cache.CreateEntry(SessionKey.CareerAllList_EN)
                    //.SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                    .SetSize(1)                                     // オブジェクト数
                    .SetPriority(CacheItemPriority.NeverRemove)     // キャッシュの優先順位
                    .SetValue(careerList)                         // キャッシュするオブジェクトを設定
                    .Dispose();                                     // キャッシュ反映

                return careerList;
            }
        }


        public static QuestionViewModel Question(IMemoryCache cache)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "ja")
            {
                // キャッシュ有無をチェック
                if (cache.TryGetValue(SessionKey.QuestionListKey_JA, out QuestionViewModel data))
                {
                    // キャッシュが存在する場合はキャッシュから返す
                    return data;
                }

                var model = new QuestionViewModel() { QuestionList = SetsumonList.GetSetsumonList_JA() };

                cache.CreateEntry(SessionKey.QuestionListKey_JA)
                    //.SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                    .SetSize(1)                                     // オブジェクト数
                    .SetPriority(CacheItemPriority.NeverRemove)     // キャッシュの優先順位
                    .SetValue(model)                                // キャッシュするオブジェクトを設定
                    .Dispose();                                     // キャッシュ反映

                return model;
            }
            else
            {
                // キャッシュ有無をチェック
                if (cache.TryGetValue(SessionKey.QuestionListKey_EN, out QuestionViewModel data))
                {
                    // キャッシュが存在する場合はキャッシュから返す
                    return data;
                }

                var model = new QuestionViewModel() { QuestionList = SetsumonList.GetSetsumonList_EN() };

                cache.CreateEntry(SessionKey.QuestionListKey_EN)
                    //.SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                    .SetSize(1)                                     // オブジェクト数
                    .SetPriority(CacheItemPriority.NeverRemove)     // キャッシュの優先順位
                    .SetValue(model)                                // キャッシュするオブジェクトを設定
                    .Dispose();                                     // キャッシュ反映

                return model;
            }
        }

    }
}
