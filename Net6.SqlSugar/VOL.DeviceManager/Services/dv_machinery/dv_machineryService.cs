/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下dv_machineryService与Idv_machineryService中编写
 */
using VOL.DeviceManager.IRepositories;
using VOL.DeviceManager.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Services
{
    public partial class dv_machineryService : ServiceBase<dv_machinery, Idv_machineryRepository>
    , Idv_machineryService, IDependency
    {
    public dv_machineryService(Idv_machineryRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static Idv_machineryService Instance
    {
      get { return AutofacContainerModule.GetService<Idv_machineryService>(); } }
    }
 }
