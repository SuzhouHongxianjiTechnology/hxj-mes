/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹tm_toolRepository编写代码
 */
using VOL.DeviceManager.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.DbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Repositories
{
    public partial class tm_toolRepository : RepositoryBase<tm_tool>
    , Itm_toolRepository
    {
    public tm_toolRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static Itm_toolRepository Instance
    {
    get {  return AutofacContainerModule.GetService<Itm_toolRepository>
        (); } }
        }
        }
