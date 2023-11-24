/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹dv_repairRepository编写代码
 */
using VOL.DeviceManager.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.DbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Repositories
{
    public partial class dv_repairRepository : RepositoryBase<dv_repair>
    , Idv_repairRepository
    {
    public dv_repairRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static Idv_repairRepository Instance
    {
    get {  return AutofacContainerModule.GetService<Idv_repairRepository>
        (); } }
        }
        }
