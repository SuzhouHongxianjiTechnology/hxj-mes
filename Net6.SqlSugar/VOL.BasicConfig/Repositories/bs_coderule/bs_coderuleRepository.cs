/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹bs_coderuleRepository编写代码
 */
using VOL.BasicConfig.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.DbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.BasicConfig.Repositories
{
    public partial class bs_coderuleRepository : RepositoryBase<bs_coderule>
    , Ibs_coderuleRepository
    {
    public bs_coderuleRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static Ibs_coderuleRepository Instance
    {
    get {  return AutofacContainerModule.GetService<Ibs_coderuleRepository>
        (); } }
        }
        }
