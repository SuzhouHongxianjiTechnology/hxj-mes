/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹dv_machinery_typeRepository编写代码
 */
using VOL.DeviceManager.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.DbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Repositories
{
    public partial class dv_machinery_typeRepository : RepositoryBase<dv_machinery_type>
    , Idv_machinery_typeRepository
    {
    public dv_machinery_typeRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static Idv_machinery_typeRepository Instance
    {
    get {  return AutofacContainerModule.GetService<Idv_machinery_typeRepository>
        (); } }
        }
        }
