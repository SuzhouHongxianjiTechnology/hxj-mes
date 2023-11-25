/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹dv_dss_recordRepository编写代码
 */
using VOL.DeviceManager.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.DbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Repositories
{
    public partial class dv_dss_recordRepository : RepositoryBase<dv_dss_record>
    , Idv_dss_recordRepository
    {
    public dv_dss_recordRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static Idv_dss_recordRepository Instance
    {
    get {  return AutofacContainerModule.GetService<Idv_dss_recordRepository>
        (); } }
        }
        }
