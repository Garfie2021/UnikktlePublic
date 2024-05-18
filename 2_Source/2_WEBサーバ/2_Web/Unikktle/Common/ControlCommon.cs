using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using Unikktle.Cache;

namespace Unikktle.Common
{
    public static class ControlCommon
    {
        public static async Task<long> GetUserNo(
            ApplicationDbContext dbContext, 
            UserManager<IdentityUser> userManager, ClaimsPrincipal User,
            ISession Session, IEmailSender emailSender)
        {
            (bool bad, IdentityUser user) = await AuthenticatBadAsync(userManager, User);
            if (bad)
            {
                // 未ログイン
                return -1;
            }

            var no2 = SessionCache.GetUserNo(dbContext, Session, emailSender, user.Id);
            if (no2 == null)
            {
                // 未ログイン
                return -1;
            }

            // ログイン済み
            return (long)no2;
        }

        public static AdverSelectPrRelation GetRelationPR(ApplicationDbContext dbContext,
            long relationWordId, long ClickUserNo, string ClickUserIP, int pageNum)
        {
            var pr = SP_AdverRelationWord.SelectPR(dbContext, relationWordId, ClickUserNo, ClickUserIP, pageNum);
            if (pr.Count < 1)
            {
                return new AdverSelectPrRelation();
            }
            else
            {
                return pr[0];
            }
        }

        public static AdverSelectPrSearch GetSearchPR(ApplicationDbContext dbContext,
            string searchString, long ClickUserNo, string ClickUserIP, int rowNum)
        {
            var pr = SP_AdverSearchWord.SelectPR(dbContext, searchString, ClickUserNo, ClickUserIP, rowNum);
            if (pr.Count < 1)
            {
                return null;
            }
            else
            {
                return pr[0];
            }
        }

        public static async Task<(bool, IdentityUser)> AuthenticatBadAsync(UserManager<IdentityUser> userManager, ClaimsPrincipal User)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return (true, null);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return (true, null);
            }

