/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下dv_dss_recordService与Idv_dss_recordService中编写
 */
using VOL.DeviceManager.IRepositories;
using VOL.DeviceManager.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace VOL.DeviceManager.Services
{
    public partial class dv_dss_recordService : ServiceBase<dv_dss_record, Idv_dss_recordRepository>
    , Idv_dss_recordService, IDependency
    {
    public dv_dss_recordService(Idv_dss_recordRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static Idv_dss_recordService Instance
    {
      get { return AutofacContainerModule.GetService<Idv_dss_recordService>(); } }
    }
 }
