/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹tm_tool_returnRepository编写代码
 */
using VOL.DeviceManager.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.DbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Repositories
{
    public partial class tm_tool_returnRepository : RepositoryBase<tm_tool_return>
    , Itm_tool_returnRepository
    {
    public tm_tool_returnRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static Itm_tool_returnRepository Instance
    {
    get {  return AutofacContainerModule.GetService<Itm_tool_returnRepository>
        (); } }
        }
        }
