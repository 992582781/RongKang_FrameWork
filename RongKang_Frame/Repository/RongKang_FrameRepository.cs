using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RongKang_Entity;
using RongKang_ViewModel;


namespace Repository
{
    public class RongKang_FrameRepository : DbContext
    {
        public RongKang_FrameRepository()
            : base("RongKang_FrameRepository")
        {

        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public DbSet<Module> Modules { get; set; }
        public DbSet<Switch> Switchs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<RoleModule> RoleModules { get; set; }

        public DbSet<UserModule> UserModules { get; set; }

        public DbSet<ViewModule> ViewModules { get; set; }
        public DbSet<ViewRole> ViewRoles { get; set; }

        public DbSet<ProvincialRegion> ProvincialRegions { get; set; }
        public DbSet<YearBudget> YearBudgets { get; set; }
        public DbSet<BranchOffice> BranchOffices { get; set; }

        public DbSet<BranchOfficeYearBudget> BranchOfficeYearBudgets { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ReimbursementRecord> ReimbursementRecords { get; set; }

    }
}
