#define TEST    // 技術検証用ソースコード

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Models;
using UnikktleMentorEngine;

namespace UnikktleMentor.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // ※要注意
        // 各DbSetでジェネリック指定しているクラスは、「{ get; set; }」プロパティ付きの「Id」項目が必須。
        // 項目名は「Id」固定。
        // データベース設計上は不要でも、「Id」に対応する項目が無いと ASP.NET Core が異常動作してハマる。



        public DbSet<Team> TeamSelect { get; set; }

        // hst
        public DbSet<AnswerHistory> AnswerHistory { get; set; }
        public DbSet<AnswerHistoryNoLogin> AnswerHistoryNoLogin { get; set; }
        public DbSet<AnswerDetail> AnswerDetailSelect { get; set; }
        public DbSet<AnswerDetailSupplement> AnswerDetailSupplementSelect { get; set; }

        //public DbSet<C01粗点> C01粗点Select { get; set; }

        public DbSet<C02系統値> C02系統値Select { get; set; }

        public DbSet<C02系統値_TeamUser> C02系統値_TeamUser_Select { get; set; }


        // dbo
        public DbSet<AspNetUser> AspNetUser { get; set; }

        // mst
        public DbSet<Models.DataModel.Attribute> Attribute { get; set; }
        public DbSet<Career> Career { get; set; }

        // usr
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<TeamUser> TeamUser { get; set; }
        public DbSet<Team_TeamID> Team_TeamID { get; set; }
        public DbSet<UserSetting> UserSettingProfile { get; set; }

    }
}
