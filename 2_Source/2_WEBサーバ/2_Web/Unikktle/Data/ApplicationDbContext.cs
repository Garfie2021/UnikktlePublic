#define TEST    // 技術検証用ソースコード

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Unikktle.Models;

namespace Unikktle.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // ※要注意
        // 各DbSetでジェネリック指定しているクラスは、「Id」項目が必須。
        // 項目名は「Id」固定。
        // データベース設計上は不要でも、「Id」に対応する項目が無いと ASP.NET Core が異常動作してハマる。


        // clt
        public DbSet<KeywordSearch> KeywordSearch { get; set; }
        public DbSet<Keyword> Keyword { get; set; }

        // dbo
        public DbSet<AspNetUser> AspNetUser { get; set; }

        // mst
        public DbSet<Mind> Mind { get; set; }
        public DbSet<MindJson> MindJson { get; set; }
        public DbSet<MindSearch> MindSearch { get; set; }
        public DbSet<Models.DataModel.Attribute> Attribute { get; set; }
        public DbSet<Career> Career { get; set; }

        // pay
        public DbSet<Credit> Credit { get; set; }

        // usr
        public DbSet<Adver> Adver { get; set; }
        public DbSet<AdverSearchSelect> AdverSearchSelect { get; set; }
        public DbSet<AdverRelationSelect> AdverRelationSelect { get; set; }
        public DbSet<AdverWordSearch> AdverWordSearch { get; set; }
        public DbSet<AdverWordRelation> AdverWordRelation { get; set; }
        public DbSet<AdverSelectPrSearch> AdverSelectPrSearch { get; set; }
        public DbSet<AdverSelectPrRelation> AdverSelectPrRelation { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<BusinessSelect> BusinessSelect { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<UnitPrice> UnitPrice { get; set; }
        public DbSet<UserSetting> UserSetting { get; set; }
        public DbSet<UserSettingProfile> UserSettingProfile { get; set; }

    }
}
