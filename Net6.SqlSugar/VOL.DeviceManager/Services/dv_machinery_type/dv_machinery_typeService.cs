/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下dv_machinery_typeService与Idv_machinery_typeService中编写
 */
using VOL.DeviceManager.IRepositories;
using VOL.DeviceManager.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Services
{
    public partial class dv_machinery_typeService : ServiceBase<dv_machinery_type, Idv_machinery_typeRepository>
    , Idv_machinery_typeService, IDependency
    {
    public dv_machinery_typeService(Idv_machinery_typeRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static Idv_machinery_typeService Instance
    {
      get { return AutofacContainerModule.GetService<Idv_machinery_typeService>(); } }
    }
 }
