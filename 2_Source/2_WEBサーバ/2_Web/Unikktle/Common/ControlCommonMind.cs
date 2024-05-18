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
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Unikktle.Common
{
    public static class ControlCommonMind
    {
        //public static void CulcWidthHeight(List<MindRow_Rect> mindRows, out int width, out int height)
        //{
        //    width = 0;
        //    height = 0;

        //    foreach (var row in mindRows)
        //    {
        //        if (width < row.X1 + row.Width)
        //        {
        //            width = row.X1 + row.Width;
        //        }

        //        //if (width < row.X2 + row.Width)
        //        //{
        //        //    width = row.X2 + row.Width;
        //        //}

        //        if (height < row.Y1 + row.Height)
        //        {
        //            height = row.Y1 + row.Height;
        //        }

        //        //if (height < row.Y2 + row.Height)
        //        //{
        //        //    height = row.Y2 + row.Height;
        //        //}
        //    }

        //    width += 100;
        //    height += 100;
        //}


        public static MindSearchViewModel Search(ApplicationDbContext dbContext,
            string searchString, int pageNum, long createUserNo)
        {
            List<MindSearch> list;
            long allCnt;

            list = SP_Mind.Select_Contains(dbContext, searchString,
                pageNum * Consts.OnePageNum, createUserNo, out allCnt);

            if (list.Count < 1)
            {
                list = SP_Mind.Select_Freetext(dbContext, searchString,
                    pageNum * Consts.OnePageNum, createUserNo, out allCnt);
            }

            return new MindSearchViewModel()
            {
                MindList = list,
                NextAvailable = NextAvailable(allCnt, pageNum)
            };
        }

        public static Mind Mind(ApplicationDbContext dbContext, long i)
        {
            var mind = SP_Mind.SelectNo(dbContext, i);

            if (mind.Count() < 1)
            {
                return null;
            }
            else
            {
                return mind[0];
            }
        }

        public static string MindJson(ApplicationDbContext dbContext, long i)
        {
            var mind = SP_Mind.SelectNo_JsonViewModel(dbContext, i);

            if (mind.Count() < 1)
            {
                return null;
            }
            else
            {
                return mind[0].JsonViewModel;
            }
        }


        private static bool NextAvailable(long allCnt, int pageNum)
        {
            return allCnt > pageNum * Consts.OnePageNum + Consts.OnePageNum ? true : false;
        }
    }
}