            return (false, user);
        }

        public static void SetAdverWord(HttpContext httpContext,
            string KeyCnt, string KeyId, string KeyWord, string KeyClickCost,
            long? id, string word, short clickCost)
        {
            int cnt;
            string cntTmp;
            if (id == null || id == 0)
            {
                cnt = (int)(httpContext.Session.GetInt32(KeyCnt) + 1);
                httpContext.Session.SetInt32(KeyCnt, cnt);
                cntTmp = cnt.ToString();
            }
            else
            {
                cnt = (int)(httpContext.Session.GetInt32(KeyCnt));
                httpContext.Session.SetInt32(KeyCnt, cnt);
                cntTmp = cnt.ToString();
            }

            httpContext.Session.SetString(KeyId + cntTmp, id.ToString());
            httpContext.Session.SetString(KeyWord + cntTmp, word);
            httpContext.Session.SetInt32(KeyClickCost + cntTmp, clickCost);
        }

        public static List<CareerCategory> GetCareer(//ISession session,
            ApplicationDbContext dbContext)
        {
            var CareerList = new List<CareerCategory>();

            var careerCategory = SP_Attribute.SelectClass(dbContext, AttributeClass.CareerCategory);

            foreach (var category in careerCategory)
            {
                var career = SP_Career.Select(dbContext, category.Id);

                CareerList.Add(new CareerCategory()
                {
                    Id = category.Id,
                    Name = category.Name,
                    CareerList = career
                });
            }

            return CareerList;
        }

        public static ExternalSearch GetExternalSearchURL(ExternalSearchEngine engine, string s)
        {
            var externalSearch = new ExternalSearch();

            if (engine == ExternalSearchEngine.Google)
            {
                externalSearch.Name = "Google search";

                if (Thread.CurrentThread.CurrentCulture.Name == "ja")
                {
                    externalSearch.URL = "https://www.google.co.jp/search?q=";
                }
                else
                {
                    externalSearch.URL = "https://www.google.com/search?q=";
                }
            }
            else if (engine == ExternalSearchEngine.Yahoo)
            {
                externalSearch.Name = "Yahoo search";

                if (Thread.CurrentThread.CurrentCulture.Name == "ja")
                {
                    externalSearch.URL = "https://search.yahoo.co.jp/search?p=";
                }
                else
                {
                    externalSearch.URL = "https://search.yahoo.com/search?p=";
                }
            }
            else
            {
                externalSearch.Name = "Bing search";
                externalSearch.URL = "https://www.bing.com/search?q=";
            }

            externalSearch.URL += HttpUtility.UrlEncode(s);

            return externalSearch;
        }

        public static SearchViewModel Search(ApplicationDbContext dbContext,
            string searchString, int pageNum)
        {
            List<KeywordSearch> list;
            long allCnt;

            list = SP_CollectKeyword.Select_Contains(dbContext, searchString,
                pageNum * Consts.OnePageNum, out allCnt);

            if (list.Count < 1)
            {
                list = SP_CollectKeyword.Select_Freetext(dbContext, searchString,
                    pageNum * Consts.OnePageNum, out allCnt);
            }

            return new SearchViewModel()
            {
                KeywordList = list,
                NextAvailable = NextAvailable(allCnt, pageNum)
            };
        }

        public static WordMapViewModel GetWordMapViewModel(ApplicationDbContext dbContext, 
            long no, int pageNum)
        {
            // 元ワードのワード
            var keyword = SP_CollectKeyword.SelectNo(dbContext, no)[0];

            var BaseWord = new RelatedKeyword()
            {
                Id = no,
                Word = keyword.Word,
                R_y = Consts.ParentRect_yStart,
                T_y = Consts.ParentText_yStart,
                R_w = keyword.r_w
            };

            var viewModel = new WordMapViewModel()
            {
                Id = no,
                R_x = 10,
                T_x = 20,
                Word = keyword.Word,
                BaseWord = BaseWord,
                SvgWordMap = GetSvgWordMap(dbContext, no, null, 10, BaseWord.R_w, pageNum)
            };

            return viewModel;
        }

        public static SvgWordMap GetSvgWordMap(ApplicationDbContext dbContext, 
            long no, long? ExcludeNo, int 元Keyword_r_x, int 元Keyword_r_w, int pageNum)
        {
            var x2 = 元Keyword_r_x + 元Keyword_r_w + 30;

            var svgWordMap = new SvgWordMap()
            {
                // 1列目と2列目連結
                Line = new Line()
                {
                    x1 = 元Keyword_r_x + 元Keyword_r_w,
                    y1 = Consts.Line_y,
                    x2 = x2,
                    y2 = Consts.Line_y
                },
                R_x = x2 + 20,
                T_x = x2 + 30
            };

            // 2列目に表示するワードリスト
            svgWordMap.WordList = GetSvgWordMap_List(dbContext,
                no, ExcludeNo, pageNum, 
                out bool nextAvailable);

            // Nextボタンの有無
            svgWordMap.NextAvailable = nextAvailable;


            return svgWordMap;
        }

        public static void 表示位置領域計算(WordMapViewModel viewModel)
        {
            //int yCnt = 0;
            //if (svgWordMap.AdverSelectPrRelation != null)
            //{
            //    yCnt++;
            //}
            // 広告が必ず表示される前提で、１つ分の領域を必ず空ける。
            // Google広告の表示が不可能な場合、元に戻す。
            int yCnt;
            if (viewModel.SvgWordMap.AdverSelectPrRelation.Id > 0)
            {
                yCnt = 1;
            }
            else
            {
                yCnt = 0;
            }

            foreach (var word in viewModel.SvgWordMap.WordList)
            {
                word.R_y = Consts.ChildRect_yStart + (Consts.y間隔 * yCnt);
                word.T_y = Consts.ChildText_yStart + (Consts.y間隔 * yCnt);

                yCnt++;
            }

            // Nextボタンのy座標
            if (!viewModel.SvgWordMap.NextAvailable)
            {
                yCnt--;
            }
            viewModel.SvgWordMap.Next_r_y = Consts.ChildRect_yStart + (Consts.y間隔 * yCnt);
            viewModel.SvgWordMap.Next_t_y = Consts.ChildText_yStart + (Consts.y間隔 * yCnt);

            // 2列目の大枠に基本領域を初期値で設定。（関連キーワードが0件 and 広告0件）
            viewModel.SvgWordMap.RectBorder = new Rect()
            {
                x = viewModel.SvgWordMap.Line.x2,
                y = Consts.ParentRect_yStart,
                w = 40, // 余白
                h = 30  // 余白
            };

            // 2列目の大枠に関連ワードの表示領域を足す。
            if (viewModel.SvgWordMap.WordList.Count() > 0)
            {
                viewModel.SvgWordMap.RectBorder.w = viewModel.SvgWordMap.RectBorder.w + viewModel.SvgWordMap.WordList.Max(x => x.R_w);
                viewModel.SvgWordMap.RectBorder.h = viewModel.SvgWordMap.RectBorder.h + viewModel.SvgWordMap.Next_r_y;
            }

            // 2列目の大枠に広告の表示領域を足す。
            if (viewModel.SvgWordMap.AdverSelectPrRelation.Id > 0)
            {
                viewModel.SvgWordMap.A_t_y_1 = Consts.PrText_yStart;
                viewModel.SvgWordMap.A_t_y_2 = viewModel.SvgWordMap.A_t_y_1 + 20;

                if (viewModel.SvgWordMap.RectBorder.w < viewModel.SvgWordMap.AdverSelectPrRelation.AdverTitle_r_w)
                {
                    viewModel.SvgWordMap.RectBorder.w = viewModel.SvgWordMap.AdverSelectPrRelation.AdverTitle_r_w;
                }
            }

            // SVG領域を設定。
            viewModel.SvgWordMap.Svg = new Svg()
            {
                w = viewModel.SvgWordMap.RectBorder.x + viewModel.SvgWordMap.RectBorder.w + 100,
                h = viewModel.SvgWordMap.RectBorder.y + viewModel.SvgWordMap.RectBorder.h + 100
            };
        }

        public static SvgWordMapJS GetSvgWordMapJS(ApplicationDbContext dbContext,
            long no, long? ExcludeNo, int pageNum)
        {
            var svgWordMapJS = new SvgWordMapJS
            {
                WordId = no,
                WordList = GetSvgWordMapJS_List(dbContext,
                    no, ExcludeNo, pageNum,
                    out int yCnt, out bool nextAvailable),

                NextAvailable = nextAvailable
            };

            return svgWordMapJS;
        }

        public static List<RelatedKeyword> GetSvgWordMap_List(ApplicationDbContext dbContext,
            long no, long? ExcludeNo, int pageNum,
            out bool nextAvailable)
        {
            var wordList = new List<RelatedKeyword>();

            long allCnt;
            foreach (var keyword in SP_CollaborateKeyword.Select(dbContext,
                no, ExcludeNo, pageNum * Consts.OnePageNum, out allCnt))
            {
                // 2列目
                wordList.Add(new RelatedKeyword()
                {
                    Id = keyword.Id,
                    Word = keyword.Word,
                    R_w = keyword.r_w
                });
            }

            nextAvailable = NextAvailable(allCnt, pageNum);

            return wordList;
        }

        public static List<RelatedKeywordJS> GetSvgWordMapJS_List(ApplicationDbContext dbContext,
            long no, long? ExcludeNo, int pageNum,
            out int yCnt, out bool nextAvailable)
        {
            yCnt = 0;
            var wordList = new List<RelatedKeywordJS>();

            long allCnt;
            foreach (var keyword in SP_CollaborateKeyword.Select(dbContext,
                no, ExcludeNo, pageNum * Consts.OnePageNum, out allCnt))
            {
                // 2列目
                wordList.Add(new RelatedKeywordJS()
                {
                    Id = keyword.Id,
                    Word = keyword.Word,
                    R_w = keyword.r_w
                });

                yCnt++;
            }

            nextAvailable = NextAvailable(allCnt, pageNum);

            return wordList;
        }

        public static bool NextAvailable(long allCnt, int pageNum)
        {
            return allCnt > pageNum * Consts.OnePageNum + Consts.OnePageNum ? true : false;
        }

        // 全て半角だと英文だと見なす
        public static 全角半角 全角半角判定(string str)
        {
            var byte_data = Encoding.UTF8.GetBytes(str);
            if (byte_data.Length == str.Length)
            {
                return 全角半角.半角のみ;
            }
            else
            {
                return 全角半角.全角含む;
            }
        }

    }
}
