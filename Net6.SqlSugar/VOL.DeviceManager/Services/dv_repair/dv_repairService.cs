/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下dv_repairService与Idv_repairService中编写
 */
using VOL.DeviceManager.IRepositories;
using VOL.DeviceManager.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Services
{
    public partial class dv_repairService : ServiceBase<dv_repair, Idv_repairRepository>
    , Idv_repairService, IDependency
    {
    public dv_repairService(Idv_repairRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static Idv_repairService Instance
    {
      get { return AutofacContainerModule.GetService<Idv_repairService>(); } }
    }
 }
