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
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace Unikktle.Cache
{
    // キャッシュ処理レイヤークラス
    public static class InMemoryCache
    {
        public static long KeywordCount(ApplicationDbContext dbContext, IMemoryCache cache)
        {
            // キャッシュ有無をチェック
            if (cache.TryGetValue("KeywordCount", out long keywordCount))
            {
                // キャッシュが存在する場合はキャッシュから返す
                return keywordCount;
            }

            keywordCount = SP_CollectKeyword.GetCount(dbContext);

            cache.CreateEntry("KeywordCount")
                //.SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                .SetSize(1)                                     // オブジェクト数
                .SetPriority(CacheItemPriority.NeverRemove)     // キャッシュの優先順位
                .SetValue(keywordCount)                         // キャッシュするオブジェクトを設定
                .Dispose();                                     // キャッシュ反映

            return keywordCount;
        }

        public static List<CareerCategory> CareerList(ApplicationDbContext dbContext, IMemoryCache cache)
        {
            // キャッシュ有無をチェック
            if (cache.TryGetValue("CareerList", out List<CareerCategory> careerList))
            {
                // キャッシュが存在する場合はキャッシュから返す
                return careerList;
            }

            careerList = ControlCommon.GetCareer(dbContext);

            cache.CreateEntry("CareerList")
                //.SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                .SetSize(1)                                     // オブジェクト数
                .SetPriority(CacheItemPriority.NeverRemove)     // キャッシュの優先順位
                .SetValue(careerList)                         // キャッシュするオブジェクトを設定
                .Dispose();                                     // キャッシュ反映

            return careerList;
        }

        public static WordMapViewModel GetWordMapViewModel(
            ApplicationDbContext dbContext, IMemoryCache cache,
            long no, int pageNum)
        {
            // キャッシュ有無をチェック
            if (cache.TryGetValue("Map0-" + no.ToString(), 
                out WordMapViewModel data))
            {
                // キャッシュが存在する場合はキャッシュから返す
                return data;
            }

            var model = ControlCommon.GetWordMapViewModel(dbContext, no, pageNum);

            if (model.SvgWordMap.WordList.Count() > 0)
            {
                cache.CreateEntry("Map0-" + no.ToString())
                .SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                .SetSize(1)                                     // オブジェクト数
                .SetPriority(CacheItemPriority.High)            // キャッシュの優先順位
                .SetValue(model)                                // キャッシュするオブジェクトを設定
                .Dispose();                                     // キャッシュ反映
            }

            return model;
        }

        public static SvgWordMapJS GetSvgWordMapJS(
            ApplicationDbContext dbContext, IMemoryCache cache,
            long no, long? ExcludeNo, int pageNum)
        {
            // キャッシュ有無をチェック
            if (cache.TryGetValue("Map-" + no.ToString() + "-" + ExcludeNo.ToString() + "-" + pageNum.ToString(),
                out SvgWordMapJS data))
            {
                // キャッシュが存在する場合はキャッシュから返す
                return data;
            }

            var model = ControlCommon.GetSvgWordMapJS(dbContext, no, ExcludeNo, pageNum);

            if (model.WordList.Count() > 0)
            {
                cache.CreateEntry("Map-" + no.ToString() + "-" + ExcludeNo.ToString() + "-" + pageNum.ToString())
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                    .SetSize(1)                                     // オブジェクト数
                    .SetPriority(pageNum == 0 ? CacheItemPriority.High : CacheItemPriority.Normal)            // キャッシュの優先順位
                    .SetValue(model)                                // キャッシュするオブジェクトを設定
                    .Dispose();                                     // キャッシュ反映
            }

            return model;
        }

        public static SearchViewModel Search(
            ApplicationDbContext dbContext, IMemoryCache cache,
            string searchString, int pageNum)
        {
            // キャッシュ有無をチェック
            if (cache.TryGetValue("Search-" + searchString + "-" + pageNum.ToString(),
                out SearchViewModel data))
            {
                // キャッシュが存在する場合はキャッシュから返す
                return data;
            }

            var model = ControlCommon.Search(dbContext, searchString, pageNum);
            model.SearchWordId = SP_SearchWord.SearchWord_GetNoWithInsert(dbContext, searchString);

            if (model.KeywordList.Count() > 0)
            {
                cache.CreateEntry("Search-" + searchString + "-" + pageNum.ToString())
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1))    // キャッシュの生存期間は1日。データの更新頻度とリクエストが少ないオブジェクトが残り続けてメモリを圧迫しない範囲の兼ね合い。
                    .SetSize(1)                                     // オブジェクト数
                    .SetPriority(pageNum == 0 ? CacheItemPriority.High : CacheItemPriority.Normal)            // キャッシュの優先順位
                    .SetValue(model)                                // キャッシュするオブジェクトを設定
                    .Dispose();                                     // キャッシュ反映
            }

            return model;
        }

    }
}
