/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹dv_machineryRepository编写代码
 */
using VOL.DeviceManager.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.DbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Repositories
{
    public partial class dv_machineryRepository : RepositoryBase<dv_machinery>
    , Idv_machineryRepository
    {
    public dv_machineryRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static Idv_machineryRepository Instance
    {
    get {  return AutofacContainerModule.GetService<Idv_machineryRepository>
        (); } }
        }
        }
